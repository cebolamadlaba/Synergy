import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
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
import { LendingConcession } from "../models/lending-concession";
import { Concession } from "../models/concession";
import { LendingConcessionDetail } from "../models/lending-concession-detail";
import { ConcessionCondition } from "../models/concession-condition";
import { Location } from '@angular/common';
import { LookupDataService } from "../services/lookup-data.service";
import { LendingService } from "../services/lending.service";

@Component({
    selector: 'app-lending-add-concession',
    templateUrl: './lending-add-concession.component.html',
    styleUrls: ['./lending-add-concession.component.css']
})
export class LendingAddConcessionComponent implements OnInit, OnDestroy {
    public lendingConcessionForm: FormGroup;
    private sub: any;
    errorMessage: String;
    validationError: String[];
    saveMessage: String;
    observableRiskGroup: Observable<RiskGroup>;
    riskGroup: RiskGroup;
    riskGroupNumber: number;
    observableLatestCrsOrMrs: Observable<number>;
    latestCrsOrMrs: number;
    selectedConditionTypes: ConditionType[];
    isLoading = true;

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
        @Inject(LendingService) private lendingService) {
        this.riskGroup = new RiskGroup();
        this.reviewFeeTypes = [new ReviewFeeType()];
        this.productTypes = [new ProductType()];
        this.periods = [new Period()];
        this.periodTypes = [new PeriodType()];
        this.conditionTypes = [new ConditionType()];
        this.selectedConditionTypes = [new ConditionType()];
        this.clientAccounts = [new ClientAccount()];
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
        });

        this.lendingConcessionForm = this.formBuilder.group({
            concessionItemRows: this.formBuilder.array([this.initConcessionItemRows()]),
            conditionItemsRows: this.formBuilder.array([]),
            mrsCrs: new FormControl(),
            smtDealNumber: new FormControl(),
            motivation: new FormControl()
        });

        Observable.forkJoin([
            this.lookupDataService.getReviewFeeTypes(),
            this.lookupDataService.getProductTypes("Lending"),
            this.lookupDataService.getPeriods(),
            this.lookupDataService.getPeriodTypes(),
            this.lookupDataService.getConditionTypes(),
            this.lookupDataService.getRiskGroup(this.riskGroupNumber),
            this.lookupDataService.getClientAccounts(this.riskGroupNumber),
            this.lendingService.getlatestCrsOrMrs(this.riskGroupNumber)
        ]).subscribe(results => {
            this.reviewFeeTypes = <any>results[0];
            this.productTypes = <any>results[1];
            this.periods = <any>results[2];
            this.periodTypes = <any>results[3];
            this.conditionTypes = <any>results[4];
            this.riskGroup = <any>results[5];
            this.clientAccounts = <any>results[6];
            this.latestCrsOrMrs = <any>results[7];

            this.isLoading = false;
        }, error => {
            this.errorMessage = <any>error;
            this.isLoading = false;
        });
    }

    initConcessionItemRows() {
        return this.formBuilder.group({
            productType: [''],
            accountNumber: [''],
            limit: [''],
            term: [''],
            marginAgainstPrime: [''],
            initiationFee: [''],
            reviewFeeType: [''],
            reviewFee: [''],
            uffFee: ['']
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
        const control = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];
        control.push(this.initConcessionItemRows());
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

        currentCondition.get('interestRate').setValue(null);
        currentCondition.get('volume').setValue(null);
        currentCondition.get('value').setValue(null);
        currentCondition.get('expectedTurnoverValue').setValue(null);
    }

    productTypeChanged(rowIndex) {
        const control = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];

        let currentRow = control.controls[rowIndex];
        var productType = currentRow.get('productType').value;

        if (productType.description === "Overdraft") {
            currentRow.get('term').disable();
            currentRow.get('term').setValue('12');
        } else {
            currentRow.get('term').enable();
        }
    }

    onSubmit() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;

        var lendingConcession = new LendingConcession();
        lendingConcession.concession = new Concession();

        if (this.lendingConcessionForm.controls['mrsCrs'].value)
            lendingConcession.concession.mrsCrs = this.lendingConcessionForm.controls['mrsCrs'].value;
        else
            this.addValidationError("MRS/CRS not captured");

        if (this.lendingConcessionForm.controls['smtDealNumber'].value)
            lendingConcession.concession.smtDealNumber = this.lendingConcessionForm.controls['smtDealNumber'].value;
        //else
        //    this.addValidationError("SMT Deal Number not captured");

        if (this.lendingConcessionForm.controls['motivation'].value)
            lendingConcession.concession.motivation = this.lendingConcessionForm.controls['motivation'].value;
        else
            this.addValidationError("Motivation not captured");

        lendingConcession.concession.riskGroupId = this.riskGroup.id;
        lendingConcession.concession.concessionType = "Lending";
        lendingConcession.concession.type = "New";

        const concessions = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];

        for (let concessionFormItem of concessions.controls) {
            if (!lendingConcession.lendingConcessionDetails)
                lendingConcession.lendingConcessionDetails = [];

            let lendingConcessionDetail = new LendingConcessionDetail();

            if (concessionFormItem.get('productType').value)
                lendingConcessionDetail.productTypeId = concessionFormItem.get('productType').value.id;
            else
                this.addValidationError("Product type not selected");

            if (concessionFormItem.get('accountNumber').value) {
                lendingConcessionDetail.legalEntityId = concessionFormItem.get('accountNumber').value.legalEntityId;
                lendingConcessionDetail.legalEntityAccountId = concessionFormItem.get('accountNumber').value.legalEntityAccountId;
            } else {
                this.addValidationError("Client account not selected");
            }

            if (concessionFormItem.get('limit').value)
                lendingConcessionDetail.limit = concessionFormItem.get('limit').value;

            if (concessionFormItem.get('term').value)
                lendingConcessionDetail.term = concessionFormItem.get('term').value;

            if (concessionFormItem.get('marginAgainstPrime').value)
                lendingConcessionDetail.marginAgainstPrime = concessionFormItem.get('marginAgainstPrime').value;

            if (concessionFormItem.get('initiationFee').value)
                lendingConcessionDetail.initiationFee = concessionFormItem.get('initiationFee').value;

            if (concessionFormItem.get('reviewFeeType').value)
                lendingConcessionDetail.reviewFeeTypeId = concessionFormItem.get('reviewFeeType').value.id;

            if (concessionFormItem.get('reviewFee').value)
                lendingConcessionDetail.reviewFee = concessionFormItem.get('reviewFee').value;

            if (concessionFormItem.get('uffFee').value)
                lendingConcessionDetail.uffFee = concessionFormItem.get('uffFee').value;

            lendingConcession.lendingConcessionDetails.push(lendingConcessionDetail);
        }

        const conditions = <FormArray>this.lendingConcessionForm.controls['conditionItemsRows'];

        for (let conditionFormItem of conditions.controls) {
            if (!lendingConcession.concessionConditions)
                lendingConcession.concessionConditions = [];

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

            lendingConcession.concessionConditions.push(concessionCondition);
        }

        if (!this.validationError) {
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

    addValidationError(validationDetail) {
        if (!this.validationError)
            this.validationError = [];

        this.validationError.push(validationDetail);
    }

    goBack() {
        this.location.back();
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}
