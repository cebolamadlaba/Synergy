import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { RiskGroup } from "../models/risk-group";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Location, DatePipe } from '@angular/common';
import { LookupDataService } from "../services/lookup-data.service";
import { TransactionalConcessionService } from "../services/transactional-concession.service";
import { Period } from "../models/period";
import { PeriodType } from "../models/period-type";
import { ConditionType } from "../models/condition-type";
import { ConditionProduct } from "../models/condition-product";
import { ClientAccount } from "../models/client-account";
import { TransactionType } from "../models/transaction-type";
import { TransactionalConcession } from "../models/transactional-concession";
import { Concession } from "../models/concession";
import { ConcessionCondition } from "../models/concession-condition";
import { TransactionalConcessionDetail } from "../models/transactional-concession-detail";
import { UserConcessionsService } from "../services/user-concessions.service";
import { ConcessionComment } from "../models/concession-comment";
import { TransactionalFinancial } from "../models/transactional-financial";

@Component({
	selector: 'app-transactional-view-concession',
	templateUrl: './transactional-view-concession.component.html',
	styleUrls: ['./transactional-view-concession.component.css'],
	providers: [DatePipe]
})
export class TransactionalViewConcessionComponent implements OnInit, OnDestroy {

	concessionReferenceId: string;
	public transactionalConcessionForm: FormGroup;
	private sub: any;
	errorMessage: String;
	validationError: String[];
	saveMessage: String;
	warningMessage: String;
	observableRiskGroup: Observable<RiskGroup>;
	riskGroup: RiskGroup;
	riskGroupNumber: number;
	selectedConditionTypes: ConditionType[];
	selectedTransactionTypes: TransactionType[];
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
	isRecalling = false;
	capturedComments: string;
	canApproveChanges: boolean;
	canResubmit = false;
	canUpdate = false;
	editType: string;
	canArchive = false;
	isInProgressExtension = false;
	isInProgressRenewal = false;

	observablePeriods: Observable<Period[]>;
	periods: Period[];

	observablePeriodTypes: Observable<PeriodType[]>;
	periodTypes: PeriodType[];

	observableConditionTypes: Observable<ConditionType[]>;
	conditionTypes: ConditionType[];

	observableClientAccounts: Observable<ClientAccount[]>;
	clientAccounts: ClientAccount[];

	observableTransactionTypes: Observable<TransactionType[]>;
	transactionTypes: TransactionType[];

	observableTransactionalConcession: Observable<TransactionalConcession>;
	transactionalConcession: TransactionalConcession;

	observableTransactionalFinancial: Observable<TransactionalFinancial>;
	transactionalFinancial: TransactionalFinancial;

	constructor(private route: ActivatedRoute,
		private formBuilder: FormBuilder,
		private location: Location,
		private datepipe: DatePipe,
		@Inject(LookupDataService) private lookupDataService,
		@Inject(TransactionalConcessionService) private transactionalConcessionService,
		@Inject(UserConcessionsService) private userConcessionsService) {
		this.riskGroup = new RiskGroup();
		this.periods = [new Period()];
		this.periodTypes = [new PeriodType()];
		this.conditionTypes = [new ConditionType()];
		this.selectedConditionTypes = [new ConditionType()];
		this.selectedTransactionTypes = [new TransactionType()];
		this.clientAccounts = [new ClientAccount()];
		this.transactionalFinancial = new TransactionalFinancial();
		this.transactionalConcession = new TransactionalConcession();
		this.transactionalConcession.concession = new Concession();
		this.transactionalConcession.concession.concessionComments = [new ConcessionComment()];
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

				this.observableTransactionalFinancial = this.transactionalConcessionService.getTransactionalFinancial(this.riskGroupNumber);
				this.observableTransactionalFinancial.subscribe(transactionalFinancial => this.transactionalFinancial = transactionalFinancial,
					error => this.errorMessage = <any>error);
			}
		});

		this.transactionalConcessionForm = this.formBuilder.group({
			concessionItemRows: this.formBuilder.array([this.initConcessionItemRows()]),
			conditionItemsRows: this.formBuilder.array([]),
			mrsCrs: new FormControl(),
			smtDealNumber: new FormControl(),
			motivation: new FormControl(),
			comments: new FormControl()
		});

		Observable.forkJoin([
			this.lookupDataService.getPeriods(),
			this.lookupDataService.getPeriodTypes(),
			this.lookupDataService.getConditionTypes(),
			this.lookupDataService.getTransactionTypes("Transactional")
		]).subscribe(results => {
			this.periods = <any>results[0];
			this.periodTypes = <any>results[1];
			this.conditionTypes = <any>results[2];
			this.transactionTypes = <any>results[3];

			this.populateForm();
		}, error => this.errorMessage = <any>error);

		this.transactionalConcessionForm.valueChanges.subscribe((value: any) => {
			if (this.transactionalConcessionForm.dirty) {
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

			this.observableTransactionalConcession = this.transactionalConcessionService.getTransactionalConcessionData(this.concessionReferenceId);
			this.observableTransactionalConcession.subscribe(transactionalConcession => {
				this.transactionalConcession = transactionalConcession;

				if (transactionalConcession.concession.status == "Pending" && transactionalConcession.concession.subStatus == "BCM Pending") {
					this.canBcmApprove = transactionalConcession.currentUser.canBcmApprove;
				}

				if (transactionalConcession.concession.status == "Pending" && transactionalConcession.concession.subStatus == "PCM Pending") {
					this.canPcmApprove = transactionalConcession.currentUser.canPcmApprove;

					if (!transactionalConcession.concession.isInProgressExtension) {
						this.canEdit = transactionalConcession.currentUser.canPcmApprove;
					}
				}

				//if it's still pending and the user is a requestor then they can recall it
				if (transactionalConcession.concession.status == "Pending" && transactionalConcession.concession.subStatus == "BCM Pending") {
					this.canRecall = transactionalConcession.currentUser.canRequest;
				}

				if (transactionalConcession.concession.status == "Pending" &&
					(transactionalConcession.concession.subStatus == "PCM Approved With Changes" || transactionalConcession.concession.subStatus == "HO Approved With Changes")) {
					this.canApproveChanges = transactionalConcession.currentUser.canRequest;
				}

				//if the concession is set to can extend and the user is a requestor, then they can extend or renew it
				this.canExtend = transactionalConcession.concession.canExtend && transactionalConcession.currentUser.canRequest;
				this.canRenew = transactionalConcession.concession.canRenew && transactionalConcession.currentUser.canRequest;

				//set the resubmit and update permissions
				this.canResubmit = transactionalConcession.concession.canResubmit && transactionalConcession.currentUser.canRequest;
				this.canUpdate = transactionalConcession.concession.canUpdate && transactionalConcession.currentUser.canRequest;

				this.canArchive = transactionalConcession.concession.canArchive && transactionalConcession.currentUser.canRequest;
				this.isInProgressExtension = transactionalConcession.concession.isInProgressExtension;
				this.isInProgressRenewal = transactionalConcession.concession.isInProgressRenewal;

				this.transactionalConcessionForm.controls['mrsCrs'].setValue(this.transactionalConcession.concession.mrsCrs);
				this.transactionalConcessionForm.controls['smtDealNumber'].setValue(this.transactionalConcession.concession.smtDealNumber);
				this.transactionalConcessionForm.controls['motivation'].setValue(this.transactionalConcession.concession.motivation);

				let rowIndex = 0;

				for (let transactionalConcessionDetail of this.transactionalConcession.transactionalConcessionDetails) {

					if (rowIndex != 0) {
						this.addNewConcessionRow();
					}

					const concessions = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];
					let currentConcession = concessions.controls[concessions.length - 1];

					currentConcession.get('transactionalConcessionDetailId').setValue(transactionalConcessionDetail.transactionalConcessionDetailId);
					currentConcession.get('concessionDetailId').setValue(transactionalConcessionDetail.concessionDetailId);

					let selectedTransactionType = this.transactionTypes.filter(_ => _.id === transactionalConcessionDetail.transactionTypeId);
					currentConcession.get('transactionType').setValue(selectedTransactionType[0]);

					let selectedAccountNo = this.clientAccounts.filter(_ => _.legalEntityAccountId == transactionalConcessionDetail.legalEntityAccountId);
					currentConcession.get('accountNumber').setValue(selectedAccountNo[0]);

					this.selectedTransactionTypes[rowIndex] = selectedTransactionType[0];

					let selectedTransactionTableNumber = selectedTransactionType[0].transactionTableNumbers.filter(_ => _.id == transactionalConcessionDetail.transactionTableNumberId);
					currentConcession.get('transactionTableNumber').setValue(selectedTransactionTableNumber[0]);

					currentConcession.get('adValorem').setValue(transactionalConcessionDetail.adValorem);
					currentConcession.get('flatFeeOrRate').setValue(transactionalConcessionDetail.fee);
					currentConcession.get('approvedTableNumber').setValue(transactionalConcessionDetail.approvedTableNumber);

					if (transactionalConcessionDetail.expiryDate) {
						var formattedExpiryDate = this.datepipe.transform(transactionalConcessionDetail.expiryDate, 'yyyy-MM-dd');
						currentConcession.get('expiryDate').setValue(formattedExpiryDate);
					}

					if (transactionalConcessionDetail.dateApproved) {
						var formattedDateApproved = this.datepipe.transform(transactionalConcessionDetail.dateApproved, 'yyyy-MM-dd');
						currentConcession.get('dateApproved').setValue(formattedDateApproved);
					}

					currentConcession.get('isExpired').setValue(transactionalConcessionDetail.isExpired);
					currentConcession.get('isExpiring').setValue(transactionalConcessionDetail.isExpiring);

					rowIndex++;
				}

				rowIndex = 0;

				for (let concessionCondition of this.transactionalConcession.concessionConditions) {
					this.addNewConditionRow();

					const conditions = <FormArray>this.transactionalConcessionForm.controls['conditionItemsRows'];
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

				this.isLoading = false;
			}, error => {
				this.isLoading = false;
				this.errorMessage = <any>error;
			});
		}
	}

	initConcessionItemRows() {
		this.selectedTransactionTypes.push(new TransactionType());

		return this.formBuilder.group({
			transactionalConcessionDetailId: [''],
			concessionDetailId: [''],
			transactionType: [''],
			accountNumber: [''],
			transactionTableNumber: [''],
			flatFeeOrRate: [{ value: '', disabled: true }],
			adValorem: [{ value: '', disabled: true }],
			approvedTableNumber: [{ value: '', disabled: true }],
			expiryDate: [''],
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
		const control = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];
		control.push(this.initConcessionItemRows());
	}

	addNewConditionRow() {
		const control = <FormArray>this.transactionalConcessionForm.controls['conditionItemsRows'];
		control.push(this.initConditionItemRows());
	}

	addNewConditionRowIfNone() {
		const control = <FormArray>this.transactionalConcessionForm.controls['conditionItemsRows'];
		if (control.length == 0)
			control.push(this.initConditionItemRows());
	}

	deleteConcessionRow(index: number) {
		const control = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];
		control.removeAt(index);

		this.selectedTransactionTypes.splice(index, 1);
	}

	deleteConditionRow(index: number) {
		const control = <FormArray>this.transactionalConcessionForm.controls['conditionItemsRows'];
		control.removeAt(index);

		this.selectedConditionTypes.splice(index, 1);
	}

	conditionTypeChanged(rowIndex) {
		const control = <FormArray>this.transactionalConcessionForm.controls['conditionItemsRows'];
		this.selectedConditionTypes[rowIndex] = control.controls[rowIndex].get('conditionType').value;

		let currentCondition = control.controls[control.length - 1];

		currentCondition.get('interestRate').setValue(null);
		currentCondition.get('volume').setValue(null);
		currentCondition.get('value').setValue(null);
		currentCondition.get('expectedTurnoverValue').setValue(null);
	}

	transactionTypeChanged(rowIndex) {
		const control = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];
		this.selectedTransactionTypes[rowIndex] = control.controls[rowIndex].get('transactionType').value;

		let currentTransactionType = control.controls[control.length - 1];

		currentTransactionType.get('adValorem').setValue(null);
		currentTransactionType.get('flatFeeOrRate').setValue(null);
	}

	transactionTableNumberChanged(rowIndex) {
		const control = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];

		control.controls[rowIndex].get('flatFeeOrRate').setValue(control.controls[rowIndex].get('transactionTableNumber').value.fee);
		control.controls[rowIndex].get('adValorem').setValue(control.controls[rowIndex].get('transactionTableNumber').value.adValorem);
	}

	getTransactionalConcession(isNew: boolean): TransactionalConcession {
		var transactionalConcession = new TransactionalConcession();
		transactionalConcession.concession = new Concession();
		transactionalConcession.concession.concessionType = "Transactional";
		transactionalConcession.concession.riskGroupId = this.riskGroup.id;
		transactionalConcession.concession.referenceNumber = this.concessionReferenceId;

		if (this.transactionalConcessionForm.controls['mrsCrs'].value)
			transactionalConcession.concession.mrsCrs = this.transactionalConcessionForm.controls['mrsCrs'].value;
		else
			this.addValidationError("MRS/CRS not captured");

		if (this.transactionalConcessionForm.controls['smtDealNumber'].value)
			transactionalConcession.concession.smtDealNumber = this.transactionalConcessionForm.controls['smtDealNumber'].value;
		else
			this.addValidationError("SMT Deal Number not captured");

		if (this.transactionalConcessionForm.controls['motivation'].value)
			transactionalConcession.concession.motivation = this.transactionalConcessionForm.controls['motivation'].value;
		else
			this.addValidationError("Motivation not captured");

		if (this.transactionalConcessionForm.controls['comments'].value)
			transactionalConcession.concession.comments = this.transactionalConcessionForm.controls['comments'].value;

		const concessions = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];

		for (let concessionFormItem of concessions.controls) {
			if (!transactionalConcession.transactionalConcessionDetails)
				transactionalConcession.transactionalConcessionDetails = [];

			let transactionalConcessionDetail = new TransactionalConcessionDetail();

			if (!isNew && concessionFormItem.get('transactionalConcessionDetailId').value)
				transactionalConcessionDetail.transactionalConcessionDetailId = concessionFormItem.get('transactionalConcessionDetailId').value;

			if (!isNew && concessionFormItem.get('concessionDetailId').value)
				transactionalConcessionDetail.concessionDetailId = concessionFormItem.get('concessionDetailId').value;

			if (concessionFormItem.get('transactionType').value)
				transactionalConcessionDetail.transactionTypeId = concessionFormItem.get('transactionType').value.id;
			else
				this.addValidationError("Transaction type not selected");

			if (concessionFormItem.get('accountNumber').value) {
				transactionalConcessionDetail.legalEntityId = concessionFormItem.get('accountNumber').value.legalEntityId;
				transactionalConcessionDetail.legalEntityAccountId = concessionFormItem.get('accountNumber').value.legalEntityAccountId;
			} else {
				this.addValidationError("Client account not selected");
			}

			if (concessionFormItem.get('transactionTableNumber').value) {
				transactionalConcessionDetail.transactionTableNumberId = concessionFormItem.get('transactionTableNumber').value.id;

				if (concessionFormItem.get('transactionTableNumber').value.adValorem)
				    transactionalConcessionDetail.adValorem = concessionFormItem.get('transactionTableNumber').value.adValorem;

				if (concessionFormItem.get('transactionTableNumber').value.fee)
					transactionalConcessionDetail.fee = concessionFormItem.get('transactionTableNumber').value.fee;
			} else {
				this.addValidationError("Table Number not selected");
			}

			if (concessionFormItem.get('expiryDate').value)
				transactionalConcessionDetail.expiryDate = new Date(concessionFormItem.get('expiryDate').value);

			transactionalConcession.transactionalConcessionDetails.push(transactionalConcessionDetail);
		}

		const conditions = <FormArray>this.transactionalConcessionForm.controls['conditionItemsRows'];

		for (let conditionFormItem of conditions.controls) {
			if (!transactionalConcession.concessionConditions)
				transactionalConcession.concessionConditions = [];

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

			transactionalConcession.concessionConditions.push(concessionCondition);
		}

		return transactionalConcession;
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

	getBackgroundColour(rowIndex: number) {
		const control = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];

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

		var transactionalConcession = this.getTransactionalConcession(false);
		transactionalConcession.concession.subStatus = "PCM Pending";
		transactionalConcession.concession.bcmUserId = this.transactionalConcession.currentUser.id;

		if (!transactionalConcession.concession.comments) {
			transactionalConcession.concession.comments = "Forwarded";
		}

		if (!this.validationError) {
			this.transactionalConcessionService.postUpdateTransactionalData(transactionalConcession).subscribe(entity => {
				console.log("data saved");
				this.canBcmApprove = false;
				this.saveMessage = entity.concession.referenceNumber;
				this.isLoading = false;
				this.transactionalConcession = entity;
				this.canEdit = false;
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

		var transactionalConcession = this.getTransactionalConcession(false);
		transactionalConcession.concession.status = "Declined";
		transactionalConcession.concession.subStatus = "BCM Declined";
		transactionalConcession.concession.bcmUserId = this.transactionalConcession.currentUser.id;

		if (!transactionalConcession.concession.comments) {
			transactionalConcession.concession.comments = "Declined";
		}

		if (!this.validationError) {
			this.transactionalConcessionService.postUpdateTransactionalData(transactionalConcession).subscribe(entity => {
				console.log("data saved");
				this.canBcmApprove = false;
				this.saveMessage = entity.concession.referenceNumber;
				this.isLoading = false;
				this.transactionalConcession = entity;
				this.canEdit = false;
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

		var transactionalConcession = this.getTransactionalConcession(false);

		if (this.hasChanges) {
			transactionalConcession.concession.status = "Pending";

			if (this.transactionalConcession.currentUser.isHO) {
				transactionalConcession.concession.subStatus = "HO Approved With Changes";
				transactionalConcession.concession.hoUserId = this.transactionalConcession.currentUser.id;
			} else {
				transactionalConcession.concession.subStatus = "PCM Approved With Changes";
				transactionalConcession.concession.pcmUserId = this.transactionalConcession.currentUser.id;
			}

			if (!transactionalConcession.concession.comments) {
				transactionalConcession.concession.comments = "Approved With Changes";
			}
		} else {
			transactionalConcession.concession.status = "Approved";

			if (this.transactionalConcession.currentUser.isHO) {
				transactionalConcession.concession.subStatus = "HO Approved";
				transactionalConcession.concession.hoUserId = this.transactionalConcession.currentUser.id;
			} else {
				transactionalConcession.concession.subStatus = "PCM Approved";
				transactionalConcession.concession.pcmUserId = this.transactionalConcession.currentUser.id;
			}

			if (!transactionalConcession.concession.comments) {
				transactionalConcession.concession.comments = "Approved";
			}
		}

		if (!this.validationError) {
			this.transactionalConcessionService.postUpdateTransactionalData(transactionalConcession).subscribe(entity => {
				console.log("data saved");
				this.canPcmApprove = false;
				this.saveMessage = entity.concession.referenceNumber;
				this.isLoading = false;
				this.transactionalConcession = entity;
				this.canEdit = false;
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

		var transactionalConcession = this.getTransactionalConcession(false);

		transactionalConcession.concession.status = "Declined";

		if (!transactionalConcession.concession.comments) {
			transactionalConcession.concession.comments = "Declined";
		}

		if (this.transactionalConcession.currentUser.isHO) {
			transactionalConcession.concession.subStatus = "HO Declined";
			transactionalConcession.concession.hoUserId = this.transactionalConcession.currentUser.id;
		} else {
			transactionalConcession.concession.subStatus = "PCM Declined";
			transactionalConcession.concession.pcmUserId = this.transactionalConcession.currentUser.id;
		}

		if (!this.validationError) {
			this.transactionalConcessionService.postUpdateTransactionalData(transactionalConcession).subscribe(entity => {
				console.log("data saved");
				this.canPcmApprove = false;
				this.saveMessage = entity.concession.referenceNumber;
				this.isLoading = false;
				this.transactionalConcession = entity;
				this.canEdit = false;
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

			this.transactionalConcessionService.postExtendConcession(this.concessionReferenceId).subscribe(entity => {
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
				this.transactionalConcession = entity;
			}, error => {
				this.errorMessage = <any>error;
				this.isLoading = false;
			});
		}
	}

	editConcession(editType: string) {
		this.canBcmApprove = false;
		this.motivationEnabled = true;
		this.canBcmApprove = false;
		this.canExtend = false;
		this.canRenew = false;
		this.canRecall = false;
		this.isEditing = true;
		this.isRecalling = false;
		this.canEdit = true;
		this.editType = editType;
		this.canResubmit = false;
		this.canUpdate = false;
		this.canArchive = false;

		this.transactionalConcessionForm.controls['motivation'].setValue('');
	}

	saveConcession() {
		this.isLoading = true;
		this.errorMessage = null;
		this.validationError = null;

		var transactionalConcession = this.getTransactionalConcession(true);

		transactionalConcession.concession.status = "Pending";
		transactionalConcession.concession.subStatus = "BCM Pending";
		transactionalConcession.concession.type = "Existing";
		transactionalConcession.concession.referenceNumber = this.concessionReferenceId;

		if (!this.validationError) {
			this.transactionalConcessionService.postChildConcession(transactionalConcession, this.editType).subscribe(entity => {
				console.log("data saved");
				this.isEditing = false;
				this.saveMessage = entity.concession.childReferenceNumber;
				this.transactionalConcession = entity;
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

	recallConcession() {
		this.isLoading = true;
		this.errorMessage = null;

		this.userConcessionsService.deactivateConcession(this.concessionReferenceId).subscribe(entity => {
			this.warningMessage = "Concession recalled, please make the required changes and save the concession or it will be lost";
			this.isRecalling = true;
			this.isLoading = false;
			this.canEdit = true;
			this.motivationEnabled = true;
			this.canArchive = false;
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

		var transactionalConcession = this.getTransactionalConcession(true);

		transactionalConcession.concession.status = "Pending";
		transactionalConcession.concession.subStatus = "BCM Pending";
		transactionalConcession.concession.referenceNumber = this.concessionReferenceId;

		if (!this.validationError) {
			this.transactionalConcessionService.postRecallTransactionalData(transactionalConcession).subscribe(entity => {
				console.log("data saved");
				this.isRecalling = false;
				this.saveMessage = entity.concession.referenceNumber;
				this.transactionalConcession = entity;
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

	requestorApproveConcession() {
		this.isLoading = true;

		this.errorMessage = null;
		this.validationError = null;

		var transactionalConcession = this.getTransactionalConcession(false);
		transactionalConcession.concession.status = "Approved With Changes";
		transactionalConcession.concession.subStatus = "Requestor Accepted Changes";
		transactionalConcession.concession.requestorId = this.transactionalConcession.currentUser.id;

		if (!transactionalConcession.concession.comments) {
			transactionalConcession.concession.comments = "Accepted Changes";
		}

		if (!this.validationError) {
			this.transactionalConcessionService.postUpdateTransactionalData(transactionalConcession).subscribe(entity => {
				console.log("data saved");
				this.canApproveChanges = false;
				this.saveMessage = entity.concession.referenceNumber;
				this.isLoading = false;
				this.transactionalConcession = entity;
				this.canEdit = false;
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

		var transactionalConcession = this.getTransactionalConcession(false);
		transactionalConcession.concession.status = "Declined";
		transactionalConcession.concession.subStatus = "Requestor Declined Changes";
		transactionalConcession.concession.requestorId = this.transactionalConcession.currentUser.id;

		if (!transactionalConcession.concession.comments) {
			transactionalConcession.concession.comments = "Declined Changes";
		}

		if (!this.validationError) {
			this.transactionalConcessionService.postUpdateTransactionalData(transactionalConcession).subscribe(entity => {
				console.log("data saved");
				this.canApproveChanges = false;
				this.saveMessage = entity.concession.referenceNumber;
				this.isLoading = false;
				this.transactionalConcession = entity;
				this.canEdit = false;
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