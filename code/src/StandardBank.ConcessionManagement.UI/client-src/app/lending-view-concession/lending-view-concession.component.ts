import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
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
import { LendingService } from "../services/lending.service";
import { Location } from '@angular/common';
import { LookupDataService } from "../services/lookup-data.service";

@Component({
  selector: 'app-lending-view-concession',
  templateUrl: './lending-view-concession.component.html',
  styleUrls: ['./lending-view-concession.component.css']
})
export class LendingViewConcessionComponent implements OnInit, OnDestroy {

    concessionReferenceId: string;
    public lendingConcessionForm: FormGroup;
    private sub: any;
    errorMessage: String;
    validationError: String[];
    saveMessage: String;
    riskGroupNumber: number;
    selectedConditionTypes: ConditionType[];
    isLoading = false;
    canBcmApprove = false;
    canPcmApprove = false;
    hasChanges = false;

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
        private router: Router,
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
            this.concessionReferenceId = params['concessionReferenceId'];

            if (this.riskGroupNumber) {
                this.observableRiskGroup = this.lookupDataService.getRiskGroup(this.riskGroupNumber);
                this.observableRiskGroup.subscribe(riskGroup => this.riskGroup = riskGroup, error => this.errorMessage = <any>error);

                this.observableClientAccounts = this.lookupDataService.getClientAccounts(this.riskGroupNumber);
                this.observableClientAccounts.subscribe(clientAccounts => this.clientAccounts = clientAccounts, error => this.errorMessage = <any>error);
            }
        });

        this.lendingConcessionForm = this.formBuilder.group({
            concessionItemRows: this.formBuilder.array([this.initConcessionItemRows()]),
            conditionItemsRows: this.formBuilder.array([]),
            mrsCrs: new FormControl(),
            smtDealNumber: new FormControl(),
            motivation: new FormControl(),
            comments: new FormControl()
        });

        this.observableReviewFeeTypes = this.lookupDataService.getReviewFeeTypes();
        this.observableReviewFeeTypes.subscribe(reviewFeeTypes => this.reviewFeeTypes = reviewFeeTypes, error => this.errorMessage = <any>error);

        this.observableProductTypes = this.lookupDataService.getProductTypes("Lending");
        this.observableProductTypes.subscribe(productTypes => this.productTypes = productTypes, error => this.errorMessage = <any>error);

        this.observablePeriods = this.lookupDataService.getPeriods();
        this.observablePeriods.subscribe(periods => this.periods = periods, error => this.errorMessage = <any>error);

        this.observablePeriodTypes = this.lookupDataService.getPeriodTypes();
        this.observablePeriodTypes.subscribe(periodTypes => this.periodTypes = periodTypes, error => this.errorMessage = <any>error);

        this.observableConditionTypes = this.lookupDataService.getConditionTypes();
        this.observableConditionTypes.subscribe(conditionTypes => this.conditionTypes = conditionTypes, error => this.errorMessage = <any>error);

        this.lendingConcessionForm.valueChanges.subscribe((value: any) => {
            if (this.lendingConcessionForm.dirty) {
                this.hasChanges = true;
            }
        });

        if (this.concessionReferenceId) {
            this.observableLendingConcession = this.lendingService.getLendingConcessionData(this.concessionReferenceId);
            this.observableLendingConcession.subscribe(lendingConcession => {
                this.lendingConcession = lendingConcession;

                if (lendingConcession.concession.status == "Pending" && lendingConcession.concession.subStatus == "BCM Pending") {
                    this.canBcmApprove = lendingConcession.currentUser.canBcmApprove;
                }

                if (lendingConcession.concession.status == "Pending" && lendingConcession.concession.subStatus == "PCM Pending") {
                    this.canPcmApprove = lendingConcession.currentUser.canPcmApprove;
                }

                this.lendingConcessionForm.controls['mrsCrs'].setValue(this.lendingConcession.concession.mrsCrs);
                this.lendingConcessionForm.controls['smtDealNumber'].setValue(this.lendingConcession.concession.smtDealNumber);
                this.lendingConcessionForm.controls['motivation'].setValue(this.lendingConcession.concession.motivation);

                let rowIndex = 0;

                for (let lendingConcessionDetail of this.lendingConcession.lendingConcessionDetails) {

                    if (rowIndex != 0) {
                        this.addNewConcessionRow();
                    }

                    const concessions = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];
                    let currentConcession = concessions.controls[concessions.length - 1];

                    let selectedProductType = this.productTypes.filter(_ => _.id === lendingConcessionDetail.productTypeId);
                    currentConcession.get('productType').setValue(selectedProductType[0]);

                    let selectedAccountNo = this.clientAccounts.filter(_ => _.legalEntityAccountId == lendingConcessionDetail.legalEntityAccountId);
                    currentConcession.get('accountNumber').setValue(selectedAccountNo[0]);

                    currentConcession.get('limit').setValue(lendingConcessionDetail.limit);
                    currentConcession.get('term').setValue(lendingConcessionDetail.term);
                    currentConcession.get('marginAgainstPrime').setValue(lendingConcessionDetail.marginAgainstPrime);
                    currentConcession.get('initiationFee').setValue(lendingConcessionDetail.initiationFee);

                    let selectedReviewFeeType = this.reviewFeeTypes.filter(_ => _.id == lendingConcessionDetail.reviewFeeTypeId);
                    currentConcession.get('reviewFeeType').setValue(selectedReviewFeeType[0]);

                    currentConcession.get('reviewFee').setValue(lendingConcessionDetail.reviewFee);
                    currentConcession.get('uffFee').setValue(lendingConcessionDetail.uffFee);

                    rowIndex++;
                }

                rowIndex = 0;

                for (let concessionCondition of this.lendingConcession.concessionConditions) {
                    this.addNewConditionRow();

                    const conditions = <FormArray>this.lendingConcessionForm.controls['conditionItemsRows'];
                    let currentCondition = conditions.controls[conditions.length - 1];

                    let selectedConditionType = this.conditionTypes.filter(_ => _.id == concessionCondition.conditionTypeId);
                    currentCondition.get('conditionType').setValue(selectedConditionType[0]);

                    this.selectedConditionTypes[rowIndex] = selectedConditionType[0];

                    let selectedConditionProduct = selectedConditionType[0].conditionProducts.filter(_ => _.id == concessionCondition.conditionProductId);
                    currentCondition.get('conditionProduct').setValue(selectedConditionProduct[0]);

                    currentCondition.get('interestRate').setValue(concessionCondition.interestRate);
                    currentCondition.get('volume').setValue(concessionCondition.conditionVolume);
                    currentCondition.get('value').setValue(concessionCondition.conditionValue);

                    let selectedPeriodType = this.periodTypes.filter(_ => _.id == concessionCondition.periodTypeId);
                    currentCondition.get('periodType').setValue(selectedPeriodType[0]);

                    let selectedPeriod = this.periods.filter(_ => _.id == concessionCondition.periodId);
                    currentCondition.get('period').setValue(selectedPeriod[0]);

                    rowIndex++;
                }

            }, error => this.errorMessage = <any>error);
        }
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

        var lendingConcession = this.getLendingConcession();
        lendingConcession.concession.concessionType = "Lending";
        lendingConcession.concession.type = "Existing";

        if (!this.validationError) {
            this.lendingService.postUpdateLendingData(lendingConcession).subscribe(entity => {
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

    getLendingConcession(): LendingConcession {
        console.log("in the get lending concession method");
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

        if (this.lendingConcessionForm.controls['comments'].value)
            lendingConcession.concession.comments = this.lendingConcessionForm.controls['comments'].value;

        lendingConcession.concession.riskGroupId = this.riskGroup.id;

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

            if (conditionFormItem.get('periodType').value)
                concessionCondition.periodTypeId = conditionFormItem.get('periodType').value.id;

            if (conditionFormItem.get('period').value)
                concessionCondition.periodId = conditionFormItem.get('period').value.id;

            lendingConcession.concessionConditions.push(concessionCondition);
        }

        return lendingConcession;
    }

    bcmApproveConcession() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;

        var lendingConcession = this.getLendingConcession();
        lendingConcession.concession.subStatus = "PCM Pending";
        lendingConcession.concession.bcmUserId = this.lendingConcession.currentUser.id;

        if (!this.validationError) {
            this.lendingService.postUpdateLendingData(lendingConcession).subscribe(entity => {
                console.log("data saved");
                this.canBcmApprove = false;
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

    bcmDeclineConcession() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;

        var lendingConcession = this.getLendingConcession();
        lendingConcession.concession.status = "Declined";
        lendingConcession.concession.subStatus = "BCM Declined";
        lendingConcession.concession.bcmUserId = this.lendingConcession.currentUser.id;

        if (!this.validationError) {
            this.lendingService.postUpdateLendingData(lendingConcession).subscribe(entity => {
                console.log("data saved");
                this.canBcmApprove = false;
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

    pcmApproveConcession() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;

        var lendingConcession = this.getLendingConcession();

        lendingConcession.concession.status = "Approved";

        if (this.lendingConcession.currentUser.isHO) {
            lendingConcession.concession.subStatus = "HO Approved";
            lendingConcession.concession.hoUserId = this.lendingConcession.currentUser.id;
        } else {
            lendingConcession.concession.subStatus = "PCM Approved";
            lendingConcession.concession.pcmUserId = this.lendingConcession.currentUser.id;
        }

        if (!this.validationError) {
            this.lendingService.postUpdateLendingData(lendingConcession).subscribe(entity => {
                console.log("data saved");
                this.canPcmApprove = false;
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

    pcmDeclineConcession() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;

        var lendingConcession = this.getLendingConcession();

        lendingConcession.concession.status = "Declined";

        if (this.lendingConcession.currentUser.isHO) {
            lendingConcession.concession.subStatus = "HO Declined";
            lendingConcession.concession.hoUserId = this.lendingConcession.currentUser.id;
        } else {
            lendingConcession.concession.subStatus = "PCM Declined";
            lendingConcession.concession.pcmUserId = this.lendingConcession.currentUser.id;
        }

        if (!this.validationError) {
            this.lendingService.postUpdateLendingData(lendingConcession).subscribe(entity => {
                console.log("data saved");
                this.canPcmApprove = false;
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

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

}
