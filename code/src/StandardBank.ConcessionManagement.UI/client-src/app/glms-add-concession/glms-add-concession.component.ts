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
import { GlmsConcession } from '../models/glms-concession';
import { Concession } from '../models/concession';
import { GlmsConcessionDetail } from '../models/glms-concession-detail';
import { GlmsBaseService } from '../services/glms-base.service';
import { UserService } from '../services/user.service';
import { Http } from '@angular/http';
import { BaseRateCode } from '../models/base-rate-code';
import { DISABLED } from '@angular/forms/src/model';
import { forEach } from '@angular/router/src/utils/collection';

@Component({
    selector: 'app-glms-add-concession',
    templateUrl: './glms-add-concession.component.html',
    styleUrls: ['./glms-add-concession.component.css']
})
export class GlmsAddConcessionComponent extends GlmsBaseService implements OnInit {
    private sub: any;

    errorMessage: String;
    saveMessage: String;
    showHide = false;
    observableRiskGroup: Observable<RiskGroup>;
    riskGroup: RiskGroup;
    riskGroupNumber: number;
    legalEntity: LegalEntity;
    sapbpid: number;
    today: string;
    glmsConcessionItemIndex: number;

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

    observableGlmsTierData: Observable<GlmsTierData[]>;
    glmsTierData: GlmsTierData[];


    constructor(private route: ActivatedRoute,
        public router: Router,
        private formBuilder: FormBuilder,
        private location: Location,
        public http: Http,
        @Inject(LookupDataService) private lookupDataService,
        @Inject(GlmsConcessionService) private glmsConcessionService,
        @Inject(BaseComponentService) private baseComponentService,
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
    }

    ngOnInit() {

        this.today = this.GetTodayDate();

        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
            this.sapbpid = +params['sapbpid'];

            this.observableGlmsView = this.glmsConcessionService.getGlmsViewData(this.riskGroupNumber, this.sapbpid);
            this.observableGlmsView.subscribe(glmsView => {
                this.glmsView = glmsView;

                if (this.riskGroupNumber || this.riskGroupNumber > 0) {
                    this.entityName = this.glmsView.riskGroup.name;
                    this.entityNumber = this.glmsView.riskGroup.number.toString();
                }
                else {
                    this.entityName = this.glmsView.legalEntity.customerName;
                    this.entityNumber = this.glmsView.legalEntity.customerNumber;
                }

                this.isLoading = false;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
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

    getglmsConcession(): GlmsConcession {
        var glmsConcession = new GlmsConcession();
        glmsConcession.concession = new Concession();

        if (this.glmsView.riskGroup) {
            glmsConcession.concession.riskGroupId = this.glmsView.riskGroup.id;
        }

        if (this.glmsView.legalEntity) {
            glmsConcession.concession.legalEntityId = this.glmsView.legalEntity.id;
        }

        if (this.glmsConcessionForm.controls['smtDealNumber'].value) {
            glmsConcession.concession.smtDealNumber = this.glmsConcessionForm.controls['smtDealNumber'].value;
        }
        else {
            this.addValidationError("SMT Deal Number not captured");
        }

        if (this.glmsConcessionForm.controls['motivation'].value) {
            glmsConcession.concession.motivation = this.glmsConcessionForm.controls['motivation'].value;
        }
        else {
            glmsConcession.concession.motivation = '.';
        }

        const concessions = <FormArray>this.glmsConcessionForm.controls['concessionItemRows'];

        for (let concessionFormItem of concessions.controls) {

            if (!glmsConcession.glmsConcessionDetails)
                glmsConcession.glmsConcessionDetails = [];

            let glmsConcessionDetail = new GlmsConcessionDetail();

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
                this.onExpiryDateChanged(concessionFormItem);
                glmsConcessionDetail.expiryDate = new Date(concessionFormItem.get('expiryDate').value);
            }
            else {
                this.addValidationError("Expiry date not selected");
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

    closeTierModal(x) {

        let tierItemsList = [new GlmsTierData()];

        const concessions = <FormArray>this.glmsConcessionForm.controls['concessionItemRows'];
        const tierForm = <FormArray>this.glmsConcessionForm.controls['tierItemsRows'];

        var lastRow = tierForm.length - 1;

        if (tierForm.length > 0) {

            //if (!this.validateTierData(false)) {
            //    return;
            //}
            this.validateTierData(false);

            tierForm.controls[lastRow].get('tieredTo').setValue(0);

            //if (tierForm.length == 1) {
            //    this.addValidationError("Minimum of 2 tiers must be added");
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

    populateTierForm(rowIndex: number) {

        const concessions = <FormArray>this.glmsConcessionForm.controls['concessionItemRows'];
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

    addNewConcessionRow() {

        const control = <FormArray>this.glmsConcessionForm.controls['concessionItemRows'];

        var newRow = this.initConcessionItemRows();

        if (this.productTypes)
            newRow.controls['productType'].setValue(this.productTypes[0]);

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

    addNewTierRowIfNone() {
        const control = <FormArray>this.glmsConcessionForm.controls['tierItemsRows'];
        if (control.length == 0)
            control.push(this.initTierItemRows());
    }

    deleteConcessionRow(index: number) {
        if (confirm("Are you sure you want to remove this row?")) {
            const control = <FormArray>this.glmsConcessionForm.controls['concessionItemRows'];

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
        const concessions = <FormArray>this.glmsConcessionForm.controls['concessionItemRows'];
        for (let concessionFormItem of concessions.controls) {

            concessionFormItem.disable();
        }
    }

    onExpiryDateChanged(itemrow) {

        var validationErrorMessage = this.expiringDateDifferenceValidation(itemrow.controls['expiryDate'].value);
        if (validationErrorMessage != null) {
            this.addValidationError(validationErrorMessage);
        }
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

    onSubmit() {

        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;

        var glmsConcession = this.getglmsConcession();

        glmsConcession.concession.concessionType = ConcessionTypes.Glms;
        glmsConcession.concession.type = "New";
        glmsConcession.concession.comments = "Created";

        if (!this.validationError) {
            this.glmsConcessionService.postNewGlmsData(glmsConcession).subscribe(entity => {
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

    goBack() {
        this.router.navigate(['/pricing', this.riskGroupNumber]);
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

    disableField(fieldname: string, index: number = null) {
        return this.disableFieldBase(fieldname, this.saveMessage == null, index, this.selectedConditionTypes, null, null)
    }
}
