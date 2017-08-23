import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { RiskGroup } from "../models/risk-group";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Location } from '@angular/common';
import { Period } from "../models/period";
import { PeriodType } from "../models/period-type";
import { ConditionType } from "../models/condition-type";
import { ClientAccount } from "../models/client-account";
import { AccrualType } from "../models/accrual-type";
import { ChannelType } from "../models/channel-type";
import { LookupDataService } from "../services/lookup-data.service";
import { CashConcession } from "../models/cash-concession";
import { Concession } from "../models/concession";
import { CashConcessionService } from "../services/cash-concession.service";
import { CashConcessionDetail } from "../models/cash-concession-detail";
import { ConcessionCondition } from "../models/concession-condition";

@Component({
  selector: 'app-cash-view-concession',
  templateUrl: './cash-view-concession.component.html',
  styleUrls: ['./cash-view-concession.component.css']
})
export class CashViewConcessionComponent implements OnInit, OnDestroy {

    concessionReferenceId: string;
    private sub: any;
    errorMessage: String;
    validationError: String[];
    saveMessage: String;
    observableRiskGroup: Observable<RiskGroup>;
    riskGroup: RiskGroup;
    riskGroupNumber: number;
    public cashConcessionForm: FormGroup;
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

    observableAccrualTypes: Observable<AccrualType[]>;
    accrualTypes: AccrualType[];

    observableChannelTypes: Observable<ChannelType[]>;
    channelTypes: ChannelType[];

    observableCashConcession: Observable<CashConcession>;
    cashConcession: CashConcession;

    constructor(private route: ActivatedRoute,
        private formBuilder: FormBuilder,
        private location: Location,
        @Inject(LookupDataService) private lookupDataService,
        @Inject(CashConcessionService) private cashConcessionService) {
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

            if (this.concessionReferenceId) {
                this.observableCashConcession = this.cashConcessionService.getCashConcessionData(this.concessionReferenceId);
                this.observableCashConcession.subscribe(cashConcession => {
                    this.cashConcession = cashConcession;

                    if (cashConcession.concession.status == "Pending" && cashConcession.concession.subStatus == "BCM Pending") {
                        this.canBcmApprove = cashConcession.currentUser.canBcmApprove;
                    }

                    if (cashConcession.concession.status == "Pending" && cashConcession.concession.subStatus == "PCM Pending") {
                        this.canPcmApprove = cashConcession.currentUser.canPcmApprove;
                    }

                    this.cashConcessionForm.controls['smtDealNumber'].setValue(this.cashConcession.concession.smtDealNumber);
                    this.cashConcessionForm.controls['motivation'].setValue(this.cashConcession.concession.motivation);

                    let rowIndex = 0;

                    for (let cashConcessionDetail of this.cashConcession.cashConcessionDetails) {

                        if (rowIndex != 0) {
                            this.addNewConcessionRow();
                        }

                        const concessions = <FormArray>this.cashConcessionForm.controls['concessionItemRows'];
                        let currentConcession = concessions.controls[concessions.length - 1];

                        let selectedChannelType = this.channelTypes.filter(_ => _.id == cashConcessionDetail.channelTypeId);
                        currentConcession.get('channelType').setValue(selectedChannelType[0]);

                        let selectedAccountNo = this.clientAccounts.filter(_ => _.legalEntityAccountId == cashConcessionDetail.legalEntityAccountId);
                        currentConcession.get('accountNumber').setValue(selectedAccountNo[0]);

                        currentConcession.get('baseRate').setValue(cashConcessionDetail.baseRate);
                        currentConcession.get('adValorem').setValue(cashConcessionDetail.adValorem);
                        currentConcession.get('tableNumber').setValue(cashConcessionDetail.cashTableNumber);

                        let selectedAccrualType = this.accrualTypes.filter(_ => _.id == cashConcessionDetail.accrualTypeId);
                        currentConcession.get('accrualType').setValue(selectedAccrualType[0]);

                        rowIndex++;
                    }

                    rowIndex = 0;

                    for (let concessionCondition of this.cashConcession.concessionConditions) {
                        this.addNewConditionRow();

                        const conditions = <FormArray>this.cashConcessionForm.controls['conditionItemsRows'];
                        let currentCondition = conditions.controls[conditions.length - 1];

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
        });

        this.cashConcessionForm = this.formBuilder.group({
            concessionItemRows: this.formBuilder.array([this.initConcessionItemRows()]),
            conditionItemsRows: this.formBuilder.array([]),
            smtDealNumber: new FormControl(),
            motivation: new FormControl()
        });

        this.observableChannelTypes = this.lookupDataService.getChannelTypes();
        this.observableChannelTypes.subscribe(channelTypes => this.channelTypes = channelTypes, error => this.errorMessage = <any>error);

        this.observablePeriods = this.lookupDataService.getPeriods();
        this.observablePeriods.subscribe(periods => this.periods = periods, error => this.errorMessage = <any>error);

        this.observablePeriodTypes = this.lookupDataService.getPeriodTypes();
        this.observablePeriodTypes.subscribe(periodTypes => this.periodTypes = periodTypes, error => this.errorMessage = <any>error);

        this.observableConditionTypes = this.lookupDataService.getConditionTypes();
        this.observableConditionTypes.subscribe(conditionTypes => this.conditionTypes = conditionTypes, error => this.errorMessage = <any>error);

        this.observableAccrualTypes = this.lookupDataService.getAccrualTypes();
        this.observableAccrualTypes.subscribe(accrualTypes => this.accrualTypes = accrualTypes, error => this.errorMessage = <any>error);

        this.cashConcessionForm.valueChanges.subscribe((value: any) => {
            if (this.cashConcessionForm.dirty) {
                this.hasChanges = true;
            }
        });
    }

    initConcessionItemRows() {
        return this.formBuilder.group({
            channelType: [''],
            accountNumber: [''],
            baseRate: [''],
            adValorem: [''],
            tableNumber: [''],
            accrualType: ['']
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
        const control = <FormArray>this.cashConcessionForm.controls['concessionItemRows'];
        control.push(this.initConcessionItemRows());
    }

    addNewConditionRow() {
        const control = <FormArray>this.cashConcessionForm.controls['conditionItemsRows'];
        control.push(this.initConditionItemRows());
    }

    addNewConditionRowIfNone() {
        const control = <FormArray>this.cashConcessionForm.controls['conditionItemsRows'];
        if (control.length == 0)
            control.push(this.initConditionItemRows());
    }

    deleteConcessionRow(index: number) {
        const control = <FormArray>this.cashConcessionForm.controls['concessionItemRows'];
        control.removeAt(index);
    }

    deleteConditionRow(index: number) {
        const control = <FormArray>this.cashConcessionForm.controls['conditionItemsRows'];
        control.removeAt(index);

        this.selectedConditionTypes.splice(index, 1);
    }

    conditionTypeChanged(rowIndex) {
        const control = <FormArray>this.cashConcessionForm.controls['conditionItemsRows'];
        this.selectedConditionTypes[rowIndex] = control.controls[rowIndex].get('conditionType').value;
    }

    addValidationError(validationDetail) {
        if (!this.validationError)
            this.validationError = [];

        this.validationError.push(validationDetail);
    }

    getCashConcession(): CashConcession {
        var cashConcession = new CashConcession();
        cashConcession.concession = new Concession();
        cashConcession.concession.riskGroupId = this.riskGroup.id;
        cashConcession.concession.referenceNumber = this.concessionReferenceId;

        if (this.cashConcessionForm.controls['smtDealNumber'].value)
            cashConcession.concession.smtDealNumber = this.cashConcessionForm.controls['smtDealNumber'].value;
        else
            this.addValidationError("SMT Deal Number not captured");

        if (this.cashConcessionForm.controls['motivation'].value)
            cashConcession.concession.motivation = this.cashConcessionForm.controls['motivation'].value;
        else
            this.addValidationError("Motivation not captured");

        const concessions = <FormArray>this.cashConcessionForm.controls['concessionItemRows'];

        for (let concessionFormItem of concessions.controls) {
            if (!cashConcession.cashConcessionDetails)
                cashConcession.cashConcessionDetails = [];

            let cashConcessionDetail = new CashConcessionDetail();

            if (concessionFormItem.get('channelType').value) {
                cashConcessionDetail.channelTypeId = concessionFormItem.get('channelType').value.id;
            } else {
                this.addValidationError("Channel type not selected");
            }

            if (concessionFormItem.get('accountNumber').value) {
                cashConcessionDetail.legalEntityId = concessionFormItem.get('accountNumber').value.legalEntityId;
                cashConcessionDetail.legalEntityAccountId = concessionFormItem.get('accountNumber').value.legalEntityAccountId;
            } else {
                this.addValidationError("Client account not selected");
            }

            if (concessionFormItem.get('baseRate').value)
                cashConcessionDetail.baseRate = concessionFormItem.get('baseRate').value;

            if (concessionFormItem.get('adValorem').value)
                cashConcessionDetail.adValorem = concessionFormItem.get('adValorem').value;

            if (concessionFormItem.get('tableNumber').value)
                cashConcessionDetail.cashTableNumber = concessionFormItem.get('tableNumber').value;

            if (concessionFormItem.get('accrualType').value) {
                cashConcessionDetail.accrualTypeId = concessionFormItem.get('accrualType').value.id;
            } else {
                this.addValidationError("Accrual type not selected");
            }

            cashConcession.cashConcessionDetails.push(cashConcessionDetail);
        }

        const conditions = <FormArray>this.cashConcessionForm.controls['conditionItemsRows'];

        for (let conditionFormItem of conditions.controls) {
            if (!cashConcession.concessionConditions)
                cashConcession.concessionConditions = [];

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

            cashConcession.concessionConditions.push(concessionCondition);
        }

        return cashConcession;
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

        var cashConcession = this.getCashConcession();
        cashConcession.concession.subStatus = "PCM Pending";
        cashConcession.concession.bcmUserId = this.cashConcession.currentUser.id;

        if (!this.validationError) {
            this.cashConcessionService.postUpdateCashData(cashConcession).subscribe(entity => {
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

        var cashConcession = this.getCashConcession();
        cashConcession.concession.status = "Declined";
        cashConcession.concession.subStatus = "BCM Declined";
        cashConcession.concession.bcmUserId = this.cashConcession.currentUser.id;

        if (!this.validationError) {
            this.cashConcessionService.postUpdateCashData(cashConcession).subscribe(entity => {
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

        var cashConcession = this.getCashConcession();

        cashConcession.concession.status = "Approved";

        if (this.cashConcession.currentUser.isHO) {
            cashConcession.concession.subStatus = "HO Approved";
            cashConcession.concession.hoUserId = this.cashConcession.currentUser.id;
        } else {
            cashConcession.concession.subStatus = "PCM Approved";
            cashConcession.concession.pcmUserId = this.cashConcession.currentUser.id;
        }

        if (!this.validationError) {
            this.cashConcessionService.postUpdateCashData(cashConcession).subscribe(entity => {
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

        var cashConcession = this.getCashConcession();

        cashConcession.concession.status = "Declined";

        if (this.cashConcession.currentUser.isHO) {
            cashConcession.concession.subStatus = "HO Declined";
            cashConcession.concession.hoUserId = this.cashConcession.currentUser.id;
        } else {
            cashConcession.concession.subStatus = "PCM Declined";
            cashConcession.concession.pcmUserId = this.cashConcession.currentUser.id;
        }

        if (!this.validationError) {
            this.cashConcessionService.postUpdateCashData(cashConcession).subscribe(entity => {
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
