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
import { extendConcessionModel } from "../models/extendConcessionModel";

import { InterestType } from "../models/interest-type";
import { SlabType } from "../models/slab-type";
import { InterestPricingCategory } from "../models/interest-pricing-category";
import { GlmsGroup } from "../models/glms-group"
import { RateType } from "../models/rate-type";
import { ArchiveType } from "../models/archive-type";
import { EditTypeEnum } from '../models/edit-type-enum';

import { BaseComponentService } from '../services/base-component.service';
import * as moment from 'moment';
import { MOnthEnum } from '../models/month-enum';
import { GlmsView } from '../models/glms-view';
import { GlmsConcessionService } from '../services/glms-concession.service';
import { ProductType } from '../models/product-type';
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
    styleUrls: ['./glms-view-concession.component.css'],
    providers: [DatePipe]
})
export class GlmsViewConcessionComponent extends GlmsBaseService implements OnInit {
    private sub: any;

    errorMessage: String;
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
    showMotivationDisclaimer = false;
    canRenew = false;
    canRecall = false;
    isEditing = false;
    motivationEnabled = false;
    canEdit = false;
    glmsConcessionItemIndex: number;
    concessionReferenceId: string;
    isApproving = false;

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

    standardFixedRate: ArchiveType.StandardFixedRate;
    standardLinkedToPrime: ArchiveType.StandardLinkedToPrime;

    isConcessionDetail = false;
    selectedConcessionDetailId: number;

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

    createdDate: string;

    constructor(private route: ActivatedRoute,
        public router: Router,
        private formBuilder: FormBuilder,
        private location: Location,
        public http: Http,
        @Inject(LookupDataService) private lookupDataService,
        @Inject(GlmsConcessionService) private glmsConcessionService,
        @Inject(UserConcessionsService) private userConcessionsService,
        @Inject(BaseComponentService) private baseComponentService,
        private datepipe: DatePipe,
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

        this.standardFixedRate = ArchiveType.StandardFixedRate;
        this.standardLinkedToPrime = ArchiveType.StandardLinkedToPrime;

        this.glmsConcession = new GlmsConcession();
        this.glmsConcession.concession = new Concession();
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

        this.isConcessionDetail = false;
        this.getInitialData();
    }

    getGlmsConcessionItemRows(): FormArray {
        return <FormArray>this.glmsConcessionForm.controls['concessionItemRows'];
    }

    populateForm() {
        if (this.concessionReferenceId) {
            this.observableGlmsConcession = this.glmsConcessionService.getGlmsConcessionData(this.concessionReferenceId);
            this.observableGlmsConcession.subscribe(glmsConcession => {
                this.glmsConcession = glmsConcession;

                this.populateFormWithGlmsConcession();

                this.isLoading = false;
            }, error => {
                this.isLoading = false;
                this.errorMessage = <any>error;
            });
        }
    }

    private populateFormWithGlmsConcession() {
        if (this.glmsConcession.concession.status == ConcessionStatus.Pending && this.glmsConcession.concession.subStatus == ConcessionSubStatus.BCMPending) {
            this.canBcmApprove = this.glmsConcession.currentUser.canBcmApprove;
        }

        if (this.glmsConcession.concession.status == ConcessionStatus.Pending && this.glmsConcession.concession.subStatus == ConcessionSubStatus.PCMPending) {
            if (this.glmsConcession.currentUser.isHO) {
                this.canPcmApprove = this.glmsConcession.currentUser.canPcmApprove
            } else {
                this.canPcmApprove = this.glmsConcession.currentUser.canPcmApprove && this.glmsConcession.currentUser.canApprove;
            }

            this.canEdit = this.glmsConcession.currentUser.canPcmApprove;
        }

        //if it's still pending and the user is a requestor then they can recall it
        if (this.glmsConcession.concession.status == ConcessionStatus.Pending && this.glmsConcession.concession.subStatus == ConcessionSubStatus.BCMPending) {
            this.canRecall = this.glmsConcession.currentUser.canRequest && this.glmsConcession.concession.isAENumberLinkedAccountExecutiveOrAssistant;
        }

        if (this.glmsConcession.concession.status == ConcessionStatus.Pending &&
            (this.glmsConcession.concession.subStatus == ConcessionSubStatus.PCMApprovedWithChanges || this.glmsConcession.concession.subStatus == ConcessionSubStatus.HOApprovedWithChanges)) {
            this.canApproveChanges = this.glmsConcession.currentUser.canRequest && this.glmsConcession.concession.isAENumberLinkedAccountExecutiveOrAssistant;
        }

        if (this.glmsConcession.concession.status === ConcessionStatus.Approved ||
            this.glmsConcession.concession.status === ConcessionStatus.ApprovedWithChanges) {
            this.isApproved = true;
        }

        //if the concession is set to can extend and the user is a requestor, then they can extend or renew it
        this.canExtend = this.glmsConcession.concession.canExtend && this.glmsConcession.currentUser.canRequest;
        this.canRenew = this.glmsConcession.concession.canRenew && this.glmsConcession.currentUser.canRequest;

        //set the resubmit and update permissions
        //can only update when concession is not "due for expiry"
        this.canResubmit = this.glmsConcession.concession.canResubmit && this.glmsConcession.currentUser.canRequest;
        this.canUpdate = !this.canRenew && this.glmsConcession.concession.canUpdate && this.glmsConcession.currentUser.canRequest;

        this.canArchive = this.glmsConcession.concession.canArchive && this.glmsConcession.currentUser.canRequest;
        this.isInProgressExtension = this.glmsConcession.concession.isInProgressExtension;
        this.isInProgressRenewal = this.glmsConcession.concession.isInProgressRenewal;

        if (this.glmsConcession.concession.dateOpened) {
            var formattedDateOpened = this.datepipe.transform(this.glmsConcession.concession.dateOpened, 'yyyy-MM-dd');
            this.createdDate = formattedDateOpened;
        }

        this.glmsConcessionForm.controls['smtDealNumber'].setValue(this.glmsConcession.concession.smtDealNumber);
        this.glmsConcessionForm.controls['motivation'].setValue(this.glmsConcession.concession.motivation);

        let rowIndex = 0;

        for (let glmsConcessionDetail of this.glmsConcession.glmsConcessionDetails) {

            if (rowIndex != 0) {
                this.addNewConcessionRow(false);
            }

            const concessions = this.getGlmsConcessionItemRows();
            let currentConcession = concessions.controls[concessions.length - 1];

            currentConcession.get('glmsConcessionDetailId').setValue(glmsConcessionDetail.glmsConcessionDetailId);
            currentConcession.get('concessionDetailId').setValue(glmsConcessionDetail.concessionDetailId);

            let selectedglmsGroup = this.glmsGroup.filter(_ => _.id === glmsConcessionDetail.glmsGroupId);
            currentConcession.get('glmsGroup').setValue(selectedglmsGroup[0]);

            this.selectedGlmsGroup[rowIndex] = selectedglmsGroup[0];

            let selectedSlapType = this.slabType.filter(_ => _.id === glmsConcessionDetail.slabTypeId);
            currentConcession.get('slabType').setValue(selectedSlapType[0]);

            let selectedInterestType = this.interestType.filter(_ => _.id === glmsConcessionDetail.interestTypeId);
            currentConcession.get('interestType').setValue(selectedInterestType[0]);

            if (glmsConcessionDetail.glmsTierData.length > 0) {
                currentConcession.get('concessionItemTier').setValue(glmsConcessionDetail.glmsTierData);
            }

            let selectedInterestPricingCategory = this.interestPricingCategory.filter(_ => _.id === glmsConcessionDetail.interestPricingCategoryId);
            currentConcession.get('interestPricingCategory').setValue(selectedInterestPricingCategory[0]);

            if (glmsConcessionDetail.expiryDate) {
                var formattedExpiryDate = this.datepipe.transform(glmsConcessionDetail.expiryDate, 'yyyy-MM-dd');
                currentConcession.get('expiryDate').setValue(formattedExpiryDate);
            }

            if (glmsConcessionDetail.dateApproved) {
                var formattedDateApproved = this.datepipe.transform(glmsConcessionDetail.dateApproved, 'yyyy-MM-dd');
                currentConcession.get('dateApproved').setValue(formattedDateApproved);
            }

            currentConcession.get('isExpired').setValue(glmsConcessionDetail.isExpired);
            currentConcession.get('isExpiring').setValue(glmsConcessionDetail.isExpiring);

            rowIndex++;
        }

        this.changearray = this.lookupDataService.checkforLC(this.glmsConcession.concession.status, this.glmsConcession.concession.subStatus, this.glmsConcession.concession.concessionComments);

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
            currentCondition.get('conditionComment').setValue(concessionCondition.conditionComment);

            let selectedPeriodType = this.periodTypes.filter(_ => _.id == concessionCondition.periodTypeId);
            currentCondition.get('periodType').setValue(selectedPeriodType[0]);

            let selectedPeriod = this.periods.filter(_ => _.id == concessionCondition.periodId);
            currentCondition.get('period').setValue(selectedPeriod[0]);

            rowIndex++;
        }
    }

    initConcessionItemRows() {

        this.selectedProductTypes.push(new ProductType());

        return this.formBuilder.group({
            disablecontrolset: [''],
            productType: [''],
            expiryDate: [''],
            slabType: [''],
            interestType: [''],
            interestPricingCategory: [''],
            glmsGroup: [''],
            concessionItemTier: [''],
            glmsConcessionDetailId: [''],
            concessionDetailId: [''],
            dateApproved: [{ value: '', disabled: true }],
            isExpired: [''],
            isExpiring: [''],
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
                super.getGlmsGroup(this.riskGroupNumber, this.sapbpid),
                super.getInterestType(),
                super.getSlabType(),
                super.getRateType(),
                super.getBaseRateCode(),
                super.getInterestPricingCategory(),
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
                super.getGlmsGroup(this.riskGroupNumber, this.sapbpid),
                super.getInterestType(),
                super.getSlabType(),
                super.getRateType(),
                super.getBaseRateCode(),
                super.getInterestPricingCategory(),

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

        if (isForRiskGroup) {
            this.riskGroup = <any>results[4];
        }
        else {
            this.legalEntity = <any>results[4];
        }
        this.periods = <any>results[1];
        this.periodTypes = <any>results[2];
        this.conditionTypes = <any>results[3];
        this.glmsGroup = <any>results[5];
        this.interestType = <any>results[6];
        this.slabType = <any>results[7];
        this.rateType = <any>results[8];
        this.baseRateCode = <any>results[9];
        this.interestPricingCategory = <any>results[10];

        this.isLoading = false;

        const control = this.getGlmsConcessionItemRows();
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
            conditionComment: [''],
            periodType: [''],
            period: ['']
        });
    }

    onTierLineAdd(index) {
        const control = <FormArray>this.glmsConcessionForm.controls['tierItemsRows'];
        if (index == 0) {
            control.controls[index].get('tieredFrom').setValue(0);
        }
        if (index > 0) {
            var newValue = control.controls[index - 1].get('tieredTo').value;
            newValue = Number(newValue) + 1;
            control.controls[index].get('tieredFrom').disable();
            control.controls[index].get('tieredFrom').setValue(newValue);
        }
    }

    checkTiervalidation(row) {

        let tieredFrom = 0;
        let tieredTo = 0;
        this.tierValidationError = null;

        if (row.get('tieredFrom').value == "") {
            tieredFrom = 0;
        } else {
            tieredFrom = row.get('tieredFrom').value;
        }

        if (row.get('tieredTo').value == "") {
            this.addTierValidationError("Tier To cannot be empty");
        } else {
            tieredTo = row.get('tieredTo').value;
        }

        if (tieredFrom > tieredTo) {
            this.addTierValidationError("Tier To cannot be less than Tier From");
        }
    }

    getGlmsConcession(isNew: boolean): GlmsConcession {
        var glmsConcession = new GlmsConcession();
        glmsConcession.concession = new Concession();

        if (this.glmsConcession.concession.riskGroupId)
            glmsConcession.concession.riskGroupId = this.glmsConcession.concession.riskGroupId;

        if (this.glmsConcession.concession.legalEntityId)
            glmsConcession.concession.legalEntityId = this.glmsConcession.concession.legalEntityId;

        if (this.glmsConcessionForm.controls['smtDealNumber'].value) {
            glmsConcession.concession.smtDealNumber = this.glmsConcessionForm.controls['smtDealNumber'].value;
        }
        else {

            this.addValidationError("SMT Deal Number not captured");
        }

        if (this.glmsConcessionForm.controls['motivation'].value)
            glmsConcession.concession.motivation = this.glmsConcessionForm.controls['motivation'].value;
        else {

            glmsConcession.concession.motivation = '.';
        }

        const concessions = this.getGlmsConcessionItemRows();

        for (let concessionFormItem of concessions.controls) {
            if (!glmsConcession.glmsConcessionDetails)
                glmsConcession.glmsConcessionDetails = [];

            let glmsConcessionDetail = new GlmsConcessionDetail();

            glmsConcessionDetail.glmsConcessionDetailId = concessionFormItem.get('glmsConcessionDetailId').value;
            glmsConcessionDetail.concessionDetailId = concessionFormItem.get('concessionDetailId').value;

            let applyexpirydate = false;

            if (concessionFormItem.get('glmsGroup').value) {

                glmsConcessionDetail.glmsGroupId = concessionFormItem.get('glmsGroup').value.id;
            }
            else {

                this.addValidationError("Group not selected");
            }

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
                if (!this.isAppprovingOrDeclining) {
                    this.onExpiryDateChanged(concessionFormItem);
                }

                glmsConcessionDetail.expiryDate = new Date(concessionFormItem.get('expiryDate').value);
            }
            else {
                if (!applyexpirydate) {
                    this.addValidationError("Expiry date not selected");
                }
            }

            if (concessionFormItem.get('concessionItemTier').value && concessionFormItem.get('concessionItemTier').value != "") {
                this.populateTierForm(concessions.controls.indexOf(concessionFormItem));
                if (this.validateTierData(true)) {
                    glmsConcessionDetail.glmsTierData = concessionFormItem.get('concessionItemTier').value;
                }
            }
            else {
                this.addValidationError("Concession line Tier data not set");
            }

            if (this.isApproving) {
                glmsConcessionDetail.dateApproved = new Date();
            }

            glmsConcession.glmsConcessionDetails.push(glmsConcessionDetail);

            if (glmsConcessionDetail.interestPricingCategoryId != null &&
                glmsConcessionDetail.interestPricingCategoryId > 0 &&
                glmsConcessionDetail.interestTypeId != null &&
                glmsConcessionDetail.interestTypeId) {
                let hasDuplicates = this.baseComponentService.HasDuplicateConcessionInterestPricingCategoryAndInterestType(
                    glmsConcession.glmsConcessionDetails,
                    glmsConcessionDetail.interestPricingCategoryId,
                    glmsConcessionDetail.interestTypeId);

                if (hasDuplicates) {
                    this.addValidationError("Duplicate 'Interest Pricing Category' and 'Interest Type' combination found. Please select different combination.");

                    break;
                }
            }
        }

        const conditions = <FormArray>this.glmsConcessionForm.controls['conditionItemsRows'];

        let concessionConditionReturnObject = this.baseComponentService.getConsessionConditionData(conditions, glmsConcession.concessionConditions, this.validationError);
        glmsConcession.concessionConditions = concessionConditionReturnObject.concessionConditions;
        this.validationError = concessionConditionReturnObject.validationError;

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
        this.populateTierForm(i);
    }

    rateTypeChange(rowIndex: number) {

        const control = <FormArray>this.glmsConcessionForm.controls['tierItemsRows'];
        let currentRow = control.controls[rowIndex];
        var rateType = currentRow.get('rateType').value;

        if (rateType.description === "V") {

            currentRow.get('value').disable();
            currentRow.get('value').setValue(null);

            currentRow.get('baseRate').enable();
            currentRow.get('spread').enable();
        }
        else {

            currentRow.get('baseRate').disable();
            currentRow.get('baseRate').setValue(null);

            currentRow.get('spread').disable();
            currentRow.get('spread').setValue(null);

            currentRow.get('value').enable();
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

        if (editType == EditTypeEnum.Renew) { // || editType == EditTypeEnum.UpdateApproved) {
            const concessions = this.getGlmsConcessionItemRows();
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

    populateTierForm(rowIndex: number) {

        const concessions = this.getGlmsConcessionItemRows();
        const tierForm = <FormArray>this.glmsConcessionForm.controls['tierItemsRows'];

        var rowAtIndex = concessions.controls[rowIndex].get('concessionItemTier').value;

        if (rowAtIndex.length > 0) {

            let roIndex = 0;

            for (let x in tierForm.controls) {
                while (tierForm.length > 0) {
                    var i = 0;
                    this.deleteTierRow(i);
                    i++;
                }
            }

            for (let glmsTierFormItem of concessions.controls[rowIndex].get('concessionItemTier').value) {

                this.addNewTierRow();

                const newTierForm = <FormArray>this.glmsConcessionForm.controls['tierItemsRows'];
                let currentConcession = newTierForm.controls[newTierForm.length - 1];

                let selectedRateType = this.rateType.filter(_ => _.id === glmsTierFormItem.rateTypeId);
                if (selectedRateType.length > 0) {
                    currentConcession.get('rateType').setValue(selectedRateType[0]);
                }

                let selectedBaseRate = this.baseRateCode.filter(_ => _.id === glmsTierFormItem.baseRateId);
                if (selectedBaseRate.length > 0) {
                    currentConcession.get('baseRate').setValue(selectedBaseRate[0]);
                } else {
                    currentConcession.get('baseRate').disable();
                }

                if (glmsTierFormItem.rateTypeId == 1) {
                    if (glmsTierFormItem.value) {
                        currentConcession.get('value').setValue(glmsTierFormItem.value);
                    } else {
                        currentConcession.get('value').disable();
                    }
                    currentConcession.get('spread').disable();
                }

                if (glmsTierFormItem.rateTypeId == 2) {

                    if (glmsTierFormItem.spread) {
                        currentConcession.get('spread').setValue(glmsTierFormItem.spread);
                    } else {
                        currentConcession.get('spread').disable();
                    }
                    currentConcession.get('value').disable();
                }

                currentConcession.get('tieredFrom').setValue(glmsTierFormItem.tierFrom);
                currentConcession.get('tieredTo').setValue(glmsTierFormItem.tierTo);

                roIndex++;
            }

        } else {

            for (let x in tierForm.controls) {
                while (tierForm.length > 0) {
                    var i = 0;
                    this.deleteTierRow(i);
                    i++;
                }
            }
            this.addNewTierRow();
        }
    }

    closeTierModal(x) {

        let tierItemsList = [new GlmsTierData()];

        const concessions = this.getGlmsConcessionItemRows();
        const tierForm = <FormArray>this.glmsConcessionForm.controls['tierItemsRows'];

        var lastRow = tierForm.length - 1;

        if (tierForm.length > 0) {

            this.validateTierData(false);

            tierForm.controls[lastRow].get('tieredTo').setValue(0);

            //if (tierForm.length == 1) {
            //    this.addValidationError("Minimum of 2 Concession Line tiers must be added");
            //}

            for (let glmsTierFormItem of tierForm.controls) {

                let tierItem = new GlmsTierData();

                if (glmsTierFormItem.get('tieredFrom').value) {
                    tierItem.tierFrom = glmsTierFormItem.get('tieredFrom').value;
                } else {
                    tierItem.tierFrom = 0;
                }

                if (glmsTierFormItem.get('tieredTo').value) {
                    tierItem.tierTo = glmsTierFormItem.get('tieredTo').value;
                } else {
                    tierItem.tierTo = 0;
                }

                if (glmsTierFormItem.get('rateType').value.description === "F") {
                    if (glmsTierFormItem.value) {
                        tierItem.value = glmsTierFormItem.get('value').value;
                    } //else {
                    //this.addValidationError("Value not selected");
                    //}
                }

                if (glmsTierFormItem.get('rateType').value.description === "V") {

                    if (glmsTierFormItem.get('baseRate').value) {
                        tierItem.baseRateId = glmsTierFormItem.get('baseRate').value.id;
                    } //else {
                    //this.addValidationError("BaseRate not selected");
                    //}

                    if (glmsTierFormItem.get('spread').value) {
                        tierItem.spread = glmsTierFormItem.get('spread').value;
                    } //else {
                    //this.addValidationError("Spread not selected");
                    //}
                }

                if (glmsTierFormItem.get('rateType').value) {
                    tierItem.rateTypeId = glmsTierFormItem.get('rateType').value.id;
                } //else {
                //this.addValidationError("RateType not selected");
                //}

                tierItemsList.push(tierItem);
            }
        }

        tierItemsList.splice(0, 1);
        concessions.controls[this.glmsConcessionItemIndex].get('concessionItemTier').setValue(tierItemsList);
    }

    validateTierData(isSaveConcession: boolean) {

        if (!isSaveConcession) {
            this.validationError = [];
        }

        let isValid = true;
        const tierForm = <FormArray>this.glmsConcessionForm.controls['tierItemsRows'];

        if (tierForm.length < 2) {
            this.addValidationError("Minimum of 2 Concession line tiers must be added ");
            isValid = false;
        }

        for (let glmsTierFormItem of tierForm.controls) {

            let tierItem = new GlmsTierData();

            if (glmsTierFormItem.get('rateType').value.description === "F") {
                if (!glmsTierFormItem.value) {
                    this.addValidationError("Value not selected");
                    isValid = false;
                }
            }

            if (glmsTierFormItem.get('rateType').value.description === "V") {

                if (!glmsTierFormItem.get('baseRate').value) {
                    this.addValidationError("BaseRate not selected");
                    isValid = false;
                }

                if (!glmsTierFormItem.get('spread').value) {
                    this.addValidationError("Spread not selected");
                    isValid = false;
                }
            }

            if (!glmsTierFormItem.get('rateType').value) {
                this.addValidationError("RateType not selected");
                isValid = false;
            }
        }

        if (!isSaveConcession && isValid) {
            this.validationError = null;
        }

        return isValid;
    }

    addNewConcessionRow(isClickEvent: boolean) {

        const control = this.getGlmsConcessionItemRows();

        var newRow = this.initConcessionItemRows();

        if (this.productTypes) {
            newRow.controls['productType'].setValue(this.productTypes[0]);
        }
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
        const control = <FormArray>this.glmsConcessionForm.controls['conditionItemsRows'];
        control.push(this.initConditionItemRows());
    }

    addNewConditionRowIfNone() {
        const control = <FormArray>this.glmsConcessionForm.controls['conditionItemsRows'];
        if (control.length == 0) {
            control.push(this.initConditionItemRows());
        }
    }

    addNewTierRow() {
        this.tierValidationError == null;
        const control = <FormArray>this.glmsConcessionForm.controls['tierItemsRows'];
        var rowNumber = control.length;
        let currentRow = control.controls[rowNumber - 1];
        if (rowNumber > 0) {
            this.checkTiervalidation(currentRow);
        }

        if (this.tierValidationError == null) {
            control.push(this.initTierItemRows());
            this.onTierLineAdd(rowNumber);
        }
    }

    addNewTierRowIfNone() {
        const control = <FormArray>this.glmsConcessionForm.controls['tierItemsRows'];
        if (control.length == 0) {
            control.push(this.initTierItemRows());
        }
    }

    deleteConcessionRow(index: number) {
        if (confirm("Are you sure you want to remove this row?")) {
            const control = this.getGlmsConcessionItemRows();

            this.selectedProductTypes.splice(index, 1);
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

        const concessions = this.getGlmsConcessionItemRows();
        for (let concessionFormItem of concessions.controls) {

            concessionFormItem.disable();
        }
    }

    onExpiryDateChanged(itemrow) {
        var validationErrorMessage = this.expiringDateDifferenceValidationForView(itemrow.controls['expiryDate'].value, this.createdDate);
        if (validationErrorMessage != null) {
            this.addValidationError(validationErrorMessage);
        }
    }

    getBackgroundColour(rowIndex: number) {
        const control = this.getGlmsConcessionItemRows();

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
        this.isApproving = true;
        this.errorMessage = null;
        this.validationError = null;
        this.isAppprovingOrDeclining = true;

        var glmsConcession = this.getGlmsConcession(false);
        glmsConcession.concession.subStatus = ConcessionSubStatus.PCMPending;
        glmsConcession.concession.bcmUserId = this.glmsConcession.currentUser.id;
        glmsConcession.concession.referenceNumber = this.concessionReferenceId;

        if (!glmsConcession.concession.comments) {
            glmsConcession.concession.comments = "Forwarded";
        }

        if (!this.validationError) {
            this.glmsConcessionService.postUpdateGlmsData(glmsConcession).subscribe(entity => {

                this.canBcmApprove = false;
                this.isApproving = false;
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
        this.isAppprovingOrDeclining = true;

        var glmsConcession = this.getGlmsConcession(false);
        glmsConcession.concession.status = ConcessionStatus.Declined;
        glmsConcession.concession.subStatus = ConcessionSubStatus.BCMDeclined;
        glmsConcession.concession.bcmUserId = this.glmsConcession.currentUser.id;
        glmsConcession.concession.referenceNumber = this.concessionReferenceId;

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
        this.isApproving = true;
        this.errorMessage = null;
        this.validationError = null;
        this.isAppprovingOrDeclining = true;

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

        glmsConcession.concession.referenceNumber = this.concessionReferenceId;

        if (!this.validationError) {
            this.glmsConcessionService.postUpdateGlmsData(glmsConcession).subscribe(entity => {

                this.glmsConcession = entity;

                this.populateFormWithGlmsConcession();

                console.log("data saved");
                this.canPcmApprove = false;
                this.isApproving = false;
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

        const concessions = this.getGlmsConcessionItemRows();

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
        this.isAppprovingOrDeclining = true;

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

        glmsConcession.concession.referenceNumber = this.concessionReferenceId;

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

        if (this.canExtend && this.motivationEnabled == false) {
            this.motivationEnabled = true;
            this.glmsConcessionForm.controls['motivation'].setValue('');
            this.showMotivationDisclaimer = true;

        } else {

            this.showMotivationDisclaimer = false;
            this.validationError = null;

            var extendConceModel = new extendConcessionModel()
            extendConceModel.concessionReferenceId = this.concessionReferenceId;

            if (this.glmsConcessionForm.controls['motivation'].value) {
                extendConceModel.motivation = this.glmsConcessionForm.controls['motivation'].value;
            } else {
                this.addValidationError("Motivation not captured");
                this.isLoading = false;
            }

            if (!this.validationError) {

                if (confirm("Are you sure you want to extend this concession?")) {
                    this.isLoading = true;
                    this.errorMessage = null;
                    this.validationError = null;
                    this.motivationEnabled = false;

                    this.glmsConcessionService.postExtendConcession(extendConceModel).subscribe(entity => {

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
                        this.glmsConcession = entity;
                        
                    }, error => {
                        this.errorMessage = <any>error;
                        this.isLoading = false;
                    });
                    }
                }
        }
    }

    loopRows() {
        const concessions = this.getGlmsConcessionItemRows();
        let rowIndex = 0;
        for (let concessionFormItem of concessions.controls) {
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

        var glmsConcession = this.getGlmsConcession(false);

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
        this.isApproving = true;
        this.errorMessage = null;
        this.validationError = null;
        this.isAppprovingOrDeclining = true;

        var glmsConcession = this.getGlmsConcession(false);
        glmsConcession.concession.status = ConcessionStatus.ApprovedWithChanges;
        glmsConcession.concession.subStatus = ConcessionSubStatus.RequestorAcceptedChanges;
        glmsConcession.concession.requestorId = this.glmsConcession.currentUser.id;
        glmsConcession.concession.referenceNumber = this.concessionReferenceId;

        if (!glmsConcession.concession.comments) {
            glmsConcession.concession.comments = "Accepted Changes";
        }

        if (!this.validationError) {
            this.glmsConcessionService.postUpdateGlmsData(glmsConcession).subscribe(entity => {

                this.canApproveChanges = false;
                this.isApproving = false;
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
        this.isAppprovingOrDeclining = true;

        var glmsConcession = this.getGlmsConcession(false);
        glmsConcession.concession.status = ConcessionStatus.Declined;
        glmsConcession.concession.subStatus = ConcessionSubStatus.RequestorDeclinedChanges;
        glmsConcession.concession.requestorId = this.glmsConcession.currentUser.id;
        glmsConcession.concession.referenceNumber = this.concessionReferenceId;

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

    setSelectedConcessionDetail(concessionDetailId: number) {
        this.selectedConcessionDetailId = concessionDetailId;
        this.isConcessionDetail = true;
    }

    archiveConcessiondetail(archiveType: ArchiveType) {

        if (confirm("Please note that the account will be put back to standard pricing. Are you sure you want to delete this concession ?")) {
            this.isLoading = true;
            this.errorMessage = null;

            this.userConcessionsService.deactivateConcessionDetailed(this.selectedConcessionDetailId, archiveType).subscribe(entity => {

                this.warningMessage = "Concession item has been deleted";

                this.isLoading = false;

                this.ngOnInit();

            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        }
        this.isConcessionDetail = false;
    }

    archiveConcession(archiveType: ArchiveType) {

        if (this.isConcessionDetail) {
            return this.archiveConcessiondetail(archiveType);
        }

        if (confirm("Please note that the account will be put back to standard pricing. Are you sure you want to delete this concession ?")) {
            this.isLoading = true;
            this.errorMessage = null;

            this.userConcessionsService.deactivateConcession(this.concessionReferenceId, archiveType).subscribe(entity => {
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
        this.location.back();
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

    getNumberInput(input) {
        this.glmsConcessionForm.controls['smtDealNumber'].setValue(this.baseComponentService.removeLetters(input.value));
    }

    disableField(fieldname: string, index: number = null) {
        let canUpdateExpiryDate: boolean = true;

        if (fieldname == "expiryDate" && this.editType != null &&
            (this.editType == EditTypeEnum.Renew || this.editType == EditTypeEnum.UpdateApproved)) {
            {
                canUpdateExpiryDate = false;
            }
        }       

        return this.disableFieldBase(
            fieldname,
            this.canEdit && canUpdateExpiryDate,
            index,
            this.selectedConditionTypes,
            this.isRecalling,
            this.motivationEnabled)
    }
}
