import { Component, OnInit, Inject, OnDestroy, ViewChild } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { RiskGroup } from "../models/risk-group";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { DecimalPipe, Location, DatePipe } from '@angular/common';
import { ModalDirective } from 'ngx-bootstrap/modal';

import { ReviewFeeType } from "../models/review-fee-type";
import { ProductType } from "../models/product-type";
import { Period } from "../models/period";
import { PeriodType } from "../models/period-type";
import { ConditionType } from "../models/condition-type";
import { ConditionProduct } from "../models/condition-product";
import { ClientAccount } from "../models/client-account";
import { ClientAccountArray } from "../models/client-account-array";
import { LendingConcession } from "../models/lending-concession";
import { Concession } from "../models/concession";
import { LendingConcessionDetail } from "../models/lending-concession-detail";
import { ConcessionCondition } from "../models/concession-condition";
import { LendingFinancial } from "../models/lending-financial";
import { ConcessionComment } from "../models/concession-comment";
import { LegalEntity } from '../models/legal-entity';
import { ProductTypeFieldLogic } from '../models/product-type-field-logic';

import { LookupDataService } from "../services/lookup-data.service";
import { UserConcessionsService } from "../services/user-concessions.service";
import { LendingService } from "../services/lending.service";
import { ConcessionTypes } from '../constants/concession-types';
import { ConcessionStatus } from '../constants/concession-status';
import { ConcessionSubStatus } from '../constants/concession-sub-status';

import { BaseComponentService } from '../services/base-component.service';
import { LendingBaseService } from '../services/lending-base.service';
import * as moment from 'moment';
import { MOnthEnum } from '../models/month-enum';
import { MrsEriEnum } from '../models/mrs-eri-enum';
import { EditTypeEnum } from '../models/edit-type-enum';
import { ConcessionConditionReturnObject } from '../models/concession-condition-return-object';
import { ProductTypeEnum } from '../models/product-type-enum';
import { LendingConcessionTieredRate } from '../models/lending-concession-tiered-rate';
import { extendConcessionModel } from "../models/extendConcessionModel";

@Component({
    selector: 'app-lending-view-concession',
    templateUrl: './lending-view-concession.component.html',
    styleUrls: ['./lending-view-concession.component.css'],
    providers: [DatePipe, LendingBaseService]
})
export class LendingViewConcessionComponent extends LendingBaseService implements OnInit, OnDestroy {

    @ViewChild('tieredRateModal') tieredRateModal: ModalDirective;
    @ViewChild('extendDisclamerModal') extendDisclamerModal;

    myDecimal: number;

    primeRate = "0.00";
    today: string;

    subHeading: string;
    title: string;

    concessionReferenceId: string;
    public lendingConcessionForm: FormGroup;
    private sub: any;
    showHide = false;
    errorMessage: String;
    validationError: String[];
    saveMessage: String;
    warningMessage: String;
    riskGroupNumber: number;
    sapbpid: number;
    selectedConditionTypes: ConditionType[];
    selectedProductTypes: ProductType[];
    selectedProductTypeFieldLogics: ProductTypeFieldLogic[] = [];
    isLoading = true;
    canBcmApprove = false;
    canPcmApprove = false;
    hasChanges = false;
    canExtend = false;
    canRenew = false;
    isAbleToRenew = true;
    showMotivationDisclaimer = false;
    isExtendable = true;
    canRecall = false;
    isEditing = false;
    motivationEnabled = false;
    canEdit = false;
    selectedAccountNumbers: ClientAccountArray[];
    isPrimeRateChanged = false;

    isExtendButtonVisible = false;
    isRenewButtonVisible = false;
    isUpdateButtonVisible = false;
    isExtendingOverdraft = false;
    isMutliLineConcession = false;
    isNewOverdraftConcession = false;

    selectedRowIndex: number;
    selectedLineItemTieredRates: LendingConcessionTieredRate[] = [];
    tieredRateMessage: string = "";
    currentRowIndex: number;
    rollbackTieredRates: LendingConcessionTieredRate[];

    capturedComments: string;
    canApproveChanges: boolean;
    canResubmit = false;
    canUpdate = false;
    editType: string;
    canArchive = false;
    isInProgressExtension = false;
    isInProgressRenewal = false;
    isApproved = false;

    isRecalling = false;
    visiblearray: object[];

    observableRiskGroup: Observable<RiskGroup>;
    riskGroup: RiskGroup;

    observableLendingConcession: Observable<LendingConcession>;
    lendingConcession: LendingConcession;

    observableReviewFeeTypes: Observable<ReviewFeeType[]>;
    reviewFeeTypes: ReviewFeeType[];

    observableProductTypes: Observable<ProductType[]>;
    productTypes: ProductType[];

    observablePeriods: Observable<Period[]>;
    periods: Period[];

    observablePeriodTypes: Observable<PeriodType[]>;
    periodTypes: PeriodType[];

    observableConditionTypes: Observable<ConditionType[]>;
    conditionTypes: ConditionType[];

    observableClientAccounts: Observable<ClientAccount[]>;
    clientAccounts: ClientAccount[];
    clientAccountsCopy: ClientAccount[];

    observableLendingFinancial: Observable<LendingFinancial>;
    lendingFinancial: LendingFinancial;

    legalEntity: LegalEntity;

    selectedExtensionFee: number = null;

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private formBuilder: FormBuilder,
        private location: Location,
        private datepipe: DatePipe,
        @Inject(LookupDataService) private lookupDataService,
        @Inject(LendingService) private lendingService,
        @Inject(UserConcessionsService) private userConcessionsService,
        private baseComponentService: BaseComponentService) {
        super();
        this.riskGroup = new RiskGroup();
        this.reviewFeeTypes = [new ReviewFeeType()];
        this.productTypes = [new ProductType()];
        this.periods = [new Period()];
        this.periodTypes = [new PeriodType()];
        this.conditionTypes = [new ConditionType()];
        this.selectedConditionTypes = [new ConditionType()];
        this.selectedProductTypes = [new ProductType()];
        this.clientAccounts = [new ClientAccount()];
        this.clientAccountsCopy = [new ClientAccount()];
        this.lendingFinancial = new LendingFinancial();
        this.lendingConcession = new LendingConcession();
        this.lendingConcession.concession = new Concession();
        this.lendingConcession.concession.concessionComments = [new ConcessionComment()];
        this.selectedAccountNumbers = [new ClientAccountArray()];


    }

    ngOnInit() {
        this.myDecimal = -2.99;

        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
            this.sapbpid = +params['sapbpid'];
            this.concessionReferenceId = params['concessionReferenceId'];
        });

        this.lendingConcessionForm = this.formBuilder.group({
            concessionItemRows: this.formBuilder.array([this.initConcessionItemRows()]),
            conditionItemsRows: this.formBuilder.array([]),
            mrsCrs: new FormControl(),
            smtDealNumber: new FormControl(),
            motivation: new FormControl(),
            comments: new FormControl(),
            prime: new FormControl()

        });

        this.getInitialData();

        this.lendingConcessionForm.valueChanges.subscribe((value: any) => {
            if (this.lendingConcessionForm.dirty) {
                //if the captured comments is still the same as the comments that means
                //the user has changed something else on the form
                if (this.capturedComments == value.comments) {
                    this.hasChanges = true;
                }

                this.capturedComments = value.comments;
            }
        });
    }

    getvisible(productType: Number) {


        return true;
    }

    getLendingConcessionItemRows(): FormArray {
        return <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];
    }

    populateForm() {
        if (this.concessionReferenceId) {
            this.observableLendingConcession = this.lendingService.getLendingConcessionData(this.concessionReferenceId);
            this.observableLendingConcession.subscribe(lendingConcession => {
                this.lendingConcession = lendingConcession;

                this.populateFormFromLendingConcession();

                this.isLoading = false;
            }, error => {
                this.isLoading = false;
                this.errorMessage = <any>error;
            });
        }
    }

    populateFormFromLendingConcession() {
        if (this.lendingConcession.concession.status == ConcessionStatus.Pending && this.lendingConcession.concession.subStatus == ConcessionSubStatus.BCMPending) {
            this.canBcmApprove = this.lendingConcession.currentUser.canBcmApprove;
        }

        if (this.lendingConcession.concession.status == ConcessionStatus.Pending && this.lendingConcession.concession.subStatus == ConcessionSubStatus.PCMPending) {
            if (this.lendingConcession.currentUser.isHO) {
                this.canPcmApprove = this.lendingConcession.currentUser.canPcmApprove
            } else {
                this.canPcmApprove = this.lendingConcession.currentUser.canPcmApprove && this.lendingConcession.currentUser.canApprove;
            }

            this.canEdit = this.lendingConcession.currentUser.canPcmApprove;
        }

        if (this.lendingConcession.primeRate) {

            this.primeRate = this.lendingConcession.primeRate;
        }


        //if it's still pending and the user is a requestor then they can recall it
        if (this.lendingConcession.concession.status == ConcessionStatus.Pending && this.lendingConcession.concession.subStatus == ConcessionSubStatus.BCMPending) {
            this.canRecall = this.lendingConcession.currentUser.canRequest && this.lendingConcession.concession.isAENumberLinkedAccountExecutiveOrAssistant;
        }

        if (this.lendingConcession.concession.status == ConcessionStatus.Pending &&
            (this.lendingConcession.concession.subStatus == ConcessionSubStatus.PCMApprovedWithChanges || this.lendingConcession.concession.subStatus == ConcessionSubStatus.HOApprovedWithChanges)) {
            this.canApproveChanges = this.lendingConcession.currentUser.canRequest && this.lendingConcession.concession.isAENumberLinkedAccountExecutiveOrAssistant;
        }

        if (this.lendingConcession.concession.status === ConcessionStatus.Approved ||
            this.lendingConcession.concession.status === ConcessionStatus.ApprovedWithChanges) {
            this.isApproved = true;
        }


        //if the concession is set to can extend and the user is a requestor, then they can extend or renew it
        this.canExtend = this.lendingConcession.concession.canExtend && this.lendingConcession.currentUser.canRequest;
        this.canRenew = this.lendingConcession.concession.canRenew && this.lendingConcession.currentUser.canRequest;

        //set the resubmit and update permissions
        //can only update when concession is not "due for expiry"
        this.canResubmit = this.lendingConcession.concession.canResubmit && this.lendingConcession.currentUser.canRequest;
        this.canUpdate = this.lendingConcession.concession.canUpdate && this.lendingConcession.currentUser.canRequest;

        this.canArchive = this.lendingConcession.concession.canArchive && this.lendingConcession.currentUser.canRequest;
        this.isInProgressExtension = this.lendingConcession.concession.isInProgressExtension;
        this.isInProgressRenewal = this.lendingConcession.concession.isInProgressRenewal;

        this.lendingConcessionForm.controls['smtDealNumber'].setValue(this.lendingConcession.concession.smtDealNumber);
        this.lendingConcessionForm.controls['motivation'].setValue(this.lendingConcession.concession.motivation);

        let rowIndex = 0;

        for (let lendingConcessionDetail of this.lendingConcession.lendingConcessionDetails) {

            if (rowIndex != 0) {
                this.addNewConcessionRow(false);
            }

            let productTypeFieldLogic = new ProductTypeFieldLogic();
            productTypeFieldLogic = super.setProductTypeFieldLogic(lendingConcessionDetail.productType, productTypeFieldLogic);
            this.selectedProductTypeFieldLogics.push(productTypeFieldLogic);

            const concessions = this.getLendingConcessionItemRows();
            let currentConcession = concessions.controls[concessions.length - 1];

            currentConcession.get('lendingConcessionDetailId').setValue(lendingConcessionDetail.lendingConcessionDetailId);
            currentConcession.get('concessionDetailId').setValue(lendingConcessionDetail.concessionDetailId);

            let selectedProductType = this.productTypes.filter(_ => _.id === lendingConcessionDetail.productTypeId);
            currentConcession.get('productType').setValue(selectedProductType[0]);

            this.selectedProductTypes[rowIndex] = selectedProductType[0];


            if (this.clientAccounts) {
                let selectedAccountNo = this.clientAccounts.filter(_ => _.legalEntityAccountId == lendingConcessionDetail.legalEntityAccountId);
                currentConcession.get('accountNumber').setValue(selectedAccountNo[0]);
            }

            if (selectedProductType[0] != null &&
                (selectedProductType[0].description == ProductTypeEnum.Overdraft ||
                    selectedProductType[0].description == ProductTypeEnum.TemporaryOverdraft)) {
                currentConcession.get('lendingTieredRates').setValue(lendingConcessionDetail.lendingConcessionDetailTieredRates);

                if (lendingConcessionDetail.lendingConcessionDetailTieredRates != null && lendingConcessionDetail.lendingConcessionDetailTieredRates.length > 0) {
                    currentConcession.get('limit').setValue(this.formatDecimal(lendingConcessionDetail.lendingConcessionDetailTieredRates[0].limit));
                    currentConcession.get('marginAgainstPrime').setValue(this.formatDecimal3(lendingConcessionDetail.lendingConcessionDetailTieredRates[0].marginToPrime));
                    currentConcession.get('approvedMarginAgainstPrime').setValue(this.formatDecimal3(lendingConcessionDetail.lendingConcessionDetailTieredRates[0].approvedMap));
                }
            }
            if (selectedProductType[0] != null &&
                (selectedProductType[0].description != ProductTypeEnum.Overdraft &&
                    selectedProductType[0].description != ProductTypeEnum.TemporaryOverdraft)) {
                currentConcession.get('limit').setValue(this.formatDecimal(lendingConcessionDetail.limit));
                currentConcession.get('marginAgainstPrime').setValue(this.formatDecimal3(lendingConcessionDetail.marginAgainstPrime));
                currentConcession.get('approvedMarginAgainstPrime').setValue(this.formatDecimal3(lendingConcessionDetail.approvedMap));
            }

            if (selectedProductType[0] != null &&
                selectedProductType[0].description == ProductTypeEnum.Overdraft &&
                    this.lendingConcession.lendingConcessionDetails.length == 1) {
                if (this.baseComponentService.isThreeMonthsExpiringConcession(this.datepipe.transform(lendingConcessionDetail.expiryDate, 'yyyy-MM-dd'))) {
                    this.isExtendButtonVisible = true;
                    this.isRenewButtonVisible = true;
                    this.isExtendingOverdraft = true;
                } else {
                    this.isUpdateButtonVisible = true;
                }
                
            }

            if (selectedProductType[0] != null &&
                (selectedProductType[0].description == ProductTypeEnum.RCP) &&
                    this.lendingConcession.lendingConcessionDetails.length == 1) {
                if (this.baseComponentService.isThreeMonthsExpiringConcession(this.datepipe.transform(lendingConcessionDetail.expiryDate, 'yyyy-MM-dd'))) {
                    this.isExtendButtonVisible = true;
                    this.isRenewButtonVisible = true;
                    this.isExtendingOverdraft = true;
                } else {
                    
                       this.isUpdateButtonVisible = true;
                }

            }

            if (selectedProductType[0] != null &&
                (selectedProductType[0].description == ProductTypeEnum.BTL || selectedProductType[0].description == ProductTypeEnum.MTL) &&
                this.lendingConcession.lendingConcessionDetails.length == 1) {             
                    this.isUpdateButtonVisible = true;              
            } 

            if (selectedProductType[0] != null &&
                (selectedProductType[0].description == ProductTypeEnum.TemporaryOverdraft) &&
                this.lendingConcession.lendingConcessionDetails.length == 1) {
                this.isUpdateButtonVisible = true;
            }

             if (selectedProductType[0] != null &&
                    this.lendingConcession.lendingConcessionDetails.length > 1) {
                 this.isUpdateButtonVisible = true;
                 this.isExtendButtonVisible = true;
                 this.isRenewButtonVisible = true;
                 this.isMutliLineConcession = true;

                 this.lendingConcession.lendingConcessionDetails.forEach(item => {
                     if (item.productType == ProductTypeEnum.Overdraft
                         || item.productType == ProductTypeEnum.RCP) {
                         if (this.baseComponentService.isThreeMonthsExpiringConcession(this.datepipe.transform(item.expiryDate, 'yyyy-MM-dd'))) {
                             this.isExtendButtonVisible = true;
                             this.isRenewButtonVisible = true;
                             this.isExtendingOverdraft = true;
                         } else {
                             this.isUpdateButtonVisible = true;
                             this.isRenewButtonVisible = false;
                             this.isExtendButtonVisible = false;
                         }
                     }
                 });

            }

            currentConcession.get('term').setValue(lendingConcessionDetail.term);

            currentConcession.get('initiationFee').setValue(this.formatDecimal3(lendingConcessionDetail.initiationFee));

            let selectedReviewFeeType = this.reviewFeeTypes.filter(_ => _.id == lendingConcessionDetail.reviewFeeTypeId);
            currentConcession.get('reviewFeeType').setValue(selectedReviewFeeType[0]);
            currentConcession.get('reviewFee').setValue(this.formatDecimal3(lendingConcessionDetail.reviewFee));
            currentConcession.get('uffFee').setValue(this.formatDecimal3(lendingConcessionDetail.uffFee));
            currentConcession.get('extensionFee').setValue(this.formatDecimal3(lendingConcessionDetail.extensionFee));

            currentConcession.get('mrsEri').setValue(lendingConcessionDetail.mrsEri);

            currentConcession.get('serviceFee').setValue(this.formatDecimal3(lendingConcessionDetail.serviceFee));
            currentConcession.get('frequency').setValue(lendingConcessionDetail.frequency);

            if (lendingConcessionDetail.expiryDate) {
                var formattedExpiryDate = this.datepipe.transform(lendingConcessionDetail.expiryDate, 'yyyy-MM-dd');
                currentConcession.get('expiryDate').setValue(formattedExpiryDate);
            }

            if (lendingConcessionDetail.dateApproved) {
                var formattedDateApproved = this.datepipe.transform(lendingConcessionDetail.dateApproved, 'yyyy-MM-dd');
                currentConcession.get('dateApproved').setValue(formattedDateApproved);
            }

            currentConcession.get('isExpired').setValue(lendingConcessionDetail.isExpired);
            currentConcession.get('isExpiring').setValue(lendingConcessionDetail.isExpiring);



            rowIndex++;
        }

        rowIndex = 0;

        for (let concessionCondition of this.lendingConcession.concessionConditions) {
            this.addNewConditionRow();

            const conditions = <FormArray>this.lendingConcessionForm.controls['conditionItemsRows'];
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

        this.changearray = this.lookupDataService.checkforLC(this.lendingConcession.concession.status, this.lendingConcession.concession.subStatus, this.lendingConcession.concession.concessionComments);

    }

    initConcessionItemRows() {
        this.selectedProductTypes.push(new ProductType());
        this.selectedAccountNumbers.push(new ClientAccountArray());
        return this.formBuilder.group({
            lendingConcessionDetailId: [''],
            concessionDetailId: [''],
            productType: [''],
            accountNumber: [''],
            limit: [''],
            term: [''],
            marginAgainstPrime: [''],
            approvedMarginAgainstPrime: [{ value: '', disabled: true }],
            initiationFee: [''],
            reviewFeeType: [''],
            reviewFee: [''],
            uffFee: [''],
            expiryDate: [{ value: '', disabled: true }],
            dateApproved: [{ value: '', disabled: true }],
            isExpired: [''],
            isExpiring: [''],
            frequency: [{ value: '', disabled: true }],
            serviceFee: [{ value: '', disabled: true }],
            mrsEri: [''],
            extensionFee: [''],
            newOverDraft: [''],
            lendingTieredRates: [[]]
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



    disableFieldOnRenew(itemRow: FormControl) {

       
        if (this.editType == EditTypeEnum.Renew && itemRow.get('newOverDraft').value != true) {
            if (itemRow.value.productType.description == ProductTypeEnum.BTL
                || itemRow.value.productType.description == ProductTypeEnum.TemporaryOverdraft
                || itemRow.value.productType.description == ProductTypeEnum.MTL) {
                itemRow.disable();
            }              
        }

   
        if (this.editType == EditTypeEnum.Extend)
        {
            if (itemRow.value.productType.description == ProductTypeEnum.BTL               
                || itemRow.value.productType.description == ProductTypeEnum.MTL
                || itemRow.value.productType.description == ProductTypeEnum.TemporaryOverdraft) {

                itemRow.disable();
            }              
        }

        if (this.editType == EditTypeEnum.UpdateApproved && this.isExtendingOverdraft) {

            if (itemRow.value.productType.description == ProductTypeEnum.Overdraft
                && itemRow.get('newOverDraft').value != true) {
                itemRow.disable();
            } else if (itemRow.value.productType.description == ProductTypeEnum.TemporaryOverdraft) {
                itemRow.disable();
                itemRow.get('productType').enable();
                itemRow.get('accountNumber').enable();
            }
            
            if (itemRow.value.productType.description == ProductTypeEnum.Overdraft
                && itemRow.get('newOverDraft').value == true) {
                itemRow.enable();
                itemRow.get('extensionFee').disable();
                itemRow.get('approvedMarginAgainstPrime').disable();
                itemRow.get('dateApproved').disable();
                itemRow.get('expiryDate').disable();
                itemRow.get('serviceFee').disable();
                itemRow.get('frequency').disable();
            }

        }

        if (this.editType == EditTypeEnum.UpdateApproved && this.isMutliLineConcession) {

            if (itemRow.value.productType.description == ProductTypeEnum.TemporaryOverdraft) {
                itemRow.disable();
                itemRow.get('productType').enable();
                itemRow.get('accountNumber').enable();
            }

        }
    }


    getInitialData() {
        if (this.riskGroupNumber != null && this.riskGroupNumber != 0) {
            Observable.forkJoin([
                this.lookupDataService.getReviewFeeTypes(),
                this.lookupDataService.getProductTypes(ConcessionTypes.Lending),
                this.lookupDataService.getPeriods(),
                this.lookupDataService.getPeriodTypes(),
                this.lookupDataService.getConditionTypes(),
                this.lookupDataService.getRiskGroup(this.riskGroupNumber),
                this.lookupDataService.getClientAccountsConcessionType(this.riskGroupNumber, ConcessionTypes.Lending),
                this.lendingService.getLendingFinancial(this.riskGroupNumber),
            ]).subscribe(results => {

                this.setInitialData(results, true);
                this.populateForm();

            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        }
        else if (this.sapbpid != null && this.sapbpid != 0) {
            Observable.forkJoin([
                this.lookupDataService.getReviewFeeTypes(),
                this.lookupDataService.getProductTypes(ConcessionTypes.Lending),
                this.lookupDataService.getPeriods(),
                this.lookupDataService.getPeriodTypes(),
                this.lookupDataService.getConditionTypes(),
                this.lookupDataService.getLegalEntity(this.sapbpid),
                this.lookupDataService.getClientAccountsConcessionType(this.riskGroupNumber, this.sapbpid, ConcessionTypes.Lending),
            ]).subscribe(results => {

                this.setInitialData(results, false);
                this.populateForm();

            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });

        }

    }
    setInitialData(results: {}[], isForRiskGroup: boolean) {
        this.reviewFeeTypes = <any>results[0];
        this.productTypes = <any>results[1];
        this.periods = <any>results[2];
        this.periodTypes = <any>results[3];
        this.conditionTypes = <any>results[4];
        if (isForRiskGroup) {
            this.riskGroup = <any>results[5];
            this.lendingFinancial = <any>results[7];
            this.subHeading = this.riskGroup.name;
            this.title = this.riskGroup.number.toString();
        }
        else {
            this.legalEntity = <any>results[5];
            this.subHeading = this.legalEntity.customerName;
            this.title = this.legalEntity.customerNumber;
        }

        this.clientAccounts = <any>results[6];
        this.clientAccountsCopy = <any>results[6]
    }

    addNewConcessionRow(isClickEvent: boolean) {
        const control = this.getLendingConcessionItemRows();
        var newRow = this.initConcessionItemRows();
        if (isClickEvent) {
            if (control != null && control.length > 0) {
                let expiryDate = control.controls[0].get('expiryDate').value;
                if (expiryDate != null) {
                    newRow.controls['expiryDate'].setValue(expiryDate);
                }
            }
            this.selectedProductTypeFieldLogics.push(new ProductTypeFieldLogic());
        }
        control.push(newRow);
    }

    addNewConditionRow() {
        const control = <FormArray>this.lendingConcessionForm.controls['conditionItemsRows'];
        control.push(this.initConditionItemRows());
    }

    addNewConditionRowIfNone() {
        const control = <FormArray>this.lendingConcessionForm.controls['conditionItemsRows'];
        if (control.length == 0)
            control.push(this.initConditionItemRows());
    }

    deleteConcessionRow(index: number) {
        if (confirm("Are you sure you want to remove this row?")) {
            const control = this.getLendingConcessionItemRows();
            control.removeAt(index);

            this.selectedProductTypes.splice(index, 1);
            this.selectedProductTypeFieldLogics.splice(index, 1);
        }
    }

    deleteConditionRow(index: number) {
        const control = <FormArray>this.lendingConcessionForm.controls['conditionItemsRows'];
        control.removeAt(index);

        this.selectedConditionTypes.splice(index, 1);
    }

    onExpiryDateChanged(itemrow) {

        if (this.lendingConcession.concession.dateOpened) {
            var formattedDateOpened = this.datepipe.transform(this.lendingConcession.concession.dateOpened, 'yyyy-MM-dd');
        }

        var validationErrorMessage = this.baseComponentService.expiringDateDifferenceValidationForView(itemrow.controls['expiryDate'].value, formattedDateOpened);
        if (validationErrorMessage != null) {
            this.addValidationError(validationErrorMessage);
        }
    }

    onTermValueChange(rowIndex) {
        this.errorMessage = null;
        this.validationError = null;

        const control = this.getLendingConcessionItemRows();
        let term = control.controls[rowIndex].get('term').value;

        if (term < MOnthEnum.ThreeMonths) {
            this.addValidationError("Minimum term captured should be 3 months");
        };
    }

    conditionTypeChanged(rowIndex) {
        const control = <FormArray>this.lendingConcessionForm.controls['conditionItemsRows'];
        this.selectedConditionTypes[rowIndex] = control.controls[rowIndex].get('conditionType').value;

        let currentCondition = control.controls[rowIndex];

        currentCondition.get('conditionProduct').setValue(null);
        currentCondition.get('interestRate').setValue(null);
        currentCondition.get('volume').setValue(null);
        currentCondition.get('value').setValue(null);
    }

    productTypeChanged(rowIndex: number) {
        const control = this.getLendingConcessionItemRows();

        let currentRow = control.controls[rowIndex];
        var productType = currentRow.get('productType').value;

        this.selectedProductTypes[rowIndex] = productType;

        if (this.clientAccounts && this.clientAccounts.length > 0) {
            this.selectedAccountNumbers[rowIndex].clientaccounts = this.clientAccounts.filter(re => re.accountType == productType.description);

            if (this.selectedAccountNumbers[rowIndex].clientaccounts.length == 0) {
                control.controls[rowIndex].get('accountNumber').setValue(null);
            }
            else {
                control.controls[rowIndex].get('accountNumber').setValue(this.selectedAccountNumbers[rowIndex].clientaccounts);
            }

        }

        this.selectedProductTypeFieldLogics[rowIndex] = super.setProductTypeFieldLogic(productType.description, this.selectedProductTypeFieldLogics[rowIndex]);

        if (productType.description === ProductTypeEnum.Overdraft) {
            currentRow.get('term').setValue('12');
            currentRow.get('newOverDraft').setValue(true);
        }

        if (this.editType == EditTypeEnum.Renew)
        {
            if (productType.description === ProductTypeEnum.MTL
                || productType.description === ProductTypeEnum.BTL
                || productType.description === ProductTypeEnum.TemporaryOverdraft) {
                currentRow.get('newOverDraft').setValue(true);
            }
        }


    }

    goBack() {
        this.location.back();
    }

    getLendingConcession(isNew: boolean): LendingConcession {
        var lendingConcession = new LendingConcession();

        lendingConcession.concession = new Concession();
        lendingConcession.concession.concessionType = ConcessionTypes.Lending;
        lendingConcession.concession.referenceNumber = this.concessionReferenceId;

        if (this.lendingConcessionForm.controls['smtDealNumber'].value)
            lendingConcession.concession.smtDealNumber = this.lendingConcessionForm.controls['smtDealNumber'].value;
        else
            this.addValidationError("SMT Deal Number not captured");

        if (this.lendingConcessionForm.controls['motivation'].value)
            lendingConcession.concession.motivation = this.lendingConcessionForm.controls['motivation'].value;
        else
            lendingConcession.concession.motivation = '.';

        if (this.lendingConcessionForm.controls['comments'].value)
            lendingConcession.concession.comments = this.lendingConcessionForm.controls['comments'].value;

        if (this.legalEntity)
            lendingConcession.concession.legalEntityId = this.legalEntity.id;
        if (this.riskGroup)
            lendingConcession.concession.riskGroupId = this.riskGroup.id;

        const concessions = this.getLendingConcessionItemRows();

        let hasProductType: boolean = false;
        let hasLegalEntityId: boolean = false;
        let hasLegalEntityAccountId: boolean = false;
        let hasValidTerm: boolean = false;
        let isOverdraftOrTempOverdraft: boolean = false;

        for (let concessionFormItem of concessions.controls) {
            if (!lendingConcession.lendingConcessionDetails)
                lendingConcession.lendingConcessionDetails = [];

            let lendingConcessionDetail = new LendingConcessionDetail();

            if (!isNew && concessionFormItem.get('lendingConcessionDetailId').value)
                lendingConcessionDetail.lendingConcessionDetailId = concessionFormItem.get('lendingConcessionDetailId').value;

            if (!isNew && concessionFormItem.get('concessionDetailId').value)
                lendingConcessionDetail.concessionDetailId = concessionFormItem.get('concessionDetailId').value;

            if (concessionFormItem.get('productType').value) {
                lendingConcessionDetail.productTypeId = concessionFormItem.get('productType').value.id;
                hasProductType = true;

                // Is the product Overdraft or Temporary Overdraft?
                if (concessionFormItem.get('productType').value.description == ProductTypeEnum.Overdraft ||
                    concessionFormItem.get('productType').value.description == ProductTypeEnum.TemporaryOverdraft) {
                    isOverdraftOrTempOverdraft = true;
                }
                else {
                    isOverdraftOrTempOverdraft = false;
                }
            }
            else {
                this.addValidationError("Product type not selected");
                hasProductType = false;
            }


            if (concessionFormItem.get('accountNumber').value) {
                lendingConcessionDetail.legalEntityId = concessionFormItem.get('accountNumber').value.legalEntityId;
                lendingConcessionDetail.legalEntityAccountId = concessionFormItem.get('accountNumber').value.legalEntityAccountId;
                hasLegalEntityId = true;
                hasLegalEntityAccountId = true;
            } else {
                this.addValidationError("Client account not selected");
                hasLegalEntityId = false;
                hasLegalEntityAccountId = false;
            }

            if (isOverdraftOrTempOverdraft) {
                if (lendingConcessionDetail.lendingConcessionDetailTieredRates == null)
                    lendingConcessionDetail.lendingConcessionDetailTieredRates = [];

                lendingConcessionDetail.lendingConcessionDetailTieredRates = concessionFormItem.get('lendingTieredRates').value;

                lendingConcessionDetail.lendingConcessionDetailTieredRates.forEach(item => {
                    if (item.concessionLendingId == null || item.concessionLendingId == 0)
                        item.concessionLendingId = lendingConcessionDetail.lendingConcessionDetailId;
                });

                if (lendingConcessionDetail.lendingConcessionDetailTieredRates == null ||
                    lendingConcessionDetail.lendingConcessionDetailTieredRates.length == 0)
                    this.addValidationError("Tiered Rate cannot be empty for Product Type: Overdraft / Temporary Overdraft");

                if (concessionFormItem.get('approvedMarginAgainstPrime').value)
                    lendingConcessionDetail.approvedMap = concessionFormItem.get('approvedMarginAgainstPrime').value;

                if (concessionFormItem.get('marginAgainstPrime').value)
                    lendingConcessionDetail.marginAgainstPrime = concessionFormItem.get('marginAgainstPrime').value;
            }
            else {
                if (concessionFormItem.get('limit').value == "") {
                    this.addValidationError("Limit cannot be empty");
                }
                if (concessionFormItem.get('marginAgainstPrime').value == "") {
                    this.addValidationError("Prime fixed rate cannot be empty");
                }

                if (concessionFormItem.get('limit').value)
                    lendingConcessionDetail.limit = concessionFormItem.get('limit').value;

                if (concessionFormItem.get('marginAgainstPrime').value)
                    lendingConcessionDetail.marginAgainstPrime = concessionFormItem.get('marginAgainstPrime').value;

                if (concessionFormItem.get('approvedMarginAgainstPrime').value)
                    lendingConcessionDetail.approvedMap = concessionFormItem.get('approvedMarginAgainstPrime').value;
            }

            if (concessionFormItem.get('term').value) {
                if (concessionFormItem.get('term').value < MOnthEnum.ThreeMonths) {
                    this.addValidationError("Minimum term captured should be 3 months");
                    hasValidTerm = false;
                } else {
                    lendingConcessionDetail.term = concessionFormItem.get('term').value;
                    hasValidTerm = true;
                }
            }

            if (concessionFormItem.get('initiationFee').value)
                lendingConcessionDetail.initiationFee = concessionFormItem.get('initiationFee').value;

            if (concessionFormItem.get('reviewFeeType').value)
                lendingConcessionDetail.reviewFeeTypeId = concessionFormItem.get('reviewFeeType').value.id;

            if (concessionFormItem.get('reviewFee').value)
                lendingConcessionDetail.reviewFee = concessionFormItem.get('reviewFee').value;

            if (concessionFormItem.get('uffFee').value)
                lendingConcessionDetail.uffFee = concessionFormItem.get('uffFee').value;

            if (concessionFormItem.get('serviceFee').value)
                lendingConcessionDetail.serviceFee = concessionFormItem.get('serviceFee').value;

            if (concessionFormItem.get('frequency').value)
                lendingConcessionDetail.frequency = concessionFormItem.get('frequency').value;

            if (concessionFormItem.get('productType').value.description == ProductTypeEnum.Overdraft ||
                concessionFormItem.get('productType').value.description == ProductTypeEnum.RCP)  {
                if (concessionFormItem.get('expiryDate').value && !this.baseComponentService.isRenewing && !this.baseComponentService.isAppprovingOrDeclining)
                    this.onExpiryDateChanged(concessionFormItem);
                lendingConcessionDetail.expiryDate = new Date(concessionFormItem.get('expiryDate').value);
            }

            if (concessionFormItem.get('mrsEri').value == "" ||
                concessionFormItem.get('mrsEri').value.toString().indexOf(".") > -1) {
                this.addValidationError("MRS/ERI cannot be empty or a decimal");

            } else {

                var mrsEriValue = parseInt(concessionFormItem.get('mrsEri').value, 10);
                if (mrsEriValue < MrsEriEnum.MinMrsEri || mrsEriValue > MrsEriEnum.MaxMrsEri) {
                    this.addValidationError("MRS/ERI numbers must from 12 to 25");
                } else {
                    lendingConcessionDetail.mrsEri = concessionFormItem.get('mrsEri').value;
                }
            }


            lendingConcession.lendingConcessionDetails.push(lendingConcessionDetail);

            if (hasProductType && hasLegalEntityId && hasLegalEntityAccountId) {
                let hasDuplicates = this.baseComponentService.HasDuplicateConcessionAccountProduct(
                    lendingConcession.lendingConcessionDetails,
                    concessionFormItem.get('productType').value.id,
                    concessionFormItem.get('accountNumber').value.legalEntityId,
                    concessionFormItem.get('accountNumber').value.legalEntityAccountId);

                if (hasDuplicates) {
                    this.addValidationError("Duplicate Account / Product pricing found. Please select different account.");

                    break;
                }
            }

        }

        const conditions = <FormArray>this.lendingConcessionForm.controls['conditionItemsRows'];

        let concessionConditionReturnObject = this.baseComponentService.getConsessionConditionData(conditions, lendingConcession.concessionConditions, this.validationError);
        lendingConcession.concessionConditions = concessionConditionReturnObject.concessionConditions;
        this.validationError = concessionConditionReturnObject.validationError;

        return lendingConcession;
    }

    getBackgroundColour(rowIndex: number) {
        const control = this.getLendingConcessionItemRows();

        if (String(control.controls[rowIndex].get('isExpired').value) == "true") {
            return "#EC7063";
        }

        if (String(control.controls[rowIndex].get('isExpiring').value) == "true") {
            return "#F5B041";
        }

        return "";
    }

    bcmApproveConcession() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;
        this.baseComponentService.isAppprovingOrDeclining = true;

        var lendingConcession = this.getLendingConcession(false);
        lendingConcession.concession.subStatus = ConcessionSubStatus.PCMPending;
        lendingConcession.concession.bcmUserId = this.lendingConcession.currentUser.id;

        if (!lendingConcession.concession.comments) {
            lendingConcession.concession.comments = "Forwarded";
        }

        if (!this.validationError) {

            lendingConcession = this.SetLendingMargin(lendingConcession);

            this.lendingService.postUpdateLendingData(lendingConcession).subscribe(entity => {
                console.log("data saved");
                this.canBcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.lendingConcession = entity;
                this.canEdit = false;
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
        this.baseComponentService.isAppprovingOrDeclining = true;

        var lendingConcession = this.getLendingConcession(false);
        lendingConcession.concession.status = ConcessionStatus.Declined;
        lendingConcession.concession.subStatus = ConcessionSubStatus.BCMDeclined;
        lendingConcession.concession.bcmUserId = this.lendingConcession.currentUser.id;

        if (!lendingConcession.concession.comments) {
            lendingConcession.concession.comments = ConcessionStatus.Declined;
        }

        if (!this.validationError) {

            lendingConcession = this.SetLendingMargin(lendingConcession);

            this.lendingService.postUpdateLendingData(lendingConcession).subscribe(entity => {
                console.log("data saved");
                this.canBcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.lendingConcession = entity;
                this.canEdit = false;
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
        this.baseComponentService.isAppprovingOrDeclining = true;

        var lendingConcession = this.getLendingConcession(false);

        if (this.hasChanges) {
            lendingConcession.concession.status = ConcessionStatus.Pending;

            if (this.lendingConcession.currentUser.isHO) {
                lendingConcession.concession.subStatus = ConcessionSubStatus.HOApprovedWithChanges;
                lendingConcession.concession.hoUserId = this.lendingConcession.currentUser.id;
            } else {
                lendingConcession.concession.subStatus = ConcessionSubStatus.PCMApprovedWithChanges;
                lendingConcession.concession.pcmUserId = this.lendingConcession.currentUser.id;
            }

            if (!lendingConcession.concession.comments) {
                lendingConcession.concession.comments = ConcessionStatus.ApprovedWithChanges;
            }

            lendingConcession.concession.concessionComments = this.GetChanges(lendingConcession.concession.id);

        } else {
            lendingConcession.concession.status = ConcessionStatus.Approved;

            if (this.lendingConcession.currentUser.isHO) {
                lendingConcession.concession.subStatus = ConcessionSubStatus.HOApproved;
                lendingConcession.concession.hoUserId = this.lendingConcession.currentUser.id;
            } else {
                lendingConcession.concession.subStatus = ConcessionSubStatus.PCMApproved;
                lendingConcession.concession.pcmUserId = this.lendingConcession.currentUser.id;
            }

            if (!lendingConcession.concession.comments) {
                lendingConcession.concession.comments = ConcessionStatus.Approved;
            }
        }

        if (!this.validationError) {

            lendingConcession = this.SetLendingMargin(lendingConcession);

            this.lendingService.postUpdateLendingData(lendingConcession).subscribe(entity => {
                console.log("data saved");
                this.canPcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.lendingConcession = entity;
                this.canEdit = false;
                this.isLoading = false;
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

        const concessions = this.getLendingConcessionItemRows();

        //this is detailed line items,  but not yet the controls
        for (let concessionFormItem of concessions.controls) {

            let controls = (<FormGroup>concessionFormItem).controls;

            for (const fieldname in controls) {

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

        var lendingConcession = this.getLendingConcession(false);

        lendingConcession.concession.status = ConcessionStatus.Declined;

        if (!lendingConcession.concession.comments) {
            lendingConcession.concession.comments = ConcessionStatus.Declined;
        }

        if (this.lendingConcession.currentUser.isHO) {
            lendingConcession.concession.subStatus = ConcessionSubStatus.HODeclined;
            lendingConcession.concession.hoUserId = this.lendingConcession.currentUser.id;
        } else {
            lendingConcession.concession.subStatus = ConcessionSubStatus.PCMDeclined;
            lendingConcession.concession.pcmUserId = this.lendingConcession.currentUser.id;
        }

        if (!this.validationError) {

            lendingConcession = this.SetLendingMargin(lendingConcession);

            this.lendingService.postUpdateLendingData(lendingConcession).subscribe(entity => {
                console.log("data saved");
                this.canPcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.lendingConcession = entity;
                this.canEdit = false;
                this.isLoading = false;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        } else {
            this.isLoading = false;
        }
    }

    onSelectedExtensionFee(extensionFee: number) {
        this.selectedExtensionFee = extensionFee;
    }

    extensionDisclamer() {

        this.editType = EditTypeEnum.Extend;

        var isOverdraft = this.lendingConcession.lendingConcessionDetails.find(item => {
            if (item.productType === ProductTypeEnum.Overdraft
                || item.productType === ProductTypeEnum.RCP) {
                return true
            }
        });

        if (isOverdraft && this.selectedExtensionFee == null) {
            this.extendDisclamerModal.show();
        } else {
            this.extendConcession();
            
        }
    }

    extensionDisclamerClose() {
        this.extendDisclamerModal.hide();
    }

    extendConcession() {

        if (this.selectedExtensionFee == null) {
            return;
        } else {

           const concessions = this.getLendingConcessionItemRows();
           for (let concessionFormItem of concessions.controls)
           {
               if (concessionFormItem.get('productType').value.description == ProductTypeEnum.Overdraft
                   || concessionFormItem.get('productType').value.description == ProductTypeEnum.RCP) {
                   concessionFormItem.get('extensionFee').setValue(this.selectedExtensionFee);
                }
            }
        }

        this.extensionDisclamerClose();

        if (this.canExtend && this.motivationEnabled == false) {
            this.motivationEnabled = true;
            this.lendingConcessionForm.controls['motivation'].setValue('');
            this.showMotivationDisclaimer = true;

        } else {


            this.showMotivationDisclaimer = false;
            this.validationError = null;

             var extendConceModel = new extendConcessionModel()
                   extendConceModel.concessionReferenceId = this.concessionReferenceId;

            if (this.lendingConcessionForm.controls['motivation'].value)
                    extendConceModel.motivation = this.lendingConcessionForm.controls['motivation'].value;
                else
                    this.addValidationError("Motivation not captured");
                    this.isLoading = false;

            if (!this.validationError) {

                if (confirm("Are you sure you want to extend this concession?")) {
                    this.isLoading = true;
                    this.errorMessage = null;
                    this.validationError = null;

                    this.lendingService.postExtendConcession(extendConceModel, this.selectedExtensionFee).subscribe(entity => {
                        this.lendingConcession = entity;
                        window.location.reload();
                        this.populateFormFromLendingConcession();
                        console.log("data saved");
                        this.canBcmApprove = false;
                        this.canBcmApprove = false;
                        this.canExtend = false;
                        this.canRenew = false;
                        this.canRecall = false;
                        this.motivationEnabled = false;
                        this.canUpdate = false;
                        this.canArchive = false;
                        this.saveMessage = entity.concession.childReferenceNumber;
                        this.isLoading = false;
                        this.lendingConcession = entity;
                        this.errorMessage = null;
                        this.validationError = null;
                    }, error => {
                        this.errorMessage = <any>error;
                        this.isLoading = false;
                    });
                  }
               }
        }
    }

    disableRows() {

        const concessions = this.getLendingConcessionItemRows();
        for (let concessionFormItem of concessions.controls) {

            concessionFormItem.disable();
        }
    }

    onLimitChanged(rowIndex: number, $event) {
        var limit = $event.target.value;
        const concessions = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];
        let rowLendingTieredRates = this.getRowTieredRates(rowIndex);
        rowLendingTieredRates[0].limit = limit;

        this.setTwoNumberDecimal($event);
    }

    onPrimeChanged(rowIndex: number, $event) {
        var marginAgainstPrime = $event.target.value;
        const concessions = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];
        let rowLendingTieredRates = this.getRowTieredRates(rowIndex);
        rowLendingTieredRates[0].marginToPrime = marginAgainstPrime;

        //update lending marging
        if (this.editType == 'UpdateApproved') {
            rowLendingTieredRates[0].approvedMap = marginAgainstPrime;
            concessions.controls[rowIndex].get('approvedMarginAgainstPrime').setValue(marginAgainstPrime);
        }

        this.setThreeNumberDecimal($event);
    }

    private getRowTieredRates(rowIndex: number) {
        const concessions = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];
        let rowLendingTieredRates: LendingConcessionTieredRate[] = concessions.controls[rowIndex].get('lendingTieredRates').value;
        if (rowLendingTieredRates == null) {
            rowLendingTieredRates = [];
        }
        if (rowLendingTieredRates.length == 0) {
            let tieredRate = new LendingConcessionTieredRate();
            tieredRate.id = 0;
            tieredRate.concessionLendingId = 0;
            tieredRate.limit = 0;
            tieredRate.marginToPrime = 0;

            rowLendingTieredRates.push(tieredRate);
        }
        return rowLendingTieredRates;
    }

    showTieredRateButton(rowIndex: number) {
        const control = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];
        let currentRow = control.controls[rowIndex];
        var productType = currentRow.get('productType').value;
        // Is the product Overdraft or Temporary Overdraft?
        if (productType.description == ProductTypeEnum.Overdraft || productType.description == ProductTypeEnum.TemporaryOverdraft) {

            return true;
        }
        else {

            return false;
        }


    }

    openTieredRateModal(rowIndex) {
        if (this.showTieredRateButton(rowIndex)) {
            this.tieredRateModal.show();
            this.selectedRowIndex = rowIndex;
            const concessions = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];
            this.selectedLineItemTieredRates = concessions.controls[rowIndex].get('lendingTieredRates').value;

            this.currentRowIndex = rowIndex;
            this.rollbackTieredRates = JSON.parse(JSON.stringify(this.selectedLineItemTieredRates));

            if (this.selectedLineItemTieredRates == null || this.selectedLineItemTieredRates.length == 0) {
                this.addNewTieredRateRow();
            }
            this.selectedLineItemTieredRates.forEach((item) => {
                item.limitString = this.baseComponentService.formatDecimal(item.limit).toString();
                item.marginToPrimeString = this.baseComponentService.formatDecimal(item.marginToPrime).toString();
                item.approvedMapString = this.baseComponentService.formatDecimal(item.approvedMap).toString();
            });
        }
    }

    addNewTieredRateRow() {
        if (this.selectedLineItemTieredRates == null) {
            this.selectedLineItemTieredRates = [];
        }

        this.tieredRateMessage = "";
        if (this.selectedLineItemTieredRates.length < 3) {
            let tieredRate = new LendingConcessionTieredRate();
            tieredRate.id = 0;
            tieredRate.concessionLendingId = 0;
            tieredRate.limit = 0;
            tieredRate.marginToPrime = 0;

            this.selectedLineItemTieredRates.push(tieredRate);
        }
        else {
            this.tieredRateMessage = "Only three Tiered Rates allowed";
        }

    }

    deleteTieredRateRow(rowIndex: number) {
        this.tieredRateMessage = "";
        this.selectedLineItemTieredRates = this.selectedLineItemTieredRates.filter((item) => {
            return item != this.selectedLineItemTieredRates[rowIndex];
        });
    }

    saveTieredRates() {
        this.selectedLineItemTieredRates.forEach((item) => {
            item.limit = this.baseComponentService.unformat(<any>item.limitString);
            item.marginToPrime = this.baseComponentService.unformat(<any>item.marginToPrimeString);
        });
        this.tieredRateMessage = "";
        const concessions = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];
        concessions.controls[this.selectedRowIndex].get('lendingTieredRates').setValue(this.selectedLineItemTieredRates);
        concessions.controls[this.selectedRowIndex].get('limit').setValue(this.baseComponentService.formatDecimal(this.selectedLineItemTieredRates[0].limit));
        concessions.controls[this.selectedRowIndex].get('marginAgainstPrime').setValue(this.baseComponentService.formatDecimalThree(this.selectedLineItemTieredRates[0].marginToPrime));
        this.selectedRowIndex = 0;
        this.selectedLineItemTieredRates = [];
        this.closeTieredRatesModal(false);
    }

    closeTieredRatesModal(isRollback: boolean) {
        if (isRollback) {
            const concessions = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];
            concessions.controls[this.currentRowIndex].get('lendingTieredRates').setValue(this.rollbackTieredRates);
        }
        this.selectedRowIndex = 0;
        this.selectedLineItemTieredRates = [];
        this.tieredRateModal.hide();
    }

    loopRows() {
        const concessions = this.getLendingConcessionItemRows();
        let rowIndex = 0;
        for (let concessionFormItem of concessions.controls) {

            this.productTypeChanged(rowIndex);

            rowIndex++;
        }

    }

    editConcession(editType: string) {
        this.canBcmApprove = false;
        this.motivationEnabled = true;
        this.canEdit = true;
        this.canBcmApprove = false;
        this.canExtend = false;
        this.canRenew = false;
        this.canRecall = false;
        this.isEditing = true;
        this.isRecalling = false;
        this.editType = editType;
        this.canResubmit = false;
        this.canUpdate = false;
        this.canArchive = false;

     
        const concessions = this.getLendingConcessionItemRows();

        if (editType == EditTypeEnum.Renew) { 

            let canUpdateExpiryDate: boolean = true;
        
            for (let concessionFormItem of concessions.controls) {
                // Existing ExpiryDate: ExpiryDate must be set 12 months from the existing ExpiryDate.
                if (concessionFormItem.get('expiryDate').value) {
                    let expiryDate = new Date(concessionFormItem.get('expiryDate').value);
                    expiryDate = new Date(expiryDate.setFullYear(expiryDate.getFullYear() + 1));
                    concessionFormItem.get('expiryDate').setValue(this.datepipe.transform(expiryDate, 'yyyy-MM-dd'));
                    
                }
                
                //The term on Overdraft must default to 12 months. 
                if (concessionFormItem.get('productType').value.description == ProductTypeEnum.Overdraft
                    || concessionFormItem.get('productType').value.description == ProductTypeEnum.RCP) {
                    concessionFormItem.get('term').setValue(12);
                    canUpdateExpiryDate = true;
                }

            }
          
        }
        if (editType == EditTypeEnum.Renew) {
            this.baseComponentService.isRenewing = true;
        }

       
        this.lendingConcessionForm.controls['motivation'].setValue('');
    }

    saveConcession() {
        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;

        var lendingConcession = this.getLendingConcession(true);

        lendingConcession.concession.status = ConcessionStatus.Pending;
        lendingConcession.concession.subStatus = ConcessionSubStatus.BCMPending;
        lendingConcession.concession.type = "Existing";
        lendingConcession.concession.referenceNumber = this.concessionReferenceId;


       if (!this.validationError) {
            this.lendingService.postChildConcession(lendingConcession, this.editType).subscribe(entity => {
                console.log("data saved");
                this.isEditing = false;
                this.saveMessage = entity.concession.childReferenceNumber;
                this.lendingConcession = entity;
                this.canEdit = false;
                this.motivationEnabled = false;
                this.isLoading = false;
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
            this.canArchive = false;
            this.motivationEnabled = true;
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

        var lendingConcession = this.getLendingConcession(false);


        lendingConcession.concession.status = ConcessionStatus.Pending;
        lendingConcession.concession.subStatus = ConcessionSubStatus.BCMPending;
        lendingConcession.concession.referenceNumber = this.concessionReferenceId;

        if (!this.validationError) {
            this.lendingService.postRecallLendingData(lendingConcession).subscribe(entity => {
                console.log("data saved");
                this.isRecalling = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.lendingConcession = entity;
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

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

    requestorApproveConcession() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;
        this.baseComponentService.isAppprovingOrDeclining = true;

        var lendingConcession = this.getLendingConcession(false);
        lendingConcession.concession.status = ConcessionStatus.ApprovedWithChanges;
        lendingConcession.concession.subStatus = ConcessionSubStatus.RequestorAcceptedChanges;
        lendingConcession.concession.requestorId = this.lendingConcession.currentUser.id;

        if (!lendingConcession.concession.comments) {
            lendingConcession.concession.comments = "Accepted Changes";
        }

        if (!this.validationError) {

            lendingConcession = this.SetLendingMargin(lendingConcession);

            this.lendingService.postUpdateLendingData(lendingConcession).subscribe(entity => {
                console.log("data saved");
                this.canApproveChanges = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.lendingConcession = entity;
                this.canEdit = false;
                this.isLoading = false;
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

        var lendingConcession = this.getLendingConcession(false);
        lendingConcession.concession.status = ConcessionStatus.Declined;
        lendingConcession.concession.subStatus = ConcessionSubStatus.RequestorDeclinedChanges;
        lendingConcession.concession.requestorId = this.lendingConcession.currentUser.id;

        if (!lendingConcession.concession.comments) {
            lendingConcession.concession.comments = "Declined Changes";
        }

        if (!this.validationError) {

            lendingConcession = this.SetLendingMargin(lendingConcession);

            this.lendingService.postUpdateLendingData(lendingConcession).subscribe(entity => {
                console.log("data saved");
                this.canApproveChanges = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.lendingConcession = entity;
                this.canEdit = false;
                this.isLoading = false;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        } else {
            this.isLoading = false;
        }
    }

    archiveConcessiondetail(concessionDetailId: number) {

        if (confirm("Please note that the account will be put back to standard pricing. Are you sure you want to delete this concession ?")) {
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

    setThreeNumberDecimal($event) {
        $event.target.value = this.baseComponentService.formatDecimalThree($event.target.value);
    }

    formatDecimal(itemValue: number) {
        return this.baseComponentService.formatDecimal(itemValue);
    }

    formatDecimal3(itemValue: number) {
        return this.baseComponentService.formatDecimalThree(itemValue);
    }

    getNumberInput(input) {
        this.lendingConcessionForm.controls['smtDealNumber'].setValue(this.baseComponentService.removeLetters(input.value));
    }

    canEditSmtDealNumber() {
        return (this.isRecalling || this.canEdit) ? null : '';
    }

    isMotivationEnabled() {

         return this.motivationEnabled ? null : '';
  
    }

    disableField(index: number, fieldname: string) {
        let canUpdateExpiryDate: boolean = true;

        if (fieldname == "expiryDate" && this.editType != null &&
            (this.editType == EditTypeEnum.Renew || this.editType == EditTypeEnum.UpdateApproved)) {
            {
                canUpdateExpiryDate = false;
            }
        }

    
        if (this.editType == EditTypeEnum.UpdateApproved &&
            this.lendingConcession.lendingConcessionDetails.length == 1
             && this.lendingConcession.lendingConcessionDetails[0].productType === ProductTypeEnum.TemporaryOverdraft) {
                    
                  return super.disableTempOverDraftField(
                        this.selectedProductTypeFieldLogics[index],
                        fieldname,
                        this.canEdit && canUpdateExpiryDate,
                        this.canEdit != null
            );

        }
        else { 

            return super.disableFieldBase(
                this.selectedConditionTypes[index],
                this.lendingConcession.lendingConcessionDetails[index],
                this.selectedProductTypeFieldLogics[index],
                fieldname,
                this.canEdit && canUpdateExpiryDate,
                this.canEdit != null
            );
        }
    }

    getNgClassForField(rowIndex: number) {
        let rowTieredRates = this.getRowTieredRates(rowIndex);
        if (rowTieredRates.length < 2) {
            return "form-control";
        }

        if (this.showTieredRateButton(rowIndex)) {
            return "form-control tiered-rate-field";
        }
        else {
            return "form-control";
        }
    }

    SetLendingMargin(lendingConcession) {

        for (var i = 0; i < lendingConcession.lendingConcessionDetails.length; i++) {
            if (lendingConcession.lendingConcessionDetails[i].lendingConcessionDetailTieredRates) {
                if (lendingConcession.lendingConcessionDetails[i].lendingConcessionDetailTieredRates.length > 0) {
                    lendingConcession.lendingConcessionDetails[i].marginAgainstPrime = lendingConcession.lendingConcessionDetails[i].lendingConcessionDetailTieredRates[0].marginToPrime
                    lendingConcession.lendingConcessionDetails[i].approvedMap = lendingConcession.lendingConcessionDetails[i].lendingConcessionDetailTieredRates[0].approvedMap
                }
            }
        }
        return lendingConcession;
    }
    
}
