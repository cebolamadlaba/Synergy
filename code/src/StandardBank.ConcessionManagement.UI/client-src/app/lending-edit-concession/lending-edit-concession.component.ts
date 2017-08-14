import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { RiskGroupService } from "../risk-group/risk-group.service";
import { RiskGroup } from "../models/risk-group";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ReviewFeeType } from "../models/review-fee-type";
import { ProductType } from "../models/product-type";
import { ReviewFeeTypeService } from "../review-fee-type/review-fee-type.service";
import { ProductTypeService } from "../product-type/product-type.service";
import { PeriodService } from "../period/period.service";
import { PeriodTypeService } from "../period-type/period-type.service";
import { Period } from "../models/period";
import { PeriodType } from "../models/period-type";
import { ConditionTypeService } from "../condition-type/condition-type.service";
import { ConditionType } from "../models/condition-type";
import { ConditionProduct } from "../models/condition-product";
import { ClientAccountService } from "../client-account/client-account.service";
import { ClientAccount } from "../models/client-account";
import { LendingConcession } from "../models/lending-concession";
import { LendingNewService } from "../lending-new/lending-new.service";
import { Concession } from "../models/concession";
import { LendingConcessionDetail } from "../models/lending-concession-detail";
import { ConcessionCondition } from "../models/concession-condition";
import { LendingService } from "../lending/lending.service";

@Component({
    selector: 'app-lending-edit-concession',
    templateUrl: './lending-edit-concession.component.html',
    styleUrls: ['./lending-edit-concession.component.css']
})

export class LendingEditConcessionComponent implements OnInit, OnDestroy {
    concessionReferenceId: string;
    public lendingConcessionForm: FormGroup;
    private sub: any;
    errorMessage: String;
    validationError: String[];
    saveMessage: String;
    riskGroupNumber: number;
    selectedConditionTypes: ConditionType[];
    isLoading = false;

    observableRiskGroup: Observable<RiskGroup>;
    riskGroup: RiskGroup;

    observableLendingConcession: Observable<LendingConcession>;
    lendingConcession: LendingConcession;

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
        @Inject(RiskGroupService) private riskGroupService,
        @Inject(ReviewFeeTypeService) private reviewFeeTypeService,
        @Inject(ProductTypeService) private productTypeService,
        @Inject(PeriodService) private periodService,
        @Inject(PeriodTypeService) private periodTypeService,
        @Inject(ConditionTypeService) private conditionTypeService,
        @Inject(ClientAccountService) private clientAccountService,
        @Inject(LendingNewService) private lendingNewService,
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
            this.concessionReferenceId = params['concessionReferenceId'];

            if (this.riskGroupNumber) {
                this.observableRiskGroup = this.riskGroupService.getData(this.riskGroupNumber);
                this.observableRiskGroup.subscribe(riskGroup => this.riskGroup = riskGroup, error => this.errorMessage = <any>error);

                this.observableClientAccounts = this.clientAccountService.getData(this.riskGroupNumber);
                this.observableClientAccounts.subscribe(clientAccounts => this.clientAccounts = clientAccounts, error => this.errorMessage = <any>error);

                this.observableLendingConcession = this.lendingService.getData(this.concessionReferenceId);
                this.observableLendingConcession.subscribe(lendingConcession => {
                    this.lendingConcession = lendingConcession;
                    this.lendingConcessionForm.controls['mrsCrs'].setValue(this.lendingConcession.concession.mrsCrs);
                    this.lendingConcessionForm.controls['smtDealNumber'].setValue(this.lendingConcession.concession.smtDealNumber);
                    this.lendingConcessionForm.controls['motivation'].setValue(this.lendingConcession.concession.motivation);

                    let rowIndex = 0;

                    for (let lendingConcessionDetail of this.lendingConcession.lendingConcessionDetails) {

                        if (rowIndex != 0) {
                            this.addNewConcessionRow();
                        }

                        //const concessions = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];
                        //let currentConcession = concessions[concessions.length - 1];

                        //currentConcession.get('productType').setValue(lendingConcessionDetail.productType);

                        rowIndex++;
                    }

                    for (let concessionCondition of this.lendingConcession.concessionConditions) {
                        this.addNewConditionRow();
                    }

                }, error => this.errorMessage = <any>error);
            }
        });

        this.lendingConcessionForm = this.formBuilder.group({
            concessionItemRows: this.formBuilder.array([this.initConcessionItemRows()]),
            conditionItemsRows: this.formBuilder.array([]),
            mrsCrs: new FormControl(),
            smtDealNumber: new FormControl(),
            motivation: new FormControl()
        });

        this.observableReviewFeeTypes = this.reviewFeeTypeService.getData();
        this.observableReviewFeeTypes.subscribe(reviewFeeTypes => this.reviewFeeTypes = reviewFeeTypes, error => this.errorMessage = <any>error);

        this.observableProductTypes = this.productTypeService.getData("Lending");
        this.observableProductTypes.subscribe(productTypes => this.productTypes = productTypes, error => this.errorMessage = <any>error);

        this.observablePeriods = this.periodService.getData();
        this.observablePeriods.subscribe(periods => this.periods = periods, error => this.errorMessage = <any>error);

        this.observablePeriodTypes = this.periodTypeService.getData();
        this.observablePeriodTypes.subscribe(periodTypes => this.periodTypes = periodTypes, error => this.errorMessage = <any>error);

        this.observableConditionTypes = this.conditionTypeService.getData();
        this.observableConditionTypes.subscribe(conditionTypes => this.conditionTypes = conditionTypes, error => this.errorMessage = <any>error);
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
        const control = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];
        control.removeAt(index);
    }

    deleteConditionRow(index: number) {
        const control = <FormArray>this.lendingConcessionForm.controls['conditionItemsRows'];
        control.removeAt(index);

        this.selectedConditionTypes.splice(index, 1);
    }

    conditionTypeChanged(rowIndex) {
        const control = <FormArray>this.lendingConcessionForm.controls['conditionItemsRows'];
        this.selectedConditionTypes[rowIndex] = control.controls[rowIndex].get('conditionType').value;
    }

    onSubmit() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;

        var lendingConcession = new LendingConcession();
        lendingConcession.concession = new Concession();
        lendingConcession.concession.referenceNumber = this.concessionReferenceId;

        if (this.lendingConcessionForm.controls['mrsCrs'].value)
            lendingConcession.concession.mrsCrs = this.lendingConcessionForm.controls['mrsCrs'].value;
        else
            this.addValidationError("MRS/CRS not captured");

        if (this.lendingConcessionForm.controls['smtDealNumber'].value)
            lendingConcession.concession.smtDealNumber = this.lendingConcessionForm.controls['smtDealNumber'].value;
        else
            this.addValidationError("SMT Deal Number not captured");

        if (this.lendingConcessionForm.controls['motivation'].value)
            lendingConcession.concession.motivation = this.lendingConcessionForm.controls['motivation'].value;
        else
            this.addValidationError("Motivation not captured");

        lendingConcession.concession.riskGroupId = this.riskGroup.id;
        lendingConcession.concession.concessionType = "Lending";
        lendingConcession.concession.type = "Existing";

        const concessions = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];

        for (let concessionFormItem of concessions.controls) {
            if (!lendingConcession.lendingConcessionDetails)
                lendingConcession.lendingConcessionDetails = [];

            let lendingConcessionDetail = new LendingConcessionDetail();

            if (concessionFormItem.get('productType').value)
                lendingConcessionDetail.productTypeId = concessionFormItem.get('productType').value.id;
            else
                this.addValidationError("Product type not selected");

            if (concessionFormItem.get('accountNumber').value)
                lendingConcessionDetail.legalEntityId = concessionFormItem.get('accountNumber').value.legalEntityId;
            else
                this.addValidationError("Client account not selected");

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

            if (conditionFormItem.get('periodType').value)
                concessionCondition.periodTypeId = conditionFormItem.get('periodType').value.id;

            if (conditionFormItem.get('period').value)
                concessionCondition.periodId = conditionFormItem.get('period').value.id;

            lendingConcession.concessionConditions.push(concessionCondition);
        }

        if (!this.validationError) {
            this.lendingNewService.postData(lendingConcession).subscribe(entity => {
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

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

}
