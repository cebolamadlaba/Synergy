import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { RiskGroup } from "../models/risk-group";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { DecimalPipe, Location, DatePipe } from '@angular/common';

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
import { LendingFinancial } from "../models/lending-financial";
import { ConcessionComment } from "../models/concession-comment";
import { LegalEntity } from '../models/legal-entity';

import { LookupDataService } from "../services/lookup-data.service";
import { UserConcessionsService } from "../services/user-concessions.service";
import { LendingService } from "../services/lending.service";
import { ConcessionTypes } from '../constants/concession-types';
import { ConcessionStatus } from '../constants/concession-status';
import { ConcessionSubStatus } from '../constants/concession-sub-status';

import { BaseComponentService } from '../services/base-component.service';
import * as moment from 'moment';
import { MOnthEnum } from '../models/month-enum';
import { MrsEriEnum } from '../models/mrs-eri-enum';

@Component({
    selector: 'app-lending-view-concession',
    templateUrl: './lending-view-concession.component.html',
    styleUrls: ['./lending-view-concession.component.css'],
    providers: [DatePipe]
})
export class LendingViewConcessionComponent implements OnInit, OnDestroy {

    myDecimal: number;

    primeRate = "0.00";
    today: string;

    subHeading: string;
    title: string;

    concessionReferenceId: string;
    public lendingConcessionForm: FormGroup;
    private sub: any;
    showHide = false;
    errorMessage: String;
    validationError: String[];
    saveMessage: String;
    warningMessage: String;
    riskGroupNumber: number;
    sapbpid: number;
    selectedConditionTypes: ConditionType[];
    selectedProductTypes: ProductType[];
    isLoading = true;
    canBcmApprove = false;
    canPcmApprove = false;
    hasChanges = false;
    canExtend = false;
    canRenew = false;
    canRecall = false;
    isEditing = false;
    motivationEnabled = false;
    canEdit = false;
    selectedAccountNumbers: ClientAccountArray[];

    capturedComments: string;
    canApproveChanges: boolean;
    canResubmit = false;
    canUpdate = false;
    editType: string;
    canArchive = false;
    isInProgressExtension = false;
    isInProgressRenewal = false;
    isApproved = false;

    isRecalling = false;
    visiblearray: object[];

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
    clientAccountsCopy: ClientAccount[];

    observableLendingFinancial: Observable<LendingFinancial>;
    lendingFinancial: LendingFinancial;

    legalEntity: LegalEntity;

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private formBuilder: FormBuilder,
        private location: Location,
        private datepipe: DatePipe,
        @Inject(LookupDataService) private lookupDataService,
        @Inject(LendingService) private lendingService,
        @Inject(UserConcessionsService) private userConcessionsService,
        private baseComponentService: BaseComponentService) {
        this.riskGroup = new RiskGroup();
        this.reviewFeeTypes = [new ReviewFeeType()];
        this.productTypes = [new ProductType()];
        this.periods = [new Period()];
        this.periodTypes = [new PeriodType()];
        this.conditionTypes = [new ConditionType()];
        this.selectedConditionTypes = [new ConditionType()];
        this.selectedProductTypes = [new ProductType()];
        this.clientAccounts = [new ClientAccount()];
        this.clientAccountsCopy = [new ClientAccount()];
        this.lendingFinancial = new LendingFinancial();
        this.lendingConcession = new LendingConcession();
        this.lendingConcession.concession = new Concession();
        this.lendingConcession.concession.concessionComments = [new ConcessionComment()];
        this.selectedAccountNumbers = [new ClientAccountArray()];


    }

    ngOnInit() {
        this.myDecimal = -2.99;

        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
            this.sapbpid = +params['sapbpid'];
            this.concessionReferenceId = params['concessionReferenceId'];
        });

        this.lendingConcessionForm = this.formBuilder.group({
            concessionItemRows: this.formBuilder.array([this.initConcessionItemRows()]),
            conditionItemsRows: this.formBuilder.array([]),
            mrsCrs: new FormControl(),
            smtDealNumber: new FormControl(),
            motivation: new FormControl(),
            comments: new FormControl(),
            prime: new FormControl()

        });

        this.getInitialData();

        this.lendingConcessionForm.valueChanges.subscribe((value: any) => {
            if (this.lendingConcessionForm.dirty) {
                //if the captured comments is still the same as the comments that means
                //the user has changed something else on the form
                if (this.capturedComments == value.comments) {
                    this.hasChanges = true;
                }

                this.capturedComments = value.comments;
            }
        });
    }

    getvisible(productType: Number) {


        return true;
    }

    populateForm() {
        if (this.concessionReferenceId) {
            this.observableLendingConcession = this.lendingService.getLendingConcessionData(this.concessionReferenceId);
            this.observableLendingConcession.subscribe(lendingConcession => {
                this.lendingConcession = lendingConcession;


                if (lendingConcession.concession.status == ConcessionStatus.Pending && lendingConcession.concession.subStatus == ConcessionSubStatus.BCMPending) {
                    this.canBcmApprove = lendingConcession.currentUser.canBcmApprove;
                }

                if (lendingConcession.concession.status == ConcessionStatus.Pending && lendingConcession.concession.subStatus == ConcessionSubStatus.PCMPending) {
                    if (this.lendingConcession.currentUser.isHO) {
                        this.canPcmApprove = lendingConcession.currentUser.canPcmApprove
                    } else {
                        this.canPcmApprove = lendingConcession.currentUser.canPcmApprove && lendingConcession.currentUser.canApprove;
                    }

                    // Removed as per SBSA.Anthony's request - 2019-07-15
                    //if (!lendingConcession.concession.isInProgressExtension) {
                    this.canEdit = lendingConcession.currentUser.canPcmApprove;
                    //}
                }

                if (lendingConcession.primeRate) {

                    this.primeRate = lendingConcession.primeRate;
                }


                //if it's still pending and the user is a requestor then they can recall it
                if (lendingConcession.concession.status == ConcessionStatus.Pending && lendingConcession.concession.subStatus == ConcessionSubStatus.BCMPending) {
                    this.canRecall = lendingConcession.currentUser.canRequest && lendingConcession.concession.isAENumberLinkedAccountExecutiveOrAssistant;
                }

                if (lendingConcession.concession.status == ConcessionStatus.Pending &&
                    (lendingConcession.concession.subStatus == ConcessionSubStatus.PCMApprovedWithChanges || lendingConcession.concession.subStatus == ConcessionSubStatus.HOApprovedWithChanges)) {
                    this.canApproveChanges = lendingConcession.currentUser.canRequest && lendingConcession.concession.isAENumberLinkedAccountExecutiveOrAssistant;
                }

                if (lendingConcession.concession.status === ConcessionStatus.Approved ||
                    lendingConcession.concession.status === ConcessionStatus.ApprovedWithChanges) {
                    this.isApproved = true;
                }

                //if the concession is set to can extend and the user is a requestor, then they can extend or renew it
                this.canExtend = lendingConcession.concession.canExtend && lendingConcession.currentUser.canRequest;
                this.canRenew = lendingConcession.concession.canRenew && lendingConcession.currentUser.canRequest;

                //set the resubmit and update permissions
                this.canResubmit = lendingConcession.concession.canResubmit && lendingConcession.currentUser.canRequest;
                this.canUpdate = lendingConcession.concession.canUpdate && lendingConcession.currentUser.canRequest;

                this.canArchive = lendingConcession.concession.canArchive && lendingConcession.currentUser.canRequest;
                this.isInProgressExtension = lendingConcession.concession.isInProgressExtension;
                this.isInProgressRenewal = lendingConcession.concession.isInProgressRenewal;

                //this.lendingConcessionForm.controls['mrsCrs'].setValue(this.lendingConcession.concession.mrsCrs);
                this.lendingConcessionForm.controls['smtDealNumber'].setValue(this.lendingConcession.concession.smtDealNumber);
                this.lendingConcessionForm.controls['motivation'].setValue(this.lendingConcession.concession.motivation);

                let rowIndex = 0;

                for (let lendingConcessionDetail of this.lendingConcession.lendingConcessionDetails) {

                    if (rowIndex != 0) {
                        this.addNewConcessionRow();
                    }

                    if (lendingConcessionDetail.productType == 'Overdraft') {

                        lendingConcessionDetail.show_term = false;
                        lendingConcessionDetail.show_reviewFeeType = true;
                        lendingConcessionDetail.show_reviewFee = true;
                        lendingConcessionDetail.show_uffFee = true;
                        lendingConcessionDetail.show_frequency = false;
                        lendingConcessionDetail.show_serviceFee = false;

                    }
                    else if (lendingConcessionDetail.productType === "Temporary Overdraft") {


                        lendingConcessionDetail.show_term = true;
                        lendingConcessionDetail.show_reviewFeeType = true;
                        lendingConcessionDetail.show_reviewFee = true;
                        lendingConcessionDetail.show_uffFee = true;
                        lendingConcessionDetail.show_frequency = false;
                        lendingConcessionDetail.show_serviceFee = false;


                    }
                    else if (lendingConcessionDetail.productType.indexOf("VAF") == 0) {

                        lendingConcessionDetail.show_term = true;
                        lendingConcessionDetail.show_reviewFeeType = false;
                        lendingConcessionDetail.show_reviewFee = false;
                        lendingConcessionDetail.show_uffFee = false;
                        lendingConcessionDetail.show_frequency = true;
                        lendingConcessionDetail.show_serviceFee = true;
                    }
                    else {

                        lendingConcessionDetail.show_term = true;
                        lendingConcessionDetail.show_reviewFeeType = false;
                        lendingConcessionDetail.show_reviewFee = false;
                        lendingConcessionDetail.show_uffFee = false;
                        lendingConcessionDetail.show_frequency = false;
                        lendingConcessionDetail.show_serviceFee = false;
                    }


                    const concessions = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];
                    let currentConcession = concessions.controls[concessions.length - 1];

                    currentConcession.get('lendingConcessionDetailId').setValue(lendingConcessionDetail.lendingConcessionDetailId);
                    currentConcession.get('concessionDetailId').setValue(lendingConcessionDetail.concessionDetailId);

                    let selectedProductType = this.productTypes.filter(_ => _.id === lendingConcessionDetail.productTypeId);
                    currentConcession.get('productType').setValue(selectedProductType[0]);

                    this.selectedProductTypes[rowIndex] = selectedProductType[0];


                    if (this.clientAccounts) {
                        let selectedAccountNo = this.clientAccounts.filter(_ => _.legalEntityAccountId == lendingConcessionDetail.legalEntityAccountId);
                        currentConcession.get('accountNumber').setValue(selectedAccountNo[0]);
                    }

                    currentConcession.get('limit').setValue(this.formatDecimal(lendingConcessionDetail.limit));
                    currentConcession.get('term').setValue(lendingConcessionDetail.term);
                    currentConcession.get('marginAgainstPrime').setValue(this.formatDecimal4(lendingConcessionDetail.marginAgainstPrime));
                    currentConcession.get('approvedMarginAgainstPrime').setValue(this.formatDecimal4(lendingConcessionDetail.approvedMap));
                    currentConcession.get('initiationFee').setValue(this.formatDecimal4(lendingConcessionDetail.initiationFee));

                    let selectedReviewFeeType = this.reviewFeeTypes.filter(_ => _.id == lendingConcessionDetail.reviewFeeTypeId);
                    currentConcession.get('reviewFeeType').setValue(selectedReviewFeeType[0]);
                    currentConcession.get('reviewFee').setValue(this.formatDecimal3(lendingConcessionDetail.reviewFee));
                    currentConcession.get('uffFee').setValue(this.formatDecimal3(lendingConcessionDetail.uffFee));

                    currentConcession.get('mrsEri').setValue(lendingConcessionDetail.mrsEri);

                    currentConcession.get('serviceFee').setValue(this.formatDecimal3(lendingConcessionDetail.serviceFee));
                    currentConcession.get('frequency').setValue(lendingConcessionDetail.frequency);

                    if (lendingConcessionDetail.expiryDate) {
                        var formattedExpiryDate = this.datepipe.transform(lendingConcessionDetail.expiryDate, 'yyyy-MM-dd');
                        currentConcession.get('expiryDate').setValue(formattedExpiryDate);
                    }

                    if (lendingConcessionDetail.dateApproved) {
                        var formattedDateApproved = this.datepipe.transform(lendingConcessionDetail.dateApproved, 'yyyy-MM-dd');
                        currentConcession.get('dateApproved').setValue(formattedDateApproved);
                    }

                    currentConcession.get('isExpired').setValue(lendingConcessionDetail.isExpired);
                    currentConcession.get('isExpiring').setValue(lendingConcessionDetail.isExpiring);

                    rowIndex++;
                }

                rowIndex = 0;

                for (let concessionCondition of this.lendingConcession.concessionConditions) {
                    this.addNewConditionRow();

                    const conditions = <FormArray>this.lendingConcessionForm.controls['conditionItemsRows'];
                    let currentCondition = conditions.controls[conditions.length - 1];

                    currentCondition.get('concessionConditionId').setValue(concessionCondition.concessionConditionId);

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

                this.changearray = this.lookupDataService.checkforLC(this.lendingConcession.concession.status, this.lendingConcession.concession.subStatus, lendingConcession.concession.concessionComments);

                this.isLoading = false;
            }, error => {
                this.isLoading = false;
                this.errorMessage = <any>error;
            });
        }
    }

    initConcessionItemRows() {
        this.selectedProductTypes.push(new ProductType());
        this.selectedAccountNumbers.push(new ClientAccountArray());
        return this.formBuilder.group({
            lendingConcessionDetailId: [''],
            concessionDetailId: [''],
            productType: [''],
            accountNumber: [''],
            limit: [''],
            term: [''],
            marginAgainstPrime: [''],
            approvedMarginAgainstPrime: [{ value: '', disabled: true }],
            initiationFee: [''],
            reviewFeeType: [''],
            reviewFee: [''],
            uffFee: [''],
            expiryDate: [{ value: '', disabled: true }],
            dateApproved: [{ value: '', disabled: true }],
            isExpired: [''],
            isExpiring: [''],
            frequency: [{ value: '', disabled: true }],
            serviceFee: [{ value: '', disabled: true }],
            mrsEri: [''],
        });
    }

    initConditionItemRows() {
        this.selectedConditionTypes.push(new ConditionType());

        return this.formBuilder.group({
            concessionConditionId: [''],
            conditionType: [''],
            conditionProduct: [''],
            interestRate: [''],
            volume: [''],
            value: [''],
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
                this.lookupDataService.getClientAccountsConcessionType(this.riskGroupNumber, ConcessionTypes.Lending),
                this.lendingService.getLendingFinancial(this.riskGroupNumber),
            ]).subscribe(results => {

                this.setInitialData(results, true);
                this.populateForm();

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
            ]).subscribe(results => {

                this.setInitialData(results, false);
                this.populateForm();

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
            this.lendingFinancial = <any>results[7];
            this.subHeading = this.riskGroup.name;
            this.title = this.riskGroup.number.toString();
        }
        else {
            this.legalEntity = <any>results[5];
            this.subHeading = this.legalEntity.customerName;
            this.title = this.legalEntity.customerNumber;
        }

        this.clientAccounts = <any>results[6];
        this.clientAccountsCopy = <any>results[6]
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

            this.selectedProductTypes.splice(index, 1);
        }
    }

    deleteConditionRow(index: number) {
        const control = <FormArray>this.lendingConcessionForm.controls['conditionItemsRows'];
        control.removeAt(index);

        this.selectedConditionTypes.splice(index, 1);
    }

    onExpiryDateChanged(itemrow) {
        this.validationError = null;
        var validationErrorMessage = this.baseComponentService.expiringDateDifferenceValidation(itemrow.controls['expiryDate'].value);
        if (validationErrorMessage != null) {
            this.addValidationError(validationErrorMessage);
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

    conditionTypeChanged(rowIndex) {
        const control = <FormArray>this.lendingConcessionForm.controls['conditionItemsRows'];
        this.selectedConditionTypes[rowIndex] = control.controls[rowIndex].get('conditionType').value;

        let currentCondition = control.controls[rowIndex];

        currentCondition.get('conditionProduct').setValue(null);
        currentCondition.get('interestRate').setValue(null);
        currentCondition.get('volume').setValue(null);
        currentCondition.get('value').setValue(null);
    }

    productTypeChanged(rowIndex: number) {


        //console.log('Row:' + rowIndex);
        this.clientAccounts = this.clientAccountsCopy;
        const control = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];

        let currentRow = control.controls[rowIndex];
        var productType = currentRow.get('productType').value;

        this.selectedProductTypes[rowIndex] = productType;

        if (this.clientAccounts && this.clientAccounts.length > 0) {
            this.clientAccounts = this.clientAccounts.filter(re => re.accountType == productType.description);

            if (this.selectedAccountNumbers[rowIndex].clientaccounts.length == 0) {
                control.controls[rowIndex].get('accountNumber').setValue(null);
            }
            else {

                control.controls[rowIndex].get('accountNumber').setValue(this.clientAccounts);

            }

        }

        if (productType.description === "Overdraft") {

            currentRow.get('term').enable();
            currentRow.get('term').setValue('12');

            currentRow.get('reviewFeeType').enable();
            currentRow.get('reviewFee').enable();
            currentRow.get('uffFee').enable();

            currentRow.get('frequency').disable();
            currentRow.get('serviceFee').disable();

        }
        else if (productType.description === "Temporary Overdraft") {

            currentRow.get('term').enable();

            currentRow.get('reviewFeeType').enable();
            currentRow.get('reviewFee').enable();
            currentRow.get('uffFee').enable();

            currentRow.get('frequency').disable();
            currentRow.get('serviceFee').disable();

        }
        else if (productType.description.indexOf("VAF") == 0) {

            currentRow.get('term').enable();

            currentRow.get('frequency').enable();
            currentRow.get('serviceFee').enable();

            currentRow.get('reviewFeeType').disable();
            currentRow.get('reviewFee').disable();
            currentRow.get('uffFee').disable();
        }
        else {

            currentRow.get('term').enable();

            currentRow.get('reviewFeeType').disable();
            currentRow.get('reviewFee').disable();
            currentRow.get('uffFee').disable();

            currentRow.get('frequency').disable();
            currentRow.get('serviceFee').disable();
        }
    }

    //productTypeChanged(rowIndex) {
    //    const control = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];

    //    let currentRow = control.controls[rowIndex];
    //    var productType = currentRow.get('productType').value;

    //    this.selectedProductTypes[rowIndex] = productType;

    //    if (productType.description === "Overdraft") {
    //        currentRow.get('term').disable();
    //        currentRow.get('term').setValue('12');

    //        currentRow.get('reviewFeeType').enable();
    //        currentRow.get('reviewFee').enable();
    //        currentRow.get('uffFee').enable();

    //        currentRow.get('frequency').disable();
    //        currentRow.get('serviceFee').disable();

    //        currentRow.get('frequency').setValue(null);
    //        currentRow.get('serviceFee').setValue(null);


    //    }
    //    else if (productType.description === "Temporary Overdraft") {

    //        //currentRow.get('term').enable();

    //        //currentRow.get('reviewFeeType').disable();
    //        //currentRow.get('reviewFee').disable();
    //        //currentRow.get('uffFee').disable();

    //        //currentRow.get('reviewFeeType').setValue(null);
    //        //currentRow.get('reviewFee').setValue(null);
    //        //currentRow.get('uffFee').setValue(null);

    //        //currentRow.get('frequency').disable();
    //        //currentRow.get('serviceFee').disable();

    //        //currentRow.get('frequency').setValue(null);
    //        //currentRow.get('serviceFee').setValue(null);

    //        ///---

    //        currentRow.get('reviewFeeType').enable();
    //        currentRow.get('reviewFee').enable();
    //        currentRow.get('uffFee').enable();

    //        currentRow.get('reviewFeeType').setValue(null);
    //        currentRow.get('reviewFee').setValue(null);
    //        currentRow.get('uffFee').setValue(null);

    //        currentRow.get('frequency').setValue(null);
    //        currentRow.get('serviceFee').setValue(null);

    //        currentRow.get('frequency').disable();
    //        currentRow.get('serviceFee').disable();

    //    }
    //    else if (productType.description.indexOf("VAF") == 0) {

    //        currentRow.get('frequency').enable();
    //        currentRow.get('serviceFee').enable();

    //        currentRow.get('reviewFeeType').disable();
    //        currentRow.get('reviewFee').disable();
    //        currentRow.get('uffFee').disable();

    //        currentRow.get('reviewFeeType').setValue(null);
    //        currentRow.get('reviewFee').setValue(null);
    //        currentRow.get('uffFee').setValue(null);


    //    }
    //    else {
    //        currentRow.get('term').enable();

    //        currentRow.get('reviewFeeType').disable();
    //        currentRow.get('reviewFee').disable();
    //        currentRow.get('uffFee').disable();

    //        currentRow.get('reviewFeeType').setValue(null);
    //        currentRow.get('reviewFee').setValue(null);
    //        currentRow.get('uffFee').setValue(null);

    //        currentRow.get('frequency').disable();
    //        currentRow.get('serviceFee').disable();

    //        currentRow.get('frequency').setValue(null);
    //        currentRow.get('serviceFee').setValue(null);

    //    }
    //} 

    addValidationError(validationDetail) {
        if (!this.validationError)
            this.validationError = [];

        this.validationError.push(validationDetail);
    }

    goBack() {
        this.location.back();
    }

    getLendingConcession(isNew: boolean): LendingConcession {
        var lendingConcession = new LendingConcession();

        lendingConcession.concession = new Concession();
        lendingConcession.concession.concessionType = ConcessionTypes.Lending;
        lendingConcession.concession.referenceNumber = this.concessionReferenceId;

        if (this.lendingConcessionForm.controls['smtDealNumber'].value)
            lendingConcession.concession.smtDealNumber = this.lendingConcessionForm.controls['smtDealNumber'].value;
        else
            this.addValidationError("SMT Deal Number not captured");

        if (this.lendingConcessionForm.controls['motivation'].value)
            lendingConcession.concession.motivation = this.lendingConcessionForm.controls['motivation'].value;
        else
            lendingConcession.concession.motivation = '.';

        if (this.lendingConcessionForm.controls['comments'].value)
            lendingConcession.concession.comments = this.lendingConcessionForm.controls['comments'].value;

        if (this.legalEntity)
            lendingConcession.concession.legalEntityId = this.legalEntity.id;
        if (this.riskGroup)
            lendingConcession.concession.riskGroupId = this.riskGroup.id;

        const concessions = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];

        let hasProductType: boolean = false;
        let hasLegalEntityId: boolean = false;
        let hasLegalEntityAccountId: boolean = false;

        for (let concessionFormItem of concessions.controls) {
            if (!lendingConcession.lendingConcessionDetails)
                lendingConcession.lendingConcessionDetails = [];

            let lendingConcessionDetail = new LendingConcessionDetail();

            if (!isNew && concessionFormItem.get('lendingConcessionDetailId').value)
                lendingConcessionDetail.lendingConcessionDetailId = concessionFormItem.get('lendingConcessionDetailId').value;

            if (!isNew && concessionFormItem.get('concessionDetailId').value)
                lendingConcessionDetail.concessionDetailId = concessionFormItem.get('concessionDetailId').value;

            if (concessionFormItem.get('productType').value) {
                lendingConcessionDetail.productTypeId = concessionFormItem.get('productType').value.id;
                hasProductType = true;
            }
            else
                this.addValidationError("Product type not selected");

            if (concessionFormItem.get('accountNumber').value) {
                lendingConcessionDetail.legalEntityId = concessionFormItem.get('accountNumber').value.legalEntityId;
                lendingConcessionDetail.legalEntityAccountId = concessionFormItem.get('accountNumber').value.legalEntityAccountId;
                hasLegalEntityId = true;
                hasLegalEntityAccountId = true;
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

            if (concessionFormItem.get('serviceFee').value)
                lendingConcessionDetail.serviceFee = concessionFormItem.get('serviceFee').value;

            if (concessionFormItem.get('frequency').value)
                lendingConcessionDetail.frequency = concessionFormItem.get('frequency').value;

            if (concessionFormItem.get('expiryDate').value)
                lendingConcessionDetail.expiryDate = new Date(concessionFormItem.get('expiryDate').value);

            if (concessionFormItem.get('mrsEri').value == "" ||
                concessionFormItem.get('mrsEri').value.toString().indexOf(".") > -1) {
                this.addValidationError("MRS/ERI cannot be empty or a decimal");
            
            } else {

                var mrsEriValue = parseInt(concessionFormItem.get('mrsEri').value, 10);
                if (mrsEriValue < MrsEriEnum.MinMrsEri || mrsEriValue > MrsEriEnum.MaxMrsEri) {
                    this.addValidationError("MRS/ERI numbers must from 12 to 25");
                } else {
                    lendingConcessionDetail.mrsEri = concessionFormItem.get('mrsEri').value;
                }   
            }
                

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

        for (let conditionFormItem of conditions.controls) {
            if (!lendingConcession.concessionConditions)
                lendingConcession.concessionConditions = [];

            let concessionCondition = new ConcessionCondition();

            if (!isNew && conditionFormItem.get('concessionConditionId').value)
                concessionCondition.concessionConditionId = conditionFormItem.get('concessionConditionId').value;

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

            if (conditionFormItem.get('periodType').value.description == 'Once-off' && conditionFormItem.get('period').value.description == 'Monthly') {
                this.addValidationError("Conditions: The Period 'Monthly' cannot be selected for Period Type 'Once-off'");
            }

            lendingConcession.concessionConditions.push(concessionCondition);
        }

        return lendingConcession;
    }

    getBackgroundColour(rowIndex: number) {
        const control = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];

        if (String(control.controls[rowIndex].get('isExpired').value) == "true") {
            return "#EC7063";
        }

        if (String(control.controls[rowIndex].get('isExpiring').value) == "true") {
            return "#F5B041";
        }

        return "";
    }

    bcmApproveConcession() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;

        var lendingConcession = this.getLendingConcession(false);
        lendingConcession.concession.subStatus = ConcessionSubStatus.PCMPending;
        lendingConcession.concession.bcmUserId = this.lendingConcession.currentUser.id;

        if (!lendingConcession.concession.comments) {
            lendingConcession.concession.comments = "Forwarded";
        }

        if (!this.validationError) {
            this.lendingService.postUpdateLendingData(lendingConcession).subscribe(entity => {
                console.log("data saved");
                this.canBcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.lendingConcession = entity;
                this.canEdit = false;
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

        var lendingConcession = this.getLendingConcession(false);
        lendingConcession.concession.status = ConcessionStatus.Declined;
        lendingConcession.concession.subStatus = ConcessionSubStatus.BCMDeclined;
        lendingConcession.concession.bcmUserId = this.lendingConcession.currentUser.id;

        if (!lendingConcession.concession.comments) {
            lendingConcession.concession.comments = ConcessionStatus.Declined;
        }

        if (!this.validationError) {
            this.lendingService.postUpdateLendingData(lendingConcession).subscribe(entity => {
                console.log("data saved");
                this.canBcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.lendingConcession = entity;
                this.canEdit = false;
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

        var lendingConcession = this.getLendingConcession(false);

        if (this.hasChanges) {
            lendingConcession.concession.status = ConcessionStatus.Pending;

            if (this.lendingConcession.currentUser.isHO) {
                lendingConcession.concession.subStatus = ConcessionSubStatus.HOApprovedWithChanges;
                lendingConcession.concession.hoUserId = this.lendingConcession.currentUser.id;
            } else {
                lendingConcession.concession.subStatus = ConcessionSubStatus.PCMApprovedWithChanges;
                lendingConcession.concession.pcmUserId = this.lendingConcession.currentUser.id;
            }

            if (!lendingConcession.concession.comments) {
                lendingConcession.concession.comments = ConcessionStatus.ApprovedWithChanges;
            }

            lendingConcession.concession.concessionComments = this.GetChanges(lendingConcession.concession.id);

        } else {
            lendingConcession.concession.status = ConcessionStatus.Approved;

            if (this.lendingConcession.currentUser.isHO) {
                lendingConcession.concession.subStatus = ConcessionSubStatus.HOApproved;
                lendingConcession.concession.hoUserId = this.lendingConcession.currentUser.id;
            } else {
                lendingConcession.concession.subStatus = ConcessionSubStatus.PCMApproved;
                lendingConcession.concession.pcmUserId = this.lendingConcession.currentUser.id;
            }

            if (!lendingConcession.concession.comments) {
                lendingConcession.concession.comments = ConcessionStatus.Approved;
            }
        }

        if (!this.validationError) {
            this.lendingService.postUpdateLendingData(lendingConcession).subscribe(entity => {
                console.log("data saved");
                this.canPcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.lendingConcession = entity;
                this.canEdit = false;
                this.isLoading = false;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        } else {
            this.isLoading = false;
        }
    }

    private GetChanges(concessionid: number): any[] {
        let comments = this.getChangedProperties();

        let commentarray = [];
        let comment = new ConcessionComment();
        comment.concessionId = concessionid;
        comment.comment = comments;
        comment.userDescription = "LogChanges";
        commentarray.push(comment);
        return commentarray;
    }

    private getChangedProperties(): string {

        let changedProperties = [];
        let rowIndex = 0;

        const concessions = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];

        //this is detailed line items,  but not yet the controls
        for (let concessionFormItem of concessions.controls) {

            let controls = (<FormGroup>concessionFormItem).controls;

            for (const fieldname in controls) { // 'field' is a string

                const abstractControl = controls[fieldname];
                if (abstractControl.dirty) {

                    changedProperties.push({ rowIndex, fieldname });
                }
            }
            rowIndex++;
        }
        return JSON.stringify(changedProperties);
    }

    changearray: any[];
    checkforchanges: boolean;
    bcmhochanged(index: number, controlname: string) {

        if (this.changearray) {

            let found = this.changearray.find(f => f.rowIndex == index && f.fieldname == controlname);
            if (found) {
                return true;
            }
        }
        return false;
    }

    pcmDeclineConcession() {
        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;

        var lendingConcession = this.getLendingConcession(false);

        lendingConcession.concession.status = ConcessionStatus.Declined;

        if (!lendingConcession.concession.comments) {
            lendingConcession.concession.comments = ConcessionStatus.Declined;
        }

        if (this.lendingConcession.currentUser.isHO) {
            lendingConcession.concession.subStatus = ConcessionSubStatus.HODeclined;
            lendingConcession.concession.hoUserId = this.lendingConcession.currentUser.id;
        } else {
            lendingConcession.concession.subStatus = ConcessionSubStatus.PCMDeclined;
            lendingConcession.concession.pcmUserId = this.lendingConcession.currentUser.id;
        }

        if (!this.validationError) {
            this.lendingService.postUpdateLendingData(lendingConcession).subscribe(entity => {
                console.log("data saved");
                this.canPcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.lendingConcession = entity;
                this.canEdit = false;
                this.isLoading = false;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        } else {
            this.isLoading = false;
        }
    }

    extendConcession() {
        if (confirm("Are you sure you want to extend this concession?")) {
            this.isLoading = true;
            this.errorMessage = null;
            this.validationError = null;

            this.lendingService.postExtendConcession(this.concessionReferenceId).subscribe(entity => {
                console.log("data saved");
                this.canBcmApprove = false;
                this.canBcmApprove = false;
                this.canExtend = false;
                this.canRenew = false;
                this.canRecall = false;
                this.canUpdate = false;
                this.canArchive = false;
                this.saveMessage = entity.concession.childReferenceNumber;
                this.isLoading = false;
                this.lendingConcession = entity;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        }
    }

    disableRows() {

        const concessions = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];
        for (let concessionFormItem of concessions.controls) {

            concessionFormItem.disable();
        }
    }

    loopRows() {
        const concessions = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];
        let rowIndex = 0;
        for (let concessionFormItem of concessions.controls) {

            this.productTypeChanged(rowIndex);

            rowIndex++;
        }

    }

    editConcession(editType: string) {
        this.canBcmApprove = false;
        this.motivationEnabled = true;
        this.canEdit = true;
        this.canBcmApprove = false;
        this.canExtend = false;
        this.canRenew = false;
        this.canRecall = false;
        this.isEditing = true;
        this.isRecalling = false;
        this.editType = editType;
        this.canResubmit = false;
        this.canUpdate = false;
        this.canArchive = false;



        this.lendingConcessionForm.controls['motivation'].setValue('');
    }

    saveConcession() {
        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;

        var lendingConcession = this.getLendingConcession(true);

        lendingConcession.concession.status = ConcessionStatus.Pending;
        lendingConcession.concession.subStatus = ConcessionSubStatus.BCMPending;
        lendingConcession.concession.type = "Existing";
        lendingConcession.concession.referenceNumber = this.concessionReferenceId;

        if (!this.validationError) {
            this.lendingService.postChildConcession(lendingConcession, this.editType).subscribe(entity => {
                console.log("data saved");
                this.isEditing = false;
                this.saveMessage = entity.concession.childReferenceNumber;
                this.lendingConcession = entity;
                this.canEdit = false;
                this.motivationEnabled = false;
                this.isLoading = false;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        } else {
            this.isLoading = false;
        }
    }

    saveUpdatedConcession() {
        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;

        var lendingConcession = this.getLendingConcession(true);

        lendingConcession.concession.type = "Existing";
        lendingConcession.concession.referenceNumber = this.concessionReferenceId;

        if (!this.validationError) {
            this.lendingService.postUpdateLendingData(lendingConcession, this.editType).subscribe(entity => {
                console.log("data saved");
                this.isEditing = false;
                this.saveMessage = entity.concession.childReferenceNumber;
                this.lendingConcession = entity;
                this.canEdit = false;
                this.motivationEnabled = false;
                this.isLoading = false;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        } else {
            this.isLoading = false;
        }
    }

    recallConcession() {
        this.isLoading = true;
        this.errorMessage = null;

        this.userConcessionsService.recallConcession(this.concessionReferenceId).subscribe(entity => {
            this.warningMessage = "Concession recalled, please make the required changes and save the concession or it will be lost";
            this.isRecalling = true;
            this.isLoading = false;
            this.canEdit = true;
            this.canArchive = false;
            this.motivationEnabled = true;
        }, error => {
            this.errorMessage = <any>error;
            this.isLoading = false;
        });
    }

    saveRecallConcession() {
        this.warningMessage = "";
        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;

        var lendingConcession = this.getLendingConcession(true);

        lendingConcession.concession.status = ConcessionStatus.Pending;
        lendingConcession.concession.subStatus = ConcessionSubStatus.BCMPending;
        lendingConcession.concession.referenceNumber = this.concessionReferenceId;

        if (!this.validationError) {
            this.lendingService.postRecallLendingData(lendingConcession).subscribe(entity => {
                console.log("data saved");
                this.isRecalling = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.lendingConcession = entity;
                this.isLoading = false;
                this.canEdit = false;
                this.motivationEnabled = false;
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

    requestorApproveConcession() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;

        var lendingConcession = this.getLendingConcession(false);
        lendingConcession.concession.status = ConcessionStatus.ApprovedWithChanges;
        lendingConcession.concession.subStatus = ConcessionSubStatus.RequestorAcceptedChanges;
        lendingConcession.concession.requestorId = this.lendingConcession.currentUser.id;

        if (!lendingConcession.concession.comments) {
            lendingConcession.concession.comments = "Accepted Changes";
        }

        if (!this.validationError) {
            this.lendingService.postUpdateLendingData(lendingConcession).subscribe(entity => {
                console.log("data saved");
                this.canApproveChanges = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.lendingConcession = entity;
                this.canEdit = false;
                this.isLoading = false;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        } else {
            this.isLoading = false;
        }
    }

    requestorDeclineConcession() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;

        var lendingConcession = this.getLendingConcession(false);
        lendingConcession.concession.status = ConcessionStatus.Declined;
        lendingConcession.concession.subStatus = ConcessionSubStatus.RequestorDeclinedChanges;
        lendingConcession.concession.requestorId = this.lendingConcession.currentUser.id;

        if (!lendingConcession.concession.comments) {
            lendingConcession.concession.comments = "Declined Changes";
        }

        if (!this.validationError) {
            this.lendingService.postUpdateLendingData(lendingConcession).subscribe(entity => {
                console.log("data saved");
                this.canApproveChanges = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.lendingConcession = entity;
                this.canEdit = false;
                this.isLoading = false;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        } else {
            this.isLoading = false;
        }
    }

    archiveConcessiondetail(concessionDetailId: number) {

        if (confirm("Please note that the account will be put back to standard pricing. Are you sure you want to delete this concession ?")) {
            this.isLoading = true;
            this.errorMessage = null;

            this.userConcessionsService.deactivateConcessionDetailed(concessionDetailId).subscribe(entity => {

                this.warningMessage = "Concession item has been deleted";

                this.isLoading = false;

                this.ngOnInit();

            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        }
    }

    archiveConcession() {
        if (confirm("Please note that the account will be put back to standard pricing. Are you sure you want to delete this concession ?")) {
            this.isLoading = true;
            this.errorMessage = null;

            this.userConcessionsService.deactivateConcession(this.concessionReferenceId).subscribe(entity => {
                this.warningMessage = "Concession has been deleted";

                this.isLoading = false;
                this.canBcmApprove = false;
                this.canPcmApprove = false;
                this.canExtend = false;
                this.canRenew = false;
                this.canRecall = false;
                this.isEditing = false;
                this.motivationEnabled = false;
                this.canEdit = false;
                this.isRecalling = false;
                this.canApproveChanges = false;
                this.canResubmit = false;
                this.canUpdate = false;
                this.canArchive = false;

            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        }
    }

    setTwoNumberDecimal($event) {
        $event.target.value = this.baseComponentService.formatDecimal($event.target.value);
        //$event.target.value = this.formatDecimal($event.target.value);
    }

    setTwoNumberDecimalMAP($event) {

        //check that it is a valid number
        if (((isNaN($event.target.value)).valueOf()) == true) {

            alert("Not a valid number for 'Prime -/+'");
            $event.target.value = 0;
        }
        else {

            $event.target.value = new DecimalPipe('en-US').transform($event.target.value, '1.3-3');
        }
    }

    setThreeNumberDecimal($event) {
        if ($event.target.value) {
            $event.target.value = new DecimalPipe('en-US').transform($event.target.value, '1.3-3');
        }
        else {

            $event.target.value = 0;
        }
    }

    formatDecimal(itemValue: number) {
        if (itemValue) {

            return new DecimalPipe('en-US').transform(itemValue, '1.2-2');
        }

        return 0;
    }

    formatDecimal3(itemValue: number) {
        if (itemValue) {

            return new DecimalPipe('en-US').transform(itemValue, '1.3-4');
        }

        return 0;
    }


    formatDecimal4(itemValue: number) {
        if (itemValue) {

            return new DecimalPipe('en-US').transform(itemValue, '1.3-4');
        }

        return 0.00;
    }

    getlendingConcessionDetail(index: number, type: string) {
        if (!this.lendingConcession.lendingConcessionDetails[index]) {
            return this.canEdit;
        }
        else {
            switch (type) {
                case 'show_term':
                    return this.lendingConcession.lendingConcessionDetails[index].show_term && this.canEdit;
                case 'show_reviewFeeType':
                    return this.lendingConcession.lendingConcessionDetails[index].show_reviewFeeType && this.canEdit;
                case 'show_reviewFee':
                    return this.lendingConcession.lendingConcessionDetails[index].show_reviewFee && this.canEdit;
                case 'show_uffFee':
                    return this.lendingConcession.lendingConcessionDetails[index].show_uffFee && this.canEdit;
                case 'show_frequency':
                    return this.lendingConcession.lendingConcessionDetails[index].show_frequency && this.canEdit;
                case 'show_serviceFee':
                    return this.lendingConcession.lendingConcessionDetails[index].show_serviceFee && this.canEdit;
            }
        }

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
