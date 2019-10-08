import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { Location, DatePipe } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { RiskGroup } from "../models/risk-group";
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';

import { Period } from "../models/period";
import { PeriodType } from "../models/period-type";

import { LookupDataService } from "../services/lookup-data.service";
import { ConcessionCondition } from "../models/concession-condition";

import { Router, RouterModule } from '@angular/router';

import { DecimalPipe } from '@angular/common';
import { ConcessionTypes } from '../constants/concession-types';
import { ConditionType } from "../models/condition-type";
import { GlmsTierData } from "../models/glms-tier-data";
import { LegalEntity } from "../models/legal-entity";

import { InterestType } from "../models/interest-type";
import { SlabType } from "../models/slab-type";
import { InterestPricingCategory } from "../models/interest-pricing-category";
import { GlmsGroup } from "../models/glms-group"
import { RateType } from "../models/rate-type";

import { BaseComponentService } from '../services/base-component.service';
import * as moment from 'moment';
import { MOnthEnum } from '../models/month-enum';
import { GlmsView } from '../models/glms-view';
import { GlmsConcessionService } from '../services/glms-concession.service';
import { ProductType } from '../models/product-type';
import { ClientAccountArray } from '../models/client-account-array';
import { ClientAccount } from '../models/client-account';
import { GlmsConcession } from '../models/glms-concession';
import { ConcessionComment } from "../models/concession-comment";
import { Concession } from '../models/concession';
import { GlmsConcessionDetail } from '../models/glms-concession-detail';
import { GlmsBaseService } from '../services/glms-base.service';
import { UserService } from '../services/user.service';
import { UserConcessionsService } from "../services/user-concessions.service";
import { Http } from '@angular/http';
import { BaseRateCode } from '../models/base-rate-code';
import { ConcessionStatus } from '../constants/concession-status';
import { ConcessionSubStatus } from '../constants/concession-sub-status';

@Component({
    selector: 'app-glms-view-concession',
    templateUrl: './glms-view-concession.component.html',
    styleUrls: ['./glms-view-concession.component.css']
})
export class GlmsViewConcessionComponent extends GlmsBaseService implements OnInit {
    private sub: any;

    errorMessage: String;
    validationError: String[];
    saveMessage: String;
    warningMessage: String;
    showHide = false;
    observableRiskGroup: Observable<RiskGroup>;
    riskGroup: RiskGroup;
    riskGroupNumber: number;
    legalEntity: LegalEntity;
    sapbpid: number;
    today: string;
    canBcmApprove = false;
    canPcmApprove = false;
    hasChanges = false;
    canExtend = false;
    canRenew = false;
    canRecall = false;
    isEditing = false;
    motivationEnabled = false;
    canEdit = false;
    glmsConcessionItemIndex: number;
    concessionReferenceId: string;

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

    public glmsConcessionForm: FormGroup;

    entityName: string;
    entityNumber: string;

    isLoading = true;
    selectedAccountNumbers: ClientAccountArray[];
    clientAccounts: ClientAccount[];

    selectedSlabType: SlabType[];
    slabType: SlabType[];

    selectedRateType: RateType[];
    rateType: RateType[];

    selectedGlmsGroup: GlmsGroup[];
    glmsGroup: GlmsGroup[];

    selectedInterestType: InterestType[];
    interestType: InterestType[];

    selectedBaseRateCode: BaseRateCode[];
    baseRateCode: BaseRateCode[];

    selectedInterestPricingCategory: InterestPricingCategory[];
    interestPricingCategory: InterestPricingCategory[];

    observableGlmsView: Observable<GlmsView>;
    glmsView: GlmsView = new GlmsView();
    selectedProductTypes: ProductType[];
    productTypes: ProductType[];

    observablePeriods: Observable<Period[]>;
    periods: Period[];

    observablePeriodTypes: Observable<PeriodType[]>;
    periodTypes: PeriodType[];

    selectedConditionTypes: ConditionType[];

    observableConditionTypes: Observable<ConditionType[]>;
    conditionTypes: ConditionType[];

    selectedGlmsTierData: GlmsTierData[];

    observableGlmsConcession: Observable<GlmsConcession>;
    glmsConcession: GlmsConcession;

    observableGlmsTierData: Observable<GlmsTierData[]>;
    glmsTierData: GlmsTierData[];


    constructor(private route: ActivatedRoute,
        public router: Router,
        private formBuilder: FormBuilder,
        private location: Location,
        public http: Http,
        @Inject(LookupDataService) private lookupDataService,
        @Inject(GlmsConcessionService) private glmsConcessionService,
        @Inject(UserConcessionsService) private userConcessionsService,
        public userService: UserService) {
        super(http, router, userService);
        this.riskGroup = new RiskGroup();

        this.periods = [new Period()];
        this.periodTypes = [new PeriodType()];

        this.productTypes = [new ProductType()];
        this.selectedProductTypes = [new ProductType()];

        this.periods = [new Period()];
        this.periodTypes = [new PeriodType()];

        this.conditionTypes = [new ConditionType()];
        this.selectedConditionTypes = [new ConditionType()];

        this.glmsTierData = [new GlmsTierData()];
        this.selectedGlmsTierData = [new GlmsTierData()];

        this.clientAccounts = [new ClientAccount()];
        this.selectedAccountNumbers = [new ClientAccountArray()];

        this.interestType = [new InterestType()];
        this.selectedInterestType = [new InterestType()];

        this.glmsGroup = [new GlmsGroup()];
        this.selectedGlmsGroup = [new GlmsGroup()];

        this.interestPricingCategory = [new InterestPricingCategory()];
        this.selectedInterestPricingCategory = [new InterestPricingCategory()];

        this.slabType = [new SlabType()];
        this.selectedSlabType = [new SlabType()];

        this.rateType = [new RateType()];
        this.selectedRateType = [new RateType()];

        this.baseRateCode = [new BaseRateCode()];
        this.selectedBaseRateCode = [new BaseRateCode()];

        this.glmsConcession.concession.concessionComments = [new ConcessionComment()];

    }

    ngOnInit() {

        this.today = this.GetTodayDate();

        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
            this.sapbpid = +params['sapbpid'];
            this.concessionReferenceId = params['concessionReferenceId'];
            
        });


        this.glmsConcessionForm = this.formBuilder.group({
            concessionItemRows: this.formBuilder.array([this.initConcessionItemRows()]),
            conditionItemsRows: this.formBuilder.array([]),
            tierItemsRows: this.formBuilder.array([]),
            smtDealNumber: new FormControl(),
            motivation: new FormControl(),
        });

        this.getInitialData();
    }

    populateForm() {
        if (this.concessionReferenceId) {
            this.observableGlmsConcession = this.glmsConcessionService.getGlmsConcessionData(this.concessionReferenceId);
            this.observableGlmsConcession.subscribe(glmsConcession => {
                this.glmsConcession = glmsConcession;


                if (glmsConcession.concession.status == ConcessionStatus.Pending && glmsConcession.concession.subStatus == ConcessionSubStatus.BCMPending) {
                    this.canBcmApprove = glmsConcession.currentUser.canBcmApprove;
                }

                if (glmsConcession.concession.status == ConcessionStatus.Pending && glmsConcession.concession.subStatus == ConcessionSubStatus.PCMPending) {
                    if (this.glmsConcession.currentUser.isHO) {
                        this.canPcmApprove = glmsConcession.currentUser.canPcmApprove
                    } else {
                        this.canPcmApprove = glmsConcession.currentUser.canPcmApprove && glmsConcession.currentUser.canApprove;
                    }

                    this.canEdit = glmsConcession.currentUser.canPcmApprove;

                }

                //if it's still pending and the user is a requestor then they can recall it
                if (glmsConcession.concession.status == ConcessionStatus.Pending && glmsConcession.concession.subStatus == ConcessionSubStatus.BCMPending) {
                    this.canRecall = glmsConcession.currentUser.canRequest && glmsConcession.concession.isAENumberLinkedAccountExecutiveOrAssistant;
                }

                if (glmsConcession.concession.status == ConcessionStatus.Pending &&
                    (glmsConcession.concession.subStatus == ConcessionSubStatus.PCMApprovedWithChanges || glmsConcession.concession.subStatus == ConcessionSubStatus.HOApprovedWithChanges)) {
                    this.canApproveChanges = glmsConcession.currentUser.canRequest && glmsConcession.concession.isAENumberLinkedAccountExecutiveOrAssistant;
                }

                if (glmsConcession.concession.status === ConcessionStatus.Approved ||
                    glmsConcession.concession.status === ConcessionStatus.ApprovedWithChanges) {
                    this.isApproved = true;
                }

                //if the concession is set to can extend and the user is a requestor, then they can extend or renew it
                this.canExtend = glmsConcession.concession.canExtend && glmsConcession.currentUser.canRequest;
                this.canRenew = glmsConcession.concession.canRenew && glmsConcession.currentUser.canRequest;

                //set the resubmit and update permissions
                this.canResubmit = glmsConcession.concession.canResubmit && glmsConcession.currentUser.canRequest;
                this.canUpdate = glmsConcession.concession.canUpdate && glmsConcession.currentUser.canRequest;

                this.canArchive = glmsConcession.concession.canArchive && glmsConcession.currentUser.canRequest;
                this.isInProgressExtension = glmsConcession.concession.isInProgressExtension;
                this.isInProgressRenewal = glmsConcession.concession.isInProgressRenewal;

                this.glmsConcessionForm.controls['smtDealNumber'].setValue(this.glmsConcession.concession.smtDealNumber);
                this.glmsConcessionForm.controls['motivation'].setValue(this.glmsConcession.concession.motivation);

                let rowIndex = 0;

                //for (let glmsConcessionDetail of this.glmsConcession.glmsConcessionDetails) {

                //    if (rowIndex != 0) {
                //        this.addNewConcessionRow();
                //    }

                //    if (glmsConcessionDetail.productType == 'Overdraft') {

                //        glmsConcessionDetail.show_term = false;
                //        glmsConcessionDetail.show_reviewFeeType = true;
                //        glmsConcessionDetail.show_reviewFee = true;
                //        glmsConcessionDetail.show_uffFee = true;
                //        glmsConcessionDetail.show_frequency = false;
                //        glmsConcessionDetail.show_serviceFee = false;

                //    }
                //    else if (glmsConcessionDetail.productType === "Temporary Overdraft") {


                //        glmsConcessionDetail.show_term = true;
                //        glmsConcessionDetail.show_reviewFeeType = true;
                //        glmsConcessionDetail.show_reviewFee = true;
                //        glmsConcessionDetail.show_uffFee = true;
                //        glmsConcessionDetail.show_frequency = false;
                //        glmsConcessionDetail.show_serviceFee = false;


                //    }
                //    else if (glmsConcessionDetail.productType.indexOf("VAF") == 0) {

                //        glmsConcessionDetail.show_term = true;
                //        glmsConcessionDetail.show_reviewFeeType = false;
                //        glmsConcessionDetail.show_reviewFee = false;
                //        glmsConcessionDetail.show_uffFee = false;
                //        glmsConcessionDetail.show_frequency = true;
                //        glmsConcessionDetail.show_serviceFee = true;
                //    }
                //    else {

                //        glmsConcessionDetail.show_term = true;
                //        glmsConcessionDetail.show_reviewFeeType = false;
                //        glmsConcessionDetail.show_reviewFee = false;
                //        glmsConcessionDetail.show_uffFee = false;
                //        glmsConcessionDetail.show_frequency = false;
                //        glmsConcessionDetail.show_serviceFee = false;
                //    }


                //    const concessions = <FormArray>this.glmsConcessionForm.controls['concessionItemRows'];
                //    let currentConcession = concessions.controls[concessions.length - 1];

                //    currentConcession.get('glmsConcessionDetailId').setValue(glmsConcessionDetail.glmsConcessionDetailId);
                //    currentConcession.get('concessionDetailId').setValue(glmsConcessionDetail.concessionDetailId);

                //    let selectedProductType = this.productTypes.filter(_ => _.id === glmsConcessionDetail.productTypeId);
                //    currentConcession.get('productType').setValue(selectedProductType[0]);

                //    this.selectedProductTypes[rowIndex] = selectedProductType[0];


                //    if (this.clientAccounts) {
                //        let selectedAccountNo = this.clientAccounts.filter(_ => _.legalEntityAccountId == glmsConcessionDetail.legalEntityAccountId);
                //        currentConcession.get('accountNumber').setValue(selectedAccountNo[0]);
                //    }

                //    currentConcession.get('limit').setValue(this.formatDecimal(glmsConcessionDetail.limit));
                //    currentConcession.get('term').setValue(glmsConcessionDetail.term);
                //    currentConcession.get('marginAgainstPrime').setValue(this.formatDecimal4(glmsConcessionDetail.marginAgainstPrime));
                //    currentConcession.get('approvedMarginAgainstPrime').setValue(this.formatDecimal4(glmsConcessionDetail.approvedMap));
                //    currentConcession.get('initiationFee').setValue(this.formatDecimal4(glmsConcessionDetail.initiationFee));

                //    let selectedReviewFeeType = this.reviewFeeTypes.filter(_ => _.id == glmsConcessionDetail.reviewFeeTypeId);
                //    currentConcession.get('reviewFeeType').setValue(selectedReviewFeeType[0]);
                //    currentConcession.get('reviewFee').setValue(this.formatDecimal3(glmsConcessionDetail.reviewFee));
                //    currentConcession.get('uffFee').setValue(this.formatDecimal3(glmsConcessionDetail.uffFee));

                //    currentConcession.get('mrsEri').setValue(glmsConcessionDetail.mrsEri);

                //    currentConcession.get('serviceFee').setValue(this.formatDecimal3(glmsConcessionDetail.serviceFee));
                //    currentConcession.get('frequency').setValue(glmsConcessionDetail.frequency);

                //    if (glmsConcessionDetail.expiryDate) {
                //        var formattedExpiryDate = this.datepipe.transform(glmsConcessionDetail.expiryDate, 'yyyy-MM-dd');
                //        currentConcession.get('expiryDate').setValue(formattedExpiryDate);
                //    }

                //    if (glmsConcessionDetail.dateApproved) {
                //        var formattedDateApproved = this.datepipe.transform(glmsConcessionDetail.dateApproved, 'yyyy-MM-dd');
                //        currentConcession.get('dateApproved').setValue(formattedDateApproved);
                //    }

                //    currentConcession.get('isExpired').setValue(glmsConcessionDetail.isExpired);
                //    currentConcession.get('isExpiring').setValue(glmsConcessionDetail.isExpiring);

                //    rowIndex++;
                //}

                this.changearray = this.lookupDataService.checkforLC(this.glmsConcession.concession.status, this.glmsConcession.concession.subStatus, glmsConcession.concession.concessionComments);

                rowIndex = 0;

                for (let concessionCondition of this.glmsConcession.concessionConditions) {
                    this.addNewConditionRow();

                    const conditions = <FormArray>this.glmsConcessionForm.controls['conditionItemsRows'];
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

        return this.formBuilder.group({
            disablecontrolset: [''],
            productType: [''],
            accountNumber: [''],
            expiryDate: [''],
            slabType: [''],
            interestType: [''],
            interestPricingCategory: [''],
            glmsGroup: [''],
            concessionItemTier: [''],

        });
    }

    initTierItemRows() {
        this.glmsTierData.push(new GlmsTierData());

        return this.formBuilder.group({
            tieredFrom: [''],
            tieredTo: [''],
            rateType: [''],
            baseRate: [''],
            spread: [''],
            value: [''],
        });
    }

    getInitialData() {
        if (this.riskGroupNumber != null && this.riskGroupNumber != 0) {
            Observable.forkJoin([
                this.lookupDataService.getProductTypes(ConcessionTypes.Glms),
                this.lookupDataService.getPeriods(),
                this.lookupDataService.getPeriodTypes(),
                this.lookupDataService.getConditionTypes(),
                this.lookupDataService.getRiskGroup(this.riskGroupNumber),
                this.lookupDataService.getClientAccountsConcessionType(this.riskGroupNumber, this.sapbpid, ConcessionTypes.Glms),
                this.getGlmsGroup(),
                this.getInterestType(),
                this.getSlabType(),
                this.getRateType(),
                this.getBaseRateCode(),
                this.getInterestPricingCategory(),
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
                this.lookupDataService.getProductTypes(ConcessionTypes.Glms),
                this.lookupDataService.getPeriods(),
                this.lookupDataService.getPeriodTypes(),
                this.lookupDataService.getConditionTypes(),
                this.lookupDataService.getLegalEntity(this.sapbpid),
                this.lookupDataService.getClientAccountsConcessionType(this.riskGroupNumber, this.sapbpid, ConcessionTypes.Glms),
                this.getGlmsGroup(),
                this.getInterestType(),
                this.getSlabType(),
                this.getRateType(),
                this.getBaseRateCode(),
                this.getInterestPricingCategory(),

            ]).subscribe(results => {
                this.setInitialData(results, false);
                this.populateForm();
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        }
    }

    addValidationError(validationDetail) {
        if (!this.validationError)
            this.validationError = [];

        this.validationError.push(validationDetail);
    }

    setInitialData(results: {}[], isForRiskGroup: boolean) {

        if (isForRiskGroup) {
            this.riskGroup = <any>results[4];
        }
        else {
            this.legalEntity = <any>results[4];
        }
        this.periods = <any>results[1];
        this.periodTypes = <any>results[2];
        this.conditionTypes = <any>results[3];
        this.clientAccounts = <any>results[5];
        this.conditionTypes = <any>results[3];
        this.clientAccounts = <any>results[5];
        this.glmsGroup = <any>results[6];
        this.interestType = <any>results[7];
        this.slabType = <any>results[8];
        this.rateType = <any>results[9];
        this.baseRateCode = <any>results[10];
        this.interestPricingCategory = <any>results[11];

        this.isLoading = false;

        const control = <FormArray>this.glmsConcessionForm.controls['concessionItemRows'];
        const tierForm = <FormArray>this.glmsConcessionForm.controls['tierItemsRows'];


        if (this.productTypes) {
            control.controls[0].get('productType').setValue(this.productTypes[0]);

            this.selectedProductTypes[0] = this.productTypes[0];
        }

        if (this.glmsGroup) {
            control.controls[0].get('glmsGroup').setValue(this.glmsGroup[0]);

            this.selectedGlmsGroup[0] = this.glmsGroup[0];
        }

        if (this.interestType) {
            control.controls[0].get('interestType').setValue(this.interestType[0]);

            this.selectedInterestType[0] = this.interestType[0];
        }

        if (this.slabType) {
            control.controls[0].get('slabType').setValue(this.slabType[0]);

            this.selectedSlabType[0] = this.slabType[0];
        }

        if (this.interestPricingCategory) {
            control.controls[0].get('interestPricingCategory').setValue(this.interestPricingCategory[0]);

            this.selectedInterestPricingCategory[0] = this.interestPricingCategory[0];
        }

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

    getGlmsConcession(isNew: boolean): GlmsConcession {
        var glmsConcession = new GlmsConcession();
        glmsConcession.concession = new Concession();

        if (this.riskGroup)
            glmsConcession.concession.riskGroupId = this.riskGroup.id;
        if (this.legalEntity)
            glmsConcession.concession.legalEntityId = this.legalEntity.id;

        if (this.glmsConcessionForm.controls['smtDealNumber'].value) {
            glmsConcession.concession.smtDealNumber = this.glmsConcessionForm.controls['smtDealNumber'].value;
        }
        else
            this.addConcessionValidationError("SMT Deal Number not captured");

        if (this.glmsConcessionForm.controls['motivation'].value)
            glmsConcession.concession.motivation = this.glmsConcessionForm.controls['motivation'].value;
        else
            glmsConcession.concession.motivation = '.';


        const concessions = <FormArray>this.glmsConcessionForm.controls['concessionItemRows'];

        let hasTypeId: boolean = false;
        let hasLegalEntityId: boolean = false;
        let hasLegalEntityAccountId: boolean = false;

        for (let concessionFormItem of concessions.controls) {
            if (!glmsConcession.glmsConcessionDetails)
                glmsConcession.glmsConcessionDetails = [];

            let glmsConcessionDetail = new GlmsConcessionDetail();

            let applyexpirydate = false;

            if (concessionFormItem.get('glmsGroup').value) {

                glmsConcessionDetail.glmsGroupId = concessionFormItem.get('glmsGroup').value.id;
            }
            else
                this.addValidationError("Group not selected");


            if (concessionFormItem.get('interestPricingCategory').value) {
                glmsConcessionDetail.interestPricingCategoryId = concessionFormItem.get('interestPricingCategory').value.id;
            } else {

                this.addValidationError("Interest Pricing Category not selected");
            }

            if (concessionFormItem.get('slabType').value) {
                glmsConcessionDetail.slabTypeId = concessionFormItem.get('slabType').value.id;
            } else {

                this.addValidationError("Slab Type not selected");
            }

            if (concessionFormItem.get('interestType').value) {
                glmsConcessionDetail.interestTypeId = concessionFormItem.get('interestType').value.id;
            } else {

                this.addValidationError("Interest Type not selected");
            }

            if (concessionFormItem.get('expiryDate').value && concessionFormItem.get('expiryDate').value != "") {
                glmsConcessionDetail.expiryDate = new Date(concessionFormItem.get('expiryDate').value);
            }
            else {
                if (!applyexpirydate) {
                    this.addValidationError("Expiry date not selected");
                }
            }

            if (concessionFormItem.get('concessionItemTier').value && concessionFormItem.get('concessionItemTier').value != "") {
                glmsConcessionDetail.glmsTierData = concessionFormItem.get('concessionItemTier').value;
            }
            else {

                this.addValidationError("Concession line Tier data not set");
            }

            glmsConcession.glmsConcessionDetails.push(glmsConcessionDetail);

        }

        const conditions = <FormArray>this.glmsConcessionForm.controls['conditionItemsRows'];

        for (let conditionFormItem of conditions.controls) {
            if (!glmsConcession.concessionConditions)
                glmsConcession.concessionConditions = [];

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

            if (conditionFormItem.get('value').value == null || (<string>conditionFormItem.get('value').value).length < 1) {
                var value = conditionFormItem.get('conditionType').value;
                if (value != null && value.enableConditionValue == true)
                    this.addValidationError("Conditions: 'Value' is a mandatory field");
            }
            else if (conditionFormItem.get('value').value)
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

            if (conditionFormItem.get('period').value) {
                concessionCondition.periodId = conditionFormItem.get('period').value.id;
            } else {
                this.addValidationError("Period not selected");
            }

            if (conditionFormItem.get('periodType').value.description == 'Once-off' && conditionFormItem.get('period').value.description == 'Monthly') {
                this.addValidationError("Conditions: The Period 'Monthly' cannot be selected for Period Type 'Once-off'");
            }

            glmsConcession.concessionConditions.push(concessionCondition);
        }

        return glmsConcession;
    }

    conditionTypeChanged(rowIndex) {

        const control = <FormArray>this.glmsConcessionForm.controls['conditionItemsRows'];
        this.selectedConditionTypes[rowIndex] = control.controls[rowIndex].get('conditionType').value;

        let currentCondition = control.controls[rowIndex];

        currentCondition.get('conditionProduct').setValue(null);
        currentCondition.get('interestRate').setValue(null);
        currentCondition.get('volume').setValue(null);
        currentCondition.get('value').setValue(null);
    }

    openTier(x, i) {
        this.glmsConcessionItemIndex = i;
    }

    rateTypeChange(rowIndex: number) {

        console.log('Row:' + rowIndex);

        const control = <FormArray>this.glmsConcessionForm.controls['tierItemsRows'];
        let currentRow = control.controls[rowIndex];
        var rateType = currentRow.get('rateType').value;

        this.selectedProductTypes[rowIndex] = rateType;

        if (rateType.description === "V") {

            currentRow.get('spread').disable();
            currentRow.get('spread').setValue(null);

            currentRow.get('value').disable();
            currentRow.get('value').setValue(null);

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

        this.glmsConcessionForm.controls['motivation'].setValue('');
    }

    closeTierModal(x) {

        let tierItemsList = [new GlmsTierData()];

        let tierItem = new GlmsTierData();

        const concessions = <FormArray>this.glmsConcessionForm.controls['concessionItemRows'];
        const tierForm = <FormArray>this.glmsConcessionForm.controls['tierItemsRows'];

        for (let glmsTierFormItem of tierForm.controls) {

            if (glmsTierFormItem.get('spread').value) {
                tierItem.spread = glmsTierFormItem.get('spread').value;
            } else {
                this.addValidationError("Spread not selected");
            }

            if (glmsTierFormItem.get('tieredFrom').value) {
                tierItem.tierFrom = glmsTierFormItem.get('tieredFrom').value;
            } else {
                this.addValidationError("TieredFrom not selected");
            }

            if (glmsTierFormItem.get('tieredTo').value) {
                tierItem.tierTo = glmsTierFormItem.get('tieredTo').value;
            } else {
                this.addValidationError("TieredTo not selected");
            }

            if (glmsTierFormItem.get('value').value) {
                tierItem.value = glmsTierFormItem.get('value').value;
            } else {
                this.addValidationError("Value not selected");
            }

            if (glmsTierFormItem.get('baseRate').value.id) {
                tierItem.baseRateId = glmsTierFormItem.get('baseRate').value.id;
            } else {
                this.addValidationError("Base Rate not selected");
            }

            if (glmsTierFormItem.get('rateType').value.id) {
                tierItem.rateTypeId = glmsTierFormItem.get('rateType').value.id;
            } else {
                this.addValidationError("Rate Type not selected");
            }

            tierItemsList.push(tierItem);
        }

        concessions.controls[this.glmsConcessionItemIndex].get('concessionItemTier').setValue(tierItemsList);
    }

    addNewConcessionRow() {

        const control = <FormArray>this.glmsConcessionForm.controls['concessionItemRows'];

        var newRow = this.initConcessionItemRows();

        if (this.productTypes)
            newRow.controls['productType'].setValue(this.productTypes[0]);

        if (this.clientAccounts)
            newRow.controls['accountNumber'].setValue(this.clientAccounts[0]);

        control.push(newRow);

    }

    addNewConditionRow() {
        const control = <FormArray>this.glmsConcessionForm.controls['conditionItemsRows'];
        control.push(this.initConditionItemRows());
    }

    addNewConditionRowIfNone() {
        const control = <FormArray>this.glmsConcessionForm.controls['conditionItemsRows'];
        if (control.length == 0)
            control.push(this.initConditionItemRows());
    }

    addNewTierRow() {
        const control = <FormArray>this.glmsConcessionForm.controls['tierItemsRows'];
        control.push(this.initTierItemRows());
    }

    addNewTierRowIfNone() {
        const control = <FormArray>this.glmsConcessionForm.controls['tierItemsRows'];
        if (control.length == 0)
            control.push(this.initTierItemRows());
    }


    deleteConcessionRow(index: number) {
        if (confirm("Are you sure you want to remove this row?")) {
            const control = <FormArray>this.glmsConcessionForm.controls['concessionItemRows'];

            this.selectedProductTypes.splice(index, 1);
            this.selectedAccountNumbers.splice(index, 1);
            this.selectedInterestType.splice(index, 1);
            this.selectedRateType.splice(index, 1);
            this.selectedGlmsGroup.splice(index, 1);
            this.selectedInterestPricingCategory.splice(index, 1);
            this.selectedSlabType.splice(index, 1);

            control.removeAt(index);
        }
    }

    setTwoNumberDecimal($event) {
        $event.target.value = this.formatDecimal($event.target.value);
    }

    deleteConditionRow(index: number) {
        const control = <FormArray>this.glmsConcessionForm.controls['conditionItemsRows'];
        control.removeAt(index);
        this.selectedConditionTypes.splice(index, 1);
    }


    deleteTierRow(index: number) {
        const control = <FormArray>this.glmsConcessionForm.controls['tierItemsRows'];
        control.removeAt(index);
        this.glmsTierData.splice(index, 1);
    }

    disableRows() {

        const concessions = <FormArray>this.glmsConcessionForm.controls['concessionItemRows'];
        for (let concessionFormItem of concessions.controls) {

            concessionFormItem.disable();
        }
    }

    onExpiryDateChanged(itemrow) {
        this.validationError = null;
        var validationErrorMessage = this.expiringDateDifferenceValidation(itemrow.controls['expiryDate'].value);
        if (validationErrorMessage != null) {
            this.addValidationError(validationErrorMessage);
        }
    }
    getBackgroundColour(rowIndex: number) {
        const control = <FormArray>this.glmsConcessionForm.controls['concessionItemRows'];

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

        var glmsConcession = this.getGlmsConcession(false);
        glmsConcession.concession.subStatus = ConcessionSubStatus.PCMPending;
        glmsConcession.concession.bcmUserId = this.glmsConcession.currentUser.id;

        if (!glmsConcession.concession.comments) {
            glmsConcession.concession.comments = "Forwarded";
        }

        if (!this.validationError) {
            this.glmsConcessionService.postUpdateGlmsData(glmsConcession).subscribe(entity => {
             
                this.canBcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.glmsConcession = entity;
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

        var glmsConcession = this.getGlmsConcession(false);
        glmsConcession.concession.status = ConcessionStatus.Declined;
        glmsConcession.concession.subStatus = ConcessionSubStatus.BCMDeclined;
        glmsConcession.concession.bcmUserId = this.glmsConcession.currentUser.id;

        if (!glmsConcession.concession.comments) {
            glmsConcession.concession.comments = ConcessionStatus.Declined;
        }

        if (!this.validationError) {
            this.glmsConcessionService.postUpdateGlmsData(glmsConcession).subscribe(entity => {
               
                this.canBcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.glmsConcession = entity;
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

        var glmsConcession = this.getGlmsConcession(false);

        if (this.hasChanges) {
            glmsConcession.concession.status = ConcessionStatus.Pending;

            if (this.glmsConcession.currentUser.isHO) {
                glmsConcession.concession.subStatus = ConcessionSubStatus.HOApprovedWithChanges;
                glmsConcession.concession.hoUserId = this.glmsConcession.currentUser.id;
            } else {
                glmsConcession.concession.subStatus = ConcessionSubStatus.PCMApprovedWithChanges;
                glmsConcession.concession.pcmUserId = this.glmsConcession.currentUser.id;
            }

            if (!glmsConcession.concession.comments) {
                glmsConcession.concession.comments = ConcessionStatus.ApprovedWithChanges;
            }

            glmsConcession.concession.concessionComments = this.GetChanges(glmsConcession.concession.id);

        } else {
            glmsConcession.concession.status = ConcessionStatus.Approved;

            if (this.glmsConcession.currentUser.isHO) {
                glmsConcession.concession.subStatus = ConcessionSubStatus.HOApproved;
                glmsConcession.concession.hoUserId = this.glmsConcession.currentUser.id;
            } else {
                glmsConcession.concession.subStatus = ConcessionSubStatus.PCMApproved;
                glmsConcession.concession.pcmUserId = this.glmsConcession.currentUser.id;
            }

            if (!glmsConcession.concession.comments) {
                glmsConcession.concession.comments = ConcessionStatus.Approved;
            }
        }

        if (!this.validationError) {
            this.glmsConcessionService.postUpdateGlmsData(glmsConcession).subscribe(entity => {
                console.log("data saved");
                this.canPcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.glmsConcession = entity;
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

        const concessions = <FormArray>this.glmsConcessionForm.controls['concessionItemRows'];

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

        var glmsConcession = this.getGlmsConcession(false);

        glmsConcession.concession.status = ConcessionStatus.Declined;

        if (!glmsConcession.concession.comments) {
            glmsConcession.concession.comments = ConcessionStatus.Declined;
        }

        if (this.glmsConcession.currentUser.isHO) {
            glmsConcession.concession.subStatus = ConcessionSubStatus.HODeclined;
            glmsConcession.concession.hoUserId = this.glmsConcession.currentUser.id;
        } else {
            glmsConcession.concession.subStatus = ConcessionSubStatus.PCMDeclined;
            glmsConcession.concession.pcmUserId = this.glmsConcession.currentUser.id;
        }

        if (!this.validationError) {
            this.glmsConcessionService.postUpdateGlmsData(glmsConcession).subscribe(entity => {
                console.log("data saved");
                this.canPcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.glmsConcession = entity;
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

    extendConcession() {
        if (confirm("Are you sure you want to extend this concession?")) {
            this.isLoading = true;
            this.errorMessage = null;
            this.validationError = null;

            this.glmsConcessionService.postExtendConcession(this.concessionReferenceId).subscribe(entity => {
               
                this.canBcmApprove = false;
                this.canBcmApprove = false;
                this.canExtend = false;
                this.canRenew = false;
                this.canRecall = false;
                this.canUpdate = false;
                this.canArchive = false;
                this.saveMessage = entity.concession.childReferenceNumber;
                this.isLoading = false;
                this.glmsConcession = entity;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        }
    }


    loopRows() {
        const concessions = <FormArray>this.glmsConcessionForm.controls['concessionItemRows'];
        let rowIndex = 0;
        for (let concessionFormItem of concessions.controls)
        {
            rowIndex++;
        }

    }

    saveConcession() {
        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;

        var glmsConcession = this.getGlmsConcession(true);

        glmsConcession.concession.status = ConcessionStatus.Pending;
        glmsConcession.concession.subStatus = ConcessionSubStatus.BCMPending;
        glmsConcession.concession.type = "Existing";
        glmsConcession.concession.referenceNumber = this.concessionReferenceId;

        if (!this.validationError) {
            this.glmsConcessionService.postChildConcession(glmsConcession, this.editType).subscribe(entity => {
              
                this.isEditing = false;
                this.saveMessage = entity.concession.childReferenceNumber;
                this.glmsConcession = entity;
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

    saveUpdatedConcession() {
        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;

        var glmsConcession = this.getGlmsConcession(true);

        glmsConcession.concession.type = "Existing";
        glmsConcession.concession.referenceNumber = this.concessionReferenceId;

        if (!this.validationError) {
            this.glmsConcessionService.postUpdateGlmsData(glmsConcession, this.editType).subscribe(entity => {
              
                this.isEditing = false;
                this.saveMessage = entity.concession.childReferenceNumber;
                this.glmsConcession = entity;
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

        var glmsConcession = this.getGlmsConcession(true);

        glmsConcession.concession.status = ConcessionStatus.Pending;
        glmsConcession.concession.subStatus = ConcessionSubStatus.BCMPending;
        glmsConcession.concession.referenceNumber = this.concessionReferenceId;

        if (!this.validationError) {
            this.glmsConcessionService.postRecallGlmsData(glmsConcession).subscribe(entity => {
               
                this.isRecalling = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.glmsConcession = entity;
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

        var glmsConcession = this.getGlmsConcession(false);
        glmsConcession.concession.status = ConcessionStatus.ApprovedWithChanges;
        glmsConcession.concession.subStatus = ConcessionSubStatus.RequestorAcceptedChanges;
        glmsConcession.concession.requestorId = this.glmsConcession.currentUser.id;

        if (!glmsConcession.concession.comments) {
            glmsConcession.concession.comments = "Accepted Changes";
        }

        if (!this.validationError) {
            this.glmsConcessionService.postUpdateGlmsData(glmsConcession).subscribe(entity => {
               
                this.canApproveChanges = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.glmsConcession = entity;
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

        var glmsConcession = this.getGlmsConcession(false);
        glmsConcession.concession.status = ConcessionStatus.Declined;
        glmsConcession.concession.subStatus = ConcessionSubStatus.RequestorDeclinedChanges;
        glmsConcession.concession.requestorId = this.glmsConcession.currentUser.id;

        if (!glmsConcession.concession.comments) {
            glmsConcession.concession.comments = "Declined Changes";
        }

        if (!this.validationError) {
            this.glmsConcessionService.postUpdateGlmsData(glmsConcession).subscribe(entity => {
                
                this.canApproveChanges = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.glmsConcession = entity;
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


    goBack() {
        this.router.navigate(['/pricing', this.riskGroupNumber]);
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

}
