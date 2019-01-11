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
import { TransactionalConcession } from "../models/transactional-concession";
import { Concession } from "../models/concession";
import { ConcessionCondition } from "../models/concession-condition";
import { TransactionalConcessionDetail } from "../models/transactional-concession-detail";
import { DecimalPipe } from '@angular/common';
import { ConcessionTypes } from '../constants/concession-types';
import { BaseComponentService } from '../services/base-component.service';

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
    selectedTransactionTypes: TransactionType[];
    isLoading = true;
    observableLatestCrsOrMrs: Observable<number>;
    latestCrsOrMrs: number;
    showHide = false;

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

    constructor(private route: ActivatedRoute,
        private formBuilder: FormBuilder,
        private location: Location,
        @Inject(LookupDataService) private lookupDataService,
        @Inject(TransactionalConcessionService) private transactionalConcessionService,
        private baseComponentService: BaseComponentService) {
        this.riskGroup = new RiskGroup();
        this.periods = [new Period()];
        this.periodTypes = [new PeriodType()];
        this.conditionTypes = [new ConditionType()];
        this.selectedConditionTypes = [new ConditionType()];
        this.selectedTransactionTypes = [new TransactionType()];
        this.clientAccounts = [new ClientAccount()];
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
        });

        this.transactionalConcessionForm = this.formBuilder.group({
            concessionItemRows: this.formBuilder.array([this.initConcessionItemRows()]),
            conditionItemsRows: this.formBuilder.array([]),
            mrsCrs: new FormControl(),
            smtDealNumber: new FormControl(),
            motivation: new FormControl()
        });

        Observable.forkJoin([
            this.lookupDataService.getPeriods(),
            this.lookupDataService.getPeriodTypes(),
            this.lookupDataService.getConditionTypes(),
            this.lookupDataService.getTransactionTypes(ConcessionTypes.Transactional),
            this.lookupDataService.getRiskGroup(this.riskGroupNumber),
            this.lookupDataService.getClientAccountsConcessionType(this.riskGroupNumber, ConcessionTypes.Transactional),
            this.transactionalConcessionService.getlatestCrsOrMrs(this.riskGroupNumber)
        ]).subscribe(results => {
            this.periods = <any>results[0];
            this.periodTypes = <any>results[1];
            this.conditionTypes = <any>results[2];
            this.transactionTypes = <any>results[3];
            this.riskGroup = <any>results[4];
            this.clientAccounts = <any>results[5];
            this.latestCrsOrMrs = <any>results[6];


            const control = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];

            if (this.transactionTypes)
                control.controls[0].get('transactionType').setValue(this.transactionTypes[0]);

            if (this.clientAccounts)
                control.controls[0].get('accountNumber').setValue(this.clientAccounts[0]);

            this.selectedTransactionTypes[0] = this.transactionTypes[0];
            let currentTransactionType = control.controls[0];
            currentTransactionType.get('adValorem').setValue(null);
            currentTransactionType.get('flatFeeOrRate').setValue(null);

            if (this.selectedTransactionTypes[0].transactionTableNumbers)
                control.controls[0].get('transactionTableNumber').setValue(this.selectedTransactionTypes[0].transactionTableNumbers[0]);

            this.transactionTableNumberChanged(0);

            this.isLoading = false;
        },
            error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
    }

    initConcessionItemRows() {
        this.selectedTransactionTypes.push(new TransactionType());

        return this.formBuilder.group({
            transactionType: [''],
            accountNumber: [''],
            transactionTableNumber: [''],
            flatFeeOrRate: [{ value: '', disabled: true }],
            adValorem: [{ value: '', disabled: true }],
            expiryDate: ['']
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

        var newRow = this.initConcessionItemRows();

        var length = control.controls.length;

        if (this.transactionTypes)
            newRow.controls['transactionType'].setValue(this.transactionTypes[0]);

        if (this.clientAccounts)
            newRow.controls['accountNumber'].setValue(this.clientAccounts[0]);

        this.selectedTransactionTypes[length] = this.transactionTypes[0];

        if (this.transactionTypes && this.transactionTypes[0].transactionTableNumbers)
            newRow.controls['transactionTableNumber'].setValue(this.transactionTypes[0].transactionTableNumbers[0]);

        control.push(newRow);

        this.transactionTableNumberChanged(length);
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
        if (confirm("Are you sure you want to remove this row?")) {
            const control = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];
            control.removeAt(index);

            this.selectedTransactionTypes.splice(index, 1);
        }
    }

    deleteConditionRow(index: number) {
        const control = <FormArray>this.transactionalConcessionForm.controls['conditionItemsRows'];
        control.removeAt(index);

        this.selectedConditionTypes.splice(index, 1);
    }

    conditionTypeChanged(rowIndex) {
        const control = <FormArray>this.transactionalConcessionForm.controls['conditionItemsRows'];
        this.selectedConditionTypes[rowIndex] = control.controls[rowIndex].get('conditionType').value;

        let currentCondition = control.controls[rowIndex];

        currentCondition.get('conditionProduct').setValue(null);
        currentCondition.get('interestRate').setValue(null);
        currentCondition.get('volume').setValue(null);
        currentCondition.get('value').setValue(null);
    }

    transactionTypeChanged(rowIndex) {
        const control = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];
        this.selectedTransactionTypes[rowIndex] = control.controls[rowIndex].get('transactionType').value;

        let currentTransactionType = control.controls[rowIndex];
        currentTransactionType.get('adValorem').setValue(null);
        currentTransactionType.get('flatFeeOrRate').setValue(null);

        control.controls[rowIndex].get('transactionTableNumber').setValue(this.selectedTransactionTypes[rowIndex].transactionTableNumbers[0]);
    }

    transactionTableNumberChanged(rowIndex) {
        const control = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];

        if (control.controls[rowIndex].get('transactionTableNumber').value.fee)
            control.controls[rowIndex].get('flatFeeOrRate').setValue(control.controls[rowIndex].get('transactionTableNumber').value.fee.toFixed(2));
        else
            control.controls[rowIndex].get('flatFeeOrRate').setValue(null);

        if (control.controls[rowIndex].get('transactionTableNumber').value.adValorem)
            control.controls[rowIndex].get('adValorem').setValue(control.controls[rowIndex].get('transactionTableNumber').value.adValorem.toFixed(3));
        else
            control.controls[rowIndex].get('adValorem').setValue(null);
    }

    getTransactionalConcession(): TransactionalConcession {
        var transactionalConcession = new TransactionalConcession();
        transactionalConcession.concession = new Concession();
        transactionalConcession.concession.riskGroupId = this.riskGroup.id;

        if (this.transactionalConcessionForm.controls['smtDealNumber'].value)
            transactionalConcession.concession.smtDealNumber = this.transactionalConcessionForm.controls['smtDealNumber'].value;
        else
            this.addValidationError("SMT Deal Number not captured");

        if (this.transactionalConcessionForm.controls['motivation'].value)
            transactionalConcession.concession.motivation = this.transactionalConcessionForm.controls['motivation'].value;
        else
            transactionalConcession.concession.motivation = '.';

        const concessions = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];

        let hasTypeId: boolean = false;
        let hasLegalEntityId: boolean = false;
        let hasLegalEntityAccountId: boolean = false;

        for (let concessionFormItem of concessions.controls) {
            if (!transactionalConcession.transactionalConcessionDetails)
                transactionalConcession.transactionalConcessionDetails = [];

            let transactionalConcessionDetail = new TransactionalConcessionDetail();

            if (concessionFormItem.get('transactionType').value) {
                transactionalConcessionDetail.transactionTypeId = concessionFormItem.get('transactionType').value.id;
                hasTypeId = true;
            }
            else
                this.addValidationError("Transaction type not selected");

            if (concessionFormItem.get('accountNumber').value) {
                transactionalConcessionDetail.legalEntityId = concessionFormItem.get('accountNumber').value.legalEntityId;
                transactionalConcessionDetail.legalEntityAccountId = concessionFormItem.get('accountNumber').value.legalEntityAccountId;
                hasLegalEntityId = true;
                hasLegalEntityAccountId = true;
            } else {
                this.addValidationError("Client account not selected");
            }

            if (concessionFormItem.get('transactionTableNumber').value) {
                transactionalConcessionDetail.transactionTableNumberId = concessionFormItem.get('transactionTableNumber').value.id;

                if (concessionFormItem.get('transactionTableNumber').value.adValorem)
                    transactionalConcessionDetail.adValorem = concessionFormItem.get('transactionTableNumber').value.adValorem;

                if (concessionFormItem.get('transactionTableNumber').value.fee)
                    transactionalConcessionDetail.fee = concessionFormItem.get('transactionTableNumber').value.fee;
            } else {
                this.addValidationError("Table Number not selected");
            }

            if (concessionFormItem.get('expiryDate').value && concessionFormItem.get('expiryDate').value != "") {
                transactionalConcessionDetail.expiryDate = new Date(concessionFormItem.get('expiryDate').value);
            }
            else {
                this.addValidationError("Expiry date not selected");
            }

            transactionalConcession.transactionalConcessionDetails.push(transactionalConcessionDetail);

            if (hasTypeId && hasLegalEntityId && hasLegalEntityAccountId) {
                let hasDuplicates = this.baseComponentService.HasDuplicateConcessionAccountTransaction(
                    transactionalConcession.transactionalConcessionDetails,
                    concessionFormItem.get('transactionType').value.id,
                    concessionFormItem.get('accountNumber').value.legalEntityId,
                    concessionFormItem.get('accountNumber').value.legalEntityAccountId);

                if (hasDuplicates) {
                    this.addValidationError("Duplicate Account / Transaction pricing found. Please select different account.");

                    break;
                }
            }
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

            if (conditionFormItem.get('periodType').value) {
                concessionCondition.periodTypeId = conditionFormItem.get('periodType').value.id;
            } else {
                this.addValidationError("Period type not selected");
            }

            if (conditionFormItem.get('period').value) {
                concessionCondition.periodId = conditionFormItem.get('period').value.id;
            } else {
                this.addValidationError("Period not selected");
            }

            if (conditionFormItem.get('periodType').value.description == 'Once-off' && conditionFormItem.get('period').value.description == 'Monthly') {
                this.addValidationError("Conditions: The Period 'Monthly' cannot be selected for Period Type 'Once-off'");
            }

            transactionalConcession.concessionConditions.push(concessionCondition);
        }

        return transactionalConcession;
    }

    onSubmit() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;

        var transactionalConcession = this.getTransactionalConcession();

        transactionalConcession.concession.concessionType = ConcessionTypes.Transactional;
        transactionalConcession.concession.type = "New";
        transactionalConcession.concession.comments = "Created";

        if (!this.validationError) {
            this.transactionalConcessionService.postNewTransactionalData(transactionalConcession).subscribe(entity => {
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

    setTwoNumberDecimal($event) {
        $event.target.value = this.formatDecimal($event.target.value);
    }

    formatDecimal(itemValue: number) {
        if (itemValue) {
            return new DecimalPipe('en-US').transform(itemValue, '1.2-2');
        }

        return null;
    }

    goBack() {
        this.location.back();
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

    validatePeriod(itemrow) {
        this.validationError = null;

        let selectedPeriodType = itemrow.controls.periodType.value.description;

        let selectedPeriod = itemrow.controls.period.value.description;

        if (selectedPeriodType == 'Once-off' && selectedPeriod == 'Monthly') {
            this.addValidationError("Conditions: The Period 'Monthly' cannot be selected for Period Type 'Once-off'");
        }
    }
}
