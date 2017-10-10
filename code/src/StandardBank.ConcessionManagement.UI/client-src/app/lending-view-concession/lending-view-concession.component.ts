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
import { Location, DatePipe } from '@angular/common';
import { LookupDataService } from "../services/lookup-data.service";
import { UserConcessionsService } from "../services/user-concessions.service";
import { LendingFinancial } from "../models/lending-financial";
import { ConcessionComment } from "../models/concession-comment";

@Component({
    selector: 'app-lending-view-concession',
    templateUrl: './lending-view-concession.component.html',
    styleUrls: ['./lending-view-concession.component.css'],
    providers: [DatePipe]
})
export class LendingViewConcessionComponent implements OnInit, OnDestroy {

    concessionReferenceId: string;
    public lendingConcessionForm: FormGroup;
    private sub: any;
    errorMessage: String;
    validationError: String[];
    saveMessage: String;
    warningMessage: String;
    riskGroupNumber: number;
    selectedConditionTypes: ConditionType[];
    isLoading = false;
    canBcmApprove = false;
    canPcmApprove = false;
    hasChanges = false;
    canExtend = false;
    canRenew = false;
    canRecall = false;
    isEditing = false;
    motivationEnabled = false;
    canEdit = false;
    isRecalling = false;
    capturedComments: string;
    canApproveChanges: boolean;
    canResubmit = false;
    canUpdate = false;
	editType: string;
	canArchive = false;
	isInProgressExtension = false;
	isInProgressRenewal = false;

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

    observableLendingFinancial: Observable<LendingFinancial>;
    lendingFinancial: LendingFinancial;

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private formBuilder: FormBuilder,
        private location: Location,
        private datepipe: DatePipe,
        @Inject(LookupDataService) private lookupDataService,
        @Inject(LendingService) private lendingService,
        @Inject(UserConcessionsService) private userConcessionsService) {
        this.riskGroup = new RiskGroup();
        this.reviewFeeTypes = [new ReviewFeeType()];
        this.productTypes = [new ProductType()];
        this.periods = [new Period()];
        this.periodTypes = [new PeriodType()];
        this.conditionTypes = [new ConditionType()];
        this.selectedConditionTypes = [new ConditionType()];
        this.clientAccounts = [new ClientAccount()];
        this.lendingFinancial = new LendingFinancial();
        this.lendingConcession = new LendingConcession();
        this.lendingConcession.concession = new Concession();
        this.lendingConcession.concession.concessionComments = [new ConcessionComment()];
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

                this.observableLendingFinancial = this.lendingService.getLendingFinancial(this.riskGroupNumber);
                this.observableLendingFinancial.subscribe(lendingFinancial => this.lendingFinancial = lendingFinancial, error => this.errorMessage = <any>error);
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

        Observable.forkJoin([
            this.lookupDataService.getReviewFeeTypes(),
            this.lookupDataService.getProductTypes("Lending"),
            this.lookupDataService.getPeriods(),
            this.lookupDataService.getPeriodTypes(),
            this.lookupDataService.getConditionTypes()
        ]).subscribe(results => {
            this.reviewFeeTypes = <any>results[0];
            this.productTypes = <any>results[1];
            this.periods = <any>results[2];
            this.periodTypes = <any>results[3];
            this.conditionTypes = <any>results[4];

            this.populateForm();
            }, error => this.errorMessage = <any>error);

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

    populateForm() {
        if (this.concessionReferenceId) {
            this.observableLendingConcession = this.lendingService.getLendingConcessionData(this.concessionReferenceId);
            this.observableLendingConcession.subscribe(lendingConcession => {
                this.lendingConcession = lendingConcession;

                if (lendingConcession.concession.status == "Pending" && lendingConcession.concession.subStatus == "BCM Pending") {
                    this.canBcmApprove = lendingConcession.currentUser.canBcmApprove;
                }

                if (lendingConcession.concession.status == "Pending" && lendingConcession.concession.subStatus == "PCM Pending") {
					this.canPcmApprove = lendingConcession.currentUser.canPcmApprove;

					if (!lendingConcession.concession.isInProgressExtension) {
						this.canEdit = lendingConcession.currentUser.canPcmApprove;
					}
                }

                //if it's still pending and the user is a requestor then they can recall it
                if (lendingConcession.concession.status == "Pending" && lendingConcession.concession.subStatus == "BCM Pending") {
                    this.canRecall = lendingConcession.currentUser.canRequest;
                }

                if (lendingConcession.concession.status == "Pending" &&
                    (lendingConcession.concession.subStatus == "PCM Approved With Changes" || lendingConcession.concession.subStatus == "HO Approved With Changes")) {
                    this.canApproveChanges = lendingConcession.currentUser.canRequest;
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

                    currentConcession.get('lendingConcessionDetailId').setValue(lendingConcessionDetail.lendingConcessionDetailId);
                    currentConcession.get('concessionDetailId').setValue(lendingConcessionDetail.concessionDetailId);

                    let selectedProductType = this.productTypes.filter(_ => _.id === lendingConcessionDetail.productTypeId);
                    currentConcession.get('productType').setValue(selectedProductType[0]);

                    let selectedAccountNo = this.clientAccounts.filter(_ => _.legalEntityAccountId == lendingConcessionDetail.legalEntityAccountId);
                    currentConcession.get('accountNumber').setValue(selectedAccountNo[0]);

                    currentConcession.get('limit').setValue(lendingConcessionDetail.limit);
                    currentConcession.get('term').setValue(lendingConcessionDetail.term);
                    currentConcession.get('marginAgainstPrime').setValue(lendingConcessionDetail.marginAgainstPrime);
                    currentConcession.get('approvedMarginAgainstPrime').setValue(lendingConcessionDetail.approvedMap);
                    currentConcession.get('initiationFee').setValue(lendingConcessionDetail.initiationFee);

                    let selectedReviewFeeType = this.reviewFeeTypes.filter(_ => _.id == lendingConcessionDetail.reviewFeeTypeId);
                    currentConcession.get('reviewFeeType').setValue(selectedReviewFeeType[0]);

                    currentConcession.get('reviewFee').setValue(lendingConcessionDetail.reviewFee);
                    currentConcession.get('uffFee').setValue(lendingConcessionDetail.uffFee);

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
                    currentCondition.get('expectedTurnoverValue').setValue(concessionCondition.expectedTurnoverValue);

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
			isExpiring: ['']
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

        let currentCondition = control.controls[control.length - 1];

        currentCondition.get('interestRate').setValue(null);
        currentCondition.get('volume').setValue(null);
        currentCondition.get('value').setValue(null);
        currentCondition.get('expectedTurnoverValue').setValue(null);
    }

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
        lendingConcession.concession.concessionType = "Lending";
        lendingConcession.concession.referenceNumber = this.concessionReferenceId;

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

        if (this.lendingConcessionForm.controls['comments'].value)
            lendingConcession.concession.comments = this.lendingConcessionForm.controls['comments'].value;

        lendingConcession.concession.riskGroupId = this.riskGroup.id;

        const concessions = <FormArray>this.lendingConcessionForm.controls['concessionItemRows'];

        for (let concessionFormItem of concessions.controls) {
            if (!lendingConcession.lendingConcessionDetails)
                lendingConcession.lendingConcessionDetails = [];

            let lendingConcessionDetail = new LendingConcessionDetail();

            if (!isNew && concessionFormItem.get('lendingConcessionDetailId').value)
                lendingConcessionDetail.lendingConcessionDetailId = concessionFormItem.get('lendingConcessionDetailId').value;

            if (!isNew && concessionFormItem.get('concessionDetailId').value)
                lendingConcessionDetail.concessionDetailId = concessionFormItem.get('concessionDetailId').value;

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
        lendingConcession.concession.subStatus = "PCM Pending";
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
        lendingConcession.concession.status = "Declined";
        lendingConcession.concession.subStatus = "BCM Declined";
        lendingConcession.concession.bcmUserId = this.lendingConcession.currentUser.id;

		if (!lendingConcession.concession.comments) {
			lendingConcession.concession.comments = "Declined";
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
            lendingConcession.concession.status = "Pending";

            if (this.lendingConcession.currentUser.isHO) {
                lendingConcession.concession.subStatus = "HO Approved With Changes";
                lendingConcession.concession.hoUserId = this.lendingConcession.currentUser.id;
            } else {
                lendingConcession.concession.subStatus = "PCM Approved With Changes";
                lendingConcession.concession.pcmUserId = this.lendingConcession.currentUser.id;
			}

			if (!lendingConcession.concession.comments) {
				lendingConcession.concession.comments = "Approved With Changes";
			}
        } else {
            lendingConcession.concession.status = "Approved";

            if (this.lendingConcession.currentUser.isHO) {
                lendingConcession.concession.subStatus = "HO Approved";
                lendingConcession.concession.hoUserId = this.lendingConcession.currentUser.id;
            } else {
                lendingConcession.concession.subStatus = "PCM Approved";
                lendingConcession.concession.pcmUserId = this.lendingConcession.currentUser.id;
			}

			if (!lendingConcession.concession.comments) {
				lendingConcession.concession.comments = "Approved";
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

    pcmDeclineConcession() {
        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;

        var lendingConcession = this.getLendingConcession(false);

		lendingConcession.concession.status = "Declined";

		if (!lendingConcession.concession.comments) {
			lendingConcession.concession.comments = "Declined";
		}

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

        lendingConcession.concession.status = "Pending";
        lendingConcession.concession.subStatus = "BCM Pending";
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

    recallConcession() {
        this.isLoading = true;
        this.errorMessage = null;

        this.userConcessionsService.deactivateConcession(this.concessionReferenceId).subscribe(entity => {
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

        lendingConcession.concession.status = "Pending";
        lendingConcession.concession.subStatus = "BCM Pending";
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
        lendingConcession.concession.status = "Approved With Changes";
        lendingConcession.concession.subStatus = "Requestor Accepted Changes";
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
        lendingConcession.concession.status = "Declined";
        lendingConcession.concession.subStatus = "Requestor Declined Changes";
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

	archiveConcession() {
		if (confirm("Are you sure you want to archive this concession?")) {
			this.isLoading = true;
			this.errorMessage = null;

			this.userConcessionsService.deactivateConcession(this.concessionReferenceId).subscribe(entity => {
				this.warningMessage = "Concession has been archived.";

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

}
