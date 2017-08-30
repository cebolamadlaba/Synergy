import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { RiskGroup } from "../models/risk-group";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Location } from '@angular/common';
import { LookupDataService } from "../services/lookup-data.service";
import { TransactionalConcessionService } from "../services/transactional-concession.service";
import { Period } from "../models/period";
import { PeriodType } from "../models/period-type";
import { ConditionType } from "../models/condition-type";
import { ConditionProduct } from "../models/condition-product";
import { ClientAccount } from "../models/client-account";
import { TransactionType } from "../models/transaction-type";
import { TableNumber } from "../models/table-number";
import { TransactionalConcession } from "../models/transactional-concession";
import { Concession } from "../models/concession";
import { ConcessionCondition } from "../models/concession-condition";
import { TransactionalConcessionDetail } from "../models/transactional-concession-detail";

@Component({
    selector: 'app-transactional-add-concession',
    templateUrl: './transactional-add-concession.component.html',
    styleUrls: ['./transactional-add-concession.component.css']
})
export class TransactionalAddConcessionComponent implements OnInit, OnDestroy {
    public transactionalConcessionForm: FormGroup;
    private sub: any;
    errorMessage: String;
    validationError: String[];
    saveMessage: String;
    observableRiskGroup: Observable<RiskGroup>;
    riskGroup: RiskGroup;
    riskGroupNumber: number;
    selectedConditionTypes: ConditionType[];
    isLoading = false;

    observablePeriods: Observable<Period[]>;
    periods: Period[];

    observablePeriodTypes: Observable<PeriodType[]>;
    periodTypes: PeriodType[];

    observableConditionTypes: Observable<ConditionType[]>;
    conditionTypes: ConditionType[];

    observableClientAccounts: Observable<ClientAccount[]>;
    clientAccounts: ClientAccount[];

    observableTransactionTypes: Observable<TransactionType[]>;
    transactionTypes: TransactionType[];

    observableTableNumbers: Observable<TableNumber[]>;
    tableNumbers: TableNumber[];

    constructor(private route: ActivatedRoute,
        private formBuilder: FormBuilder,
        private location: Location,
        @Inject(LookupDataService) private lookupDataService,
        @Inject(TransactionalConcessionService) private transactionalConcessionService) {
        this.riskGroup = new RiskGroup();
        this.periods = [new Period()];
        this.periodTypes = [new PeriodType()];
        this.conditionTypes = [new ConditionType()];
        this.selectedConditionTypes = [new ConditionType()];
        this.clientAccounts = [new ClientAccount()];
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];

            if (this.riskGroupNumber) {
                this.observableRiskGroup = this.lookupDataService.getRiskGroup(this.riskGroupNumber);
                this.observableRiskGroup.subscribe(riskGroup => this.riskGroup = riskGroup, error => this.errorMessage = <any>error);

                this.observableClientAccounts = this.lookupDataService.getClientAccounts(this.riskGroupNumber);
                this.observableClientAccounts.subscribe(clientAccounts => this.clientAccounts = clientAccounts, error => this.errorMessage = <any>error);
            }
        });

        this.transactionalConcessionForm = this.formBuilder.group({
            concessionItemRows: this.formBuilder.array([this.initConcessionItemRows()]),
            conditionItemsRows: this.formBuilder.array([]),
            mrsCrs: new FormControl(),
            smtDealNumber: new FormControl(),
            motivation: new FormControl()
        });

        this.observablePeriods = this.lookupDataService.getPeriods();
        this.observablePeriods.subscribe(periods => this.periods = periods, error => this.errorMessage = <any>error);

        this.observablePeriodTypes = this.lookupDataService.getPeriodTypes();
        this.observablePeriodTypes.subscribe(periodTypes => this.periodTypes = periodTypes, error => this.errorMessage = <any>error);

        this.observableConditionTypes = this.lookupDataService.getConditionTypes();
        this.observableConditionTypes.subscribe(conditionTypes => this.conditionTypes = conditionTypes, error => this.errorMessage = <any>error);

        this.observableTransactionTypes = this.lookupDataService.getTransactionTypes("Transactional");
        this.observableTransactionTypes.subscribe(transactionTypes => this.transactionTypes = transactionTypes, error => this.errorMessage = <any>error);

        this.observableTableNumbers = this.lookupDataService.getTableNumbers();
        this.observableTableNumbers.subscribe(tableNumbers => this.tableNumbers = tableNumbers, error => this.errorMessage = <any>error);
    }

    initConcessionItemRows() {
        return this.formBuilder.group({
            transactionType: [''],
            accountNumber: [''],
            tableNumber: [''],
            flatFeeOrRate: [{ value: '', disabled: true }],
            adValorem: [{ value: '', disabled: true }]
        });
    }

    initConditionItemRows() {
        this.selectedConditionTypes.push(new ConditionType());

        return this.formBuilder.group({
            conditionType: [''],
            conditionProduct: [''],
            interestRate: [''],
            volume: [''],
            value: [''],
            periodType: [''],
            period: ['']
        });
    }

    addNewConcessionRow() {
        const control = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];
        control.push(this.initConcessionItemRows());
    }

    addNewConditionRow() {
        const control = <FormArray>this.transactionalConcessionForm.controls['conditionItemsRows'];
        control.push(this.initConditionItemRows());
    }

    addNewConditionRowIfNone() {
        const control = <FormArray>this.transactionalConcessionForm.controls['conditionItemsRows'];
        if (control.length == 0)
            control.push(this.initConditionItemRows());
    }

    deleteConcessionRow(index: number) {
        const control = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];
        control.removeAt(index);
    }

    deleteConditionRow(index: number) {
        const control = <FormArray>this.transactionalConcessionForm.controls['conditionItemsRows'];
        control.removeAt(index);

        this.selectedConditionTypes.splice(index, 1);
    }

    conditionTypeChanged(rowIndex) {
        const control = <FormArray>this.transactionalConcessionForm.controls['conditionItemsRows'];
        this.selectedConditionTypes[rowIndex] = control.controls[rowIndex].get('conditionType').value;
    }

    tableNumberChanged(rowIndex) {
        const control = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];

        control.controls[rowIndex].get('flatFeeOrRate').setValue(control.controls[rowIndex].get('tableNumber').value.baseRate);
        control.controls[rowIndex].get('adValorem').setValue(control.controls[rowIndex].get('tableNumber').value.adValorem);
    }

    getTransactionalConcession(): TransactionalConcession {
        var transactionalConcession = new TransactionalConcession();
        transactionalConcession.concession = new Concession();
        transactionalConcession.concession.riskGroupId = this.riskGroup.id;

        if (this.transactionalConcessionForm.controls['mrsCrs'].value)
            transactionalConcession.concession.mrsCrs = this.transactionalConcessionForm.controls['mrsCrs'].value;
        else
            this.addValidationError("MRS/CRS not captured");

        if (this.transactionalConcessionForm.controls['smtDealNumber'].value)
            transactionalConcession.concession.smtDealNumber = this.transactionalConcessionForm.controls['smtDealNumber'].value;
        else
            this.addValidationError("SMT Deal Number not captured");

        if (this.transactionalConcessionForm.controls['motivation'].value)
            transactionalConcession.concession.motivation = this.transactionalConcessionForm.controls['motivation'].value;
        else
            this.addValidationError("Motivation not captured");


        const concessions = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];

        for (let concessionFormItem of concessions.controls) {
            if (!transactionalConcession.transactionalConcessionDetails)
                transactionalConcession.transactionalConcessionDetails = [];

            let transactionalConcessionDetail = new TransactionalConcessionDetail();

            if (concessionFormItem.get('transactionType').value)
                transactionalConcessionDetail.transactionTypeId = concessionFormItem.get('transactionType').value.id;
            else
                this.addValidationError("Transaction type not selected");

            if (concessionFormItem.get('accountNumber').value) {
                transactionalConcessionDetail.legalEntityId = concessionFormItem.get('accountNumber').value.legalEntityId;
                transactionalConcessionDetail.legalEntityAccountId = concessionFormItem.get('accountNumber').value.legalEntityAccountId;
            } else {
                this.addValidationError("Client account not selected");
            }

            if (concessionFormItem.get('tableNumber').value) {
                transactionalConcessionDetail.tableNumberId = concessionFormItem.get('tableNumber').value.id;
                transactionalConcessionDetail.adValorem = concessionFormItem.get('tableNumber').value.adValorem;

                if (concessionFormItem.get('tableNumber').value.baseRate)
                    transactionalConcessionDetail.baseRate = concessionFormItem.get('tableNumber').value.baseRate;
            } else {
                this.addValidationError("Table Number not selected");
            }

            transactionalConcession.transactionalConcessionDetails.push(transactionalConcessionDetail);
        }

        const conditions = <FormArray>this.transactionalConcessionForm.controls['conditionItemsRows'];

        for (let conditionFormItem of conditions.controls) {
            if (!transactionalConcession.concessionConditions)
                transactionalConcession.concessionConditions = [];

            let concessionCondition = new ConcessionCondition();

            if (conditionFormItem.get('conditionType').value)
                concessionCondition.conditionTypeId = conditionFormItem.get('conditionType').value.id;
            else
                this.addValidationError("Condition type not selected");

            if (conditionFormItem.get('conditionProduct').value)
                concessionCondition.conditionProductId = conditionFormItem.get('conditionProduct').value.id;
            else
                this.addValidationError("Condition product not selected");

            if (conditionFormItem.get('interestRate').value)
                concessionCondition.interestRate = conditionFormItem.get('interestRate').value;

            if (conditionFormItem.get('volume').value)
                concessionCondition.conditionVolume = conditionFormItem.get('volume').value;

            if (conditionFormItem.get('value').value)
                concessionCondition.conditionValue = conditionFormItem.get('value').value;

            if (conditionFormItem.get('periodType').value)
                concessionCondition.periodTypeId = conditionFormItem.get('periodType').value.id;

            if (conditionFormItem.get('period').value)
                concessionCondition.periodId = conditionFormItem.get('period').value.id;

            transactionalConcession.concessionConditions.push(concessionCondition);
        }

        return transactionalConcession;
    }

    onSubmit() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;

        var transactionalConcession = this.getTransactionalConcession();

        transactionalConcession.concession.concessionType = "Transactional";
        transactionalConcession.concession.type = "New";

        if (!this.validationError) {
            this.transactionalConcessionService.postNewTransactionalData(transactionalConcession).subscribe(entity => {
                console.log("data saved");
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        } else {
            this.isLoading = false;
        }
    }

    addValidationError(validationDetail) {
        if (!this.validationError)
            this.validationError = [];

        this.validationError.push(validationDetail);
    }

    goBack() {
        this.location.back();
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}
