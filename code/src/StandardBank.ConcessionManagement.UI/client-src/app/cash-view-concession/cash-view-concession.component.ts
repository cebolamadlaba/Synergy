import { DatePipe, Location } from '@angular/common';
import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable } from "rxjs";
import { ConcessionStatus } from '../constants/concession-status';
import { ConcessionSubStatus } from '../constants/concession-sub-status';
import { ConcessionTypes } from '../constants/concession-types';
import { AccrualType } from "../models/accrual-type";
import { CashConcession } from "../models/cash-concession";
import { CashConcessionDetail } from "../models/cash-concession-detail";
import { extendConcessionModel } from "../models/extendConcessionModel";
import { CashFinancial } from "../models/cash-financial";
import { ChannelType } from "../models/channel-type";
import { ClientAccount } from "../models/client-account";
import { Concession } from "../models/concession";
import { ConcessionComment } from "../models/concession-comment";
import { ConditionType } from "../models/condition-type";
import { LegalEntity } from "../models/legal-entity";
import { Period } from "../models/period";
import { PeriodType } from "../models/period-type";
import { RiskGroup } from "../models/risk-group";
import { TableNumber } from "../models/table-number";
import { BaseComponentService } from '../services/base-component.service';
import { CashBaseService } from '../services/cash-base.service';
import { CashConcessionService } from "../services/cash-concession.service";
import { FileService } from '../services/file.service';
import { LookupDataService } from "../services/lookup-data.service";
import { UserConcessionsService } from "../services/user-concessions.service";
import { EditTypeEnum } from '../models/edit-type-enum';

@Component({
    selector: 'app-cash-view-concession',
    templateUrl: './cash-view-concession.component.html',
    styleUrls: ['./cash-view-concession.component.css'],
    providers: [DatePipe]
})
export class CashViewConcessionComponent extends CashBaseService implements OnInit, OnDestroy {

    concessionReferenceId: string;
    private sub: any;
    errorMessage: String;
    saveMessage: String;
    warningMessage: String;
    observableRiskGroup: Observable<RiskGroup>;
    riskGroup: RiskGroup;
    riskGroupNumber: number;
    sapbpid: number;
    legalEntity: LegalEntity;
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
    isApproved = false;
    showHide = false;

    subHeading: string;
    title: string;

    observablePeriods: Observable<Period[]>;
    periods: Period[];

    observablePeriodTypes: Observable<PeriodType[]>;
    periodTypes: PeriodType[];

    observableConditionTypes: Observable<ConditionType[]>;
    conditionTypes: ConditionType[];

    observableClientAccounts: Observable<ClientAccount[]>;

    observableAccrualTypes: Observable<AccrualType[]>;

    observableChannelTypes: Observable<ChannelType[]>;

    observableCashConcession: Observable<CashConcession>;
    cashConcession: CashConcession;

    observableTableNumbers: Observable<TableNumber[]>;

    observableCashFinancial: Observable<CashFinancial>;
    cashFinancial: CashFinancial;

    constructor(private route: ActivatedRoute,
        formBuilder: FormBuilder,
        private location: Location,
        datepipe: DatePipe,
        @Inject(LookupDataService) private lookupDataService,
        @Inject(CashConcessionService) private cashConcessionService,
        @Inject(UserConcessionsService) private userConcessionsService,
        fileService: FileService,
        private baseComponentService: BaseComponentService) {
        super();
        this.formBuilder = formBuilder;
        this.datepipe = datepipe;
        this.fileService = fileService;
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
            this.sapbpid = +params['sapbpid'];
            this.concessionReferenceId = params['concessionReferenceId'];
        });

        this.cashConcessionForm = this.formBuilder.group({
            concessionItemRows: this.formBuilder.array([this.initConcessionItemRowsUpdate()]),
            conditionItemsRows: this.formBuilder.array([]),
            smtDealNumber: new FormControl(),
            motivation: new FormControl(),
            comments: new FormControl()
        });

        this.getInitialData();

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

    getCashConcessionItemRows(): FormArray {
        return <FormArray>this.cashConcessionForm.controls['concessionItemRows'];
    }

    populateForm() {
        if (this.concessionReferenceId) {
            this.observableCashConcession = this.cashConcessionService.getCashConcessionData(this.concessionReferenceId);
            this.observableCashConcession.subscribe(cashConcession => {
                this.cashConcession = cashConcession;

                this.populateFormFromConcession(cashConcession);

                this.isLoading = false;
            }, error => {
                this.isLoading = false;
                this.errorMessage = <any>error;
            });
        }
    }

    populateFormFromConcession(cashConcession) {
        if (cashConcession.concession.status == ConcessionStatus.Pending && cashConcession.concession.subStatus == ConcessionSubStatus.BCMPending) {
            this.canBcmApprove = cashConcession.currentUser.canBcmApprove;
        }

        if (cashConcession.concession.status == ConcessionStatus.Pending && cashConcession.concession.subStatus == ConcessionSubStatus.PCMPending) {
            if (this.cashConcession.currentUser.isHO) {
                this.canPcmApprove = cashConcession.currentUser.canPcmApprove
            } else {
                this.canPcmApprove = cashConcession.currentUser.canPcmApprove && cashConcession.currentUser.canApprove;
            }

            // Removed as per SBSA.Anthony's request - 2019-07-15
            this.canEdit = cashConcession.currentUser.canPcmApprove;
        }

        //if it's still pending and the user is a requestor then they can recall it
        if (cashConcession.concession.status == ConcessionStatus.Pending && cashConcession.concession.subStatus == ConcessionSubStatus.BCMPending) {
            this.canRecall = cashConcession.currentUser.canRequest && cashConcession.concession.isAENumberLinkedAccountExecutiveOrAssistant;
        }

        if (cashConcession.concession.status == ConcessionStatus.Pending &&
            (cashConcession.concession.subStatus == ConcessionSubStatus.PCMApprovedWithChanges || cashConcession.concession.subStatus == ConcessionSubStatus.HOApprovedWithChanges)) {
            this.canApproveChanges = cashConcession.currentUser.canRequest && cashConcession.concession.isAENumberLinkedAccountExecutiveOrAssistant;
        }

        if (cashConcession.concession.status === ConcessionStatus.Approved ||
            cashConcession.concession.status === ConcessionStatus.ApprovedWithChanges) {
            this.isApproved = true;
        }

        //if the concession is set to can extend and the user is a requestor, then they can extend or renew it
        this.canExtend = cashConcession.concession.canExtend && cashConcession.currentUser.canRequest;
        this.canRenew = cashConcession.concession.canRenew && cashConcession.currentUser.canRequest;

        //set the resubmit and update permissions
        //can only update when concession is not "due for expiry"
        this.canResubmit = cashConcession.concession.canResubmit && cashConcession.currentUser.canRequest;
        this.canUpdate = !this.canRenew && cashConcession.concession.canUpdate && cashConcession.currentUser.canRequest;

        this.canArchive = cashConcession.concession.canArchive && cashConcession.currentUser.canRequest;
        this.isInProgressExtension = cashConcession.concession.isInProgressExtension;
        this.isInProgressRenewal = cashConcession.concession.isInProgressRenewal;

        this.cashConcessionForm.controls['smtDealNumber'].setValue(this.cashConcession.concession.smtDealNumber);
        this.cashConcessionForm.controls['motivation'].setValue(this.cashConcession.concession.motivation);

        let rowIndex = 0;

        for (let cashConcessionDetail of this.cashConcession.cashConcessionDetails) {

            if (rowIndex != 0) {
                this.addNewConcessionRow(false);
            }

            const concessions = this.getCashConcessionItemRows();
            let currentConcession = concessions.controls[concessions.length - 1];

            currentConcession.get('cashConcessionDetailId').setValue(cashConcessionDetail.cashConcessionDetailId);
            currentConcession.get('concessionDetailId').setValue(cashConcessionDetail.concessionDetailId);

            let selectedChannelType = this.channelTypes.filter(_ => _.id == cashConcessionDetail.channelTypeId);
            currentConcession.get('channelType').setValue(selectedChannelType[0]);

            if (this.clientAccounts) {
                let selectedAccountNo = this.clientAccounts.filter(_ => _.legalEntityAccountId == cashConcessionDetail.legalEntityAccountId);
                currentConcession.get('accountNumber').setValue(selectedAccountNo[0]);
            }

            if (cashConcessionDetail.baseRate)
                currentConcession.get('baseRate').setValue(cashConcessionDetail.baseRate.toFixed(2));

            if (cashConcessionDetail.adValorem)
                currentConcession.get('adValorem').setValue(cashConcessionDetail.adValorem.toFixed(3));

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

            currentCondition.get('interestRate').setValue(this.baseComponentService.formatDecimal(concessionCondition.interestRate));
            currentCondition.get('volume').setValue(concessionCondition.conditionVolume);
            currentCondition.get('value').setValue(concessionCondition.conditionValue);
            currentCondition.get('conditionComment').setValue(concessionCondition.conditionComment);

            let selectedPeriodType = this.periodTypes.filter(_ => _.id == concessionCondition.periodTypeId);
            currentCondition.get('periodType').setValue(selectedPeriodType[0]);

            let selectedPeriod = this.periods.filter(_ => _.id == concessionCondition.periodId);
            currentCondition.get('period').setValue(selectedPeriod[0]);

            rowIndex++;
        }

        this.changearray = this.lookupDataService.checkforLC(this.cashConcession.concession.status, this.cashConcession.concession.subStatus, cashConcession.concession.concessionComments);

    }

    getInitialData() {
        if (this.riskGroupNumber != null && this.riskGroupNumber != 0) {
            Observable.forkJoin([
                this.lookupDataService.getChannelTypes(),
                this.lookupDataService.getPeriods(),
                this.lookupDataService.getPeriodTypes(),
                this.lookupDataService.getConditionTypes(),
                this.lookupDataService.getAccrualTypes(),
                this.lookupDataService.getTableNumbers(ConcessionTypes.Cash),
                this.lookupDataService.getRiskGroup(this.riskGroupNumber),
                this.lookupDataService.getClientAccountsConcessionType(this.riskGroupNumber, this.sapbpid, ConcessionTypes.Cash),
                this.cashConcessionService.getCashFinancial(this.riskGroupNumber)
            ]).subscribe(results => {
                this.setInitialData(results, true);
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        }
        else if (this.sapbpid != null && this.sapbpid != 0) {
            Observable.forkJoin([
                this.lookupDataService.getChannelTypes(),
                this.lookupDataService.getPeriods(),
                this.lookupDataService.getPeriodTypes(),
                this.lookupDataService.getConditionTypes(),
                this.lookupDataService.getAccrualTypes(),
                this.lookupDataService.getTableNumbers(ConcessionTypes.Cash),
                this.lookupDataService.getLegalEntity(this.sapbpid),
                this.lookupDataService.getClientAccountsConcessionType(this.riskGroupNumber, this.sapbpid, ConcessionTypes.Cash),

            ]).subscribe(results => {
                this.setInitialData(results, false);
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        }
    }
    setInitialData(results: {}[], isForRiskGroup: boolean) {
        this.channelTypes = <any>results[0];
        this.periods = <any>results[1];
        this.periodTypes = <any>results[2];
        this.conditionTypes = <any>results[3];
        this.accrualTypes = <any>results[4];
        this.tableNumbers = <any>results[5];

        if (isForRiskGroup) {
            this.riskGroup = <any>results[6];
            this.cashFinancial = <any>results[8];
            this.subHeading = this.riskGroup.name;
            this.title = this.riskGroup.number.toString();
        }
        else {
            this.legalEntity = <any>results[6];
            this.subHeading = this.legalEntity.customerName;
            this.title = this.legalEntity.customerNumber;
        }
        this.clientAccounts = <any>results[7];

        this.populateForm();
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
            conditionComment: [''],
            periodType: [''],
            period: ['']
        });
    }

    addNewConcessionRow(isClickEvent: boolean) {
        const control = this.getCashConcessionItemRows();
        var newRow = this.initConcessionItemRowsUpdate();
        newRow.controls['accrualType'].setValue(this.accrualTypes[0]);
        if (isClickEvent) {
            if (control != null && control.length > 0) {
                let expiryDate = control.controls[0].get('expiryDate').value;
                if (expiryDate != null) {
                    newRow.controls['expiryDate'].setValue(expiryDate);
                }
            }
        }
        control.push(newRow);
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
        if (confirm("Are you sure you want to remove this row?")) {
            const control = this.getCashConcessionItemRows();
            control.removeAt(index);
        }
    }

    deleteConditionRow(index: number) {
        const control = <FormArray>this.cashConcessionForm.controls['conditionItemsRows'];
        control.removeAt(index);

        this.selectedConditionTypes.splice(index, 1);
    }

    onExpiryDateChanged(itemrow) {

        if (this.cashConcession.concession.dateOpened) {
            var formattedDateOpened = this.datepipe.transform(this.cashConcession.concession.dateOpened, 'yyyy-MM-dd');
        }

        var validationErrorMessage = this.baseComponentService.expiringDateDifferenceValidationForView(itemrow.controls['expiryDate'].value, formattedDateOpened);
        if (validationErrorMessage != null) {
            this.addValidationError(validationErrorMessage);
        }
    }

    conditionTypeChanged(rowIndex) {
        const control = <FormArray>this.cashConcessionForm.controls['conditionItemsRows'];
        this.selectedConditionTypes[rowIndex] = control.controls[rowIndex].get('conditionType').value;

        let currentCondition = control.controls[rowIndex];

        currentCondition.get('conditionProduct').setValue(null);
        currentCondition.get('interestRate').setValue(null);
        currentCondition.get('volume').setValue(null);
        currentCondition.get('value').setValue(null);
    }

    getCashConcession(isNew: boolean): CashConcession {
        var cashConcession = new CashConcession();
        cashConcession.concession = new Concession();
        cashConcession.concession.concessionType = ConcessionTypes.Cash;
        if (this.riskGroup)
            cashConcession.concession.riskGroupId = this.riskGroup.id;
        if (this.legalEntity)
            cashConcession.concession.legalEntityId = this.legalEntity.id;
        cashConcession.concession.referenceNumber = this.concessionReferenceId;

        if (this.cashConcessionForm.controls['smtDealNumber'].value)
            cashConcession.concession.smtDealNumber = this.cashConcessionForm.controls['smtDealNumber'].value;
        else
            this.addValidationError("SMT Deal Number not captured");

        if (this.cashConcessionForm.controls['comments'].value)
            cashConcession.concession.comments = this.cashConcessionForm.controls['comments'].value;

        if (this.cashConcessionForm.controls['motivation'].value)
            cashConcession.concession.motivation = this.cashConcessionForm.controls['motivation'].value;
        else
            cashConcession.concession.motivation = '.';

        const concessions = this.getCashConcessionItemRows();

        let hasChannelType: boolean = false;
        let hasLegalEntityId: boolean = false;
        let hasLegalEntityAccountId: boolean = false;

        for (let concessionFormItem of concessions.controls) {
            if (!cashConcession.cashConcessionDetails)
                cashConcession.cashConcessionDetails = [];

            let cashConcessionDetail = new CashConcessionDetail();

            if (concessionFormItem.get('expiryDate').value && concessionFormItem.get('expiryDate').value != "") {

                if (!this.baseComponentService.isAppprovingOrDeclining) {
                    this.onExpiryDateChanged(concessionFormItem);
                }

                cashConcessionDetail.expiryDate = new Date(concessionFormItem.get('expiryDate').value);
            }
            else {
                this.addValidationError("Expiry date not selected");
            }

            if (!isNew && concessionFormItem.get('cashConcessionDetailId').value)
                cashConcessionDetail.cashConcessionDetailId = concessionFormItem.get('cashConcessionDetailId').value;

            if (!isNew && concessionFormItem.get('concessionDetailId').value)
                cashConcessionDetail.concessionDetailId = concessionFormItem.get('concessionDetailId').value;

            if (concessionFormItem.get('channelType').value) {
                cashConcessionDetail.channelTypeId = concessionFormItem.get('channelType').value.id;
                hasChannelType = true;
            } else {
                this.addValidationError("Channel type not selected");
            }

            if (concessionFormItem.get('accountNumber').value) {
                cashConcessionDetail.legalEntityId = concessionFormItem.get('accountNumber').value.legalEntityId;
                cashConcessionDetail.legalEntityAccountId = concessionFormItem.get('accountNumber').value.legalEntityAccountId;
                hasLegalEntityId = true;
                hasLegalEntityAccountId = true;
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

            cashConcession.cashConcessionDetails.push(cashConcessionDetail);

            if (hasChannelType && hasLegalEntityId && hasLegalEntityAccountId) {
                let hasDuplicates = this.baseComponentService.HasDuplicateConcessionAccountChannel(
                    cashConcession.cashConcessionDetails,
                    concessionFormItem.get('channelType').value.id,
                    concessionFormItem.get('accountNumber').value.legalEntityId,
                    concessionFormItem.get('accountNumber').value.legalEntityAccountId);

                if (hasDuplicates) {
                    this.addValidationError("Duplicate Account / Channel pricing found. Please select different account.");

                    break;
                }
            }
        }

        const conditions = <FormArray>this.cashConcessionForm.controls['conditionItemsRows'];

        let concessionConditionReturnObject = this.baseComponentService.getConsessionConditionData(conditions, cashConcession.concessionConditions, this.validationError);
        cashConcession.concessionConditions = concessionConditionReturnObject.concessionConditions;
        this.validationError = concessionConditionReturnObject.validationError;

        super.checkConcessionExpiryDate(cashConcession);

        return cashConcession;
    }

    getBackgroundColour(rowIndex: number) {
        const control = this.getCashConcessionItemRows();

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
        this.baseComponentService.isAppprovingOrDeclining = true;

        var cashConcession = this.getCashConcession(false);
        cashConcession.concession.subStatus = ConcessionSubStatus.PCMPending;
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
        this.baseComponentService.isAppprovingOrDeclining = true;

        var cashConcession = this.getCashConcession(false);
        cashConcession.concession.status = ConcessionStatus.Declined;
        cashConcession.concession.subStatus = ConcessionSubStatus.BCMDeclined;
        cashConcession.concession.bcmUserId = this.cashConcession.currentUser.id;

        if (!cashConcession.concession.comments) {
            cashConcession.concession.comments = ConcessionStatus.Declined;
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
        this.baseComponentService.isAppprovingOrDeclining = true;

        var cashConcession = this.getCashConcession(false);

        if (this.hasChanges) {
            cashConcession.concession.status = ConcessionStatus.Pending;

            if (this.cashConcession.currentUser.isHO) {
                cashConcession.concession.subStatus = ConcessionSubStatus.HOApprovedWithChanges;
                cashConcession.concession.hoUserId = this.cashConcession.currentUser.id;
            } else {
                cashConcession.concession.subStatus = ConcessionSubStatus.PCMApprovedWithChanges;
                cashConcession.concession.pcmUserId = this.cashConcession.currentUser.id;
            }

            if (!cashConcession.concession.comments) {
                cashConcession.concession.comments = ConcessionStatus.ApprovedWithChanges;
            }

            cashConcession.concession.concessionComments = this.GetChanges(cashConcession.concession.id);

        } else {
            cashConcession.concession.status = ConcessionStatus.Approved;

            if (this.cashConcession.currentUser.isHO) {
                cashConcession.concession.subStatus = ConcessionSubStatus.HOApproved;
                cashConcession.concession.hoUserId = this.cashConcession.currentUser.id;
            } else {
                cashConcession.concession.subStatus = ConcessionSubStatus.PCMApproved;
                cashConcession.concession.pcmUserId = this.cashConcession.currentUser.id;
            }

            if (!cashConcession.concession.comments) {
                cashConcession.concession.comments = ConcessionStatus.Approved;
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

        const concessions = this.getCashConcessionItemRows();

        //this is detailed line items,  but not yet the controls
        for (let concessionFormItem of concessions.controls) {

            let controls = (<FormGroup>concessionFormItem).controls;

            for (const fieldname in controls) {

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
        this.baseComponentService.isAppprovingOrDeclining = true;

        var cashConcession = this.getCashConcession(false);

        cashConcession.concession.status = ConcessionStatus.Declined;

        if (this.cashConcession.currentUser.isHO) {
            cashConcession.concession.subStatus = ConcessionSubStatus.HODeclined;
            cashConcession.concession.hoUserId = this.cashConcession.currentUser.id;
        } else {
            cashConcession.concession.subStatus = ConcessionSubStatus.PCMDeclined;
            cashConcession.concession.pcmUserId = this.cashConcession.currentUser.id;
        }

        if (!cashConcession.concession.comments) {
            cashConcession.concession.comments = ConcessionStatus.Declined;
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

             var extendConceModel = new extendConcessionModel()
                extendConceModel.concessionReferenceId = this.concessionReferenceId;

            if (this.cashConcessionForm.controls['motivation'].value)
                extendConceModel.motivation = this.cashConcessionForm.controls['motivation'].value;
            else
                this.addValidationError("Motivation not captured");
                 this.isLoading = false;

            if(!this.validationError) {
         
                this.cashConcessionService.postExtendConcession(extendConceModel).subscribe(entity => {
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
                    this.errorMessage = null;
                    this.validationError = null;
                }, error => {
                    this.errorMessage = <any>error;
                    this.isLoading = false;
                        });
            }
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

        if (editType == EditTypeEnum.Renew) { //|| editType == EditTypeEnum.UpdateApproved) {
            const concessions = this.getCashConcessionItemRows();
            for (let concessionFormItem of concessions.controls) {
                // Existing ExpiryDate: ExpiryDate must be set 12 months from the existing ExpiryDate.
                if (concessionFormItem.get('expiryDate').value) {
                    let expiryDate = new Date(concessionFormItem.get('expiryDate').value);
                    expiryDate = new Date(expiryDate.setFullYear(expiryDate.getFullYear() + 1));
                    concessionFormItem.get('expiryDate').setValue(this.datepipe.transform(expiryDate, 'yyyy-MM-dd'));
                }
            }
        }
    }

    saveConcession() {
        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;

        var cashConcession = this.getCashConcession(true);

        cashConcession.concession.status = ConcessionStatus.Pending;
        cashConcession.concession.subStatus = ConcessionSubStatus.BCMPending;
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


    saveUpdatedConcession() {
        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;

        var cashConcession = this.getCashConcession(true);

        cashConcession.concession.type = "Existing";
        cashConcession.concession.referenceNumber = this.concessionReferenceId;

        if (!this.validationError) {
            this.cashConcessionService.postUpdateCashData(cashConcession).subscribe(entity => {
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

        this.userConcessionsService.recallConcession(this.concessionReferenceId).subscribe(entity => {
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

        var cashConcession = this.getCashConcession(false);

        cashConcession.concession.status = ConcessionStatus.Pending;
        cashConcession.concession.subStatus = ConcessionSubStatus.BCMPending;
        cashConcession.concession.referenceNumber = this.concessionReferenceId;

        if (!this.validationError) {
            this.cashConcessionService.postRecallCashData(cashConcession).subscribe(entity => {

                this.populateFormFromConcession(entity);
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
        this.baseComponentService.isAppprovingOrDeclining = true;

        var cashConcession = this.getCashConcession(false);
        cashConcession.concession.status = ConcessionStatus.ApprovedWithChanges;
        cashConcession.concession.subStatus = ConcessionSubStatus.RequestorAcceptedChanges;
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
        this.baseComponentService.isAppprovingOrDeclining = true;

        var cashConcession = this.getCashConcession(false);
        cashConcession.concession.status = ConcessionStatus.Declined;
        cashConcession.concession.subStatus = ConcessionSubStatus.RequestorDeclinedChanges;
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

    archiveConcessiondetail(concessionDetailId: number) {

        if (confirm("Please note that the account will be put back to standard pricing. Are you sure you want to delete the concession item ?")) {
            this.isLoading = true;
            this.errorMessage = null;

            this.userConcessionsService.deactivateConcessionDetailed(concessionDetailId).subscribe(entity => {

                this.warningMessage = "Concession item has been deleted, and account put back to standard pricing.";

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
                this.warningMessage = "Concession has been deleted, and account put back to standard pricing.";

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
    }

    canEditSMTDealNumber() {
        return (this.isRecalling || this.canEdit) ? null : '';
    }

    isMotivationEnabled() {
     
        if (!this.canExtend) {
           return this.motivationEnabled ? null : '';
        }
    }

    getNumberInput(input) {
        this.cashConcessionForm.controls['smtDealNumber'].setValue(this.baseComponentService.removeLetters(input.value));
    }

    disableField(index: number, fieldname: string) {
        let canUpdateExpiryDate: boolean = true;

        if (fieldname == "expiryDate" && this.editType != null &&
            (this.editType == EditTypeEnum.Renew || this.editType == EditTypeEnum.UpdateApproved)) {
            {
                canUpdateExpiryDate = false;
            }
        }

        return this.disableFieldBase(
            this.selectedConditionTypes[index],
            fieldname,
            this.canEdit && canUpdateExpiryDate,
            this.canEdit != null
        );
    }
}
