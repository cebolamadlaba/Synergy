import { Component, OnInit, Inject, OnDestroy, ViewChild } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { RiskGroup } from "../models/risk-group";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';

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
import { LegalEntity } from '../models/legal-entity';
import * as moment from 'moment';
import { MOnthEnum } from '../models/month-enum';
import { MrsEriEnum } from '../models/mrs-eri-enum';
import { ConcessionConditionReturnObject } from '../models/concession-condition-return-object';
import { ProductTypeEnum } from '../models/product-type-enum';
import { LendingConcessionTieredRate } from '../models/lending-concession-tiered-rate';
import { ProductTypeFieldLogic } from '../models/product-type-field-logic';

import { Location } from '@angular/common';
import { LookupDataService } from "../services/lookup-data.service";
import { LendingService } from "../services/lending.service";
import { DecimalPipe } from '@angular/common';
import { ConcessionTypes } from '../constants/concession-types';

import { BaseComponentService } from '../services/base-component.service';
import { LendingBaseService } from '../services/lending-base.service';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
    selector: 'app-lending-add-concession',
    templateUrl: './lending-add-concession.component.html',
    styleUrls: ['./lending-add-concession.component.css'],
    providers: [LendingBaseService]
})
export class LendingAddConcessionComponent extends LendingBaseService implements OnInit, OnDestroy {

    @ViewChild('tieredRateModal') tieredRateModal: ModalDirective;

    public lendingConcessionForm: FormGroup;
    private sub: any;
    showHide = false;
    errorMessage: String;
    saveMessage: String;
    observableRiskGroup: Observable<RiskGroup>;
    riskGroup: RiskGroup;
    riskGroupNumber: number;
    legalEntity: LegalEntity;
    sapbpid: number;

    observableLatestCrsOrMrs: Observable<number>;
    latestCrsOrMrs: number;
    selectedConditionTypes: ConditionType[];
    selectedProductTypes: ProductType[];
    selectedAccountNumbers: ClientAccountArray[];

    selectedRowIndex: number;
    selectedLineItemTieredRates: LendingConcessionTieredRate[] = [];
    selectedProductTypeFieldLogics: ProductTypeFieldLogic[] = [];
    tieredRateMessage: string = "";
    currentRowIndex: number;
    rollbackTieredRates: LendingConcessionTieredRate[];

    entityName: string;
    entityNumber: string;

    isLoading = true;
    isOverdraftProductType = false;

    primeRate = "0.00";
    today: string;

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

    constructor(
        private route: ActivatedRoute,
        private formBuilder: FormBuilder,
        private location: Location,
        @Inject(LookupDataService) private lookupDataService,
        @Inject(LendingService) private lendingService,
        private baseComponentService: BaseComponentService) {
        super();
        this.riskGroup = new RiskGroup();
        this.reviewFeeTypes = [new ReviewFeeType()];
        this.productTypes = [new ProductType()];
        this.selectedProductTypes = [new ProductType()];
        this.selectedAccountNumbers = [new ClientAccountArray()];
        this.periods = [new Period()];
        this.periodTypes = [new PeriodType()];
        this.conditionTypes = [new ConditionType()];
        this.selectedConditionTypes = [new ConditionType()];
        this.clientAccounts = [new ClientAccount()];
    }

    ngOnInit() {
        this.today = new Date().toISOString().split('T')[0];

        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
            this.sapbpid = +params['sapbpid'];
        });

        this.lendingConcessionForm = this.formBuilder.group({
            concessionItemRows: this.formBuilder.array([this.initConcessionItemRows()]),
            conditionItemsRows: this.formBuilder.array([]),
            mrsCrs: new FormControl(),
            smtDealNumber: new FormControl(),
            motivation: new FormControl(),
            prime: new FormControl()
        });

        this.getInitialData();
    }

    initConcessionItemRows() {

        this.selectedProductTypes.push(new ProductType());
        this.selectedAccountNumbers.push(new ClientAccountArray());
        this.selectedProductTypeFieldLogics.push(new ProductTypeFieldLogic());

        return this.formBuilder.group({
            productType: [''],
            accountNumber: [''],
            limit: [''],
            term: [{ value: '' }],
            marginAgainstPrime: [''],
            initiationFee: [''],
            reviewFeeType: [''],
            reviewFee: [''],
            uffFee: [''],
            frequency: [{ value: '', disabled: true }],
            serviceFee: [{ value: '', disabled: true }],
            mrsEri: [''],
            lendingTieredRates: [[]]
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

    getInitialData() {
        if (this.riskGroupNumber != null && this.riskGroupNumber != 0) {
            Observable.forkJoin([
                this.lookupDataService.getReviewFeeTypes(),
                this.lookupDataService.getProductTypes(ConcessionTypes.Lending),
                this.lookupDataService.getPeriods(),
                this.lookupDataService.getPeriodTypes(),
                this.lookupDataService.getConditionTypes(),
                this.lookupDataService.getRiskGroup(this.riskGroupNumber),
                this.lookupDataService.getClientAccountsConcessionType(this.riskGroupNumber, this.sapbpid, ConcessionTypes.Lending),
                this.lookupDataService.getPrimeRate(this.today),
                this.lendingService.getlatestCrsOrMrs(this.riskGroupNumber)
            ]).subscribe(results => {

                this.setInitialData(results, true);

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
        this.reviewFeeTypes = <any>results[0];
        this.productTypes = <any>results[1];
        this.periods = <any>results[2];
        this.periodTypes = <any>results[3];
        this.conditionTypes = <any>results[4];
        if (isForRiskGroup) {
            this.riskGroup = <any>results[5];
            this.latestCrsOrMrs = <any>results[8];
            this.entityName = this.riskGroup.name;
            this.entityNumber = this.riskGroup.number.toString();
        }
        else {
            this.legalEntity = <any>results[5];
            this.entityName = this.legalEntity.customerName;
            this.entityNumber = this.legalEntity.customerNumber;
        }
        this.clientAccounts = <any>results[6];
        this.primeRate = <string>results[7];


        this.isLoading = false;

        const control = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];
        if (this.productTypes) {
            control.controls[0].get('productType').setValue(this.productTypes[0]);

            this.selectedProductTypes[0] = this.productTypes[0];
            this.productTypeChanged(0);
        }

        if (this.clientAccounts)
            control.controls[0].get('accountNumber').setValue(this.clientAccounts[0]);
    }

    addNewConcessionRow() {
        const control = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];

        var newRow = this.initConcessionItemRows();

        if (this.productTypes)
            newRow.controls['productType'].setValue(this.productTypes[0]);

        if (this.clientAccounts)
            newRow.controls['accountNumber'].setValue(this.clientAccounts[0]);

        control.push(newRow);

        //this.selectedProductTypeFieldLogics.push(new ProductTypeFieldLogic());

        this.productTypeChanged(control.controls.length - 1)
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
            const control = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];

            this.selectedProductTypes.splice(index, 1);
            this.selectedAccountNumbers.splice(index, 1);
            this.selectedProductTypeFieldLogics.splice(index, 1);

            control.removeAt(index);
        }
    }

    deleteConditionRow(index: number) {
        const control = <FormArray>this.lendingConcessionForm.controls['conditionItemsRows'];
        control.removeAt(index);

        this.selectedConditionTypes.splice(index, 1);

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

    disableRows() {

        const concessions = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];
        for (let concessionFormItem of concessions.controls) {

            concessionFormItem.disable();
        }
    }

    productTypeChanged(rowIndex: number) {
        const control = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];

        let currentRow = control.controls[rowIndex];
        var productType = currentRow.get('productType').value;

        this.selectedProductTypes[rowIndex] = productType;

        if (this.clientAccounts && this.clientAccounts.length > 0) {
            this.selectedAccountNumbers[rowIndex].clientaccounts = this.clientAccounts.filter(re => re.accountType == productType.description);

            if (this.selectedAccountNumbers[rowIndex].clientaccounts.length == 0) {
                control.controls[rowIndex].get('accountNumber').setValue(null);
            }
            else {
                control.controls[rowIndex].get('accountNumber').setValue(this.selectedAccountNumbers[rowIndex].clientaccounts[0]);
            }
        }

        this.selectedProductTypeFieldLogics[rowIndex] = super.setProductTypeFieldLogic(productType.description, this.selectedProductTypeFieldLogics[rowIndex]);

        if (productType.description === ProductTypeEnum.Overdraft) {
            currentRow.get('term').setValue('12');
        }

        if (productType.description === ProductTypeEnum.TemporaryOverdraft) {
            currentRow.get('term').setValue('3');
            currentRow.get('term').disable;
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

    onSubmit() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;

        var lendingConcession = new LendingConcession();
        lendingConcession.concession = new Concession();

        if (this.lendingConcessionForm.controls['smtDealNumber'].value)
            lendingConcession.concession.smtDealNumber = this.lendingConcessionForm.controls['smtDealNumber'].value;
        else
            this.addValidationError("SMT Deal Number not captured");

        if (this.lendingConcessionForm.controls['motivation'].value)
            lendingConcession.concession.motivation = this.lendingConcessionForm.controls['motivation'].value;
        else
            lendingConcession.concession.motivation = '.';

        if (this.legalEntity)
            lendingConcession.concession.legalEntityId = this.legalEntity.id;
        if (this.riskGroup)
            lendingConcession.concession.riskGroupId = this.riskGroup.id;

        lendingConcession.concession.concessionType = ConcessionTypes.Lending;
        lendingConcession.concession.type = "New";
        lendingConcession.concession.comments = "Created";

        const concessions = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];

        let hasProductType: boolean = false;
        let hasLegalEntityId: boolean = false;
        let hasLegalEntityAccountId: boolean = false;
        let isOverdraftOrTempOverdraft: boolean = false;

        for (let concessionFormItem of concessions.controls) {
            if (!lendingConcession.lendingConcessionDetails)
                lendingConcession.lendingConcessionDetails = [];

            let lendingConcessionDetail = new LendingConcessionDetail();

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
                hasProductType = true;
            }

            if (concessions.controls.length == 1
                && concessionFormItem.get('productType').value.description == ProductTypeEnum.TemporaryOverdraft) {
                if (concessionFormItem.get('term').value != MOnthEnum.ThreeMonths) {
                    this.addValidationError("Term captured for Temporary Overdraft should be 3 months");
                }
            }

            if (concessionFormItem.get('accountNumber').value) {
                lendingConcessionDetail.legalEntityId = concessionFormItem.get('accountNumber').value.legalEntityId;
                lendingConcessionDetail.legalEntityAccountId = concessionFormItem.get('accountNumber').value.legalEntityAccountId;
                hasLegalEntityId = true;
                hasLegalEntityAccountId = true;
            } else {
                this.addValidationError("Client account not selected");
            }

            if (concessionFormItem.get('productType').value.description === ProductTypeEnum.Overdraft ||
                concessionFormItem.get('productType').value.description === ProductTypeEnum.TemporaryOverdraft) {

                if (concessionFormItem.get('term').value == "") {
                    this.addValidationError("Term cannot be empty");
                }

                if (concessionFormItem.get('reviewFeeType').value == "") {
                    this.addValidationError("Review Fee Type cannot be empty");
                }

                if (concessionFormItem.get('reviewFee').value == "") {
                    this.addValidationError("Review Fee cannot be empty");
                }

                if (concessionFormItem.get('uffFee').value == "") {
                    this.addValidationError("UffFee cannot be empty");
                }
            }
            else if (
                concessionFormItem.get('productType').value.description === ProductTypeEnum.MTL ||
                concessionFormItem.get('productType').value.description === "Agricultural Production Loan" ||
                concessionFormItem.get('productType').value.description === ProductTypeEnum.RCP ||
                concessionFormItem.get('productType').value.description === ProductTypeEnum.BTL) {

                if (concessionFormItem.get('term').value == "") {
                    this.addValidationError("Term cannot be empty");
                }

            } else if (concessionFormItem.get('productType').value.description === "VAF Installment sale" ||
                concessionFormItem.get('productType').value.description === "VAF Full Maintenance Lease" ||
                concessionFormItem.get('productType').value.description === "VAF operating rental") {

                if (concessionFormItem.get('term').value == "") {
                    this.addValidationError("Term cannot be empty");
                }

                if (concessionFormItem.get('serviceFee').value == "") {
                    this.addValidationError("Service Fee cannot be empty");
                }

                if (concessionFormItem.get('frequency').value == "") {
                    this.addValidationError("Frequency cannot be empty");
                }
            }

            // validate for all.


            if (concessionFormItem.get('initiationFee').value == "") {
                this.addValidationError("Initiation Fee cannot be empty");
            }

            if (concessionFormItem.get('mrsEri').value == "" ||
                (<string>concessionFormItem.get('mrsEri').value).trim() == "." ||
                (<string>concessionFormItem.get('mrsEri').value).split(".").length > 1) {
                this.addValidationError("MRS/ERI cannot be empty or a decimal");

            } else {
                var mrsEriValue = parseInt(concessionFormItem.get('mrsEri').value, 10);
                if (mrsEriValue < MrsEriEnum.MinMrsEri || mrsEriValue > MrsEriEnum.MaxMrsEri) {
                    this.addValidationError("MRS/ERI numbers must from 10 to 25");
                };
            }

            if (isOverdraftOrTempOverdraft) {
                if (lendingConcessionDetail.lendingConcessionDetailTieredRates == null)
                    lendingConcessionDetail.lendingConcessionDetailTieredRates = [];

                lendingConcessionDetail.lendingConcessionDetailTieredRates = concessionFormItem.get('lendingTieredRates').value;

                if (lendingConcessionDetail.lendingConcessionDetailTieredRates == null ||
                    lendingConcessionDetail.lendingConcessionDetailTieredRates.length == 0)
                    this.addValidationError("Tiered Rate cannot be empty for Product Type: Overdraft / Temporary Overdraft");
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
            }

            if (concessionFormItem.get('term').value)
                if (concessionFormItem.get('term').value < MOnthEnum.ThreeMonths) {
                    this.addValidationError("Minimum term captured should be 3 months");
                } else {
                    lendingConcessionDetail.term = concessionFormItem.get('term').value;
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

            if (concessionFormItem.get('mrsEri').value)
                lendingConcessionDetail.mrsEri = concessionFormItem.get('mrsEri').value;

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

        if (!this.validationError) {
            this.disableRows();
            this.lendingService.postNewLendingData(lendingConcession).subscribe(entity => {
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

    onTermValueChange(rowIndex) {
        this.errorMessage = null;
        this.validationError = null;

        const control = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];
        let term = control.controls[rowIndex].get('term').value;

        if (term < MOnthEnum.ThreeMonths) {
            this.addValidationError("Minimum term captured should be 3 months");
        };
    }

    setTwoNumberDecimal($event) {
        $event.target.value = this.baseComponentService.formatDecimal($event.target.value);
    }

    setTwoNumberDecimalMAP($event) {
        $event.target.value = this.baseComponentService.formatDecimalThree($event.target.value);
    }

    setThreeNumberDecimal($event) {
        $event.target.value = this.baseComponentService.formatDecimalThree($event.target.value);
    }

    canSaveMessage() {
        return this.saveMessage ? '' : null;
    }

    getNumberInput(input) {
        this.lendingConcessionForm.controls['smtDealNumber'].setValue(this.baseComponentService.removeLetters(input.value));
    }

    disableField(index: number, fieldname: string) {
        return super.disableFieldBase(
            this.selectedConditionTypes[index],
            new LendingConcessionDetail(),
            this.selectedProductTypeFieldLogics[index],
            fieldname,
            this.saveMessage == null,
            this.saveMessage != null
        );
    }

    goBack() {
        this.location.back();
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
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

}
