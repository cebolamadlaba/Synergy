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
import { ClientAccountArray } from "../models/client-account-array";

import { LookupDataService } from "../services/lookup-data.service";
import { ConcessionCondition } from "../models/concession-condition";

import { DecimalPipe } from '@angular/common';
import { ConcessionTypes } from '../constants/concession-types';
import { ConditionType } from "../models/condition-type";
import { ProductType } from "../models/product-type";
import { InvestmentProduct } from "../models/investment-product";

import { LegalEntityGBBNumber } from "../models/legal-entity-gbb-number";
import { LegalEntity } from "../models/legal-entity";

import { InvestmentConcession } from "../models/investment-concession";
import { InvestmentConcessionDetail } from "../models/investment-concession-detail";
import { InvestmentConcessionService } from "../services/investment-concession.service";

import { InvestmentView } from "../models/investment-view";
import { Concession } from "../models/concession";
import { UserService } from "../services/user.service";

import { BaseComponentService } from '../services/base-component.service';
import * as moment from 'moment';
import { MOnthEnum } from '../models/month-enum';

@Component({
    selector: 'app-investments-add-concession',
    templateUrl: './investments-add-concession.component.html',
    styleUrls: ['./investments-add-concession.component.css']
})
export class InvestmentAddConcessionComponent implements OnInit, OnDestroy {
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

    primeRate = "0.00";
    today: string;

    observableInvestmentView: Observable<InvestmentView>;
    investmentView: InvestmentView = new InvestmentView();

    investmentConcessionForm: FormGroup;

    isLoading = true;
    selectedAccountNumbers: ClientAccountArray[];

    observablePeriods: Observable<Period[]>;
    periods: Period[];

    observablePeriodTypes: Observable<PeriodType[]>;
    periodTypes: PeriodType[];

    observableInvestmentProducts: Observable<InvestmentProduct[]>;
    investmentproducts: InvestmentProduct[];

    observableLegalEntityGBbNumbers: Observable<LegalEntityGBBNumber[]>;
    legalentitygbbnumbers: LegalEntityGBBNumber[];

    selectedConditionTypes: ConditionType[];
    selectedProductTypes: ProductType[];

    observableProductTypes: Observable<ProductType[]>;
    productTypes: ProductType[];

    selectedInvestmentConcession: boolean[];

    observableConditionTypes: Observable<ConditionType[]>;
    conditionTypes: ConditionType[];

    observableClientAccounts: Observable<ClientAccount[]>;
    clientAccounts: ClientAccount[];

    entityName: string;
    entityNumber: string;

    constructor(private route: ActivatedRoute,
        private router: Router,
        private formBuilder: FormBuilder,
        private location: Location,
        @Inject(LookupDataService) private lookupDataService,
        @Inject(InvestmentConcessionService) private investmentConcessionService,
        private baseComponentService: BaseComponentService) {
        this.riskGroup = new RiskGroup();

        this.productTypes = [new ProductType()];
        this.selectedProductTypes = [new ProductType()];
        this.selectedAccountNumbers = [new ClientAccountArray()];
        this.investmentproducts = [new InvestmentProduct()];

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

    }

    ngOnInit() {

        this.today = new Date().toISOString().split('T')[0];

        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
            this.sapbpid = +params['sapbpid'];


            this.observableInvestmentView = this.investmentConcessionService.getInvestmentViewData(this.riskGroupNumber, this.sapbpid);
            this.observableInvestmentView.subscribe(investmentView => {
                this.investmentView = investmentView;

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

        });


        this.investmentConcessionForm = this.formBuilder.group({
            concessionItemRows: this.formBuilder.array([this.initConcessionItemRows()]),
            conditionItemsRows: this.formBuilder.array([]),
            smtDealNumber: new FormControl(),
            motivation: new FormControl(),
            prime: new FormControl()
        });

        this.getInitialData();
    }

    getInitialData() {
        if (this.riskGroupNumber != null && this.riskGroupNumber != 0) {
            Observable.forkJoin([
                this.lookupDataService.getProductTypes(ConcessionTypes.Investment),
                this.lookupDataService.getPeriods(),
                this.lookupDataService.getPeriodTypes(),
                this.lookupDataService.getConditionTypes(),
                this.lookupDataService.getRiskGroup(this.riskGroupNumber),
                this.lookupDataService.getClientAccountsConcessionType(this.riskGroupNumber, this.sapbpid, ConcessionTypes.Investment),
                this.lookupDataService.getPrimeRate(this.today)
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
                this.lookupDataService.getClientAccountsConcessionType(this.riskGroupNumber, this.sapbpid, ConcessionTypes.Investment),
                this.lookupDataService.getPrimeRate(this.today)
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

        this.isLoading = false;

        const control = <FormArray>this.investmentConcessionForm.controls['concessionItemRows'];
        if (this.productTypes) {
            control.controls[0].get('productType').setValue(this.productTypes[0]);

            this.selectedProductTypes[0] = this.productTypes[0];
            this.productTypeChanged(0);
        }
    }

    initConcessionItemRows() {

        this.selectedProductTypes.push(new ProductType());
        this.selectedAccountNumbers.push(new ClientAccountArray());

        return this.formBuilder.group({
            disablecontrolset: [''],
            productType: [''],
            accountNumber: [''],
            balance: [''],
            noticeperiod: [''],
            rate: [''],
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
            periodType: [''],
            period: ['']
        });
    }

    addNewConcessionRow() {

        const control = <FormArray>this.investmentConcessionForm.controls['concessionItemRows'];

        var newRow = this.initConcessionItemRows();

        if (this.productTypes)
            newRow.controls['productType'].setValue(this.productTypes[0]);

        if (this.clientAccounts)
            newRow.controls['accountNumber'].setValue(this.clientAccounts[0]);

        control.push(newRow);
        this.productTypeChanged(control.controls.length - 1)

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
            const control = <FormArray>this.investmentConcessionForm.controls['concessionItemRows'];
            control.removeAt(index);

            this.selectedProductTypes.splice(index, 1);
            this.selectedInvestmentConcession.splice(index, 1);
        }
    }

    deleteConditionRow(index: number) {
        const control = <FormArray>this.investmentConcessionForm.controls['conditionItemsRows'];
        control.removeAt(index);

        this.selectedConditionTypes.splice(index, 1);

    }

    onExpiryDateChanged(rowIndex) {

        const control = <FormArray>this.investmentConcessionForm.controls['concessionItemRows'];
        let selectedExpiryDate = control.controls[rowIndex].get('expiryDate').value;

        var currentMonth = moment().month()
        var selectedExpiryDateMonth = moment(selectedExpiryDate).month();
        let monthsDifference = currentMonth - selectedExpiryDateMonth;

        if (monthsDifference < MOnthEnum.ThreeMonths) {
            this.addValidationError("Concession expiry date must be greater than 3 months");
        };
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

    productTypeChanged(rowIndex) {

        const control = <FormArray>this.investmentConcessionForm.controls['concessionItemRows'];

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
            currentRow.get('expiryDate').setValue('');
            currentRow.get('expiryDate').disable();
        }
        else {
            currentRow.get('expiryDate').enable();
            this.selectedInvestmentConcession[rowIndex] = true;

        }
    }

    addValidationError(validationDetail) {
        if (!this.validationError)
            this.validationError = [];

        this.validationError.push(validationDetail);
    }

    getInvestmentConcession(): InvestmentConcession {
        var investmentConcession = new InvestmentConcession();
        investmentConcession.concession = new Concession();

        if (this.riskGroup)
            investmentConcession.concession.riskGroupId = this.riskGroup.id;
        if (this.legalEntity)
            investmentConcession.concession.legalEntityId = this.legalEntity.id;

        if (this.investmentConcessionForm.controls['smtDealNumber'].value) {
            investmentConcession.concession.smtDealNumber = this.investmentConcessionForm.controls['smtDealNumber'].value;
        }
        else
            this.addValidationError("SMT Deal Number not captured");

        if (this.investmentConcessionForm.controls['motivation'].value)
            investmentConcession.concession.motivation = this.investmentConcessionForm.controls['motivation'].value;
        else
            investmentConcession.concession.motivation = '.';


        const concessions = <FormArray>this.investmentConcessionForm.controls['concessionItemRows'];

        let hasTypeId: boolean = false;
        let hasLegalEntityId: boolean = false;
        let hasLegalEntityAccountId: boolean = false;

        for (let concessionFormItem of concessions.controls) {
            if (!investmentConcession.investmentConcessionDetails)
                investmentConcession.investmentConcessionDetails = [];

            let investmentConcessionDetail = new InvestmentConcessionDetail();

            investmentConcessionDetail.disablecontrolset = concessionFormItem.get('disablecontrolset').value;

            let applyexpirydate = false;

            if (concessionFormItem.get('productType').value) {

                if (concessionFormItem.get('productType').value.description == 'Notice deposit (BND)') {
                    applyexpirydate = true;
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

                if (applyexpirydate) {

                    this.addValidationError("Notice period value not entered");
                }
            }

            if (concessionFormItem.get('rate').value) {
                investmentConcessionDetail.loadedRate = concessionFormItem.get('rate').value;
            } else {

                this.addValidationError("Rate value not entered");
            }

            if (concessionFormItem.get('expiryDate').value && concessionFormItem.get('expiryDate').value != "") {
                investmentConcessionDetail.expiryDate = new Date(concessionFormItem.get('expiryDate').value);
            }
            else {
                if (!applyexpirydate) {
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

        for (let conditionFormItem of conditions.controls) {
            if (!investmentConcession.concessionConditions)
                investmentConcession.concessionConditions = [];

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

            investmentConcession.concessionConditions.push(concessionCondition);
        }

        return investmentConcession;
    }

    onSubmit() {

        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;

        var investmentConcession = this.getInvestmentConcession();

        investmentConcession.concession.concessionType = ConcessionTypes.Investment;
        investmentConcession.concession.type = "New";
        investmentConcession.concession.comments = "Created";

        if (!this.validationError) {
            this.investmentConcessionService.postNewInvestmentData(investmentConcession).subscribe(entity => {
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
            $event.target.value = this.baseComponentService.formatDecimalThree($event.target.value);
            //$event.target.value = new DecimalPipe('en-US').transform($event.target.value, '1.3-3');
        }
        else {

            $event.target.value = null;
        }
    }

    setZeroNumberDecimal($event) {

        if ($event.target.value) {
            $event.target.value = new DecimalPipe('en-US').transform($event.target.value, '1.0-0');
        }
        else {

            $event.target.value = null;
        }
    }

    //formatDecimal(itemValue: number) {
    //    if (itemValue) {
    //        return new DecimalPipe('en-US').transform(itemValue, '1.2-2');
    //    }

    //    return null;
    //}

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
