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
import { UserService } from "../services/user.service";


@Component({
    selector: 'app-trade-add-concession',
    templateUrl: './trade-add-concession.component.html',
    styleUrls: ['./trade-add-concession.component.css']
})
export class TradeAddConcessionComponent implements OnInit, OnDestroy {
    private sub: any;

    errorMessage: String;
    validationError: String[];
    saveMessage: String;
    showHide = false;
    observableRiskGroup: Observable<RiskGroup>;
    riskGroup: RiskGroup;
    riskGroupNumber: number;

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
        @Inject(TradeConcessionService) private tradeConcessionService) {
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

            if (this.riskGroupNumber) {

                this.observableRiskGroup = this.lookupDataService.getRiskGroup(this.riskGroupNumber);
                this.observableRiskGroup.subscribe(riskGroup => this.riskGroup = riskGroup, error => this.errorMessage = <any>error);

                this.observableClientAccounts = this.lookupDataService.getClientAccountsConcessionType(this.riskGroupNumber, ConcessionTypes.Trade);
                this.observableClientAccounts.subscribe(clientAccounts => this.clientAccounts = clientAccounts, error => this.errorMessage = <any>error);
            }

            if (this.riskGroupNumber) {
                this.observableTradeView = this.tradeConcessionService.getTradeViewData(this.riskGroupNumber);
                this.observableTradeView.subscribe(bolView => {
                    this.tradeView = bolView;

                    this.isLoading = false;
                }, error => {
                    this.errorMessage = <any>error;
                    this.isLoading = false;
                });
            }
        });


        this.tradeConcessionForm = this.formBuilder.group({
            concessionItemRows: this.formBuilder.array([this.initConcessionItemRows()]),
            conditionItemsRows: this.formBuilder.array([]),
            smtDealNumber: new FormControl(),
            motivation: new FormControl()
        });

        Observable.forkJoin([

            this.lookupDataService.getConditionTypes(),

            this.lookupDataService.getTradeProductTypes(),
            this.lookupDataService.getTradeProducts(),
       
            this.lookupDataService.getPeriods(),
            this.lookupDataService.getPeriodTypes(),
            this.lookupDataService.getLegalEntityGBBNumbers(this.riskGroupNumber),

        ]).subscribe(results => {

            this.conditionTypes = <any>results[0];
            this.tradeproducttypes = <any>results[1];
            this.tradeproducts = <any>results[2];          

            this.periods = <any>results[3];
            this.periodTypes = <any>results[4];
            this.legalentitygbbnumbers = <any>results[5];

            const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];

            //if (this.legalentitygbbnumbers)
            //    control.controls[0].get('gbbnumber').setValue(this.legalentitygbbnumbers[0]);

            //if (this.bolchargecodetypes)
            //    control.controls[0].get('product').setValue(this.bolchargecodetypes[0]);

            //if (this.legalentitybolusers)
            //    control.controls[0].get('userid').setValue(this.legalentitybolusers[0]);

            //this.selectedProducts[0] = this.bolchargecodetypes[0];

            //if (this.selectedProducts && this.selectedProducts[0].bolchargecodes)
            //    control.controls[0].get('chargecode').setValue(this.selectedProducts[0].bolchargecodes[0]);

            //this.productTypeChanged(0);

            this.isLoading = false;
        },
            error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
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
            expiryDate: [''],
            accountNumber: [''],
            term: [''],
            advalorem: [''],
            min: [''],
            max: [''],
            communication: [''],
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
            expectedTurnoverValue: [''],
            periodType: [''],
            period: ['']
        });
    }

    addNewConcessionRow() {
        const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];
        var newRow = this.initConcessionItemRows();

        //if (this.legalentitygbbnumbers)
        //    newRow.controls['gbbnumber'].setValue(this.legalentitygbbnumbers[0]);

        //var length = control.controls.length;

        //if (this.bolchargecodetypes)
        //    newRow.controls['product'].setValue(this.bolchargecodetypes[0]);

        //if (this.legalentitybolusers)
        //    newRow.controls['userid'].setValue(this.legalentitybolusers[0]);

        //this.selectedProducts[length] = this.bolchargecodetypes[0];

        //if (this.selectedProducts && this.selectedProducts[0].bolchargecodes)
        //    newRow.controls['chargecode'].setValue(this.selectedProducts[0].bolchargecodes[0]);

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
        currentCondition.get('expectedTurnoverValue').setValue(null);
    }   

    productTypeChanged(rowIndex) {

        const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];

        this.selectedProductTypes[rowIndex] = control.controls[rowIndex].get('producttype').value;

        let currentProduct = control.controls[rowIndex];
        var selectedproducttype = currentProduct.get('producttype').value;

        this.selectedProductTypes[rowIndex].products = this.tradeproducts.filter(re => re.tradeProductTypeId == selectedproducttype.tradeProductTypeID);

        currentProduct.get('product').setValue(this.selectedProductTypes[rowIndex].products[0]);

        if (selectedproducttype.tradeProductType == "Local guarantee") {

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

        }
        else {

            this.selectedTradeConcession[rowIndex] = false;
            currentProduct.get('disablecontrolset').setValue(false);
            currentProduct.get('gbbnumber').setValue(null);          

            currentProduct.get('term').setValue(null);
            currentProduct.get('estfee').setValue(null);
            currentProduct.get('rate').setValue(null);
        }


    }

    addValidationError(validationDetail) {
        if (!this.validationError)
            this.validationError = [];

        this.validationError.push(validationDetail);
    }
  

    getTradeConcession(): TradeConcession {
        var tradeConcession = new TradeConcession();
        tradeConcession.concession = new Concession();
        tradeConcession.concession.riskGroupId = this.riskGroup.id;

        if (this.tradeConcessionForm.controls['motivation'].value)
            tradeConcession.concession.motivation = this.tradeConcessionForm.controls['motivation'].value;
        else
            tradeConcession.concession.motivation = '.';


        const concessions = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];

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

                if (concessionFormItem.get('producttype').value.tradeProductType == "Local guarantee") {
                    applyexpirydate = false;
                }

            } else {
                this.addValidationError("Product Type code not selected");
            }

            if ((concessionFormItem.get('accountNumber').value && concessionFormItem.get('accountNumber').value.legalEntityId)) {
                tradeConcessionDetail.legalEntityId = concessionFormItem.get('accountNumber').value.legalEntityId;
                tradeConcessionDetail.legalEntityAccountId = concessionFormItem.get('accountNumber').value.legalEntityAccountId;
            } else {

                if (!tradeConcessionDetail.disablecontrolset) {
                    this.addValidationError("Client account not selected");
                }
            }

            if (concessionFormItem.get('gbbnumber').value) {
               
                var gbbnumber = concessionFormItem.get('gbbnumber').value;
                tradeConcessionDetail.legalEntityId = gbbnumber.legalEntityId;
                tradeConcessionDetail.legalEntityAccountId = gbbnumber.legalEntityAccountId;
                tradeConcessionDetail.fkLegalEntityGBBNumber = gbbnumber.pkLegalEntityGBBNumber;

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

            if (concessionFormItem.get('advalorem').value) {
                tradeConcessionDetail.adValorem = concessionFormItem.get('advalorem').value;
            } else {
                if (!tradeConcessionDetail.disablecontrolset) {
                    this.addValidationError("AdValorem value not entered");
                }
            }

            if (concessionFormItem.get('min').value) {
                tradeConcessionDetail.min = concessionFormItem.get('min').value;
            } else {
                if (!tradeConcessionDetail.disablecontrolset) {
                    this.addValidationError("Min value not entered");
                }
            }

            if (concessionFormItem.get('max').value) {
                tradeConcessionDetail.max = concessionFormItem.get('max').value;
            } else {
                if (!tradeConcessionDetail.disablecontrolset) {
                    this.addValidationError("Max value not entered");
                }
            }
           
            if (concessionFormItem.get('communication').value || concessionFormItem.get('communication').value == 0) {
                tradeConcessionDetail.communication = concessionFormItem.get('communication').value;
            } else {
                if (!tradeConcessionDetail.disablecontrolset) {
                    this.addValidationError("Communication not entered");
                }
            }
            if (concessionFormItem.get('flatfee').value) {
                tradeConcessionDetail.flatFee = concessionFormItem.get('flatfee').value;
            } else {
                if (!tradeConcessionDetail.disablecontrolset) {
                    this.addValidationError("Flat fee not entered");
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

            if (concessionFormItem.get('rate').value) {
                tradeConcessionDetail.loadedRate = concessionFormItem.get('rate').value;
            } else {
                if (tradeConcessionDetail.disablecontrolset) {
                    this.addValidationError("Rate not entered");
                }
            } 

            if (concessionFormItem.get('expiryDate').value && concessionFormItem.get('expiryDate').value != "") {
                tradeConcessionDetail.expiryDate = new Date(concessionFormItem.get('expiryDate').value);
            }
            else {
                if (applyexpirydate) {
                    this.addValidationError("Expiry date not selected");
                }
            }

            tradeConcession.tradeConcessionDetails.push(tradeConcessionDetail);
        }

        const conditions = <FormArray>this.tradeConcessionForm.controls['conditionItemsRows'];

        for (let conditionFormItem of conditions.controls) {
            if (!tradeConcession.concessionConditions)
                tradeConcession.concessionConditions = [];

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

            if (conditionFormItem.get('expectedTurnoverValue').value)
                concessionCondition.expectedTurnoverValue = conditionFormItem.get('expectedTurnoverValue').value;

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

            tradeConcession.concessionConditions.push(concessionCondition);
        }

        return tradeConcession;
    }

    onSubmit() {

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
        $event.target.value = this.formatDecimal($event.target.value);
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

    formatDecimal(itemValue: number) {
        if (itemValue) {
            return new DecimalPipe('en-US').transform(itemValue, '1.2-2');
        }

        return null;
    }

    goBack() {
        this.router.navigate(['/pricing', this.riskGroupNumber]);
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}
