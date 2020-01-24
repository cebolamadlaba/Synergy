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

import { ClientAccount } from "../models/client-account";
import { LookupDataService } from "../services/lookup-data.service";
import { ConcessionCondition } from "../models/concession-condition";

import { DecimalPipe } from '@angular/common';
import { ConcessionTypes } from '../constants/concession-types';
import { ConditionType } from "../models/condition-type";

import { TradeProduct } from "../models/trade-product";
import { TradeProductType } from "../models/trade-product-type";

import { LegalEntityGBBNumber } from "../models/legal-entity-gbb-number";

import { TradeConcession } from "../models/trade-concession";
import { TradeConcessionDetail } from "../models/trade-concession-detail";
import { TradeConcessionService } from "../services/trade-concession.service";

import { TradeView } from "../models/trade-view";
import { Concession } from "../models/concession";
import { LegalEntity } from "../models/legal-entity";
import { UserService } from "../services/user.service";
import { TradeConcessionBaseService } from "../services/trade-concession-base.service";

import { BaseComponentService } from '../services/base-component.service';
import * as moment from 'moment';
import { MOnthEnum } from '../models/month-enum';

@Component({
    selector: 'app-trade-add-concession',
    templateUrl: './trade-add-concession.component.html',
    styleUrls: ['./trade-add-concession.component.css']
})
export class TradeAddConcessionComponent extends TradeConcessionBaseService implements OnInit, OnDestroy {
    private sub: any;

    errorMessage: String;
    validationError: String[];
    notificationMessage: string;

    saveMessage: String;
    showHide = false;
    observableRiskGroup: Observable<RiskGroup>;
    riskGroup: RiskGroup;
    riskGroupNumber: number;
    legalEntity: LegalEntity;
    sapbpid: number;

    entityName: string;
    entityNumber: string;

    observableTradeView: Observable<TradeView>;
    tradeView: TradeView = new TradeView();

    tradeConcessionForm: FormGroup;

    isLoading = true;

    observablePeriods: Observable<Period[]>;
    periods: Period[];

    observablePeriodTypes: Observable<PeriodType[]>;
    periodTypes: PeriodType[];

    observableTradeProductTypes: Observable<TradeProductType[]>;
    tradeproducttypes: TradeProductType[];

    observableTradeProducts: Observable<TradeProduct[]>;
    tradeproducts: TradeProduct[];

    observableLegalEntityGBbNumbers: Observable<LegalEntityGBBNumber[]>;
    legalentitygbbnumbers: LegalEntityGBBNumber[];

    selectedConditionTypes: ConditionType[];
    selectedProductTypes: TradeProductType[];
    selectedTradeConcession: boolean[];

    observableConditionTypes: Observable<ConditionType[]>;
    conditionTypes: ConditionType[];

    observableClientAccounts: Observable<ClientAccount[]>;
    clientAccounts: ClientAccount[];


    constructor(private route: ActivatedRoute,
        private router: Router,
        private formBuilder: FormBuilder,
        private location: Location,
        @Inject(LookupDataService) private lookupDataService,
        @Inject(TradeConcessionService) private tradeConcessionService,
        private baseComponentService: BaseComponentService) {
        super();
        this.riskGroup = new RiskGroup();

        this.tradeproducttypes = [new TradeProductType()];
        this.tradeproducts = [new TradeProduct()];

        this.periods = [new Period()];
        this.periodTypes = [new PeriodType()];

        this.legalentitygbbnumbers = [new LegalEntityGBBNumber()];

        this.conditionTypes = [new ConditionType()];
        this.selectedConditionTypes = [new ConditionType()];
        this.selectedProductTypes = [new TradeProductType()];
        this.selectedTradeConcession = [false];

        this.clientAccounts = [new ClientAccount()];

        this.tradeView.riskGroup = new RiskGroup();
        this.tradeView.tradeConcessions = [new TradeConcession()];
        this.tradeView.tradeConcessions[0].concession = new Concession();

    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
            this.sapbpid = +params['sapbpid'];

            this.observableClientAccounts = this.lookupDataService.getClientAccountsConcessionType(this.riskGroupNumber, this.sapbpid, ConcessionTypes.Trade);
            this.observableClientAccounts.subscribe(clientAccounts => this.clientAccounts = clientAccounts, error => this.errorMessage = <any>error);

            this.observableTradeView = this.tradeConcessionService.getTradeViewData(this.riskGroupNumber, this.sapbpid);
            this.observableTradeView.subscribe(tradeView => {
                this.tradeView = tradeView;

                this.isLoading = false;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });

        });


        this.tradeConcessionForm = this.formBuilder.group({
            concessionItemRows: this.formBuilder.array([this.initConcessionItemRows()]),
            conditionItemsRows: this.formBuilder.array([]),
            smtDealNumber: new FormControl(),
            motivation: new FormControl()
        });

        this.getInitialData();
    }

    getInitialData() {
        if (this.riskGroupNumber != null && this.riskGroupNumber != 0) {
            Observable.forkJoin([
                this.lookupDataService.getConditionTypes(),
                this.lookupDataService.getTradeProductTypes(),
                this.lookupDataService.getTradeProducts(),
                this.lookupDataService.getPeriods(),
                this.lookupDataService.getPeriodTypes(),
                this.lookupDataService.getLegalEntityGBBNumbers(this.riskGroupNumber),
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
                this.lookupDataService.getTradeProductTypes(),
                this.lookupDataService.getTradeProducts(),
                this.lookupDataService.getPeriods(),
                this.lookupDataService.getPeriodTypes(),
                this.lookupDataService.getLegalEntityGBBNumbersBySAPBPID(this.sapbpid),
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
        this.tradeproducttypes = <any>results[1];
        this.tradeproducts = <any>results[2];
        this.periods = <any>results[3];
        this.periodTypes = <any>results[4];
        this.legalentitygbbnumbers = <any>results[5];

        const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];

        this.isLoading = false;
    }

    initConcessionItemRows() {

        this.selectedProductTypes.push(new TradeProductType());
        this.selectedTradeConcession.push(false)

        return this.formBuilder.group({
            disablecontrolset: [''],
            product: [''],
            producttype: [''],
            accountnumber: [''],
            gbbnumber: [''],
            gbbnumberText: [''],
            expiryDate: [''],
            accountNumber: [''],
            term: [''],
            advalorem: [''],
            min: [''],
            max: [''],
            communication: [null],
            flatfee: [''],
            currency: [''],
            estfee: [''],
            rate: ['']
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
        const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];
        var newRow = this.initConcessionItemRows();

        control.push(newRow);

        this.productTypeChanged(length);

    }

    addNewConditionRow() {
        const control = <FormArray>this.tradeConcessionForm.controls['conditionItemsRows'];
        control.push(this.initConditionItemRows());
    }

    addNewConditionRowIfNone() {
        const control = <FormArray>this.tradeConcessionForm.controls['conditionItemsRows'];
        if (control.length == 0)
            control.push(this.initConditionItemRows());
    }

    deleteConcessionRow(index: number) {
        if (confirm("Are you sure you want to remove this row?")) {
            const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];
            control.removeAt(index);

            this.selectedProductTypes.splice(index, 1);
            this.selectedTradeConcession.splice(index, 1);
        }
    }

    deleteConditionRow(index: number) {
        const control = <FormArray>this.tradeConcessionForm.controls['conditionItemsRows'];
        control.removeAt(index);

        this.selectedConditionTypes.splice(index, 1);

    }

    conditionTypeChanged(rowIndex) {
        const control = <FormArray>this.tradeConcessionForm.controls['conditionItemsRows'];
        this.selectedConditionTypes[rowIndex] = control.controls[rowIndex].get('conditionType').value;

        let currentCondition = control.controls[rowIndex];

        currentCondition.get('conditionProduct').setValue(null);
        currentCondition.get('interestRate').setValue(null);
        currentCondition.get('volume').setValue(null);
        currentCondition.get('value').setValue(null);
    }

    productTypeChanged(rowIndex) {
        this.notificationMessage = null;

        const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];

        this.selectedProductTypes[rowIndex] = control.controls[rowIndex].get('producttype').value;

        let currentProduct = control.controls[rowIndex];
        var selectedproducttype = currentProduct.get('producttype').value;

        this.selectedProductTypes[rowIndex].products = this.tradeproducts.filter(re => re.tradeProductTypeId == selectedproducttype.tradeProductTypeID);

        currentProduct.get('product').setValue(this.selectedProductTypes[rowIndex].products[0]);

        switch (selectedproducttype.tradeProductType) {
            case TradeProductType.LocalGuarantee:
                this.notificationMessage = "Please note there is no system integration for GBBs, therefore collateral Centres need to load fees/ rates.";

                this.selectedTradeConcession[rowIndex] = true;

                currentProduct.get('disablecontrolset').setValue(true);
                currentProduct.get('accountNumber').setValue(null);
                currentProduct.get('advalorem').setValue(null);
                currentProduct.get('min').setValue(null);
                currentProduct.get('max').setValue(null);

                currentProduct.get('communication').setValue(null);
                currentProduct.get('flatfee').setValue(null);
                currentProduct.get('currency').setValue(null);
                currentProduct.get('expiryDate').setValue('');
                break;
            default:
                this.selectedTradeConcession[rowIndex] = false;
                currentProduct.get('disablecontrolset').setValue(false);
                currentProduct.get('gbbnumber').setValue(null);

                currentProduct.get('term').setValue(null);
                currentProduct.get('estfee').setValue(null);
                currentProduct.get('rate').setValue(null);
                break;
        }

    }

    onExpiryDateChanged(itemrow) {

        var validationErrorMessage = this.baseComponentService.expiringDateDifferenceValidation(itemrow.controls['expiryDate'].value);
        if (validationErrorMessage != null) {
            this.addValidationError(validationErrorMessage);
        }
    }

    onTermValueChange(rowIndex) {

        this.errorMessage = null;
        this.validationError = null;

        const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];
        let term = control.controls[rowIndex].get('term').value;

        if (term < MOnthEnum.ThreeMonths) {
            this.addValidationError("Minimum term captured should be 3 months");
        };
    }

    addValidationError(validationDetail) {
        if (!this.validationError)
            this.validationError = [];

        this.validationError.push(validationDetail);
    }

    showGbbDeclaimer() {
        this.notificationMessage = "For New GBB, insert C/A number and update once the M-number is issued. For existing GBB use existing M- number.";
    }

    disableField(rowIndex, fieldname) {

        return super.disableFieldBase(
            this.selectedTradeConcession[rowIndex],
            null,
            this.tradeConcessionForm,
            rowIndex,
            fieldname,
            this.saveMessage == null,
            this.saveMessage != null
        )
    }

    disableCommunicationFee(rowIndex) {

        const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];
        let currentrow = control.controls[rowIndex];

        let productype = currentrow.get('producttype').value;

        if (productype != null && productype.tradeProductType != "" && productype.tradeProductType != TradeProductType.OutwardTT) {
            currentrow.get('communication').disable();
            currentrow.get('communication').setValue(null);
        }
        else {
            currentrow.get('communication').enable();
            if (this.saveMessage) {
                currentrow.get('communication').disable();
            }
            else {
                currentrow.get('communication').enable();
            }
        }
    }

    clearMessages() {
        this.errorMessage = null;
        this.notificationMessage = null;
        this.saveMessage = null;
        this.validationError = null;
    }

    getTradeConcession(): TradeConcession {
        var tradeConcession = new TradeConcession();
        tradeConcession.concession = new Concession();

        if (this.riskGroup)
            tradeConcession.concession.riskGroupId = this.riskGroup.id;
        if (this.legalEntity)
            tradeConcession.concession.legalEntityId = this.legalEntity.id;


        if (this.tradeConcessionForm.controls['motivation'].value)
            tradeConcession.concession.motivation = this.tradeConcessionForm.controls['motivation'].value;
        else
            tradeConcession.concession.motivation = '.';

        const concessions = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];

        let hasTypeId: boolean = false;
        let hasLegalEntityId: boolean = false;
        let hasLegalEntityAccountId: boolean = false;

        for (let concessionFormItem of concessions.controls) {
            if (!tradeConcession.tradeConcessionDetails)
                tradeConcession.tradeConcessionDetails = [];

            let tradeConcessionDetail = new TradeConcessionDetail();
            let applyexpirydate = true;

            tradeConcessionDetail.disablecontrolset = concessionFormItem.get('disablecontrolset').value;

            if (concessionFormItem.get('product').value) {
                tradeConcessionDetail.fkTradeProductId = concessionFormItem.get('product').value.tradeProductId;

            } else {
                this.addValidationError("Product not selected");
            }

            if (concessionFormItem.get('producttype').value) {
                tradeConcessionDetail.tradeProductTypeID = concessionFormItem.get('producttype').value.tradeProductTypeID;

                if (concessionFormItem.get('producttype').value.tradeProductType == TradeProductType.LocalGuarantee) {
                    applyexpirydate = false;
                }
                hasTypeId = true;
            } else {
                this.addValidationError("Product Type code not selected");
            }

            if ((concessionFormItem.get('accountNumber').value && concessionFormItem.get('accountNumber').value.legalEntityId)) {
                tradeConcessionDetail.legalEntityId = concessionFormItem.get('accountNumber').value.legalEntityId;
                tradeConcessionDetail.legalEntityAccountId = concessionFormItem.get('accountNumber').value.legalEntityAccountId;
                hasLegalEntityId = true;
                hasLegalEntityAccountId = true;
            } else {
                hasLegalEntityId = false;
                hasLegalEntityAccountId = false;
                if (!tradeConcessionDetail.disablecontrolset) {
                    this.addValidationError("Client account not selected");
                }
            }

            let hasValueGbbNumber = (concessionFormItem.get('gbbnumberText').value);

            //if (concessionFormItem.get('gbbnumber').value) {
            if (hasValueGbbNumber) {
                tradeConcessionDetail.gbbNumber = concessionFormItem.get('gbbnumberText').value;
            } else {
                if (tradeConcessionDetail.disablecontrolset) {
                    this.addValidationError("GBB Number not entered");
                }
            }


            if (concessionFormItem.get('term').value) {
                tradeConcessionDetail.term = concessionFormItem.get('term').value;
            } else {
                if (tradeConcessionDetail.disablecontrolset) {
                    this.addValidationError("Term not entered");
                }
            }

            let advaloremfound = false;

            if ((concessionFormItem.get('advalorem').value != null && concessionFormItem.get('advalorem').value != "") ||
                concessionFormItem.get('advalorem').value === 0) {
                advaloremfound = true;
                tradeConcessionDetail.adValorem = concessionFormItem.get('advalorem').value;
            }

            if (concessionFormItem.get('min').value && advaloremfound) {
                tradeConcessionDetail.min = concessionFormItem.get('min').value;
            } else {
                if (!tradeConcessionDetail.disablecontrolset && advaloremfound) {
                    this.addValidationError("Min value not entered");
                }
            }

            if (concessionFormItem.get('max').value && advaloremfound) {
                tradeConcessionDetail.max = concessionFormItem.get('max').value;
            } else {
                if (!tradeConcessionDetail.disablecontrolset && advaloremfound) {
                    this.addValidationError("Max value not entered");
                }
            }

            if (!concessionFormItem.get('communication').disabled) {
                let communicationVal = concessionFormItem.get('communication').value;
                if (communicationVal != null || communicationVal == 0) {
                    tradeConcessionDetail.communication = communicationVal;
                } else {
                    if (!tradeConcessionDetail.disablecontrolset) {
                        this.addValidationError("Communication not entered");
                    }
                }
            }


            let flatfeefound = false;

            if ((concessionFormItem.get('flatfee').value != null && concessionFormItem.get('flatfee').value != "") ||
                concessionFormItem.get('flatfee').value === 0) {
                flatfeefound = true;
                tradeConcessionDetail.flatFee = concessionFormItem.get('flatfee').value;
            } else {
                if (!tradeConcessionDetail.disablecontrolset && !advaloremfound) {
                    //this.addValidationError("Flat fee not entered");
                }
            }

            if ((flatfeefound == false) && (advaloremfound == false)) {

                if (applyexpirydate) {
                    this.addValidationError("Either AdValorem or Flat fee must be entered");
                }
            }

            if (concessionFormItem.get('currency').value) {
                tradeConcessionDetail.currency = concessionFormItem.get('currency').value;
            } else {
                if (!tradeConcessionDetail.disablecontrolset) {
                    this.addValidationError("Currency not selected");
                }
            }

            if (concessionFormItem.get('estfee').value) {
                tradeConcessionDetail.establishmentFee = concessionFormItem.get('estfee').value;
            } else {
                if (tradeConcessionDetail.disablecontrolset) {
                    this.addValidationError("Est. fee not entered");
                }
            }

            if (concessionFormItem.get('rate').value || concessionFormItem.get('rate').value == 0) {
                tradeConcessionDetail.rate = concessionFormItem.get('rate').value;
            } else {
                if (tradeConcessionDetail.disablecontrolset) {
                    this.addValidationError("Rate not entered");
                }
            }

            if (concessionFormItem.get('expiryDate').value && concessionFormItem.get('expiryDate').value != "") {
                this.onExpiryDateChanged(concessionFormItem);
                tradeConcessionDetail.expiryDate = new Date(concessionFormItem.get('expiryDate').value);
            }
            else {
                if (applyexpirydate) {
                    this.addValidationError("Expiry date not selected");
                }
            }

            tradeConcession.tradeConcessionDetails.push(tradeConcessionDetail);

            if (hasTypeId && hasLegalEntityId && hasLegalEntityAccountId) {
                let hasDuplicates = this.baseComponentService.HasDuplicateConcessionAccountTradeProduct(
                    tradeConcession.tradeConcessionDetails,
                    concessionFormItem.get('producttype').value.tradeProductTypeID,
                    concessionFormItem.get('accountNumber').value.legalEntityId,
                    concessionFormItem.get('accountNumber').value.legalEntityAccountId);

                if (hasDuplicates) {
                    this.addValidationError("Duplicate Account / Trade Product pricing found. Please select different account.");

                    break;
                }
            }
        }

        const conditions = <FormArray>this.tradeConcessionForm.controls['conditionItemsRows'];

        let concessionConditionReturnObject = this.baseComponentService.getConsessionConditionData(conditions, tradeConcession.concessionConditions, this.validationError);
        tradeConcession.concessionConditions = concessionConditionReturnObject.concessionConditions;
        this.validationError = concessionConditionReturnObject.validationError;

        return tradeConcession;
    }

    onSubmit() {
        this.clearMessages();

        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;

        var tradeConcession = this.getTradeConcession();

        tradeConcession.concession.concessionType = ConcessionTypes.Trade;
        tradeConcession.concession.type = "New";
        tradeConcession.concession.comments = "Created";

        if (!this.validationError) {
            this.tradeConcessionService.postNewTradeData(tradeConcession).subscribe(entity => {
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
        //$event.target.value = this.formatDecimal($event.target.value);
    }


    setThreeNumberDecimal($event) {

        if ($event.target.value) {
            $event.target.value = new DecimalPipe('en-US').transform($event.target.value, '1.3-3');
        }
        else {

            $event.target.value = null;
        }
    }

    setadvalorem($event, rowIndex, controlname) {

        if (event.target != null && $event.target.value != "") {

            const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];
            let currentrow = control.controls[rowIndex];

            if (controlname == "advalorem") {

                currentrow.get('flatfee').disable();
                currentrow.get('flatfee').setValue(null);
            }
            else {
                currentrow.get('advalorem').disable();
                currentrow.get('min').disable();
                currentrow.get('max').disable();

                currentrow.get('advalorem').setValue(null);
                currentrow.get('min').setValue(null);
                currentrow.get('max').setValue(null);

            }
        }
        else {
            const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];
            let currentrow = control.controls[rowIndex];

            if (controlname == "advalorem") {

                currentrow.get('flatfee').enable();
            }
            else {

                currentrow.get('advalorem').enable();
                currentrow.get('min').enable();
                currentrow.get('max').enable();


            }
        }


        //$event.target.value = this.formatDecimal($event.target.value);
    }

    setFlatFee($event, rowIndex, controlname) {

        const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];
        let currentrow = control.controls[rowIndex];

        if (event.target != null && $event.target.value != "") {

            if (controlname == "flatfee") {
                currentrow.get('advalorem').disable();
                currentrow.get('advalorem').setValue(null);
                currentrow.get('min').disable();
                currentrow.get('max').disable();
                currentrow.get('min').setValue(null);
                currentrow.get('max').setValue(null);
            }
        }
        else {
            currentrow.get('advalorem').enable();
            currentrow.get('min').enable();
            currentrow.get('max').enable();
            currentrow.get('advalorem').setValue(null);
            currentrow.get('min').setValue(null);
            currentrow.get('max').setValue(null);
        }
    }

    getSelectedProductType(rowIndex): TradeProductType {
        const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];

        let currentConcession = control.controls[rowIndex];

        let selectedProductType: TradeProductType = currentConcession.get('producttype').value;

        return selectedProductType;
    }

    goBack() {
        this.router.navigate(['/pricing', this.riskGroupNumber]);
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
