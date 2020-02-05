import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { RiskGroup } from "../models/risk-group";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';


import { Router, RouterModule } from '@angular/router';

import { Location } from '@angular/common';
import { Period } from "../models/period";
import { PeriodType } from "../models/period-type";

import { BolChargeCodeType } from "../models/bol-chargecodetype";
import { BolChargeCode } from "../models/bol-chargecode";
import { LegalEntityBOLUser } from "../models/legal-entity-bol-user";

import { ClientAccount } from "../models/client-account";
import { LookupDataService } from "../services/lookup-data.service";
import { ConcessionCondition } from "../models/concession-condition";
import { LegalEntity } from "../models/legal-entity";

import { DecimalPipe } from '@angular/common';
import { ConcessionTypes } from '../constants/concession-types';
import { ConditionType } from "../models/condition-type";

import { BolConcession } from "../models/bol-concession";
import { BolConcessionDetail } from "../models/bol-concession-detail";
import { BolConcessionService } from "../services/bol-concession.service";

import { BolView } from "../models/bol-view";
import { Concession } from "../models/concession";
import { UserService } from "../services/user.service";

import { BaseComponentService } from '../services/base-component.service';
import * as moment from 'moment';
import { MOnthEnum } from '../models/month-enum';
import { BolConcessionBaseService } from '../services/bol-concession-base.service';

@Component({
    selector: 'app-bol-add-concession',
    templateUrl: './bol-add-concession.component.html',
    styleUrls: ['./bol-add-concession.component.css']
})
export class BolAddConcessionComponent extends BolConcessionBaseService implements OnInit, OnDestroy {
    private sub: any;

    errorMessage: String;
    saveMessage: String;
    showHide = false;
    observableRiskGroup: Observable<RiskGroup>;
    riskGroup: RiskGroup;
    riskGroupNumber: number;
    legalEntity: LegalEntity;
    sapbpid: number;

    observableBolView: Observable<BolView>;
    bolView: BolView = new BolView();

    public bolConcessionForm: FormGroup;

    entityName: string;
    entityNumber: string;

    isLoading = true;

    observablePeriods: Observable<Period[]>;
    periods: Period[];

    observablePeriodTypes: Observable<PeriodType[]>;
    periodTypes: PeriodType[];

    observable: Observable<BolChargeCodeType[]>;
    bolchargecodetypes: BolChargeCodeType[];

    observableBolChargeCodes: Observable<BolChargeCode[]>;
    bolchargecodes: BolChargeCode[];

    observableLegalEntityBOLUsers: Observable<LegalEntityBOLUser[]>;
    legalentitybolusers: LegalEntityBOLUser[];

    selectedConditionTypes: ConditionType[];

    selectedProducts: BolChargeCodeType[];

    observableConditionTypes: Observable<ConditionType[]>;
    conditionTypes: ConditionType[];


    constructor(private route: ActivatedRoute,
        private router: Router,
        private formBuilder: FormBuilder,
        private location: Location,
        @Inject(LookupDataService) private lookupDataService,
        @Inject(BolConcessionService) private bolConcessionService,
        private baseComponentService: BaseComponentService) {
        super();
        this.riskGroup = new RiskGroup();

        this.bolchargecodetypes = [new BolChargeCodeType()];
        this.bolchargecodes = [new BolChargeCode()];
        this.legalentitybolusers = [new LegalEntityBOLUser()];
        this.periods = [new Period()];
        this.periodTypes = [new PeriodType()];

        this.conditionTypes = [new ConditionType()];
        this.selectedConditionTypes = [new ConditionType()];
        this.selectedProducts = [new BolChargeCodeType()];

    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
            this.sapbpid = +params['sapbpid'];
        });

        this.getInitialData();

        this.bolConcessionForm = this.formBuilder.group({
            concessionItemRows: this.formBuilder.array([this.initConcessionItemRows()]),
            conditionItemsRows: this.formBuilder.array([]),
            smtDealNumber: new FormControl(),
            motivation: new FormControl()
        });

    }

    getInitialData() {
        if (this.riskGroupNumber !== 0) {
            Observable.forkJoin([
                this.lookupDataService.getConditionTypes(),
                this.lookupDataService.getBOLChargeCodeTypes(),
                this.lookupDataService.getBOLChargeCodes(this.riskGroupNumber),
                this.lookupDataService.getLegalEntityBOLUsers(this.riskGroupNumber),
                this.lookupDataService.getPeriods(),
                this.lookupDataService.getPeriodTypes(),
                this.lookupDataService.getRiskGroup(this.riskGroupNumber)
            ]).subscribe(results => {
                this.setInitialData(results, true);
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        }
        else if (this.sapbpid != null && this.sapbpid != 0) {
            Observable.forkJoin([
                this.lookupDataService.getConditionTypes(),
                this.lookupDataService.getBOLChargeCodeTypes(),
                this.lookupDataService.getBOLChargeCodesAll(),
                this.lookupDataService.getLegalEntityBOLUsersBySAPBPID(this.sapbpid),
                this.lookupDataService.getPeriods(),
                this.lookupDataService.getPeriodTypes(),
                this.lookupDataService.getLegalEntity(this.sapbpid)
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
            this.riskGroup = <any>results[6];
            this.entityName = this.riskGroup.name;
            this.entityNumber = this.riskGroup.number.toString();
        }
        else {
            this.legalEntity = <any>results[6];
            this.entityName = this.legalEntity.customerName;
            this.entityNumber = this.legalEntity.customerNumber;
        }

        this.conditionTypes = <any>results[0];
        this.bolchargecodetypes = <any>results[1];
        this.bolchargecodes = <any>results[2];
        this.legalentitybolusers = <any>results[3];
        this.periods = <any>results[4];
        this.periodTypes = <any>results[5];

        const control = <FormArray>this.bolConcessionForm.controls['concessionItemRows'];

        if (this.bolchargecodetypes)
            control.controls[0].get('product').setValue(this.bolchargecodetypes[0]);

        if (this.legalentitybolusers)
            control.controls[0].get('userid').setValue(this.legalentitybolusers[0]);

        this.selectedProducts[0] = this.bolchargecodetypes[0];

        if (this.selectedProducts && this.selectedProducts[0].bolchargecodes)
            control.controls[0].get('chargecode').setValue(this.selectedProducts[0].bolchargecodes[0]);

        this.productTypeChanged(0);

        this.isLoading = false;
    }


    initConcessionItemRows() {

        this.selectedProducts.push(new BolChargeCodeType());

        return this.formBuilder.group({
            product: [''],
            chargecode: [''],
            unitcharge: [''],
            userid: [''],
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
            conditionComment: [''],
            periodType: [''],
            period: ['']
        });
    }

    addNewConcessionRow() {
        const control = <FormArray>this.bolConcessionForm.controls['concessionItemRows'];
        var newRow = this.initConcessionItemRows();

        var length = control.controls.length;

        if (this.bolchargecodetypes)
            newRow.controls['product'].setValue(this.bolchargecodetypes[0]);

        if (this.legalentitybolusers)
            newRow.controls['userid'].setValue(this.legalentitybolusers[0]);

        this.selectedProducts[length] = this.bolchargecodetypes[0];

        if (this.selectedProducts && this.selectedProducts[0].bolchargecodes)
            newRow.controls['chargecode'].setValue(this.selectedProducts[0].bolchargecodes[0]);

        control.push(newRow);

        this.productTypeChanged(length);

    }

    addNewConditionRow() {
        const control = <FormArray>this.bolConcessionForm.controls['conditionItemsRows'];
        control.push(this.initConditionItemRows());
    }

    addNewConditionRowIfNone() {
        const control = <FormArray>this.bolConcessionForm.controls['conditionItemsRows'];
        if (control.length == 0)
            control.push(this.initConditionItemRows());
    }

    deleteConcessionRow(index: number) {
        if (confirm("Are you sure you want to remove this row?")) {
            const control = <FormArray>this.bolConcessionForm.controls['concessionItemRows'];
            control.removeAt(index);

            this.selectedProducts.splice(index, 1);
        }
    }

    deleteConditionRow(index: number) {
        const control = <FormArray>this.bolConcessionForm.controls['conditionItemsRows'];
        control.removeAt(index);

        this.selectedConditionTypes.splice(index, 1);

    }

    onExpiryDateChanged(itemrow) {

        var validationErrorMessage = this.baseComponentService.expiringDateDifferenceValidation(itemrow.controls['expiryDate'].value);
        if (validationErrorMessage != null) {
            this.addValidationError(validationErrorMessage);
        }
    }

    conditionTypeChanged(rowIndex) {
        const control = <FormArray>this.bolConcessionForm.controls['conditionItemsRows'];
        this.selectedConditionTypes[rowIndex] = control.controls[rowIndex].get('conditionType').value;

        let currentCondition = control.controls[rowIndex];

        currentCondition.get('conditionProduct').setValue(null);
        currentCondition.get('interestRate').setValue(null);
        currentCondition.get('volume').setValue(null);
        currentCondition.get('value').setValue(null);
    }

    productTypeChanged(rowIndex) {

        const control = <FormArray>this.bolConcessionForm.controls['concessionItemRows'];
        this.selectedProducts[rowIndex] = control.controls[rowIndex].get('product').value;

        let currentProduct = control.controls[rowIndex];
        var selectedproduct = currentProduct.get('product').value;

        this.selectedProducts[rowIndex].bolchargecodes = this.bolchargecodes.filter(re => re.fkChargeCodeTypeId == selectedproduct.pkChargeCodeTypeId);

        currentProduct.get('chargecode').setValue(this.selectedProducts[rowIndex].bolchargecodes[0]);

    }

    getBolConcession(): BolConcession {
        var bolConcession = new BolConcession();
        bolConcession.concession = new Concession();

        if (this.riskGroup)
            bolConcession.concession.riskGroupId = this.riskGroup.id;
        if (this.legalEntity)
            bolConcession.concession.legalEntityId = this.legalEntity.id;

        if (this.bolConcessionForm.controls['smtDealNumber'].value)
            bolConcession.concession.smtDealNumber = this.bolConcessionForm.controls['smtDealNumber'].value;
        else
            this.addValidationError("SMT Deal Number not captured");

        if (this.bolConcessionForm.controls['motivation'].value)
            bolConcession.concession.motivation = this.bolConcessionForm.controls['motivation'].value;
        else
            bolConcession.concession.motivation = '.';

        const concessions = <FormArray>this.bolConcessionForm.controls['concessionItemRows'];

        let hasTypeId: boolean = false;
        let hasLegalEntityId: boolean = false;
        let hasLegalEntityAccountId: boolean = false;

        for (let concessionFormItem of concessions.controls) {
            if (!bolConcession.bolConcessionDetails)
                bolConcession.bolConcessionDetails = [];

            let bolConcessionDetail = new BolConcessionDetail();

            if (concessionFormItem.get('product').value) {

            } else {
                this.addValidationError("Product not selected");
            }


            if (concessionFormItem.get('chargecode').value) {
                bolConcessionDetail.fkChargeCodeId = concessionFormItem.get('chargecode').value.pkChargeCodeId;
                hasTypeId = true;
            } else {
                this.addValidationError("Charge code not selected");
            }

            if (concessionFormItem.get('unitcharge').value || concessionFormItem.get('unitcharge').value === 0) {
                bolConcessionDetail.loadedRate = concessionFormItem.get('unitcharge').value;
            } else {
                this.addValidationError("Rate not entered");
            }

            if (concessionFormItem.get('userid').value) {
                bolConcessionDetail.fkLegalEntityBOLUserId = concessionFormItem.get('userid').value.pkLegalEntityBOLUserId;
                bolConcessionDetail.legalEntityId = concessionFormItem.get('userid').value.legalEntityId;
                bolConcessionDetail.legalEntityAccountId = concessionFormItem.get('userid').value.legalEntityAccountId;
                hasLegalEntityId = true;
                hasLegalEntityAccountId = true;
            } else {
                this.addValidationError("User ID not selected");
            }


            if (concessionFormItem.get('expiryDate').value && concessionFormItem.get('expiryDate').value != "") {
                this.onExpiryDateChanged(concessionFormItem);
                bolConcessionDetail.expiryDate = new Date(concessionFormItem.get('expiryDate').value);
            }
            else {
                this.addValidationError("Expiry date not selected");
            }

            bolConcession.bolConcessionDetails.push(bolConcessionDetail);

            if (hasTypeId && hasLegalEntityId && hasLegalEntityAccountId) {
                let hasDuplicates = this.baseComponentService.HasDuplicateConcessionUserIdChargeCode(
                    bolConcession.bolConcessionDetails,
                    concessionFormItem.get('chargecode').value.pkChargeCodeId,
                    concessionFormItem.get('userid').value.pkLegalEntityBOLUserId);

                if (hasDuplicates) {
                    this.addValidationError("Duplicate Account / Product pricing found. Please select different account.");

                    break;
                }
            }
        }

        const conditions = <FormArray>this.bolConcessionForm.controls['conditionItemsRows'];

        let concessionConditionReturnObject = this.baseComponentService.getConsessionConditionData(conditions, bolConcession.concessionConditions, this.validationError);
        bolConcession.concessionConditions = concessionConditionReturnObject.concessionConditions;
        this.validationError = concessionConditionReturnObject.validationError;

        return bolConcession;
    }

    onSubmit() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;

        var bolConcession = this.getBolConcession();

        bolConcession.concession.concessionType = ConcessionTypes.BOL;
        bolConcession.concession.type = "New";
        bolConcession.concession.comments = "Created";

        if (!this.validationError) {
            this.bolConcessionService.postNewBolData(bolConcession).subscribe(entity => {
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

    setTwoNumberDecimal($event) {
        $event.target.value = this.baseComponentService.formatDecimal($event.target.value);
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
