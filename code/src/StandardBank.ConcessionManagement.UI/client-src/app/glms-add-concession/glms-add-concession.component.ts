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
import { Concession } from '../models/concession';
import { GlmsConcessionDetail } from '../models/glms-concession-detail';
import { GlmsBaseService } from '../services/glms-base.service';
import { UserService } from '../services/user.service';
import { Http } from '@angular/http';
import { BaseRateCode } from '../models/base-rate-code';

@Component({
  selector: 'app-glms-add-concession',
  templateUrl: './glms-add-concession.component.html',
  styleUrls: ['./glms-add-concession.component.css']
})
export class GlmsAddConcessionComponent extends GlmsBaseService implements OnInit {
    private sub: any;

    errorMessage: String;
    validationError: String[];
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

    observableGlmsTierData: Observable<GlmsTierData[]>;
    glmsTierData: GlmsTierData[];


    constructor(private route: ActivatedRoute,
        public router: Router,
        private formBuilder: FormBuilder,
        private location: Location,
        public http: Http,
        @Inject(LookupDataService) private lookupDataService,
        @Inject(GlmsConcessionService) private glmsConcessionService,
        public userService: UserService) {
        super(http,router,userService);
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
            conditionType: [''],
            conditionProduct: [''],
            interestRate: [''],
            volume: [''],
            value: [''],
            periodType: [''],
            period: ['']
        });
    }

    getglmsConcession(): GlmsConcession {
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

    rateTypeChange(rowIndex: number){

        const control = <FormArray>this.glmsConcessionForm.controls['tierItemsRows'];
        let currentRow = control.controls[rowIndex];
        var rateType = currentRow.get('rateType').value;

        if (rateType.description === "V") {

            currentRow.get('spread').disable();
            currentRow.get('spread').setValue(null);

            currentRow.get('value').disable();
            currentRow.get('value').setValue(null);

        }
        else {

            currentRow.get('baseRate').disable();
            currentRow.get('baseRate').setValue(null);
        }

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
                this.addValidationError("BaseRate not selected");
            }

            if (glmsTierFormItem.get('rateType').value.id) {
                tierItem.rateTypeId = glmsTierFormItem.get('rateType').value.id;
            } else {
                this.addValidationError("RateType not selected");
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

}
