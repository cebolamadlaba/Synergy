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

@Component({
    selector: 'app-lending-add-concession',
    templateUrl: './lending-add-concession.component.html',
    styleUrls: ['./lending-add-concession.component.css']
})
export class LendingAddConcessionComponent implements OnInit, OnDestroy {
    public lendingConcessionForm: FormGroup;
    private sub: any;
    errorMessage: String;
    observableRiskGroup: Observable<RiskGroup>;
    riskGroup: RiskGroup;
    riskGroupNumber: number;
    selectedConditionTypes: ConditionType[];

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
        @Inject(ClientAccountService) private clientAccountService) {
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

            if (this.riskGroupNumber) {
                this.observableRiskGroup = this.riskGroupService.getData(this.riskGroupNumber);
                this.observableRiskGroup.subscribe(riskGroup => this.riskGroup = riskGroup, error => this.errorMessage = <any>error);

                this.observableClientAccounts = this.clientAccountService.getData(this.riskGroupNumber);
                this.observableClientAccounts.subscribe(clientAccounts => this.clientAccounts = clientAccounts, error => this.errorMessage = <any>error);
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

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}
