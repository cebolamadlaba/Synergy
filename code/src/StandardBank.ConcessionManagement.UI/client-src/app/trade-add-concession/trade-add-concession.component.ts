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

    observableBolView: Observable<TradeView>;
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
 
    selectedConditionTypes: ConditionType[];
    selectedProducts: TradeProduct[];

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

        this.conditionTypes = [new ConditionType()];
        this.selectedConditionTypes = [new ConditionType()];
        this.selectedProducts = [new TradeProduct()];
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

                this.observableClientAccounts = this.lookupDataService.getClientAccounts(this.riskGroupNumber);
                this.observableClientAccounts.subscribe(clientAccounts => this.clientAccounts = clientAccounts, error => this.errorMessage = <any>error);

            }

            if (this.riskGroupNumber) {
                this.observableBolView = this.tradeConcessionService.getTradeViewData(this.riskGroupNumber);
                this.observableBolView.subscribe(bolView => {
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
            this.lookupDataService.getPeriodTypes()

        ]).subscribe(results => {

            this.conditionTypes = <any>results[0];
            this.tradeproducttypes = <any>results[1];
            this.tradeproducts = <any>results[2];
          

            this.periods = <any>results[3];
            this.periodTypes = <any>results[4];

            const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];

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

        this.selectedProducts.push(new TradeProduct());

        return this.formBuilder.group({
            product: [''],
            chargecode: [''],
            unitcharge: [''],
            userid: [''],
            expiryDate: [''],
            accountNumber: [''],
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

            this.selectedProducts.splice(index, 1);
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

        currentCondition.get('interestRate').setValue(null);
        currentCondition.get('volume').setValue(null);
        currentCondition.get('value').setValue(null);
        currentCondition.get('expectedTurnoverValue').setValue(null);
    }

    productTypeChanged(rowIndex) {

        //const control = <FormArray>this.bolConcessionForm.controls['concessionItemRows'];
        //this.selectedProducts[rowIndex] = control.controls[rowIndex].get('product').value;

        //let currentProduct = control.controls[rowIndex];
        //var selectedproduct = currentProduct.get('product').value;

        //this.selectedProducts[rowIndex].bolchargecodes = this.bolchargecodes.filter(re => re.fkChargeCodeTypeId == selectedproduct.pkChargeCodeTypeId);

        //currentProduct.get('chargecode').setValue(this.selectedProducts[rowIndex].bolchargecodes[0]);

    }


    addValidationError(validationDetail) {
        if (!this.validationError)
            this.validationError = [];

        this.validationError.push(validationDetail);
    }

    //getBolConcession(): BolConcession {
    //    var bolConcession = new BolConcession();
    //    bolConcession.concession = new Concession();
    //    bolConcession.concession.riskGroupId = this.riskGroup.id;


    //    if (this.bolConcessionForm.controls['smtDealNumber'].value)
    //        bolConcession.concession.smtDealNumber = this.bolConcessionForm.controls['smtDealNumber'].value;
    //    else
    //        this.addValidationError("SMT Deal Number not captured");

    //    if (this.bolConcessionForm.controls['motivation'].value)
    //        bolConcession.concession.motivation = this.bolConcessionForm.controls['motivation'].value;
    //    else
    //        this.addValidationError("Motivation not captured");

    //    const concessions = <FormArray>this.bolConcessionForm.controls['concessionItemRows'];

    //    for (let concessionFormItem of concessions.controls) {
    //        if (!bolConcession.bolConcessionDetails)
    //            bolConcession.bolConcessionDetails = [];

    //        let bolConcessionDetail = new BolConcessionDetail();

    //        if (concessionFormItem.get('product').value) {

    //        } else {
    //            this.addValidationError("Product not selected");
    //        }


    //        if (concessionFormItem.get('chargecode').value) {
    //            bolConcessionDetail.fkChargeCodeId = concessionFormItem.get('chargecode').value.pkChargeCodeId;

    //        } else {
    //            this.addValidationError("Charge code not selected");
    //        }


    //        if (concessionFormItem.get('unitcharge').value) {
    //            bolConcessionDetail.loadedRate = concessionFormItem.get('unitcharge').value;
    //        } else {
    //            this.addValidationError("Charge rate not entered");
    //        }

    //        if (concessionFormItem.get('userid').value) {
    //            bolConcessionDetail.fkLegalEntityBOLUserId = concessionFormItem.get('userid').value.pkLegalEntityBOLUserId;
    //            bolConcessionDetail.legalEntityId = concessionFormItem.get('userid').value.legalEntityId;
    //            bolConcessionDetail.legalEntityAccountId = concessionFormItem.get('userid').value.legalEntityAccountId;
    //        } else {
    //            this.addValidationError("User ID not selected");
    //        }

    //        if (concessionFormItem.get('expiryDate').value)
    //            bolConcessionDetail.expiryDate = new Date(concessionFormItem.get('expiryDate').value);

    //        bolConcession.bolConcessionDetails.push(bolConcessionDetail);
    //    }

    //    const conditions = <FormArray>this.bolConcessionForm.controls['conditionItemsRows'];

    //    for (let conditionFormItem of conditions.controls) {
    //        if (!bolConcession.concessionConditions)
    //            bolConcession.concessionConditions = [];

    //        let concessionCondition = new ConcessionCondition();

    //        if (conditionFormItem.get('conditionType').value)
    //            concessionCondition.conditionTypeId = conditionFormItem.get('conditionType').value.id;
    //        else
    //            this.addValidationError("Condition type not selected");

    //        if (conditionFormItem.get('conditionProduct').value)
    //            concessionCondition.conditionProductId = conditionFormItem.get('conditionProduct').value.id;
    //        else
    //            this.addValidationError("Condition product not selected");

    //        if (conditionFormItem.get('interestRate').value)
    //            concessionCondition.interestRate = conditionFormItem.get('interestRate').value;

    //        if (conditionFormItem.get('volume').value)
    //            concessionCondition.conditionVolume = conditionFormItem.get('volume').value;

    //        if (conditionFormItem.get('value').value)
    //            concessionCondition.conditionValue = conditionFormItem.get('value').value;

    //        if (conditionFormItem.get('expectedTurnoverValue').value)
    //            concessionCondition.expectedTurnoverValue = conditionFormItem.get('expectedTurnoverValue').value;

    //        if (conditionFormItem.get('periodType').value) {
    //            concessionCondition.periodTypeId = conditionFormItem.get('periodType').value.id;
    //        } else {
    //            this.addValidationError("Period type not selected");
    //        }

    //        if (conditionFormItem.get('period').value) {
    //            concessionCondition.periodId = conditionFormItem.get('period').value.id;
    //        } else {
    //            this.addValidationError("Period not selected");
    //        }

    //        bolConcession.concessionConditions.push(concessionCondition);
    //    }

    //    return bolConcession;
    //}

    onSubmit() {
        //this.isLoading = true;

        //this.errorMessage = null;
        //this.validationError = null;

        //var bolConcession = this.getBolConcession();

        //bolConcession.concession.concessionType = ConcessionTypes.BOL;
        //bolConcession.concession.type = "New";

        //if (!this.validationError) {
        //    this.bolConcessionService.postNewBolData(bolConcession).subscribe(entity => {
        //        console.log("data saved");
        //        this.saveMessage = entity.concession.referenceNumber;
        //        this.isLoading = false;
        //    }, error => {
        //        this.errorMessage = <any>error;
        //        this.isLoading = false;
        //    });
        //} else {
        //    this.isLoading = false;
        //}
    }

    setTwoNumberDecimal($event) {
        $event.target.value = this.formatDecimal($event.target.value);
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
