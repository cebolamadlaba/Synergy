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
import { Concession } from "../models/concession";
import { extendConcessionModel } from "../models/extendConcessionModel";

import { ConcessionCondition } from "../models/concession-condition";
import { TableNumber } from "../models/table-number";
import { UserConcessionsService } from "../services/user-concessions.service";
import { ConcessionComment } from "../models/concession-comment";

import { DecimalPipe } from '@angular/common';
import { ConcessionTypes } from '../constants/concession-types';
import { ConcessionStatus } from '../constants/concession-status';
import { ConcessionSubStatus } from '../constants/concession-sub-status';

import { BolConcession } from "../models/bol-concession";
import { BolConcessionDetail } from "../models/bol-concession-detail";
import { BolConcessionService } from "../services/bol-concession.service";
import { BolView } from "../models/bol-view";
import { UserService } from "../services/user.service";

import { BolChargeCodeType } from "../models/bol-chargecodetype";
import { BolChargeCode } from "../models/bol-chargecode";
import { BolChargeCodeRelationship } from "../models/bol-chargeCodeRelationship";
import { LegalEntityBOLUser } from "../models/legal-entity-bol-user";
import { LegalEntity } from "../models/legal-entity";
import { EditTypeEnum } from '../models/edit-type-enum';

import { BaseComponentService } from '../services/base-component.service';
import * as moment from 'moment';
import { MOnthEnum } from '../models/month-enum';
import { BolConcessionBaseService } from '../services/bol-concession-base.service';

@Component({
    selector: 'app-bol-view-concession',
    templateUrl: './bol-view-concession.component.html',
    styleUrls: ['./bol-view-concession.component.css'],
    providers: [DatePipe]
})
export class BolViewConcessionComponent extends BolConcessionBaseService implements OnInit, OnDestroy {

    concessionReferenceId: string;
    private sub: any;
    errorMessage: String;
    saveMessage: String;
    warningMessage: String;
    observableRiskGroup: Observable<RiskGroup>;

    observableBolView: Observable<BolView>;
    bolView: BolView = new BolView();

    riskGroup: RiskGroup;
    riskGroupNumber: number;

    legalEntity: LegalEntity;
    sapbpid: number;

    entityName: string;
    entityNumber: string;

    public bolConcessionForm: FormGroup;
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
    showMotivationDisclaimer = false;
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


    observablePeriods: Observable<Period[]>;
    periods: Period[];

    observablePeriodTypes: Observable<PeriodType[]>;
    periodTypes: PeriodType[];

    observable: Observable<BolChargeCodeType[]>;
    bolchargecodetypes: BolChargeCodeType[];

    observableBolChargeCodes: Observable<BolChargeCode[]>;
    bolchargecodes: BolChargeCode[];

    observableBolChargeCodeRelationships: Observable<BolChargeCodeRelationship[]>;
    bolChargeCodeRelationships: BolChargeCodeRelationship[];

    observableLegalEntityBOLUsers: Observable<LegalEntityBOLUser[]>;
    legalentitybolusers: LegalEntityBOLUser[];

    observableConditionTypes: Observable<ConditionType[]>;
    conditionTypes: ConditionType[];

    observableClientAccounts: Observable<ClientAccount[]>;
    clientAccounts: ClientAccount[];

    observableBolConcession: Observable<BolConcession>;
    bolConcession: BolConcession;

    selectedProducts: BolChargeCodeType[];

    constructor(private route: ActivatedRoute,
        private formBuilder: FormBuilder,
        private location: Location,
        private datepipe: DatePipe,
        @Inject(LookupDataService) private lookupDataService,
        @Inject(UserConcessionsService) private userConcessionsService,
        @Inject(BolConcessionService) private bolConcessionService,
        private baseComponentService: BaseComponentService) {

        super();
        this.riskGroup = new RiskGroup();
        this.bolchargecodetypes = [new BolChargeCodeType()];
        this.bolchargecodes = [new BolChargeCode()];
        this.bolChargeCodeRelationships = [new BolChargeCodeRelationship()];
        this.legalentitybolusers = [new LegalEntityBOLUser()];
        this.periods = [new Period()];
        this.periodTypes = [new PeriodType()];

        this.selectedProducts = [new BolChargeCodeType()];

        this.conditionTypes = [new ConditionType()];
        this.selectedConditionTypes = [new ConditionType()];
        this.clientAccounts = [new ClientAccount()];

        this.bolView.riskGroup = new RiskGroup();
        this.bolView.bolConcessions = [new BolConcession()];
        this.bolView.bolConcessions[0].concession = new Concession();

        this.bolConcession = new BolConcession();
        this.bolConcession.concession = new Concession();
        this.bolConcession.concession.concessionComments = [new ConcessionComment()];
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
            this.sapbpid = +params['sapbpid'];
            this.concessionReferenceId = params['concessionReferenceId'];
        });

        this.getInitialData();

        this.bolConcessionForm = this.formBuilder.group({
            concessionItemRows: this.formBuilder.array([this.initConcessionItemRows()]),
            conditionItemsRows: this.formBuilder.array([]),
            smtDealNumber: new FormControl(),
            motivation: new FormControl(),
            comments: new FormControl()
        });

        this.bolConcessionForm.valueChanges.subscribe((value: any) => {
            if (this.bolConcessionForm.dirty) {
                //if the captured comments is still the same as the comments that means
                //the user has changed something else on the form
                if (this.capturedComments == value.comments) {
                    this.hasChanges = true;
                }

                this.capturedComments = value.comments;
            }
        });
    }

    getBolConcessionItemRows(): FormArray {
        return <FormArray>this.bolConcessionForm.controls['concessionItemRows'];
    }

    getInitialData() {
        if (this.riskGroupNumber !== 0) {
            Observable.forkJoin([
                this.lookupDataService.getConditionTypes(),
                this.lookupDataService.getBOLChargeCodeTypes(),
                this.lookupDataService.getBOLChargeCodes(this.riskGroupNumber),
                this.lookupDataService.getLegalEntityBOLUsers(this.riskGroupNumber),
                this.lookupDataService.getPeriods(),
                this.lookupDataService.getPeriodTypes(),
                this.lookupDataService.getRiskGroup(this.riskGroupNumber),
                this.lookupDataService.getBOLChargeCodeRelationships()
            ]).subscribe(results => {
                this.setInitialData(results, true);
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        }
        else if (this.sapbpid != null && this.sapbpid != 0) {
            Observable.forkJoin([
                this.lookupDataService.getConditionTypes(),
                this.lookupDataService.getBOLChargeCodeTypes(),
                this.lookupDataService.getBOLChargeCodesAll(),
                this.lookupDataService.getLegalEntityBOLUsersBySAPBPID(this.sapbpid),
                this.lookupDataService.getPeriods(),
                this.lookupDataService.getPeriodTypes(),
                this.lookupDataService.getLegalEntity(this.sapbpid),
                this.lookupDataService.getBOLChargeCodeRelationships()
            ]).subscribe(results => {
                this.setInitialData(results, false);
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        }
    }

    setInitialData(results: {}[], isForRiskGroup: boolean) {
        if (isForRiskGroup) {
            this.riskGroup = <any>results[6];
            this.entityName = this.riskGroup.name;
            this.entityNumber = this.riskGroup.number.toString();
        }
        else {
            this.legalEntity = <any>results[6];
            this.entityName = this.legalEntity.customerName;
            this.entityNumber = this.legalEntity.customerNumber;
        }

        this.conditionTypes = <any>results[0];
        this.bolchargecodetypes = <any>results[1];
        this.bolchargecodes = <any>results[2];
        this.legalentitybolusers = <any>results[3];
        this.periods = <any>results[4];
        this.periodTypes = <any>results[5];
        this.bolChargeCodeRelationships = <any>results[7];

        this.populateForm();
    }

    populateForm() {
        if (this.concessionReferenceId) {
            this.observableBolConcession = this.bolConcessionService.getBolConcessionData(this.concessionReferenceId);
            this.observableBolConcession.subscribe(bolConcession => {
                this.bolConcession = bolConcession;

                if (bolConcession.concession.status == ConcessionStatus.Pending && bolConcession.concession.subStatus == ConcessionSubStatus.BCMPending) {
                    this.canBcmApprove = bolConcession.currentUser.canBcmApprove;
                }

                if (bolConcession.concession.status == ConcessionStatus.Pending && bolConcession.concession.subStatus == ConcessionSubStatus.PCMPending) {
                    if (this.bolConcession.currentUser.isHO) {
                        this.canPcmApprove = bolConcession.currentUser.canPcmApprove
                    } else {
                        this.canPcmApprove = bolConcession.currentUser.canPcmApprove && bolConcession.currentUser.canApprove;
                    }

                    // Removed as per SBSA.Anthony's request - 2019-07-15
                    this.canEdit = bolConcession.currentUser.canPcmApprove;
                }

                //if it's still pending and the user is a requestor then they can recall it
                if (bolConcession.concession.status == ConcessionStatus.Pending && bolConcession.concession.subStatus == ConcessionSubStatus.BCMPending) {
                    this.canRecall = bolConcession.currentUser.canRequest && bolConcession.concession.isAENumberLinkedAccountExecutiveOrAssistant;
                }

                if (bolConcession.concession.status == ConcessionStatus.Pending &&
                    (bolConcession.concession.subStatus == ConcessionSubStatus.PCMApprovedWithChanges || bolConcession.concession.subStatus == ConcessionSubStatus.HOApprovedWithChanges)) {
                    this.canApproveChanges = bolConcession.currentUser.canRequest && bolConcession.concession.isAENumberLinkedAccountExecutiveOrAssistant;
                }

                if (bolConcession.concession.status === ConcessionStatus.Approved ||
                    bolConcession.concession.status === ConcessionStatus.ApprovedWithChanges) {
                    this.isApproved = true;
                }

                //if the concession is set to can extend and the user is a requestor, then they can extend or renew it
                this.canExtend = bolConcession.concession.canExtend && bolConcession.currentUser.canRequest;               
                this.canRenew = bolConcession.concession.canRenew && bolConcession.currentUser.canRequest;
               

                 //set the resubmit and update permissions
                 //can only update when concession is not "due for expiry"
                this.canResubmit = bolConcession.concession.canResubmit && bolConcession.currentUser.canRequest;
                this.canUpdate = !this.canRenew && bolConcession.concession.canUpdate && bolConcession.currentUser.canRequest;

                this.canArchive = bolConcession.concession.canArchive && bolConcession.currentUser.canRequest;
                this.isInProgressExtension = bolConcession.concession.isInProgressExtension;
                this.isInProgressRenewal = bolConcession.concession.isInProgressRenewal;

                this.bolConcessionForm.controls['smtDealNumber'].setValue(this.bolConcession.concession.smtDealNumber);
                this.bolConcessionForm.controls['motivation'].setValue(this.bolConcession.concession.motivation);

                let rowIndex = 0;

                for (let bolConcessionDetail of this.bolConcession.bolConcessionDetails) {

                    if (rowIndex != 0) {
                        this.addNewConcessionRow(false);
                    }

                    const concessions = this.getBolConcessionItemRows();
                    let currentConcession = concessions.controls[concessions.length - 1];

                    currentConcession.get('bolConcessionDetailId').setValue(bolConcessionDetail.bolConcessionDetailId);
                    currentConcession.get('concessionDetailId').setValue(bolConcessionDetail.concessionDetailId);

                    if (bolConcessionDetail.loadedRate) {
                        var loadedRate = bolConcessionDetail.loadedRate.replace(',', '.');
                        bolConcessionDetail.loadedRate = this.baseComponentService.formatDecimalThree(Number(loadedRate));
                        currentConcession.get('unitcharge').setValue(bolConcessionDetail.loadedRate);
                    }

                    if (bolConcessionDetail.approvedRate) {
                        var approvedRate = bolConcessionDetail.loadedRate.replace(',', '.');
                        bolConcessionDetail.approvedRate = this.baseComponentService.formatDecimalThree(Number(approvedRate));
                        currentConcession.get('unitchargeApproved').setValue(bolConcessionDetail.approvedRate);
                    }                        

                    let selectedBOLUser = this.legalentitybolusers.filter(_ => _.pkLegalEntityBOLUserId == bolConcessionDetail.fkLegalEntityBOLUserId);
                    currentConcession.get('userid').setValue(selectedBOLUser[0]);

                    let selectedChargeCode = this.bolchargecodes.filter(_ => _.pkChargeCodeId == bolConcessionDetail.fkChargeCodeId);
                    currentConcession.get('chargecode').setValue(selectedChargeCode[0]);

                    if (bolConcessionDetail.fkChargeCodeTypeId == null) {
                        let chargecodetypeid = selectedChargeCode[0].fkChargeCodeTypeId.valueOf();

                        let selectedChargeCodeType = this.bolchargecodetypes.filter(_ => _.pkChargeCodeTypeId == chargecodetypeid);
                        currentConcession.get('product').setValue(selectedChargeCodeType[0]);

                        let chargecodes = this.bolchargecodes.filter(re => re.fkChargeCodeTypeId == selectedChargeCodeType[0].pkChargeCodeTypeId);
                        this.selectedProducts[rowIndex].bolchargecodes = chargecodes;                        
                    }
                    else {
                        let selectedChargeCodeType = this.bolchargecodetypes.filter(_ => _.pkChargeCodeTypeId == bolConcessionDetail.fkChargeCodeTypeId);
                        currentConcession.get('product').setValue(selectedChargeCodeType[0]);

                        var selectedProductRelationships = this.bolChargeCodeRelationships.filter(cr => cr.fkChargeCodeTypeId == bolConcessionDetail.fkChargeCodeTypeId);

                        this.selectedProducts[rowIndex].bolchargecodes = this.bolchargecodes.filter(re =>
                            selectedProductRelationships.find(({ fkChargeCodeId }) => re.pkChargeCodeId == fkChargeCodeId));
                    }                 

                    if (bolConcessionDetail.expiryDate) {
                        var formattedExpiryDate = this.datepipe.transform(bolConcessionDetail.expiryDate, 'yyyy-MM-dd');
                        currentConcession.get('expiryDate').setValue(formattedExpiryDate);
                    }

                    if (bolConcessionDetail.dateApproved) {
                        var formattedDateApproved = this.datepipe.transform(bolConcessionDetail.dateApproved, 'yyyy-MM-dd');
                        currentConcession.get('dateApproved').setValue(formattedDateApproved);
                    }

                    currentConcession.get('isExpired').setValue(bolConcessionDetail.isExpired);
                    currentConcession.get('isExpiring').setValue(bolConcessionDetail.isExpiring);     

                    rowIndex++;
                }

                rowIndex = 0;

                for (let concessionCondition of this.bolConcession.concessionConditions) {
                    this.addNewConditionRow();

                    const conditions = <FormArray>this.bolConcessionForm.controls['conditionItemsRows'];
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
                    currentCondition.get('conditionComment').setValue(concessionCondition.conditionComment);

                    let selectedPeriodType = this.periodTypes.filter(_ => _.id == concessionCondition.periodTypeId);
                    currentCondition.get('periodType').setValue(selectedPeriodType[0]);

                    let selectedPeriod = this.periods.filter(_ => _.id == concessionCondition.periodId);
                    currentCondition.get('period').setValue(selectedPeriod[0]);

                    rowIndex++;
                }

                this.changearray = this.lookupDataService.checkforLC(this.bolConcession.concession.status, this.bolConcession.concession.subStatus, bolConcession.concession.concessionComments);

                this.isLoading = false;
            }, error => {
                this.isLoading = false;
                this.errorMessage = <any>error;
            });
        }
    }

    initConcessionItemRows() {

        this.selectedProducts.push(new BolChargeCodeType());

        return this.formBuilder.group({
            bolConcessionDetailId: [''],
            concessionDetailId: [''],
            product: [{ value: '', disabled: true }],
            chargecode: [{ value: '', disabled: true }],
            unitcharge: [{ value: '', disabled: true }],
            unitchargeApproved: [{ value: '', disabled: true }],
            userid: [{ value: '', disabled: true }],
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
            conditionComment: [''],
            periodType: [''],
            period: ['']
        });
    }

    addNewConcessionRow(isClickEvent: boolean) {

        const control = this.getBolConcessionItemRows();
        var newRow = this.initConcessionItemRows();

        var length = control.controls.length;

        if (this.bolchargecodetypes)
            newRow.controls['product'].setValue(this.bolchargecodetypes[0]);

        if (this.legalentitybolusers)
            newRow.controls['userid'].setValue(this.legalentitybolusers[0]);

        if (isClickEvent) {
            if (control != null && control.length > 0) {
                let expiryDate = control.controls[0].get('expiryDate').value;
                if (expiryDate != null) {
                    newRow.controls['expiryDate'].setValue(expiryDate);
                }
            }
        }

        control.push(newRow);
        this.productTypeChanged(length);

    }

    addNewConditionRow() {
        const control = <FormArray>this.bolConcessionForm.controls['conditionItemsRows'];
        control.push(this.initConditionItemRows());
    }

    addNewConditionRowIfNone() {
        const control = <FormArray>this.bolConcessionForm.controls['conditionItemsRows'];
        if (control.length == 0)
            control.push(this.initConditionItemRows());
    }

    deleteConcessionRow(index: number) {
        if (confirm("Please note that the account will be put back to standard pricing. Are you sure you want to delete this concession ?")) {

            this.selectedProducts.splice(index, 1);


            const control = this.getBolConcessionItemRows();
            control.removeAt(index);
        }
    }

    deleteConditionRow(index: number) {
        const control = <FormArray>this.bolConcessionForm.controls['conditionItemsRows'];
        control.removeAt(index);

        this.selectedConditionTypes.splice(index, 1);
    }

    conditionTypeChanged(rowIndex) {
        const control = <FormArray>this.bolConcessionForm.controls['conditionItemsRows'];
        this.selectedConditionTypes[rowIndex] = control.controls[rowIndex].get('conditionType').value;

        let currentCondition = control.controls[rowIndex];

        currentCondition.get('conditionProduct').setValue(null);
        currentCondition.get('interestRate').setValue(null);
        currentCondition.get('volume').setValue(null);
        currentCondition.get('value').setValue(null);
    }

    onExpiryDateChanged(itemrow) {

        if (this.bolConcession.concession.dateOpened) {
            var formattedDateOpened = this.datepipe.transform(this.bolConcession.concession.dateOpened, 'yyyy-MM-dd');
        }

        var validationErrorMessage = this.baseComponentService.expiringDateDifferenceValidationForView(itemrow.controls['expiryDate'].value, formattedDateOpened);
        if (validationErrorMessage != null) {
            this.addValidationError(validationErrorMessage);
        }
    }

    productTypeChanged(rowIndex) {

        const control = this.getBolConcessionItemRows();

        let currentProduct = control.controls[rowIndex];
        var selectedproduct = currentProduct.get('product').value;

        var selectedProductRelationships = this.bolChargeCodeRelationships.filter(cr => cr.fkChargeCodeTypeId == selectedproduct.pkChargeCodeTypeId);
        this.selectedProducts[rowIndex].bolchargecodes = this.bolchargecodes.filter(re =>
            selectedProductRelationships.find(({ fkChargeCodeId }) => re.pkChargeCodeId == fkChargeCodeId));

        currentProduct.get('chargecode').setValue(this.selectedProducts[rowIndex].bolchargecodes[0]);
    }

    getBolConcession(isNew: boolean): BolConcession {
        var bolConcession = new BolConcession();
        bolConcession.concession = new Concession();

        if (this.riskGroup)
            bolConcession.concession.riskGroupId = this.riskGroup.id;
        if (this.legalEntity)
            bolConcession.concession.legalEntityId = this.legalEntity.id;

        bolConcession.concession.referenceNumber = this.concessionReferenceId;
        bolConcession.concession.concessionType = ConcessionTypes.BOL;

        if (this.bolConcessionForm.controls['smtDealNumber'].value)
            bolConcession.concession.smtDealNumber = this.bolConcessionForm.controls['smtDealNumber'].value;
        else
            this.addValidationError("SMT Deal Number not captured");

        if (this.bolConcessionForm.controls['motivation'].value)
            bolConcession.concession.motivation = this.bolConcessionForm.controls['motivation'].value;
        else
            bolConcession.concession.motivation = '.';

        if (this.bolConcessionForm.controls['comments'].value)
            bolConcession.concession.comments = this.bolConcessionForm.controls['comments'].value;


        const concessions = this.getBolConcessionItemRows();

        let hasTypeId: boolean = false;
        let hasLegalEntityId: boolean = false;
        let hasLegalEntityAccountId: boolean = false;

        for (let concessionFormItem of concessions.controls) {
            if (!bolConcession.bolConcessionDetails)
                bolConcession.bolConcessionDetails = [];

            let bolConcessionDetail = new BolConcessionDetail();

            if (!isNew && concessionFormItem.get('bolConcessionDetailId').value)
                bolConcessionDetail.bolConcessionDetailId = concessionFormItem.get('bolConcessionDetailId').value;

            if (!isNew && concessionFormItem.get('concessionDetailId').value)
                bolConcessionDetail.concessionDetailId = concessionFormItem.get('concessionDetailId').value;

            if (concessionFormItem.get('product').value) {
                bolConcessionDetail.fkChargeCodeTypeId = concessionFormItem.get('product').value.pkChargeCodeTypeId;
            } else {
                this.addValidationError("Product not selected");
            }

            if (concessionFormItem.get('chargecode').value) {
                bolConcessionDetail.fkChargeCodeId = concessionFormItem.get('chargecode').value.pkChargeCodeId;
                hasTypeId = true;
            } else {
                this.addValidationError("Charge code not selected");
            }

            if (concessionFormItem.get('unitcharge').value || concessionFormItem.get('unitcharge').value == 0) {
                bolConcessionDetail.loadedRate = concessionFormItem.get('unitcharge').value;
            } else {
                this.addValidationError("Rate not entered");
            }

            if (concessionFormItem.get('userid').value) {
                bolConcessionDetail.fkLegalEntityBOLUserId = concessionFormItem.get('userid').value.pkLegalEntityBOLUserId;
                bolConcessionDetail.legalEntityId = concessionFormItem.get('userid').value.legalEntityId;
                bolConcessionDetail.legalEntityAccountId = concessionFormItem.get('userid').value.legalEntityAccountId;
                hasLegalEntityId = true;
                hasLegalEntityAccountId = true;
            } else {
                this.addValidationError("User ID not selected");
            }

            if (concessionFormItem.get('expiryDate').value && !this.baseComponentService.isAppprovingOrDeclining)
                this.onExpiryDateChanged(concessionFormItem);
            bolConcessionDetail.expiryDate = new Date(concessionFormItem.get('expiryDate').value);

            bolConcession.bolConcessionDetails.push(bolConcessionDetail);

            if (hasTypeId && hasLegalEntityId && hasLegalEntityAccountId) {
                let hasDuplicates = this.baseComponentService.HasDuplicateConcessionUserIdChargeCode(
                    bolConcession.bolConcessionDetails,
                    concessionFormItem.get('chargecode').value.pkChargeCodeId,
                    concessionFormItem.get('userid').value.pkLegalEntityBOLUserId);

                if (hasDuplicates) {
                    this.addValidationError("Duplicate Account / Product pricing found. Please select different account.");
                    break;
                }
            }
        }

        const conditions = <FormArray>this.bolConcessionForm.controls['conditionItemsRows'];

        let concessionConditionReturnObject = this.baseComponentService.getConsessionConditionData(conditions, bolConcession.concessionConditions, this.validationError);
        bolConcession.concessionConditions = concessionConditionReturnObject.concessionConditions;
        this.validationError = concessionConditionReturnObject.validationError;

        super.checkConcessionExpiryDate(bolConcession);

        return bolConcession;
    }

    getBackgroundColour(rowIndex: number) {
        const control = this.getBolConcessionItemRows();

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
        this.baseComponentService.isAppprovingOrDeclining = true;
        this.errorMessage = null;
        this.validationError = null;

        var bolConcession = this.getBolConcession(false);
        bolConcession.concession.subStatus = ConcessionSubStatus.PCMPending;
        bolConcession.concession.bcmUserId = this.bolConcession.currentUser.id;

        if (!bolConcession.concession.comments) {
            bolConcession.concession.comments = "Forwarded";
        }

        if (!this.validationError) {

            this.canBcmApprove = false;

            this.bolConcessionService.postUpdateBolData(bolConcession).subscribe(entity => {
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
                this.bolConcession = entity;
                this.canEdit = false;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
                this.canBcmApprove = true;
            });
        } else {
            this.isLoading = false;
        }
    }

    bcmDeclineConcession() {
        this.isLoading = true;

        this.baseComponentService.isAppprovingOrDeclining = true;

        this.errorMessage = null;
        this.validationError = null;

        var bolConcession = this.getBolConcession(false);
        bolConcession.concession.status = ConcessionStatus.Declined;
        bolConcession.concession.subStatus = ConcessionSubStatus.BCMDeclined;
        bolConcession.concession.bcmUserId = this.bolConcession.currentUser.id;

        if (!bolConcession.concession.comments) {
            bolConcession.concession.comments = ConcessionStatus.Declined;
        }

        this.canBcmApprove = false;

        if (!this.validationError) {
            this.bolConcessionService.postUpdateBolData(bolConcession).subscribe(entity => {
                console.log("data saved");

                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
                this.bolConcession = entity;
                this.canEdit = false;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
                this.canBcmApprove = true;
            });
        } else {
            this.isLoading = false;
        }
    }

    pcmApproveConcession() {
        this.isLoading = true;
        this.baseComponentService.isAppprovingOrDeclining = true;
        this.errorMessage = null;
        this.validationError = null;

        var bolConcession = this.getBolConcession(false);

        if (this.hasChanges) {
            bolConcession.concession.status = ConcessionStatus.Pending;

            if (this.bolConcession.currentUser.isHO) {
                bolConcession.concession.subStatus = ConcessionSubStatus.HOApprovedWithChanges;
                bolConcession.concession.hoUserId = this.bolConcession.currentUser.id;
            } else {
                bolConcession.concession.subStatus = ConcessionSubStatus.PCMApprovedWithChanges;
                bolConcession.concession.pcmUserId = this.bolConcession.currentUser.id;
            }

            if (!bolConcession.concession.comments) {
                bolConcession.concession.comments = ConcessionStatus.ApprovedWithChanges;
            }

            bolConcession.concession.concessionComments = this.GetChanges(bolConcession.concession.id);

        } else {
            bolConcession.concession.status = ConcessionStatus.Approved;

            if (this.bolConcession.currentUser.isHO) {
                bolConcession.concession.subStatus = ConcessionSubStatus.HOApproved;
                bolConcession.concession.hoUserId = this.bolConcession.currentUser.id;
            } else {
                bolConcession.concession.subStatus = ConcessionSubStatus.PCMApproved;
                bolConcession.concession.pcmUserId = this.bolConcession.currentUser.id;
            }

            if (!bolConcession.concession.comments) {
                bolConcession.concession.comments = ConcessionStatus.Approved;
            }
        }

        if (!this.validationError) {
            this.bolConcessionService.postUpdateBolData(bolConcession).subscribe(entity => {
                console.log("data saved");
                this.canPcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
                this.bolConcession = entity;
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

        const concessions = this.getBolConcessionItemRows();

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

        const conditions = <FormArray>this.bolConcessionForm.controls['conditionItemsRows'];

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

        var bolConcession = this.getBolConcession(false);

        bolConcession.concession.status = ConcessionStatus.Declined;

        if (this.bolConcession.currentUser.isHO) {
            bolConcession.concession.subStatus = ConcessionSubStatus.HODeclined;
            bolConcession.concession.hoUserId = this.bolConcession.currentUser.id;
        } else {
            bolConcession.concession.subStatus = ConcessionSubStatus.PCMDeclined;
            bolConcession.concession.pcmUserId = this.bolConcession.currentUser.id;
        }

        if (!bolConcession.concession.comments) {
            bolConcession.concession.comments = ConcessionStatus.Declined;
        }

        if (!this.validationError) {
            this.bolConcessionService.postUpdateBolData(bolConcession).subscribe(entity => {
                console.log("data saved");
                this.canPcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
                this.bolConcession = entity;
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


        if (this.canExtend && this.motivationEnabled == false) {
            this.showMotivationDisclaimer = true;
            this.motivationEnabled = true;
            this.bolConcessionForm.controls['motivation'].setValue('');

        } else {

            this.showMotivationDisclaimer = false;
            this.validationError = null;

            var extendConceModel = new extendConcessionModel()
            extendConceModel.concessionReferenceId = this.concessionReferenceId;

            if (this.bolConcessionForm.controls['motivation'].value) {
                extendConceModel.motivation = this.bolConcessionForm.controls['motivation'].value;
            } else {
                this.addValidationError("Motivation not captured");
                this.isLoading = false;
            }

            if (!this.validationError) {

                if (confirm("Are you sure you want to extend this concession?")) {
                    this.isLoading = true;
                    this.errorMessage = null;
                    this.validationError = null;

                    this.bolConcessionService.postExtendConcession(extendConceModel).subscribe(entity => {
                        console.log("data saved");
                        this.canBcmApprove = false;
                        this.canBcmApprove = false;
                        this.canExtend = false;
                        this.canRenew = false;
                        this.canRecall = false;
                        this.canUpdate = false;
                        this.motivationEnabled = false;
                        this.canArchive = false;
                        this.saveMessage = entity.concession.childReferenceNumber;
                        this.isLoading = false;
                        this.bolConcession = entity;
                    }, error => {
                        this.errorMessage = <any>error;
                        this.isLoading = false;
                    });
                }
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

        this.bolConcessionForm.controls['motivation'].setValue('');

        if (editType == EditTypeEnum.Renew) {
            const concessions = this.getBolConcessionItemRows();
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

        var bolConcession = this.getBolConcession(true);

        bolConcession.concession.status = ConcessionStatus.Pending;
        bolConcession.concession.subStatus = ConcessionSubStatus.BCMPending;
        bolConcession.concession.type = "Existing";
        bolConcession.concession.referenceNumber = this.concessionReferenceId;

        if (!this.validationError) {
            this.bolConcessionService.postChildConcession(bolConcession, this.editType).subscribe(entity => {
                console.log("data saved");
                this.isEditing = false;
                this.saveMessage = entity.concession.childReferenceNumber;
                this.bolConcession = entity;
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

        var bolConcession = this.getBolConcession(true);

        bolConcession.concession.type = "Existing";
        bolConcession.concession.referenceNumber = this.concessionReferenceId;

        if (!this.validationError) {
            this.bolConcessionService.postUpdateBolData(bolConcession, this.editType).subscribe(entity => {
                console.log("data saved");
                this.isEditing = false;
                this.saveMessage = entity.concession.childReferenceNumber;
                this.bolConcession = entity;
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

        var bolConcession = this.getBolConcession(false);

        bolConcession.concession.status = ConcessionStatus.Pending;
        bolConcession.concession.subStatus = ConcessionSubStatus.BCMPending;
        bolConcession.concession.referenceNumber = this.concessionReferenceId;

        if (!this.validationError) {
            this.bolConcessionService.postRecallBolData(bolConcession).subscribe(entity => {
                console.log("data saved");
                this.isRecalling = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.bolConcession = entity;
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
        this.baseComponentService.isAppprovingOrDeclining = true;
        this.errorMessage = null;
        this.validationError = null;

        var bolConcession = this.getBolConcession(false);
        bolConcession.concession.status = ConcessionStatus.ApprovedWithChanges;
        bolConcession.concession.subStatus = ConcessionSubStatus.RequestorAcceptedChanges;
        bolConcession.concession.requestorId = this.bolConcession.currentUser.id;

        if (!bolConcession.concession.comments) {
            bolConcession.concession.comments = "Accepted Changes";
        }

        if (!this.validationError) {
            this.bolConcessionService.postUpdateBolData(bolConcession).subscribe(entity => {
                console.log("data saved");
                this.canApproveChanges = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
                this.bolConcession = entity;
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

        var bolConcession = this.getBolConcession(false);
        bolConcession.concession.status = ConcessionStatus.Declined;
        bolConcession.concession.subStatus = ConcessionSubStatus.RequestorDeclinedChanges;
        bolConcession.concession.requestorId = this.bolConcession.currentUser.id;

        if (!bolConcession.concession.comments) {
            bolConcession.concession.comments = "Declined Changes";
        }

        if (!this.validationError) {
            this.bolConcessionService.postUpdateBolData(bolConcession).subscribe(entity => {
                console.log("data saved");
                this.canApproveChanges = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
                this.bolConcession = entity;
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
    }

    getNumberInput(input) {
        this.bolConcessionForm.controls['smtDealNumber'].setValue(this.baseComponentService.removeLetters(input.value));
    }

    disableField(fieldname: string, index: number = null) {
        let canUpdateExpiryDate: boolean = true;

        if (fieldname == "expiryDate" && this.editType != null &&
            (this.editType == EditTypeEnum.Renew || this.editType == EditTypeEnum.UpdateApproved)) {
            {
                canUpdateExpiryDate = false;
            }
        }

        return this.disableFieldBase(
            fieldname,
            this.canEdit && canUpdateExpiryDate,
            index,
            this.selectedConditionTypes,
            this.isRecalling,
            this.motivationEnabled)
    }
}
