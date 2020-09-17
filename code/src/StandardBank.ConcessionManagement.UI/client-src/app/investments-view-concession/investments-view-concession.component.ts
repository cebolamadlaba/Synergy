import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { RiskGroup } from "../models/risk-group";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Location, DatePipe } from '@angular/common';
import { Period } from "../models/period";
import { extendConcessionModel } from "../models/extendConcessionModel";
import { PeriodType } from "../models/period-type";
import { ConditionType } from "../models/condition-type";
import { ClientAccount } from "../models/client-account";
import { ClientAccountArray } from "../models/client-account-array";

import { AccrualType } from "../models/accrual-type";
import { ChannelType } from "../models/channel-type";
import { LookupDataService } from "../services/lookup-data.service";
import { Concession } from "../models/concession";

import { ConcessionCondition } from "../models/concession-condition";
import { TableNumber } from "../models/table-number";
import { UserConcessionsService } from "../services/user-concessions.service";
import { ConcessionComment } from "../models/concession-comment";

import { DecimalPipe } from '@angular/common';
import { ConcessionTypes } from '../constants/concession-types';
import { ConcessionStatus } from '../constants/concession-status';
import { ConcessionSubStatus } from '../constants/concession-sub-status';

import { UserService } from "../services/user.service";

import { LegalEntityGBBNumber } from "../models/legal-entity-gbb-number";
import { LegalEntity } from "../models/legal-entity";

import { InvestmentProduct } from "../models/investment-product";
import { ProductType } from "../models/product-type";

import { InvestmentConcession } from "../models/investment-concession";
import { InvestmentConcessionDetail } from "../models/investment-concession-detail";
import { InvestmentConcessionService } from "../services/investment-concession.service";

import { InvestmentView } from "../models/investment-view";
import { EditTypeEnum } from '../models/edit-type-enum';

import { BaseComponentService } from '../services/base-component.service';
import { InvestmentBaseService } from '../services/investment-base.service';
import * as moment from 'moment';
import { MOnthEnum } from '../models/month-enum';

@Component({
    selector: 'app-investments-view-concession',
    templateUrl: './investments-view-concession.component.html',
    styleUrls: ['./investments-view-concession.component.css'],
    providers: [DatePipe, InvestmentBaseService]
})
export class InvestmentsViewConcessionComponent extends InvestmentBaseService implements OnInit, OnDestroy {

    concessionReferenceId: string;
    private sub: any;
    errorMessage: String;
    saveMessage: String;
    warningMessage: String;

    observableRiskGroup: Observable<RiskGroup>;
    observableInvestmentView: Observable<InvestmentView>;
    investmentView: InvestmentView = new InvestmentView();

    riskGroup: RiskGroup;
    riskGroupNumber: number;
    legalEntity: LegalEntity;
    sapbpid: number;

    primeRate = "0.00";
    today: string;

    public investmentConcessionForm: FormGroup;

    isLoading = true;
    canBcmApprove = false;
    canPcmApprove = false;
    hasChanges = false;
    canExtend = false;
    canRenew = false;
    canRecall = false;
    isEditing = false;
    motivationEnabled = false;
    canEdit = false;
    isRecalling = false;
    capturedComments: string;
    canApproveChanges = false;
    canResubmit = false;
    canUpdate = false;
    editType: string;
    canArchive = false;
    isInProgressExtension = false;
    isInProgressRenewal = false;
    isApproved = false;
    showHide = false;

    entityName: string;
    entityNumber: string;

    observablePeriods: Observable<Period[]>;
    periods: Period[];

    observablePeriodTypes: Observable<PeriodType[]>;
    periodTypes: PeriodType[];

    productTypes: ProductType[];

    observableInvestmentProducts: Observable<InvestmentProduct[]>;
    investmentproducts: InvestmentProduct[];

    observableLegalEntityGBbNumbers: Observable<LegalEntityGBBNumber[]>;
    legalentitygbbnumbers: LegalEntityGBBNumber[];

    selectedConditionTypes: ConditionType[];
    // Specifies whether Field:NoticePeriod must be disabled[true]:NotDisabled[false]
    selectedInvestmentConcession: boolean[];

    observableConditionTypes: Observable<ConditionType[]>;
    conditionTypes: ConditionType[];

    observableClientAccounts: Observable<ClientAccount[]>;
    clientAccounts: ClientAccount[];

    observableInvestmentConcession: Observable<InvestmentConcession>;
    investmentConcession: InvestmentConcession;

    selectedProductTypes: ProductType[];
    selectedAccountNumbers: ClientAccountArray[];


    constructor(private route: ActivatedRoute,
        private formBuilder: FormBuilder,
        private location: Location,
        private datepipe: DatePipe,
        @Inject(LookupDataService) private lookupDataService,
        @Inject(UserConcessionsService) private userConcessionsService,
        @Inject(InvestmentConcessionService) private investmentConcessionService,
        private baseComponentService: BaseComponentService) {

        super();
        this.riskGroup = new RiskGroup();

        this.productTypes = [new ProductType()];
        this.periods = [new Period()];
        this.periodTypes = [new PeriodType()];

        this.legalentitygbbnumbers = [new LegalEntityGBBNumber()];

        this.conditionTypes = [new ConditionType()];
        this.selectedConditionTypes = [new ConditionType()];

        this.selectedInvestmentConcession = [false];

        this.clientAccounts = [new ClientAccount()];

        this.investmentView.riskGroup = new RiskGroup();
        this.investmentView.investmentConcessions = [new InvestmentConcession()];
        this.investmentView.investmentConcessions[0].concession = new Concession();

        this.investmentConcession = new InvestmentConcession();
        this.investmentConcession.concession = new Concession();
        this.investmentConcession.concession.concessionComments = [new ConcessionComment()];

        this.selectedProductTypes = [new ProductType()];
        this.selectedAccountNumbers = [new ClientAccountArray()];

    }

    ngOnInit() {

        this.today = new Date().toISOString().split('T')[0];

        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
            this.sapbpid = +params['sapbpid'];
            this.concessionReferenceId = params['concessionReferenceId'];
        });

        this.observableInvestmentView = this.investmentConcessionService.getInvestmentViewData(this.riskGroupNumber, this.sapbpid);
        this.observableInvestmentView.subscribe(tempView => {
            this.investmentView = tempView;

            if (this.riskGroupNumber || this.riskGroupNumber > 0) {
                this.entityName = this.investmentView.riskGroup.name;
                this.entityNumber = this.investmentView.riskGroup.number.toString();
            }
            else {
                this.entityName = this.investmentView.legalEntity.customerName;
                this.entityNumber = this.investmentView.legalEntity.customerNumber;
            }

            this.isLoading = false;
        }, error => {
            this.errorMessage = <any>error;
            this.isLoading = false;
        });


        this.investmentConcessionForm = this.formBuilder.group({
            concessionItemRows: this.formBuilder.array([this.initConcessionItemRows()]),
            conditionItemsRows: this.formBuilder.array([]),
            smtDealNumber: new FormControl(),
            motivation: new FormControl(),
            comments: new FormControl()
        });

        this.getInitialData();

        this.investmentConcessionForm.valueChanges.subscribe((value: any) => {
            if (this.investmentConcessionForm.dirty) {
                //if the captured comments is still the same as the comments that means
                //the user has changed something else on the form
                if (this.capturedComments == value.comments) {
                    this.hasChanges = true;
                }

                this.capturedComments = value.comments;
            }
        });
    }

    getInvestmentConcessionItemRows(): FormArray {
        return <FormArray>this.investmentConcessionForm.controls['concessionItemRows'];
    }

    getInitialData() {
        if (this.riskGroupNumber != null && this.riskGroupNumber != 0) {
            Observable.forkJoin([
                this.lookupDataService.getProductTypes(ConcessionTypes.Investment),
                this.lookupDataService.getPeriods(),
                this.lookupDataService.getPeriodTypes(),
                this.lookupDataService.getConditionTypes(),
                this.lookupDataService.getRiskGroup(this.riskGroupNumber),
                this.lookupDataService.getClientAccountsConcessionType(this.riskGroupNumber, this.sapbpid, ConcessionTypes.Investment)
            ]).subscribe(results => {
                this.setInitialData(results, true);
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        }
        else if (this.sapbpid != null && this.sapbpid != 0) {
            Observable.forkJoin([
                this.lookupDataService.getProductTypes(ConcessionTypes.Investment),
                this.lookupDataService.getPeriods(),
                this.lookupDataService.getPeriodTypes(),
                this.lookupDataService.getConditionTypes(),
                this.lookupDataService.getLegalEntity(this.sapbpid),
                this.lookupDataService.getClientAccountsConcessionType(this.riskGroupNumber, this.sapbpid, ConcessionTypes.Investment)
            ]).subscribe(results => {
                this.setInitialData(results, false);
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        }
    }

    setInitialData(results: {}[], isForRiskGroup: boolean) {
        if (isForRiskGroup) {
            this.riskGroup = <any>results[4];
        }
        else {
            this.legalEntity = <any>results[4];
        }

        this.productTypes = <any>results[0];
        this.periods = <any>results[1];
        this.periodTypes = <any>results[2];
        this.conditionTypes = <any>results[3];
        this.clientAccounts = <any>results[5];
        this.primeRate = <string>results[6];

        this.populateForm();
    }

    onExpiryDateChanged(itemrow) {

        if (this.investmentConcession.concession.dateOpened) {
            var formattedDateOpened = this.datepipe.transform(this.investmentConcession.concession.dateOpened, 'yyyy-MM-dd');
        }

        var validationErrorMessage = this.baseComponentService.expiringDateDifferenceValidationForView(itemrow.controls['expiryDate'].value, formattedDateOpened);
        if (validationErrorMessage != null) {
            this.addValidationError(validationErrorMessage);
        }
    }

    populateForm() {
        if (this.concessionReferenceId) {
            this.observableInvestmentConcession = this.investmentConcessionService.getInvestmentConcessionData(this.concessionReferenceId);
            this.observableInvestmentConcession.subscribe(investmentConcession => {
                this.investmentConcession = investmentConcession;

                if (investmentConcession.concession.status == ConcessionStatus.Pending && investmentConcession.concession.subStatus == ConcessionSubStatus.BCMPending) {
                    this.canBcmApprove = investmentConcession.currentUser.canBcmApprove;
                }

                if (investmentConcession.concession.status == ConcessionStatus.Pending && (investmentConcession.concession.subStatus == ConcessionSubStatus.PCMPending ||
                    investmentConcession.concession.subStatus == ConcessionSubStatus.PCMSnIPending)) {
                    if (this.investmentConcession.currentUser.isHO) {
                        this.canPcmApprove = investmentConcession.currentUser.canPcmApprove
                    } else {
                        this.canPcmApprove = investmentConcession.currentUser.canPcmApprove && investmentConcession.currentUser.canApprove;
                    }

                    // Removed as per SBSA.Anthony's request - 2019-07-15
                    this.canEdit = investmentConcession.currentUser.canPcmApprove;
                }


                if (investmentConcession.primeRate) {

                    this.primeRate = investmentConcession.primeRate;
                }

                //if it's still pending and the user is a requestor then they can recall it
                if (investmentConcession.concession.status == ConcessionStatus.Pending && investmentConcession.concession.subStatus == ConcessionSubStatus.BCMPending) {
                    this.canRecall = investmentConcession.currentUser.canRequest && investmentConcession.concession.isAENumberLinkedAccountExecutiveOrAssistant;
                }

                if (investmentConcession.concession.status == ConcessionStatus.Pending &&
                    (investmentConcession.concession.subStatus == ConcessionSubStatus.PCMApprovedWithChanges || investmentConcession.concession.subStatus == ConcessionSubStatus.HOApprovedWithChanges)) {
                    this.canApproveChanges = investmentConcession.currentUser.canRequest && investmentConcession.concession.isAENumberLinkedAccountExecutiveOrAssistant;
                }

                if (investmentConcession.concession.status === ConcessionStatus.Approved ||
                    investmentConcession.concession.status === ConcessionStatus.ApprovedWithChanges) {
                    this.isApproved = true;
                }

                //if the concession is set to can extend and the user is a requestor, then they can extend or renew it
                this.canExtend = investmentConcession.concession.canExtend && investmentConcession.currentUser.canRequest;
                this.canRenew = investmentConcession.concession.canRenew && investmentConcession.currentUser.canRequest;

                //set the resubmit and update permissions
                this.canResubmit = investmentConcession.concession.canResubmit && investmentConcession.currentUser.canRequest;
                this.canUpdate = investmentConcession.concession.canUpdate && investmentConcession.currentUser.canRequest;

                this.canArchive = investmentConcession.concession.canArchive && investmentConcession.currentUser.canRequest;
                this.isInProgressExtension = investmentConcession.concession.isInProgressExtension;
                this.isInProgressRenewal = investmentConcession.concession.isInProgressRenewal;

                this.investmentConcessionForm.controls['motivation'].setValue(this.investmentConcession.concession.motivation);
                this.investmentConcessionForm.controls['smtDealNumber'].setValue(this.investmentConcession.concession.smtDealNumber);

                let rowIndex = 0;

                for (let investmentConcessionDetail of this.investmentConcession.investmentConcessionDetails) {

                    if (rowIndex != 0) {
                        this.addNewConcessionRow(false);
                    }

                    const concessions = this.getInvestmentConcessionItemRows();
                    let currentConcession = concessions.controls[concessions.length - 1];

                    currentConcession.get('investmentConcessionDetailId').setValue(investmentConcessionDetail.investmentConcessionDetailId);
                    currentConcession.get('concessionDetailId').setValue(investmentConcessionDetail.concessionDetailId);


                    if (this.clientAccounts) {
                        let selectedAccountNo = this.clientAccounts.filter(_ => _.legalEntityAccountId == investmentConcessionDetail.legalEntityAccountId);
                        currentConcession.get('accountNumber').setValue(selectedAccountNo[0]);
                    }


                    if (this.productTypes) {

                        let selectedProductType = this.productTypes.filter(_ => _.id === investmentConcessionDetail.productTypeId);

                        if (selectedProductType != null) {
                            currentConcession.get('productType').setValue(selectedProductType[0]);

                            if (selectedProductType[0].description == 'Notice deposit (BND)') {
                                this.selectedInvestmentConcession[rowIndex] = false;
                            }
                            else {
                                this.selectedInvestmentConcession[rowIndex] = true;
                            }
                        }


                    }

                    if (investmentConcessionDetail.balance)
                        currentConcession.get('balance').setValue(new DecimalPipe('en-US').transform(investmentConcessionDetail.balance, '1.0-0'));

                    if (investmentConcessionDetail.approvedRate)
                        currentConcession.get('approvedRate').setValue(investmentConcessionDetail.approvedRate);

                    if (investmentConcessionDetail.loadedRate)
                        currentConcession.get('loadedRate').setValue(investmentConcessionDetail.loadedRate);

                    if (investmentConcessionDetail.term)
                        currentConcession.get('noticeperiod').setValue(investmentConcessionDetail.term);

                    if (investmentConcessionDetail.expiryDate) {
                        var formattedExpiryDate = this.datepipe.transform(investmentConcessionDetail.expiryDate, 'yyyy-MM-dd');
                        currentConcession.get('expiryDate').setValue(formattedExpiryDate);
                    }

                    if (investmentConcessionDetail.dateApproved) {
                        var formattedDateApproved = this.datepipe.transform(investmentConcessionDetail.dateApproved, 'yyyy-MM-dd');
                        currentConcession.get('dateApproved').setValue(formattedDateApproved);
                    }

                    currentConcession.get('isExpired').setValue(investmentConcessionDetail.isExpired);
                    currentConcession.get('isExpiring').setValue(investmentConcessionDetail.isExpiring);

                    rowIndex++;
                }

                rowIndex = 0;

                for (let concessionCondition of this.investmentConcession.concessionConditions) {
                    this.addNewConditionRow();

                    const conditions = <FormArray>this.investmentConcessionForm.controls['conditionItemsRows'];
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
                    currentCondition.get('conditionComment').setValue(concessionCondition.conditionComment);

                    let selectedPeriodType = this.periodTypes.filter(_ => _.id == concessionCondition.periodTypeId);
                    currentCondition.get('periodType').setValue(selectedPeriodType[0]);

                    let selectedPeriod = this.periods.filter(_ => _.id == concessionCondition.periodId);
                    currentCondition.get('period').setValue(selectedPeriod[0]);

                    rowIndex++;
                }

                this.changearray = this.lookupDataService.checkforLC(this.investmentConcession.concession.status, this.investmentConcession.concession.subStatus, investmentConcession.concession.concessionComments);

                this.isLoading = false;
            }, error => {
                this.isLoading = false;
                this.errorMessage = <any>error;
            });
        }
    }


    initConcessionItemRows() {

        this.selectedProductTypes.push(new ProductType());
        this.selectedAccountNumbers.push(new ClientAccountArray());

        this.selectedInvestmentConcession.push(false)

        return this.formBuilder.group({

            disablecontrolset: [''],
            investmentConcessionDetailId: [''],
            concessionDetailId: [''],
            product: [{ value: '', disabled: true }],
            productType: [''],
            accountNumber: [''],
            balance: [''],
            noticeperiod: [''],
            approvedRate: [''],
            loadedRate: [''],
            expiryDate: [''],
            dateApproved: [{ value: '', disabled: true }],
            isExpired: [''],
            isExpiring: [''],
            show_uffFee: true
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
            conditionComment: [''],
            periodType: [''],
            period: ['']
        });
    }

    addNewConcessionRow(isClickEvent: boolean) {
        const control = this.getInvestmentConcessionItemRows();
        var newRow = this.initConcessionItemRows();
        if (isClickEvent) {
            if (control != null && control.length > 0) {
                let expiryDate = control.controls[0].get('expiryDate').value;
                if (expiryDate != null) {
                    newRow.controls['expiryDate'].setValue(expiryDate);
                }
            }
        }
        control.push(newRow);
    }

    addNewConditionRow() {
        const control = <FormArray>this.investmentConcessionForm.controls['conditionItemsRows'];
        control.push(this.initConditionItemRows());
    }

    addNewConditionRowIfNone() {
        const control = <FormArray>this.investmentConcessionForm.controls['conditionItemsRows'];
        if (control.length == 0)
            control.push(this.initConditionItemRows());
    }

    deleteConcessionRow(index: number) {
        if (confirm("Are you sure you want to remove this row?")) {
            const control = this.getInvestmentConcessionItemRows();
            control.removeAt(index);

            this.selectedInvestmentConcession.splice(index, 1);

            this.selectedProductTypes.splice(index, 1);
            this.selectedInvestmentConcession.splice(index, 1);
        }
    }

    deleteConditionRow(index: number) {
        const control = <FormArray>this.investmentConcessionForm.controls['conditionItemsRows'];
        control.removeAt(index);

        this.selectedConditionTypes.splice(index, 1);
    }

    conditionTypeChanged(rowIndex) {
        const control = <FormArray>this.investmentConcessionForm.controls['conditionItemsRows'];
        this.selectedConditionTypes[rowIndex] = control.controls[rowIndex].get('conditionType').value;

        let currentCondition = control.controls[rowIndex];

        currentCondition.get('conditionProduct').setValue(null);
        currentCondition.get('interestRate').setValue(null);
        currentCondition.get('volume').setValue(null);
        currentCondition.get('value').setValue(null);
    }


    isEnabledExpiryDate(rowIndex: number) {
        const control = this.getInvestmentConcessionItemRows();

        let currentRow = control.controls[rowIndex];
        var productType = currentRow.get('productType').value;

        if (productType.description == 'Notice deposit (BND)') {
            return false;
        }
        else {
            if (this.editType == EditTypeEnum.Renew || this.editType == EditTypeEnum.UpdateApproved) {
                return false;
            }
            else {
                return true;
            }
        }
    }

    productTypeChanged(rowIndex) {

        const control = this.getInvestmentConcessionItemRows();

        let currentRow = control.controls[rowIndex];
        var productType = currentRow.get('productType').value;

        this.selectedProductTypes[rowIndex] = productType;

        if (this.clientAccounts && this.clientAccounts.length > 0) {
            this.selectedAccountNumbers[rowIndex].clientaccounts = this.clientAccounts.filter(re => re.accountType == productType.description);

            if (this.selectedAccountNumbers[rowIndex].clientaccounts.length == 0) {
                currentRow.get('accountNumber').setValue(null);
            }
            else {

                currentRow.get('accountNumber').setValue(this.selectedAccountNumbers[rowIndex].clientaccounts[0]);
            }
        }

        if (productType.description == 'Notice deposit (BND)') {

            this.selectedInvestmentConcession[rowIndex] = false;
            currentRow.get('noticeperiod').setValue(null);
        }
        else {
            this.selectedInvestmentConcession[rowIndex] = true;

        }
    }

    getInvestmentConcession(isNew: boolean): InvestmentConcession {
        var investmentConcession = new InvestmentConcession();
        investmentConcession.concession = new Concession();

        if (this.riskGroup)
            investmentConcession.concession.riskGroupId = this.riskGroup.id;
        if (this.legalEntity)
            investmentConcession.concession.legalEntityId = this.legalEntity.id;

        investmentConcession.concession.referenceNumber = this.concessionReferenceId;
        investmentConcession.concession.concessionType = ConcessionTypes.Investment;



        if (this.investmentConcessionForm.controls['smtDealNumber'].value) {
            investmentConcession.concession.smtDealNumber = this.investmentConcessionForm.controls['smtDealNumber'].value;
        }
        else
            this.addValidationError("SMT Deal Number not captured");

        if (this.investmentConcessionForm.controls['comments'].value)
            investmentConcession.concession.comments = this.investmentConcessionForm.controls['comments'].value;

        if (this.investmentConcessionForm.controls['motivation'].value)
            investmentConcession.concession.motivation = this.investmentConcessionForm.controls['motivation'].value;
        else
            investmentConcession.concession.motivation = '.';

        const concessions = this.getInvestmentConcessionItemRows();

        let hasTypeId: boolean = false;
        let hasLegalEntityId: boolean = false;
        let hasLegalEntityAccountId: boolean = false;

        for (let concessionFormItem of concessions.controls) {
            if (!investmentConcession.investmentConcessionDetails)
                investmentConcession.investmentConcessionDetails = [];

            let investmentConcessionDetail = new InvestmentConcessionDetail();

            investmentConcessionDetail.disablecontrolset = concessionFormItem.get('disablecontrolset').value;

            if (!isNew && concessionFormItem.get('investmentConcessionDetailId').value)
                investmentConcessionDetail.investmentConcessionDetailId = concessionFormItem.get('investmentConcessionDetailId').value;

            if (!isNew && concessionFormItem.get('concessionDetailId').value)
                investmentConcessionDetail.concessionDetailId = concessionFormItem.get('concessionDetailId').value;


            let applyExpirydate = false;

            if (concessionFormItem.get('productType').value) {

                if (concessionFormItem.get('productType').value.description == 'Notice deposit (BND)') {
                    applyExpirydate = true;
                }
                investmentConcessionDetail.productTypeId = concessionFormItem.get('productType').value.id;
                hasTypeId = true;
            }
            else
                this.addValidationError("Product not selected");


            if ((concessionFormItem.get('accountNumber').value && concessionFormItem.get('accountNumber').value.legalEntityId)) {
                investmentConcessionDetail.legalEntityId = concessionFormItem.get('accountNumber').value.legalEntityId;
                investmentConcessionDetail.legalEntityAccountId = concessionFormItem.get('accountNumber').value.legalEntityAccountId;
                hasLegalEntityId = true;
                hasLegalEntityAccountId = true;
            } else {

                this.addValidationError("Client account not selected");

            }

            if (concessionFormItem.get('balance').value) {
                investmentConcessionDetail.balance = concessionFormItem.get('balance').value;
            } else {

                this.addValidationError("Balance not entered");

            }

            if (concessionFormItem.get('noticeperiod').value) {
                investmentConcessionDetail.term = concessionFormItem.get('noticeperiod').value;
            } else {
                if (applyExpirydate) {
                    this.addValidationError("Notice period value not entered");
                }
            }

            if (concessionFormItem.get('loadedRate').value) {
                investmentConcessionDetail.loadedRate = concessionFormItem.get('loadedRate').value;
            } else {
                if (applyExpirydate) {
                    this.addValidationError("Rate value not entered");
                }
            }

            if (concessionFormItem.get('expiryDate').value && concessionFormItem.get('expiryDate').value != "") {
                if (!this.baseComponentService.isAppprovingOrDeclining) {
                    this.onExpiryDateChanged(concessionFormItem);
                }

                investmentConcessionDetail.expiryDate = new Date(concessionFormItem.get('expiryDate').value);
            }
            else {
                if (!applyExpirydate) {
                    this.addValidationError("Expiry date not selected");
                }
            }

            investmentConcession.investmentConcessionDetails.push(investmentConcessionDetail);

            if (hasTypeId && hasLegalEntityId && hasLegalEntityAccountId) {
                let hasDuplicates = this.baseComponentService.HasDuplicateConcessionAccountProduct(
                    investmentConcession.investmentConcessionDetails,
                    concessionFormItem.get('productType').value.id,
                    concessionFormItem.get('accountNumber').value.legalEntityId,
                    concessionFormItem.get('accountNumber').value.legalEntityAccountId);

                if (hasDuplicates) {
                    this.addValidationError("Duplicate Account / Product pricing found. Please select different account.");

                    break;
                }
            }
        }

        const conditions = <FormArray>this.investmentConcessionForm.controls['conditionItemsRows'];

        let concessionConditionReturnObject = this.baseComponentService.getConsessionConditionData(conditions, investmentConcession.concessionConditions, this.validationError);
        investmentConcession.concessionConditions = concessionConditionReturnObject.concessionConditions;
        this.validationError = concessionConditionReturnObject.validationError;

        return investmentConcession;
    }


    getBackgroundColour(rowIndex: number) {
        const control = this.getInvestmentConcessionItemRows();

        if (String(control.controls[rowIndex].get('isExpired').value) == "true") {
            return "#EC7063";
        }

        if (String(control.controls[rowIndex].get('isExpiring').value) == "true") {
            return "#F5B041";
        }

        return "";
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
        this.baseComponentService.isAppprovingOrDeclining = true;

        var investmentConcession = this.getInvestmentConcession(false);
        investmentConcession.concession.subStatus = ConcessionSubStatus.PCMSnIPending;
        investmentConcession.concession.bcmUserId = this.investmentConcession.currentUser.id;

        if (!investmentConcession.concession.comments) {
            investmentConcession.concession.comments = "Forwarded";
        }

        if (!this.validationError) {
            this.investmentConcessionService.postUpdateInvestmentData(investmentConcession).subscribe(entity => {
                this.canBcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
                this.investmentConcession = entity;
                this.canEdit = false;
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
        this.baseComponentService.isAppprovingOrDeclining = true;

        var investmentConcession = this.getInvestmentConcession(false);
        investmentConcession.concession.status = ConcessionStatus.Declined;
        investmentConcession.concession.subStatus = ConcessionSubStatus.BCMDeclined;
        investmentConcession.concession.bcmUserId = this.investmentConcession.currentUser.id;

        if (!investmentConcession.concession.comments) {
            investmentConcession.concession.comments = ConcessionStatus.Declined;
        }

        if (!this.validationError) {
            this.investmentConcessionService.postUpdateInvestmentData(investmentConcession).subscribe(entity => {
                console.log("data saved");
                this.canBcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
                this.investmentConcession = entity;
                this.canEdit = false;
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
        this.baseComponentService.isAppprovingOrDeclining = true;

        var investmentConcession = this.getInvestmentConcession(false);

        if (this.hasChanges) {

            investmentConcession.concession.status = ConcessionStatus.Pending;

            if (this.investmentConcession.currentUser.isHO) {
                investmentConcession.concession.subStatus = ConcessionSubStatus.HOApprovedWithChanges;
                investmentConcession.concession.hoUserId = this.investmentConcession.currentUser.id;
            } else {
                investmentConcession.concession.subStatus = ConcessionSubStatus.PCMApprovedWithChanges;
                investmentConcession.concession.pcmUserId = this.investmentConcession.currentUser.id;
            }

            if (!investmentConcession.concession.comments) {
                investmentConcession.concession.comments = ConcessionStatus.ApprovedWithChanges;
            }

            investmentConcession.concession.concessionComments = this.GetChanges(investmentConcession.concession.id);

        } else {
            investmentConcession.concession.status = ConcessionStatus.Approved;

            if (this.investmentConcession.currentUser.isHO) {
                investmentConcession.concession.subStatus = ConcessionSubStatus.HOApproved;
                investmentConcession.concession.hoUserId = this.investmentConcession.currentUser.id;
            } else {
                investmentConcession.concession.subStatus = ConcessionSubStatus.PCMApproved;
                investmentConcession.concession.pcmUserId = this.investmentConcession.currentUser.id;
            }

            if (!investmentConcession.concession.comments) {
                investmentConcession.concession.comments = ConcessionStatus.Approved;
            }
        }

        if (!this.validationError) {
            this.investmentConcessionService.postUpdateInvestmentData(investmentConcession).subscribe(entity => {
                console.log("data saved");
                this.canPcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
                this.investmentConcession = entity;
                this.canEdit = false;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        } else {
            this.isLoading = false;
        }
    }

    private GetChanges(concessionid: number): any[] {
        let comments = this.getChangedProperties();

        let commentarray = [];
        let comment = new ConcessionComment();
        comment.concessionId = concessionid;
        comment.comment = comments;
        comment.userDescription = "LogChanges";
        commentarray.push(comment);
        return commentarray;
    }

    private getChangedProperties(): string {

        let changedProperties = [];
        let rowIndex = 0;

        const concessions = this.getInvestmentConcessionItemRows();

        //this is detailed line items,  but not yet the controls
        for (let concessionFormItem of concessions.controls) {

            let controls = (<FormGroup>concessionFormItem).controls;

            for (const fieldname in controls) { // 'field' is a string

                const abstractControl = controls[fieldname];
                if (abstractControl.dirty) {

                    changedProperties.push({ rowIndex, fieldname });
                }
            }
            rowIndex++;
        }
        return JSON.stringify(changedProperties);
    }

    changearray: any[];
    checkforchanges: boolean;
    bcmhochanged(index: number, controlname: string) {

        if (this.changearray) {

            let found = this.changearray.find(f => f.rowIndex == index && f.fieldname == controlname);
            if (found) {
                return true;
            }
        }
        return false;
    }

    pcmDeclineConcession() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;
        this.baseComponentService.isAppprovingOrDeclining = true;

        var investmentConcession = this.getInvestmentConcession(false);

        investmentConcession.concession.status = ConcessionStatus.Declined;

        if (this.investmentConcession.currentUser.isHO) {
            investmentConcession.concession.subStatus = ConcessionSubStatus.HODeclined;
            investmentConcession.concession.hoUserId = this.investmentConcession.currentUser.id;
        } else {
            investmentConcession.concession.subStatus = ConcessionSubStatus.PCMDeclined;
            investmentConcession.concession.pcmUserId = this.investmentConcession.currentUser.id;
        }

        if (!investmentConcession.concession.comments) {
            investmentConcession.concession.comments = ConcessionStatus.Declined;
        }

        if (!this.validationError) {
            this.investmentConcessionService.postUpdateInvestmentData(investmentConcession).subscribe(entity => {
                console.log("data saved");
                this.canPcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
                this.investmentConcession = entity;
                this.canEdit = false;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        } else {
            this.isLoading = false;
        }
    }

    extendConcession() {

        if (confirm("Are you sure you want to extend this concession?")) {
            this.isLoading = true;
            this.errorMessage = null;
            this.validationError = null;

            var extendConcessionModel = new extendConcessionModel()
            extendConcessionModel.concessionReferenceId = this.concessionReferenceId;

            if (this.investmentConcessionForm.controls['motivation'].value)
                extendConcessionModel.motivation = this.investmentConcessionForm.controls['motivation'].value;
            else
                this.addValidationError("Motivation not captured");
                this.isLoading = false;

            if (!this.validationError) {

                this.investmentConcessionService.postExtendConcession(extendConcessionModel).subscribe(entity => {
                    console.log("data saved");
                    this.canBcmApprove = false;
                    this.canBcmApprove = false;
                    this.canExtend = false;
                    this.canRenew = false;
                    this.canRecall = false;
                    this.canUpdate = false;
                    this.canArchive = false;
                    this.saveMessage = entity.concession.childReferenceNumber;
                    this.isLoading = false;
                    this.investmentConcession = entity;
                }, error => {
                    this.errorMessage = <any>error;
                    this.isLoading = false;
                });
                }
        }
    }

    editConcession(editType: string) {
        this.canBcmApprove = false;
        this.motivationEnabled = true;
        this.canBcmApprove = false;
        this.canExtend = false;
        this.canRenew = false;
        this.canRecall = false;
        this.isEditing = true;
        this.isRecalling = false;
        this.canEdit = true;
        this.editType = editType;
        this.canResubmit = false;
        this.canUpdate = false;
        this.canArchive = false;

        this.investmentConcessionForm.controls['motivation'].setValue('');

        if (editType == EditTypeEnum.Renew) { // || editType == EditTypeEnum.UpdateApproved) {
            const concessions = this.getInvestmentConcessionItemRows();
            for (let concessionFormItem of concessions.controls) {
                // Existing ExpiryDate: ExpiryDate must be set 12 months from the existing ExpiryDate.
                if (concessionFormItem.get('expiryDate').value) {
                    let expiryDate = new Date(concessionFormItem.get('expiryDate').value);
                    expiryDate = new Date(expiryDate.setFullYear(expiryDate.getFullYear() + 1));
                    concessionFormItem.get('expiryDate').setValue(this.datepipe.transform(expiryDate, 'yyyy-MM-dd'));
                }
            }
        }
    }

    saveConcession() {
        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;

        var investmentConcession = this.getInvestmentConcession(true);

        investmentConcession.concession.status = ConcessionStatus.Pending;
        investmentConcession.concession.subStatus = ConcessionSubStatus.BCMPending;
        investmentConcession.concession.type = "Existing";
        investmentConcession.concession.referenceNumber = this.concessionReferenceId;

        if (!this.validationError) {
            this.investmentConcessionService.postChildConcession(investmentConcession, this.editType).subscribe(entity => {
                console.log("data saved");
                this.isEditing = false;
                this.saveMessage = entity.concession.childReferenceNumber;
                this.investmentConcession = entity;
                this.isLoading = false;
                this.canEdit = false;
                this.motivationEnabled = false;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        } else {
            this.isLoading = false;
        }
    }

    saveUpdatedConcession() {
        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;

        var investmentConcession = this.getInvestmentConcession(true);

        investmentConcession.concession.type = "Existing";
        investmentConcession.concession.referenceNumber = this.concessionReferenceId;

        if (!this.validationError) {
            this.investmentConcessionService.postChildConcession(investmentConcession, this.editType).subscribe(entity => {
                console.log("data saved");
                this.isEditing = false;
                this.saveMessage = entity.concession.childReferenceNumber;
                this.investmentConcession = entity;
                this.isLoading = false;
                this.canEdit = false;
                this.motivationEnabled = false;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        } else {
            this.isLoading = false;
        }
    }

    recallConcession() {
        this.isLoading = true;
        this.errorMessage = null;

        this.userConcessionsService.recallConcession(this.concessionReferenceId).subscribe(entity => {
            this.warningMessage = "Concession recalled, please make the required changes and save the concession or it will be lost";
            this.isRecalling = true;
            this.isLoading = false;
            this.canEdit = true;
            this.motivationEnabled = true;
            this.canArchive = false;
        }, error => {
            this.errorMessage = <any>error;
            this.isLoading = false;
        });
    }

    saveRecallConcession() {
        this.warningMessage = "";
        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;

        var investmentConcession = this.getInvestmentConcession(false);

        investmentConcession.concession.status = ConcessionStatus.Pending;
        investmentConcession.concession.subStatus = ConcessionSubStatus.BCMPending;
        investmentConcession.concession.referenceNumber = this.concessionReferenceId;

        if (!this.validationError) {
            this.investmentConcessionService.postRecallInvestmentData(investmentConcession).subscribe(entity => {
                console.log("data saved");
                this.isRecalling = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.investmentConcession = entity;
                this.isLoading = false;
                this.canEdit = false;
                this.motivationEnabled = false;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        } else {
            this.isLoading = false;
        }
    }

    requestorApproveConcession() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;
        this.baseComponentService.isAppprovingOrDeclining = true;

        var investmentConcession = this.getInvestmentConcession(false);
        investmentConcession.concession.status = ConcessionStatus.ApprovedWithChanges;
        investmentConcession.concession.subStatus = ConcessionSubStatus.RequestorAcceptedChanges;
        investmentConcession.concession.requestorId = this.investmentConcession.currentUser.id;

        if (!investmentConcession.concession.comments) {
            investmentConcession.concession.comments = "Accepted Changes";
        }

        if (!this.validationError) {
            this.investmentConcessionService.postUpdateInvestmentData(investmentConcession).subscribe(entity => {
                console.log("data saved");
                this.canApproveChanges = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
                this.investmentConcession = entity;
                this.canEdit = false;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        } else {
            this.isLoading = false;
        }
    }

    requestorDeclineConcession() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;
        this.baseComponentService.isAppprovingOrDeclining = true;

        var investmentConcession = this.getInvestmentConcession(false);
        investmentConcession.concession.status = ConcessionStatus.Declined;
        investmentConcession.concession.subStatus = ConcessionSubStatus.RequestorDeclinedChanges;
        investmentConcession.concession.requestorId = this.investmentConcession.currentUser.id;

        if (!investmentConcession.concession.comments) {
            investmentConcession.concession.comments = "Declined Changes";
        }

        if (!this.validationError) {
            this.investmentConcessionService.postUpdateInvestmentData(investmentConcession).subscribe(entity => {
                console.log("data saved");
                this.canApproveChanges = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
                this.investmentConcession = entity;
                this.canEdit = false;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        } else {
            this.isLoading = false;
        }
    }

    archiveConcessiondetail(concessionDetailId: number) {

        if (confirm("Are you sure you want to delete the concession item ?")) {
            this.isLoading = true;
            this.errorMessage = null;

            this.userConcessionsService.deactivateConcessionDetailed(concessionDetailId).subscribe(entity => {

                this.warningMessage = "Concession item has been deleted";

                this.isLoading = false;

                this.ngOnInit();

            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        }
    }


    archiveConcession() {
        if (confirm("Please note that the account will be put back to standard pricing. Are you sure you want to delete this concession ?")) {
            this.isLoading = true;
            this.errorMessage = null;

            this.userConcessionsService.deactivateConcession(this.concessionReferenceId).subscribe(entity => {
                this.warningMessage = "Concession has been deleted";

                this.isLoading = false;
                this.canBcmApprove = false;
                this.canPcmApprove = false;
                this.canExtend = false;
                this.canRenew = false;
                this.canRecall = false;
                this.isEditing = false;
                this.motivationEnabled = false;
                this.canEdit = false;
                this.isRecalling = false;
                this.canApproveChanges = false;
                this.canResubmit = false;
                this.canUpdate = false;
                this.canArchive = false;

            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        }
    }

    setTwoNumberDecimal($event) {
        $event.target.value = this.baseComponentService.formatDecimal($event.target.value);
    }

    setZeroNumberDecimal($event) {

        if ($event.target.value) {
            $event.target.value = new DecimalPipe('en-US').transform($event.target.value, '1.0-0');
        }
        else {
            $event.target.value = null;
        }
    }


    setThreeNumberDecimal($event) {
        if ($event.target.value) {
            $event.target.value = this.baseComponentService.formatDecimalThree($event.target.value);
        }
        else {
            $event.target.value = null;
        }
    }

    canAddConcessionRowOrManageCondition() {
        return this.canPcmApprove || this.isEditing || this.isRecalling;
    }

    canAddNewCondition() {
        return this.canBcmApprove || this.canPcmApprove || this.isEditing || this.isRecalling;
    }

    canAddComments() {
        return this.canBcmApprove || this.canPcmApprove || this.canApproveChanges;
    }

    canEditSmtDealNumber() {
        return (this.isRecalling || this.canEdit) ? null : '';
    }

    isMotivationEnabled() {

        if (!this.canExtend)
        {
            return this.motivationEnabled ? null : '';
        }
    }

    getNumberInput(input) {
        this.investmentConcessionForm.controls['smtDealNumber'].setValue(this.baseComponentService.removeLetters(input.value));
    }

    disableField(index, fieldName) {
        return super.disableFieldBase(
            this.selectedConditionTypes[index],
            fieldName,
            this.canEdit,
            this.canEdit != null,
            this.isEnabledExpiryDate(index),
            this.selectedInvestmentConcession[index]);
    }
}
