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
  selector: 'app-transactional-view-concession',
  templateUrl: './transactional-view-concession.component.html',
  styleUrls: ['./transactional-view-concession.component.css']
})
export class TransactionalViewConcessionComponent implements OnInit, OnDestroy {

    concessionReferenceId: string;
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
    canBcmApprove = false;
    canPcmApprove = false;
    hasChanges = false;

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

    observableTransactionalConcession: Observable<TransactionalConcession>;
    transactionalConcession: TransactionalConcession;

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
            this.concessionReferenceId = params['concessionReferenceId'];

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
            motivation: new FormControl(),
            comments: new FormControl()
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
        this.observableTableNumbers.subscribe(tableNumbers => {
            this.tableNumbers = tableNumbers;
            this.populateForm();
        }, error => this.errorMessage = <any>error);

        this.transactionalConcessionForm.valueChanges.subscribe((value: any) => {
            if (this.transactionalConcessionForm.dirty) {
                this.hasChanges = true;
            }
        });

    }

    populateForm() {
        if (this.concessionReferenceId) {

            this.observableTransactionalConcession = this.transactionalConcessionService.getTransactionalConcessionData(this.concessionReferenceId);
            this.observableTransactionalConcession.subscribe(transactionalConcession => {
                this.transactionalConcession = transactionalConcession;

                if (transactionalConcession.concession.status == "Pending" && transactionalConcession.concession.subStatus == "BCM Pending") {
                    this.canBcmApprove = transactionalConcession.currentUser.canBcmApprove;
                }

                if (transactionalConcession.concession.status == "Pending" && transactionalConcession.concession.subStatus == "PCM Pending") {
                    this.canPcmApprove = transactionalConcession.currentUser.canPcmApprove;
                }

                this.transactionalConcessionForm.controls['mrsCrs'].setValue(this.transactionalConcession.concession.mrsCrs);
                this.transactionalConcessionForm.controls['smtDealNumber'].setValue(this.transactionalConcession.concession.smtDealNumber);
                this.transactionalConcessionForm.controls['motivation'].setValue(this.transactionalConcession.concession.motivation);

                let rowIndex = 0;

                for (let transactionalConcessionDetail of this.transactionalConcession.transactionalConcessionDetails) {

                    if (rowIndex != 0) {
                        this.addNewConcessionRow();
                    }

                    const concessions = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];
                    let currentConcession = concessions.controls[concessions.length - 1];

                    currentConcession.get('transactionalConcessionDetailId').setValue(transactionalConcessionDetail.transactionalConcessionDetailId);

                    let selectedTransactionType = this.transactionTypes.filter(_ => _.id === transactionalConcessionDetail.transactionTypeId);
                    currentConcession.get('transactionType').setValue(selectedTransactionType[0]);

                    let selectedAccountNo = this.clientAccounts.filter(_ => _.legalEntityAccountId == transactionalConcessionDetail.legalEntityAccountId);
                    currentConcession.get('accountNumber').setValue(selectedAccountNo[0]);

                    let selectedTableNumber = this.tableNumbers.filter(_ => _.id == transactionalConcessionDetail.tableNumberId);
                    currentConcession.get('tableNumber').setValue(selectedTableNumber[0]);

                    currentConcession.get('adValorem').setValue(transactionalConcessionDetail.adValorem);
                    currentConcession.get('flatFeeOrRate').setValue(transactionalConcessionDetail.baseRate);

                    rowIndex++;
                }

                rowIndex = 0;

                for (let concessionCondition of this.transactionalConcession.concessionConditions) {
                    this.addNewConditionRow();

                    const conditions = <FormArray>this.transactionalConcessionForm.controls['conditionItemsRows'];
                    let currentCondition = conditions.controls[conditions.length - 1];

                    currentCondition.get('concessionConditionId').setValue(concessionCondition.concessionConditionId);

                    let selectedConditionType = this.conditionTypes.filter(_ => _.id == concessionCondition.conditionTypeId);
                    currentCondition.get('conditionType').setValue(selectedConditionType[0]);

                    this.selectedConditionTypes[rowIndex] = selectedConditionType[0];

                    let selectedConditionProduct = selectedConditionType[0].conditionProducts.filter(_ => _.id == concessionCondition.conditionProductId);
                    currentCondition.get('conditionProduct').setValue(selectedConditionProduct[0]);

                    currentCondition.get('interestRate').setValue(concessionCondition.interestRate);
                    currentCondition.get('volume').setValue(concessionCondition.conditionVolume);
                    currentCondition.get('value').setValue(concessionCondition.conditionValue);

                    let selectedPeriodType = this.periodTypes.filter(_ => _.id == concessionCondition.periodTypeId);
                    currentCondition.get('periodType').setValue(selectedPeriodType[0]);

                    let selectedPeriod = this.periods.filter(_ => _.id == concessionCondition.periodId);
                    currentCondition.get('period').setValue(selectedPeriod[0]);

                    rowIndex++;
                }

            }, error => this.errorMessage = <any>error);
        }
    }

    initConcessionItemRows() {
        return this.formBuilder.group({
            transactionalConcessionDetailId: [''],
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
            concessionConditionId: [''],
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
        transactionalConcession.concession.concessionType = "Transactional";
        transactionalConcession.concession.riskGroupId = this.riskGroup.id;
        transactionalConcession.concession.referenceNumber = this.concessionReferenceId;

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

        if (this.transactionalConcessionForm.controls['comments'].value)
            transactionalConcession.concession.comments = this.transactionalConcessionForm.controls['comments'].value;

        const concessions = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];

        for (let concessionFormItem of concessions.controls) {
            if (!transactionalConcession.transactionalConcessionDetails)
                transactionalConcession.transactionalConcessionDetails = [];

            let transactionalConcessionDetail = new TransactionalConcessionDetail();

            if (concessionFormItem.get('transactionalConcessionDetailId').value)
                transactionalConcessionDetail.transactionalConcessionDetailId = concessionFormItem.get('transactionalConcessionDetailId').value;

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

            if (conditionFormItem.get('concessionConditionId').value)
                concessionCondition.concessionConditionId = conditionFormItem.get('concessionConditionId').value;

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

            transactionalConcession.concessionConditions.push(concessionCondition);
        }

        return transactionalConcession;
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

    bcmApproveConcession() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;

        var transactionalConcession = this.getTransactionalConcession();
        transactionalConcession.concession.subStatus = "PCM Pending";
        transactionalConcession.concession.bcmUserId = this.transactionalConcession.currentUser.id;

        if (!this.validationError) {
            this.transactionalConcessionService.postUpdateTransactionalData(transactionalConcession).subscribe(entity => {
                console.log("data saved");
                this.canBcmApprove = false;
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

    bcmDeclineConcession() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;

        var transactionalConcession = this.getTransactionalConcession();
        transactionalConcession.concession.status = "Declined";
        transactionalConcession.concession.subStatus = "BCM Declined";
        transactionalConcession.concession.bcmUserId = this.transactionalConcession.currentUser.id;

        if (!this.validationError) {
            this.transactionalConcessionService.postUpdateTransactionalData(transactionalConcession).subscribe(entity => {
                console.log("data saved");
                this.canBcmApprove = false;
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

    pcmApproveConcession() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;

        var transactionalConcession = this.getTransactionalConcession();

        transactionalConcession.concession.status = "Approved";

        if (this.transactionalConcession.currentUser.isHO) {
            transactionalConcession.concession.subStatus = "HO Approved";
            transactionalConcession.concession.hoUserId = this.transactionalConcession.currentUser.id;
        } else {
            transactionalConcession.concession.subStatus = "PCM Approved";
            transactionalConcession.concession.pcmUserId = this.transactionalConcession.currentUser.id;
        }

        if (!this.validationError) {
            this.transactionalConcessionService.postUpdateTransactionalData(transactionalConcession).subscribe(entity => {
                console.log("data saved");
                this.canPcmApprove = false;
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

    pcmDeclineConcession() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;

        var transactionalConcession = this.getTransactionalConcession();

        transactionalConcession.concession.status = "Declined";

        if (this.transactionalConcession.currentUser.isHO) {
            transactionalConcession.concession.subStatus = "HO Declined";
            transactionalConcession.concession.hoUserId = this.transactionalConcession.currentUser.id;
        } else {
            transactionalConcession.concession.subStatus = "PCM Declined";
            transactionalConcession.concession.pcmUserId = this.transactionalConcession.currentUser.id;
        }

        if (!this.validationError) {
            this.transactionalConcessionService.postUpdateTransactionalData(transactionalConcession).subscribe(entity => {
                console.log("data saved");
                this.canPcmApprove = false;
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
}
