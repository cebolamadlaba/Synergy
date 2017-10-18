import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { RiskGroup } from "../models/risk-group";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Location, DatePipe } from '@angular/common';
import { Period } from "../models/period";
import { PeriodType } from "../models/period-type";
import { ConditionType } from "../models/condition-type";
import { ClientAccount } from "../models/client-account";
import { AccrualType } from "../models/accrual-type";
import { ChannelType } from "../models/channel-type";
import { LookupDataService } from "../services/lookup-data.service";
import { CashConcession } from "../models/cash-concession";
import { Concession } from "../models/concession";
import { CashConcessionService } from "../services/cash-concession.service";
import { CashConcessionDetail } from "../models/cash-concession-detail";
import { ConcessionCondition } from "../models/concession-condition";
import { TableNumber } from "../models/table-number";
import { UserConcessionsService } from "../services/user-concessions.service";
import { ConcessionComment } from "../models/concession-comment";
import { CashFinancial } from "../models/cash-financial";

@Component({
	selector: 'app-cash-view-concession',
	templateUrl: './cash-view-concession.component.html',
	styleUrls: ['./cash-view-concession.component.css'],
	providers: [DatePipe]
})
export class CashViewConcessionComponent implements OnInit, OnDestroy {

	concessionReferenceId: string;
	private sub: any;
	errorMessage: String;
	validationError: String[];
	saveMessage: String;
	warningMessage: String;
	observableRiskGroup: Observable<RiskGroup>;
	riskGroup: RiskGroup;
	riskGroupNumber: number;
	public cashConcessionForm: FormGroup;
	selectedConditionTypes: ConditionType[];
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
	canApproveChanges = false;
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

	observableAccrualTypes: Observable<AccrualType[]>;
	accrualTypes: AccrualType[];

	observableChannelTypes: Observable<ChannelType[]>;
	channelTypes: ChannelType[];

	observableCashConcession: Observable<CashConcession>;
	cashConcession: CashConcession;

	observableTableNumbers: Observable<TableNumber[]>;
	tableNumbers: TableNumber[];

	observableCashFinancial: Observable<CashFinancial>;
	cashFinancial: CashFinancial;

	constructor(private route: ActivatedRoute,
		private formBuilder: FormBuilder,
		private location: Location,
		private datepipe: DatePipe,
		@Inject(LookupDataService) private lookupDataService,
		@Inject(CashConcessionService) private cashConcessionService,
		@Inject(UserConcessionsService) private userConcessionsService) {
		this.riskGroup = new RiskGroup();
		this.periods = [new Period()];
		this.periodTypes = [new PeriodType()];
		this.conditionTypes = [new ConditionType()];
		this.selectedConditionTypes = [new ConditionType()];
		this.clientAccounts = [new ClientAccount()];
		this.cashFinancial = new CashFinancial();
		this.cashConcession = new CashConcession();
		this.cashConcession.concession = new Concession();
		this.cashConcession.concession.concessionComments = [new ConcessionComment()];
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

				this.observableCashFinancial = this.cashConcessionService.getCashFinancial(this.riskGroupNumber);
				this.observableCashFinancial.subscribe(cashFinancial => this.cashFinancial = cashFinancial, error => this.errorMessage = <any>error);
			}
		});

		this.cashConcessionForm = this.formBuilder.group({
			concessionItemRows: this.formBuilder.array([this.initConcessionItemRows()]),
			conditionItemsRows: this.formBuilder.array([]),
			smtDealNumber: new FormControl(),
			motivation: new FormControl(),
			comments: new FormControl()
		});

		Observable.forkJoin([
			this.lookupDataService.getChannelTypes(),
			this.lookupDataService.getPeriods(),
			this.lookupDataService.getPeriodTypes(),
			this.lookupDataService.getConditionTypes(),
			this.lookupDataService.getAccrualTypes(),
			this.lookupDataService.getTableNumbers("Cash")
		]).subscribe(results => {
			this.channelTypes = <any>results[0];
			this.periods = <any>results[1];
			this.periodTypes = <any>results[2];
			this.conditionTypes = <any>results[3];
			this.accrualTypes = <any>results[4];
			this.tableNumbers = <any>results[5];

			this.populateForm();
		}, error => this.errorMessage = <any>error);

		this.cashConcessionForm.valueChanges.subscribe((value: any) => {
			if (this.cashConcessionForm.dirty) {
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
			this.observableCashConcession = this.cashConcessionService.getCashConcessionData(this.concessionReferenceId);
			this.observableCashConcession.subscribe(cashConcession => {
				this.cashConcession = cashConcession;

				if (cashConcession.concession.status == "Pending" && cashConcession.concession.subStatus == "BCM Pending") {
					this.canBcmApprove = cashConcession.currentUser.canBcmApprove;
				}

				if (cashConcession.concession.status == "Pending" && cashConcession.concession.subStatus == "PCM Pending") {
					this.canPcmApprove = cashConcession.currentUser.canPcmApprove;

					if (!cashConcession.concession.isInProgressExtension) {
						this.canEdit = cashConcession.currentUser.canPcmApprove;
					}
				}

				//if it's still pending and the user is a requestor then they can recall it
				if (cashConcession.concession.status == "Pending" && cashConcession.concession.subStatus == "BCM Pending") {
					this.canRecall = cashConcession.currentUser.canRequest;
				}

				if (cashConcession.concession.status == "Pending" &&
					(cashConcession.concession.subStatus == "PCM Approved With Changes" || cashConcession.concession.subStatus == "HO Approved With Changes")) {
					this.canApproveChanges = cashConcession.currentUser.canRequest;
				}

				//if the concession is set to can extend and the user is a requestor, then they can extend or renew it
				this.canExtend = cashConcession.concession.canExtend && cashConcession.currentUser.canRequest;
				this.canRenew = cashConcession.concession.canRenew && cashConcession.currentUser.canRequest;

				//set the resubmit and update permissions
				this.canResubmit = cashConcession.concession.canResubmit && cashConcession.currentUser.canRequest;
				this.canUpdate = cashConcession.concession.canUpdate && cashConcession.currentUser.canRequest;

				this.canArchive = cashConcession.concession.canArchive && cashConcession.currentUser.canRequest;
				this.isInProgressExtension = cashConcession.concession.isInProgressExtension;
				this.isInProgressRenewal = cashConcession.concession.isInProgressRenewal;

				this.cashConcessionForm.controls['smtDealNumber'].setValue(this.cashConcession.concession.smtDealNumber);
				this.cashConcessionForm.controls['motivation'].setValue(this.cashConcession.concession.motivation);

				let rowIndex = 0;

				for (let cashConcessionDetail of this.cashConcession.cashConcessionDetails) {

					if (rowIndex != 0) {
						this.addNewConcessionRow();
					}

					const concessions = <FormArray>this.cashConcessionForm.controls['concessionItemRows'];
					let currentConcession = concessions.controls[concessions.length - 1];

					currentConcession.get('cashConcessionDetailId').setValue(cashConcessionDetail.cashConcessionDetailId);
					currentConcession.get('concessionDetailId').setValue(cashConcessionDetail.concessionDetailId);

					let selectedChannelType = this.channelTypes.filter(_ => _.id == cashConcessionDetail.channelTypeId);
					currentConcession.get('channelType').setValue(selectedChannelType[0]);

					let selectedAccountNo = this.clientAccounts.filter(_ => _.legalEntityAccountId == cashConcessionDetail.legalEntityAccountId);
					currentConcession.get('accountNumber').setValue(selectedAccountNo[0]);

					currentConcession.get('baseRate').setValue(cashConcessionDetail.baseRate);
					currentConcession.get('adValorem').setValue(cashConcessionDetail.adValorem);
					currentConcession.get('approvedTableNumber').setValue(cashConcessionDetail.approvedTableNumber);

					let selectedTableNumber = this.tableNumbers.filter(_ => _.id == cashConcessionDetail.tableNumberId);
					currentConcession.get('tableNumber').setValue(selectedTableNumber[0]);

					let selectedAccrualType = this.accrualTypes.filter(_ => _.id == cashConcessionDetail.accrualTypeId);
					currentConcession.get('accrualType').setValue(selectedAccrualType[0]);

					if (cashConcessionDetail.expiryDate) {
						var formattedExpiryDate = this.datepipe.transform(cashConcessionDetail.expiryDate, 'yyyy-MM-dd');
						currentConcession.get('expiryDate').setValue(formattedExpiryDate);
					}

					if (cashConcessionDetail.dateApproved) {
						var formattedDateApproved = this.datepipe.transform(cashConcessionDetail.dateApproved, 'yyyy-MM-dd');
						currentConcession.get('dateApproved').setValue(formattedDateApproved);
					}

					currentConcession.get('isExpired').setValue(cashConcessionDetail.isExpired);
					currentConcession.get('isExpiring').setValue(cashConcessionDetail.isExpiring);

					rowIndex++;
				}

				rowIndex = 0;

				for (let concessionCondition of this.cashConcession.concessionConditions) {
					this.addNewConditionRow();

					const conditions = <FormArray>this.cashConcessionForm.controls['conditionItemsRows'];
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
		return this.formBuilder.group({
			cashConcessionDetailId: [''],
			concessionDetailId: [''],
			channelType: [''],
			accountNumber: [''],
			baseRate: [{ value: '', disabled: true }],
			adValorem: [{ value: '', disabled: true }],
			tableNumber: [''],
			approvedTableNumber: [{ value: '', disabled: true }],
			accrualType: [''],
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
		const control = <FormArray>this.cashConcessionForm.controls['concessionItemRows'];
		control.push(this.initConcessionItemRows());
	}

	addNewConditionRow() {
		const control = <FormArray>this.cashConcessionForm.controls['conditionItemsRows'];
		control.push(this.initConditionItemRows());
	}

	addNewConditionRowIfNone() {
		const control = <FormArray>this.cashConcessionForm.controls['conditionItemsRows'];
		if (control.length == 0)
			control.push(this.initConditionItemRows());
	}

	deleteConcessionRow(index: number) {
		const control = <FormArray>this.cashConcessionForm.controls['concessionItemRows'];
		control.removeAt(index);
	}

	deleteConditionRow(index: number) {
		const control = <FormArray>this.cashConcessionForm.controls['conditionItemsRows'];
		control.removeAt(index);

		this.selectedConditionTypes.splice(index, 1);
	}

	conditionTypeChanged(rowIndex) {
		const control = <FormArray>this.cashConcessionForm.controls['conditionItemsRows'];
		this.selectedConditionTypes[rowIndex] = control.controls[rowIndex].get('conditionType').value;

		let currentCondition = control.controls[control.length - 1];

		currentCondition.get('interestRate').setValue(null);
		currentCondition.get('volume').setValue(null);
		currentCondition.get('value').setValue(null);
		currentCondition.get('expectedTurnoverValue').setValue(null);
	}

	tableNumberChanged(rowIndex) {
		const control = <FormArray>this.cashConcessionForm.controls['concessionItemRows'];

		control.controls[rowIndex].get('baseRate').setValue(control.controls[rowIndex].get('tableNumber').value.baseRate);
		control.controls[rowIndex].get('adValorem').setValue(control.controls[rowIndex].get('tableNumber').value.adValorem);
	}

	addValidationError(validationDetail) {
		if (!this.validationError)
			this.validationError = [];

		this.validationError.push(validationDetail);
	}

	getCashConcession(isNew: boolean): CashConcession {
		var cashConcession = new CashConcession();
		cashConcession.concession = new Concession();
		cashConcession.concession.concessionType = "Cash";
		cashConcession.concession.riskGroupId = this.riskGroup.id;
		cashConcession.concession.referenceNumber = this.concessionReferenceId;

		if (this.cashConcessionForm.controls['smtDealNumber'].value)
			cashConcession.concession.smtDealNumber = this.cashConcessionForm.controls['smtDealNumber'].value;
		else
			this.addValidationError("SMT Deal Number not captured");

		if (this.cashConcessionForm.controls['motivation'].value)
			cashConcession.concession.motivation = this.cashConcessionForm.controls['motivation'].value;
		else
			this.addValidationError("Motivation not captured");

		if (this.cashConcessionForm.controls['comments'].value)
			cashConcession.concession.comments = this.cashConcessionForm.controls['comments'].value;

		const concessions = <FormArray>this.cashConcessionForm.controls['concessionItemRows'];

		for (let concessionFormItem of concessions.controls) {
			if (!cashConcession.cashConcessionDetails)
				cashConcession.cashConcessionDetails = [];

			let cashConcessionDetail = new CashConcessionDetail();

			if (!isNew && concessionFormItem.get('cashConcessionDetailId').value)
				cashConcessionDetail.cashConcessionDetailId = concessionFormItem.get('cashConcessionDetailId').value;

			if (!isNew && concessionFormItem.get('concessionDetailId').value)
				cashConcessionDetail.concessionDetailId = concessionFormItem.get('concessionDetailId').value;

			if (concessionFormItem.get('channelType').value) {
				cashConcessionDetail.channelTypeId = concessionFormItem.get('channelType').value.id;
			} else {
				this.addValidationError("Channel type not selected");
			}

			if (concessionFormItem.get('accountNumber').value) {
				cashConcessionDetail.legalEntityId = concessionFormItem.get('accountNumber').value.legalEntityId;
				cashConcessionDetail.legalEntityAccountId = concessionFormItem.get('accountNumber').value.legalEntityAccountId;
			} else {
				this.addValidationError("Client account not selected");
			}

			if (concessionFormItem.get('tableNumber').value) {
				cashConcessionDetail.tableNumberId = concessionFormItem.get('tableNumber').value.id;
				cashConcessionDetail.adValorem = concessionFormItem.get('tableNumber').value.adValorem;

				if (concessionFormItem.get('tableNumber').value.baseRate)
					cashConcessionDetail.baseRate = concessionFormItem.get('tableNumber').value.baseRate;
			} else {
				this.addValidationError("Table Number not selected");
			}

			if (concessionFormItem.get('accrualType').value) {
				cashConcessionDetail.accrualTypeId = concessionFormItem.get('accrualType').value.id;
			} else {
				this.addValidationError("Accrual type not selected");
			}

			if (concessionFormItem.get('expiryDate').value)
				cashConcessionDetail.expiryDate = new Date(concessionFormItem.get('expiryDate').value);

			cashConcession.cashConcessionDetails.push(cashConcessionDetail);
		}

		const conditions = <FormArray>this.cashConcessionForm.controls['conditionItemsRows'];

		for (let conditionFormItem of conditions.controls) {
			if (!cashConcession.concessionConditions)
				cashConcession.concessionConditions = [];

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

			cashConcession.concessionConditions.push(concessionCondition);
		}

		return cashConcession;
	}

	getBackgroundColour(rowIndex: number) {
		const control = <FormArray>this.cashConcessionForm.controls['concessionItemRows'];

		if (String(control.controls[rowIndex].get('isExpired').value) == "true") {
			return "#EC7063";
		}

		if (String(control.controls[rowIndex].get('isExpiring').value) == "true") {
			return "#F5B041";
		}

		return "";
	}

	goBack() {
		this.location.back();
	}

	ngOnDestroy() {
		this.sub.unsubscribe();
	}

	bcmApproveConcession() {
		this.isLoading = true;

		this.errorMessage = null;
		this.validationError = null;

		var cashConcession = this.getCashConcession(false);
		cashConcession.concession.subStatus = "PCM Pending";
		cashConcession.concession.bcmUserId = this.cashConcession.currentUser.id;

		if (!cashConcession.concession.comments) {
			cashConcession.concession.comments = "Forwarded";
		}

		if (!this.validationError) {
			this.cashConcessionService.postUpdateCashData(cashConcession).subscribe(entity => {
				this.canBcmApprove = false;
				this.saveMessage = entity.concession.referenceNumber;
				this.isLoading = false;
				this.cashConcession = entity;
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

		var cashConcession = this.getCashConcession(false);
		cashConcession.concession.status = "Declined";
		cashConcession.concession.subStatus = "BCM Declined";
		cashConcession.concession.bcmUserId = this.cashConcession.currentUser.id;

		if (!cashConcession.concession.comments) {
			cashConcession.concession.comments = "Declined";
		}

		if (!this.validationError) {
			this.cashConcessionService.postUpdateCashData(cashConcession).subscribe(entity => {
				console.log("data saved");
				this.canBcmApprove = false;
				this.saveMessage = entity.concession.referenceNumber;
				this.isLoading = false;
				this.cashConcession = entity;
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

		var cashConcession = this.getCashConcession(false);

		if (this.hasChanges) {
			cashConcession.concession.status = "Pending";

			if (this.cashConcession.currentUser.isHO) {
				cashConcession.concession.subStatus = "HO Approved With Changes";
				cashConcession.concession.hoUserId = this.cashConcession.currentUser.id;
			} else {
				cashConcession.concession.subStatus = "PCM Approved With Changes";
				cashConcession.concession.pcmUserId = this.cashConcession.currentUser.id;
			}

			if (!cashConcession.concession.comments) {
				cashConcession.concession.comments = "Approved With Changes";
			}
		} else {
			cashConcession.concession.status = "Approved";

			if (this.cashConcession.currentUser.isHO) {
				cashConcession.concession.subStatus = "HO Approved";
				cashConcession.concession.hoUserId = this.cashConcession.currentUser.id;
			} else {
				cashConcession.concession.subStatus = "PCM Approved";
				cashConcession.concession.pcmUserId = this.cashConcession.currentUser.id;
			}

			if (!cashConcession.concession.comments) {
				cashConcession.concession.comments = "Approved";
			}
		}

		if (!this.validationError) {
			this.cashConcessionService.postUpdateCashData(cashConcession).subscribe(entity => {
				console.log("data saved");
				this.canPcmApprove = false;
				this.saveMessage = entity.concession.referenceNumber;
				this.isLoading = false;
				this.cashConcession = entity;
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

		var cashConcession = this.getCashConcession(false);

		cashConcession.concession.status = "Declined";

		if (this.cashConcession.currentUser.isHO) {
			cashConcession.concession.subStatus = "HO Declined";
			cashConcession.concession.hoUserId = this.cashConcession.currentUser.id;
		} else {
			cashConcession.concession.subStatus = "PCM Declined";
			cashConcession.concession.pcmUserId = this.cashConcession.currentUser.id;
		}

		if (!cashConcession.concession.comments) {
			cashConcession.concession.comments = "Declined";
		}

		if (!this.validationError) {
			this.cashConcessionService.postUpdateCashData(cashConcession).subscribe(entity => {
				console.log("data saved");
				this.canPcmApprove = false;
				this.saveMessage = entity.concession.referenceNumber;
				this.isLoading = false;
				this.cashConcession = entity;
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

			this.cashConcessionService.postExtendConcession(this.concessionReferenceId).subscribe(entity => {
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
				this.cashConcession = entity;
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

		this.cashConcessionForm.controls['motivation'].setValue('');
	}

	saveConcession() {
		this.isLoading = true;
		this.errorMessage = null;
		this.validationError = null;

		var cashConcession = this.getCashConcession(true);

		cashConcession.concession.status = "Pending";
		cashConcession.concession.subStatus = "BCM Pending";
		cashConcession.concession.type = "Existing";
		cashConcession.concession.referenceNumber = this.concessionReferenceId;

		if (!this.validationError) {
			this.cashConcessionService.postChildConcession(cashConcession, this.editType).subscribe(entity => {
				console.log("data saved");
				this.isEditing = false;
				this.saveMessage = entity.concession.childReferenceNumber;
				this.cashConcession = entity;
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

		var cashConcession = this.getCashConcession(true);

		cashConcession.concession.status = "Pending";
		cashConcession.concession.subStatus = "BCM Pending";
		cashConcession.concession.referenceNumber = this.concessionReferenceId;

		if (!this.validationError) {
			this.cashConcessionService.postRecallCashData(cashConcession).subscribe(entity => {
				console.log("data saved");
				this.isRecalling = false;
				this.saveMessage = entity.concession.referenceNumber;
				this.cashConcession = entity;
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

		var cashConcession = this.getCashConcession(false);
		cashConcession.concession.status = "Approved With Changes";
		cashConcession.concession.subStatus = "Requestor Accepted Changes";
		cashConcession.concession.requestorId = this.cashConcession.currentUser.id;

		if (!cashConcession.concession.comments) {
			cashConcession.concession.comments = "Accepted Changes";
		}

		if (!this.validationError) {
			this.cashConcessionService.postUpdateCashData(cashConcession).subscribe(entity => {
				console.log("data saved");
				this.canApproveChanges = false;
				this.saveMessage = entity.concession.referenceNumber;
				this.isLoading = false;
				this.cashConcession = entity;
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

		var cashConcession = this.getCashConcession(false);
		cashConcession.concession.status = "Declined";
		cashConcession.concession.subStatus = "Requestor Declined Changes";
		cashConcession.concession.requestorId = this.cashConcession.currentUser.id;

		if (!cashConcession.concession.comments) {
			cashConcession.concession.comments = "Declined Changes";
		}

		if (!this.validationError) {
			this.cashConcessionService.postUpdateCashData(cashConcession).subscribe(entity => {
				console.log("data saved");
				this.canApproveChanges = false;
				this.saveMessage = entity.concession.referenceNumber;
				this.isLoading = false;
				this.cashConcession = entity;
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