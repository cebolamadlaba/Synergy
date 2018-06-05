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

import { ConcessionCondition } from "../models/concession-condition";
import { TableNumber } from "../models/table-number";
import { UserConcessionsService } from "../services/user-concessions.service";
import { ConcessionComment } from "../models/concession-comment";

import { DecimalPipe } from '@angular/common';
import { ConcessionTypes } from '../constants/concession-types';
import { ConcessionStatus } from '../constants/concession-status';
import { ConcessionSubStatus } from '../constants/concession-sub-status';

import { UserService } from "../services/user.service";


import { TradeProduct } from "../models/trade-product";
import { TradeProductType } from "../models/trade-product-type";

import { TradeConcession } from "../models/trade-concession";
import { TradeConcessionDetail } from "../models/trade-concession-detail";
import { TradeConcessionService } from "../services/trade-concession.service";

import { TradeView } from "../models/trade-view";

@Component({
    selector: 'app-trade-view-concession',
    templateUrl: './trade-view-concession.component.html',
    styleUrls: ['./trade-view-concession.component.css'],
    providers: [DatePipe]
})
export class TradeViewConcessionComponent implements OnInit, OnDestroy {

    concessionReferenceId: string;
    private sub: any;
    errorMessage: String;
    validationError: String[];
    saveMessage: String;
    warningMessage: String;

    observableRiskGroup: Observable<RiskGroup>;
    observableTradeView: Observable<TradeView>;
    tradeView: TradeView = new TradeView();

    riskGroup: RiskGroup;
    riskGroupNumber: number;

    public tradeConcessionForm: FormGroup;
   
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

    observablePeriods: Observable<Period[]>;
    periods: Period[];

    observablePeriodTypes: Observable<PeriodType[]>;
    periodTypes: PeriodType[];

    observableTradeProductTypes: Observable<TradeProductType[]>;
    tradeproducttypes: TradeProductType[];

    observableTradeProducts: Observable<TradeProduct[]>;
    tradeproducts: TradeProduct[];

    selectedConditionTypes: ConditionType[];
    selectedProducts: TradeProduct[];

    observableConditionTypes: Observable<ConditionType[]>;
    conditionTypes: ConditionType[];

    observableClientAccounts: Observable<ClientAccount[]>;
    clientAccounts: ClientAccount[];

    observableTradeConcession: Observable<TradeConcession>;
    tradeConcession: TradeConcession;


    constructor(private route: ActivatedRoute,
        private formBuilder: FormBuilder,
        private location: Location,
        private datepipe: DatePipe,
        @Inject(LookupDataService) private lookupDataService,
        @Inject(UserConcessionsService) private userConcessionsService,
        @Inject(TradeConcessionService) private tradeConcessionService) {

        this.riskGroup = new RiskGroup();
        this.tradeproducttypes = [new TradeProductType()];
        this.tradeproducts = [new TradeProduct()];
        this.periods = [new Period()];
        this.periodTypes = [new PeriodType()];

        this.conditionTypes = [new ConditionType()];
        this.selectedConditionTypes = [new ConditionType()];
        this.selectedProducts = [new TradeProduct()];

        this.clientAccounts = [new ClientAccount()];

        this.tradeView.riskGroup = new RiskGroup();
        this.tradeView.tradeConcessions = [new TradeConcession()];
        this.tradeView.tradeConcessions[0].concession = new Concession();

        this.tradeConcession = new TradeConcession();
        this.tradeConcession.concession = new Concession();
        this.tradeConcession.concession.concessionComments = [new ConcessionComment()];



    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
            this.concessionReferenceId = params['concessionReferenceId'];
        });

        if (this.riskGroupNumber) {

            this.observableRiskGroup = this.lookupDataService.getRiskGroup(this.riskGroupNumber);
            this.observableRiskGroup.subscribe(riskGroup => this.riskGroup = riskGroup, error => this.errorMessage = <any>error);

            this.observableClientAccounts = this.lookupDataService.getClientAccounts(this.riskGroupNumber);
            this.observableClientAccounts.subscribe(clientAccounts => this.clientAccounts = clientAccounts, error => this.errorMessage = <any>error);
        }

        if (this.riskGroupNumber) {
            this.observableTradeView = this.tradeConcessionService.getTradeViewData(this.riskGroupNumber);
            this.observableTradeView.subscribe(bolView => {
                this.tradeView = bolView;
            }, error => {
                this.errorMessage = <any>error;
            });
        }

        this.tradeConcessionForm = this.formBuilder.group({
            concessionItemRows: this.formBuilder.array([this.initConcessionItemRows()]),
            conditionItemsRows: this.formBuilder.array([]),
            smtDealNumber: new FormControl(),
            motivation: new FormControl(),
            comments: new FormControl()
        });

        Observable.forkJoin([
            this.lookupDataService.getConditionTypes(),

            this.lookupDataService.getTradeProductTypes(),
            this.lookupDataService.getTradeProducts(),

            this.lookupDataService.getPeriods(),
            this.lookupDataService.getPeriodTypes()

        ]).subscribe(results => {

            this.conditionTypes = <any>results[0];
            this.tradeproducttypes = <any>results[1];
            this.tradeproducts = <any>results[2];

            this.periods = <any>results[3];
            this.periodTypes = <any>results[4];

            this.populateForm();
        },
            error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });

        this.tradeConcessionForm.valueChanges.subscribe((value: any) => {
            if (this.tradeConcessionForm.dirty) {
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
            this.observableTradeConcession = this.tradeConcessionService.getTradeConcessionData(this.concessionReferenceId);
            this.observableTradeConcession.subscribe(tradeConcession => {
                this.tradeConcession = tradeConcession;

                if (tradeConcession.concession.status == ConcessionStatus.Pending && tradeConcession.concession.subStatus == ConcessionSubStatus.BCMPending) {
                    this.canBcmApprove = tradeConcession.currentUser.canBcmApprove;
                }

                if (tradeConcession.concession.status == ConcessionStatus.Pending && tradeConcession.concession.subStatus == ConcessionSubStatus.PCMPending) {
                    if (this.tradeConcession.currentUser.isHO) {
                        this.canPcmApprove = tradeConcession.currentUser.canPcmApprove
                    } else {
                        this.canPcmApprove = tradeConcession.currentUser.canPcmApprove && tradeConcession.currentUser.canApprove;
                    }

                    if (!tradeConcession.concession.isInProgressExtension) {
                        this.canEdit = tradeConcession.currentUser.canPcmApprove;
                    }
                }

                //if it's still pending and the user is a requestor then they can recall it
                if (tradeConcession.concession.status == ConcessionStatus.Pending && tradeConcession.concession.subStatus == ConcessionSubStatus.BCMPending) {
                    this.canRecall = tradeConcession.currentUser.canRequest;
                }

                if (tradeConcession.concession.status == ConcessionStatus.Pending &&
                    (tradeConcession.concession.subStatus == ConcessionSubStatus.PCMApprovedWithChanges || tradeConcession.concession.subStatus == ConcessionSubStatus.HOApprovedWithChanges)) {
                    this.canApproveChanges = tradeConcession.currentUser.canRequest;
                }

                if (tradeConcession.concession.status === ConcessionStatus.Approved ||
                    tradeConcession.concession.status === ConcessionStatus.ApprovedWithChanges) {
                    this.isApproved = true;
                }

                //if the concession is set to can extend and the user is a requestor, then they can extend or renew it
                this.canExtend = tradeConcession.concession.canExtend && tradeConcession.currentUser.canRequest;
                this.canRenew = tradeConcession.concession.canRenew && tradeConcession.currentUser.canRequest;

                //set the resubmit and update permissions
                this.canResubmit = tradeConcession.concession.canResubmit && tradeConcession.currentUser.canRequest;
                this.canUpdate = tradeConcession.concession.canUpdate && tradeConcession.currentUser.canRequest;

                this.canArchive = tradeConcession.concession.canArchive && tradeConcession.currentUser.canRequest;
                this.isInProgressExtension = tradeConcession.concession.isInProgressExtension;
                this.isInProgressRenewal = tradeConcession.concession.isInProgressRenewal;

                this.tradeConcessionForm.controls['smtDealNumber'].setValue(this.tradeConcession.concession.smtDealNumber);
                this.tradeConcessionForm.controls['motivation'].setValue(this.tradeConcession.concession.motivation);

               

                let rowIndex = 0;

                for (let tradeConcessionDetail of this.tradeConcession.tradeConcessionDetails) {

                    if (rowIndex != 0) {
                        this.addNewConcessionRow();
                    }

                    const concessions = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];
                    let currentConcession = concessions.controls[concessions.length - 1];

                    currentConcession.get('tradeConcessionDetailId').setValue(tradeConcessionDetail.tradeConcessionDetailId);
                    currentConcession.get('concessionDetailId').setValue(tradeConcessionDetail.concessionDetailId);

                    //if (bolConcessionDetail.accountNumber)
                    //    currentConcession.get('accountNumber').setValue(bolConcessionDetail.accountNumber);

                    //if (bolConcessionDetail.approvedRate)
                    //    currentConcession.get('unitchargeApproved').setValue(bolConcessionDetail.approvedRate);

                    //let selectedBOLUser = this.legalentitybolusers.filter(_ => _.pkLegalEntityBOLUserId == bolConcessionDetail.fkLegalEntityBOLUserId);
                    //currentConcession.get('userid').setValue(selectedBOLUser[0]);

                    //let selectedChargeCode = this.bolchargecodes.filter(_ => _.pkChargeCodeId == bolConcessionDetail.fkChargeCodeId);
                    //currentConcession.get('chargecode').setValue(selectedChargeCode[0]);

                    //let selectedChargeCode2 = this.bolchargecodes.filter(_ => _.pkChargeCodeId == bolConcessionDetail.fkChargeCodeId);
                    //let chargecodetypeid = selectedChargeCode2[0].fkChargeCodeTypeId.valueOf();

                    //let selectedChargeCodeType = this.bolchargecodetypes.filter(_ => _.pkChargeCodeTypeId == chargecodetypeid);
                    //currentConcession.get('product').setValue(selectedChargeCodeType[0]);


                    if (tradeConcessionDetail.adValorem)
                        currentConcession.get('advalorem').setValue(tradeConcessionDetail.adValorem);



                    if (tradeConcessionDetail.communication)
                        currentConcession.get('communication').setValue(tradeConcessionDetail.communication);


                    if (tradeConcessionDetail.flatFee)
                        currentConcession.get('flatfee').setValue(tradeConcessionDetail.flatFee);


                    if (tradeConcessionDetail.currency)
                        currentConcession.get('currency').setValue(tradeConcessionDetail.currency);


                    if (tradeConcessionDetail.gbbNumber)
                        currentConcession.get('gbbnumber').setValue(tradeConcessionDetail.gbbNumber);

                    if (tradeConcessionDetail.term)
                        currentConcession.get('term').setValue(tradeConcessionDetail.term);

                    if (tradeConcessionDetail.establishmentFee)
                        currentConcession.get('estfee').setValue(tradeConcessionDetail.establishmentFee);

                    if (tradeConcessionDetail.approvedRate)
                        currentConcession.get('approvedRate').setValue(tradeConcessionDetail.approvedRate);

                    if (tradeConcessionDetail.loadedRate)
                        currentConcession.get('loadedRate').setValue(tradeConcessionDetail.loadedRate);

                    if (tradeConcessionDetail.min)
                        currentConcession.get('min').setValue(tradeConcessionDetail.min);


                    if (tradeConcessionDetail.max)
                        currentConcession.get('max').setValue(tradeConcessionDetail.max);

                    //this.selectedProducts[rowIndex] = sele[0];

                    if (tradeConcessionDetail.expiryDate) {
                        var formattedExpiryDate = this.datepipe.transform(tradeConcessionDetail.expiryDate, 'yyyy-MM-dd');
                        currentConcession.get('expiryDate').setValue(formattedExpiryDate);
                    }

                    if (tradeConcessionDetail.dateApproved) {
                        var formattedDateApproved = this.datepipe.transform(tradeConcessionDetail.dateApproved, 'yyyy-MM-dd');
                        currentConcession.get('dateApproved').setValue(formattedDateApproved);
                    }

                    currentConcession.get('isExpired').setValue(tradeConcessionDetail.isExpired);
                    currentConcession.get('isExpiring').setValue(tradeConcessionDetail.isExpiring);

                    rowIndex++;
                }

                rowIndex = 0;

                for (let concessionCondition of this.tradeConcession.concessionConditions) {
                    this.addNewConditionRow();

                    const conditions = <FormArray>this.tradeConcessionForm.controls['conditionItemsRows'];
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

        this.selectedProducts.push(new TradeProduct());

        return this.formBuilder.group({
           

            tradeConcessionDetailId: [''],
            concessionDetailId: [''],
            product: [{ value: '', disabled: true }],
            producttype: [''],
            accountnumber: [''],
            gbbnumber: [''],
        
            accountNumber: [''],
            term: [''],
            advalorem: [''],
            min: [''],
            max: [''],
            communication: [''],
            flatfee: [''],
            currency: [''],
            estfee: [''],
            approvedRate: [''],
            loadedRate: [''],
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
            expectedTurnoverValue: [''],
            periodType: [''],
            period: ['']
        });
    }

    addNewConcessionRow() {
        const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];
        var newRow = this.initConcessionItemRows();
        control.push(newRow);
    }

    addNewConditionRow() {
        const control = <FormArray>this.tradeConcessionForm.controls['conditionItemsRows'];
        control.push(this.initConditionItemRows());
    }

    addNewConditionRowIfNone() {
        const control = <FormArray>this.tradeConcessionForm.controls['conditionItemsRows'];
        if (control.length == 0)
            control.push(this.initConditionItemRows());
    }

    deleteConcessionRow(index: number) {
        if (confirm("Are you sure you want to remove this row?")) {
            const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];
            control.removeAt(index);
        }
    }

    deleteConditionRow(index: number) {
        const control = <FormArray>this.tradeConcessionForm.controls['conditionItemsRows'];
        control.removeAt(index);

        this.selectedConditionTypes.splice(index, 1);
    }

    conditionTypeChanged(rowIndex) {
        const control = <FormArray>this.tradeConcessionForm.controls['conditionItemsRows'];
        this.selectedConditionTypes[rowIndex] = control.controls[rowIndex].get('conditionType').value;

        let currentCondition = control.controls[rowIndex];

        currentCondition.get('interestRate').setValue(null);
        currentCondition.get('volume').setValue(null);
        currentCondition.get('value').setValue(null);
        currentCondition.get('expectedTurnoverValue').setValue(null);
    }

    productTypeChanged(rowIndex) {

        //const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];

        //let currentCondition = control.controls[rowIndex];

        //var selectedproduct = currentCondition.get('product').value;

        //this.bolchargecodesFiltered = this.bolchargecodes.filter(re => re.fkChargeCodeTypeId == selectedproduct.pkChargeCodeTypeId);
        


        const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];
        this.selectedProducts[rowIndex] = control.controls[rowIndex].get('product').value;

        let currentProduct = control.controls[rowIndex];
        var selectedproduct = currentProduct.get('product').value;

        this.selectedProducts[rowIndex].producttypes = this.tradeproducttypes.filter(re => re.tradeProductTypeID == selectedproduct.tradeProductTypeId);

        currentProduct.get('producttype').setValue(this.selectedProducts[rowIndex].producttypes[0]);

    

    }

    addValidationError(validationDetail) {
        if (!this.validationError)
            this.validationError = [];

        this.validationError.push(validationDetail);
    }


    //getBolConcession(isNew: boolean): BolConcession {
    //    var bolConcession = new BolConcession();
    //    bolConcession.concession = new Concession();
    //    bolConcession.concession.riskGroupId = this.riskGroup.id;
    //    bolConcession.concession.referenceNumber = this.concessionReferenceId;
    //    bolConcession.concession.concessionType = ConcessionTypes.BOL;

    //    if (this.tradeConcessionForm.controls['smtDealNumber'].value)
    //        bolConcession.concession.smtDealNumber = this.tradeConcessionForm.controls['smtDealNumber'].value;
    //    else
    //        this.addValidationError("SMT Deal Number not captured");

    //    if (this.tradeConcessionForm.controls['motivation'].value)
    //        bolConcession.concession.motivation = this.tradeConcessionForm.controls['motivation'].value;
    //    else
    //        this.addValidationError("Motivation not captured");

    //    const concessions = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];

    //    for (let concessionFormItem of concessions.controls) {
    //        if (!bolConcession.bolConcessionDetails)
    //            bolConcession.bolConcessionDetails = [];

    //        let bolConcessionDetail = new BolConcessionDetail();

    //        if (!isNew && concessionFormItem.get('bolConcessionDetailId').value)
    //            bolConcessionDetail.bolConcessionDetailId = concessionFormItem.get('bolConcessionDetailId').value;

    //        if (!isNew && concessionFormItem.get('concessionDetailId').value)
    //            bolConcessionDetail.concessionDetailId = concessionFormItem.get('concessionDetailId').value;

    //        if (concessionFormItem.get('product').value) {

    //        } else {
    //            this.addValidationError("Product not selected");
    //        }

    //        if (concessionFormItem.get('chargecode').value) {
    //            bolConcessionDetail.fkChargeCodeId = concessionFormItem.get('chargecode').value.pkChargeCodeId;

    //        } else {
    //            this.addValidationError("Charge code not selected");
    //        }

    //        if (concessionFormItem.get('unitcharge').value) {
    //            bolConcessionDetail.loadedRate = concessionFormItem.get('unitcharge').value;
    //        } else {
    //            this.addValidationError("Charge rate not entered");
    //        }

    //        if (concessionFormItem.get('userid').value) {
    //            bolConcessionDetail.fkLegalEntityBOLUserId = concessionFormItem.get('userid').value.pkLegalEntityBOLUserId;
    //            bolConcessionDetail.legalEntityId = concessionFormItem.get('userid').value.legalEntityId;
    //            bolConcessionDetail.legalEntityAccountId = concessionFormItem.get('userid').value.legalEntityAccountId;
    //        } else {
    //            this.addValidationError("User ID not selected");
    //        }

    //        if (concessionFormItem.get('expiryDate').value)
    //            bolConcessionDetail.expiryDate = new Date(concessionFormItem.get('expiryDate').value);

    //        bolConcession.bolConcessionDetails.push(bolConcessionDetail);
    //    }

    //    const conditions = <FormArray>this.tradeConcessionForm.controls['conditionItemsRows'];

    //    for (let conditionFormItem of conditions.controls) {

    //        if (!bolConcession.concessionConditions)
    //            bolConcession.concessionConditions = [];


    //        let concessionCondition = new ConcessionCondition();

    //        if (!isNew && conditionFormItem.get('concessionConditionId').value)
    //            concessionCondition.concessionConditionId = conditionFormItem.get('concessionConditionId').value;

    //        if (conditionFormItem.get('conditionType').value)
    //            concessionCondition.conditionTypeId = conditionFormItem.get('conditionType').value.id;
    //        else
    //            this.addValidationError("Condition type not selected");

    //        if (conditionFormItem.get('conditionProduct').value)
    //            concessionCondition.conditionProductId = conditionFormItem.get('conditionProduct').value.id;
    //        else
    //            this.addValidationError("Condition product not selected");

    //        if (conditionFormItem.get('interestRate').value)
    //            concessionCondition.interestRate = conditionFormItem.get('interestRate').value;

    //        if (conditionFormItem.get('volume').value)
    //            concessionCondition.conditionVolume = conditionFormItem.get('volume').value;

    //        if (conditionFormItem.get('value').value)
    //            concessionCondition.conditionValue = conditionFormItem.get('value').value;

    //        if (conditionFormItem.get('expectedTurnoverValue').value)
    //            concessionCondition.expectedTurnoverValue = conditionFormItem.get('expectedTurnoverValue').value;

    //        if (conditionFormItem.get('periodType').value) {
    //            concessionCondition.periodTypeId = conditionFormItem.get('periodType').value.id;
    //        } else {
    //            this.addValidationError("Period type not selected");
    //        }

    //        if (conditionFormItem.get('period').value) {
    //            concessionCondition.periodId = conditionFormItem.get('period').value.id;
    //        } else {
    //            this.addValidationError("Period not selected");
    //        }

    //        bolConcession.concessionConditions.push(concessionCondition);
    //    }

    //    return bolConcession;
    //}

    getBackgroundColour(rowIndex: number) {
        const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];

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

    //bcmApproveConcession() {
    //    this.isLoading = true;

    //    this.errorMessage = null;
    //    this.validationError = null;

    //    var bolConcession = this.getBolConcession(false);
    //    bolConcession.concession.subStatus = ConcessionSubStatus.PCMPending;
    //    bolConcession.concession.bcmUserId = this.bolConcession.currentUser.id;

    //    if (!bolConcession.concession.comments) {
    //        bolConcession.concession.comments = "Forwarded";
    //    }

    //    if (!this.validationError) {
    //        this.bolConcessionService.postUpdateBolData(bolConcession).subscribe(entity => {
    //            this.canBcmApprove = false;
    //            this.saveMessage = entity.concession.referenceNumber;
    //            this.isLoading = false;
    //            this.bolConcession = entity;
    //            this.canEdit = false;
    //        }, error => {
    //            this.errorMessage = <any>error;
    //            this.isLoading = false;
    //        });
    //    } else {
    //        this.isLoading = false;
    //    }
    //}

    //bcmDeclineConcession() {
    //    this.isLoading = true;

    //    this.errorMessage = null;
    //    this.validationError = null;

    //    var bolConcession = this.getBolConcession(false);
    //    bolConcession.concession.status = ConcessionStatus.Declined;
    //    bolConcession.concession.subStatus = ConcessionSubStatus.BCMDeclined;
    //    bolConcession.concession.bcmUserId = this.bolConcession.currentUser.id;

    //    if (!bolConcession.concession.comments) {
    //        bolConcession.concession.comments = ConcessionStatus.Declined;
    //    }

    //    if (!this.validationError) {
    //        this.bolConcessionService.postUpdateBolData(bolConcession).subscribe(entity => {
    //            console.log("data saved");
    //            this.canBcmApprove = false;
    //            this.saveMessage = entity.concession.referenceNumber;
    //            this.isLoading = false;
    //            this.bolConcession = entity;
    //            this.canEdit = false;
    //        }, error => {
    //            this.errorMessage = <any>error;
    //            this.isLoading = false;
    //        });
    //    } else {
    //        this.isLoading = false;
    //    }
    //}

    //pcmApproveConcession() {
    //    this.isLoading = true;

    //    this.errorMessage = null;
    //    this.validationError = null;

    //    var bolConcession = this.getBolConcession(false);

    //    if (this.hasChanges) {
    //        bolConcession.concession.status = ConcessionStatus.Pending;

    //        if (this.bolConcession.currentUser.isHO) {
    //            bolConcession.concession.subStatus = ConcessionSubStatus.HOApprovedWithChanges;
    //            bolConcession.concession.hoUserId = this.bolConcession.currentUser.id;
    //        } else {
    //            bolConcession.concession.subStatus = ConcessionSubStatus.PCMApprovedWithChanges;
    //            bolConcession.concession.pcmUserId = this.bolConcession.currentUser.id;
    //        }

    //        if (!bolConcession.concession.comments) {
    //            bolConcession.concession.comments = ConcessionStatus.ApprovedWithChanges;
    //        }
    //    } else {
    //        bolConcession.concession.status = ConcessionStatus.Approved;

    //        if (this.bolConcession.currentUser.isHO) {
    //            bolConcession.concession.subStatus = ConcessionSubStatus.HOApproved;
    //            bolConcession.concession.hoUserId = this.bolConcession.currentUser.id;
    //        } else {
    //            bolConcession.concession.subStatus = ConcessionSubStatus.PCMApproved;
    //            bolConcession.concession.pcmUserId = this.bolConcession.currentUser.id;
    //        }

    //        if (!bolConcession.concession.comments) {
    //            bolConcession.concession.comments = ConcessionStatus.Approved;
    //        }
    //    }

    //    if (!this.validationError) {
    //        this.bolConcessionService.postUpdateBolData(bolConcession).subscribe(entity => {
    //            console.log("data saved");
    //            this.canPcmApprove = false;
    //            this.saveMessage = entity.concession.referenceNumber;
    //            this.isLoading = false;
    //            this.bolConcession = entity;
    //            this.canEdit = false;
    //        }, error => {
    //            this.errorMessage = <any>error;
    //            this.isLoading = false;
    //        });
    //    } else {
    //        this.isLoading = false;
    //    }
    //}

    //pcmDeclineConcession() {
    //    this.isLoading = true;

    //    this.errorMessage = null;
    //    this.validationError = null;

    //    var bolConcession = this.getBolConcession(false);

    //    bolConcession.concession.status = ConcessionStatus.Declined;

    //    if (this.bolConcession.currentUser.isHO) {
    //        bolConcession.concession.subStatus = ConcessionSubStatus.HODeclined;
    //        bolConcession.concession.hoUserId = this.bolConcession.currentUser.id;
    //    } else {
    //        bolConcession.concession.subStatus = ConcessionSubStatus.PCMDeclined;
    //        bolConcession.concession.pcmUserId = this.bolConcession.currentUser.id;
    //    }

    //    if (!bolConcession.concession.comments) {
    //        bolConcession.concession.comments = ConcessionStatus.Declined;
    //    }

    //    if (!this.validationError) {
    //        this.bolConcessionService.postUpdateBolData(bolConcession).subscribe(entity => {
    //            console.log("data saved");
    //            this.canPcmApprove = false;
    //            this.saveMessage = entity.concession.referenceNumber;
    //            this.isLoading = false;
    //            this.bolConcession = entity;
    //            this.canEdit = false;
    //        }, error => {
    //            this.errorMessage = <any>error;
    //            this.isLoading = false;
    //        });
    //    } else {
    //        this.isLoading = false;
    //    }
    //}

    //extendConcession() {
    //    if (confirm("Are you sure you want to extend this concession?")) {
    //        this.isLoading = true;
    //        this.errorMessage = null;
    //        this.validationError = null;

    //        this.bolConcessionService.postExtendConcession(this.concessionReferenceId).subscribe(entity => {
    //            console.log("data saved");
    //            this.canBcmApprove = false;
    //            this.canBcmApprove = false;
    //            this.canExtend = false;
    //            this.canRenew = false;
    //            this.canRecall = false;
    //            this.canUpdate = false;
    //            this.canArchive = false;
    //            this.saveMessage = entity.concession.childReferenceNumber;
    //            this.isLoading = false;
    //            this.bolConcession = entity;
    //        }, error => {
    //            this.errorMessage = <any>error;
    //            this.isLoading = false;
    //        });
    //    }
    //}

    //editConcession(editType: string) {
    //    this.canBcmApprove = false;
    //    this.motivationEnabled = true;
    //    this.canBcmApprove = false;
    //    this.canExtend = false;
    //    this.canRenew = false;
    //    this.canRecall = false;
    //    this.isEditing = true;
    //    this.isRecalling = false;
    //    this.canEdit = true;
    //    this.editType = editType;
    //    this.canResubmit = false;
    //    this.canUpdate = false;
    //    this.canArchive = false;

    //    this.tradeConcessionForm.controls['motivation'].setValue('');
    //}

    //saveConcession() {
    //    this.isLoading = true;
    //    this.errorMessage = null;
    //    this.validationError = null;

    //    var bolConcession = this.getBolConcession(true);

    //    bolConcession.concession.status = ConcessionStatus.Pending;
    //    bolConcession.concession.subStatus = ConcessionSubStatus.BCMPending;
    //    bolConcession.concession.type = "Existing";
    //    bolConcession.concession.referenceNumber = this.concessionReferenceId;

    //    if (!this.validationError) {
    //        this.bolConcessionService.postChildConcession(bolConcession, this.editType).subscribe(entity => {
    //            console.log("data saved");
    //            this.isEditing = false;
    //            this.saveMessage = entity.concession.childReferenceNumber;
    //            this.bolConcession = entity;
    //            this.isLoading = false;
    //            this.canEdit = false;
    //            this.motivationEnabled = false;
    //        }, error => {
    //            this.errorMessage = <any>error;
    //            this.isLoading = false;
    //        });
    //    } else {
    //        this.isLoading = false;
    //    }
    //}

    //recallConcession() {
    //    this.isLoading = true;
    //    this.errorMessage = null;

    //    this.userConcessionsService.deactivateConcession(this.concessionReferenceId).subscribe(entity => {
    //        this.warningMessage = "Concession recalled, please make the required changes and save the concession or it will be lost";
    //        this.isRecalling = true;
    //        this.isLoading = false;
    //        this.canEdit = true;
    //        this.motivationEnabled = true;
    //        this.canArchive = false;
    //    }, error => {
    //        this.errorMessage = <any>error;
    //        this.isLoading = false;
    //    });
    //}

    //saveRecallConcession() {
    //    this.warningMessage = "";
    //    this.isLoading = true;
    //    this.errorMessage = null;
    //    this.validationError = null;

    //    var bolConcession = this.getBolConcession(true);

    //    bolConcession.concession.status = ConcessionStatus.Pending;
    //    bolConcession.concession.subStatus = ConcessionSubStatus.BCMPending;
    //    bolConcession.concession.referenceNumber = this.concessionReferenceId;

    //    if (!this.validationError) {
    //        this.bolConcessionService.postRecallBolData(bolConcession).subscribe(entity => {
    //            console.log("data saved");
    //            this.isRecalling = false;
    //            this.saveMessage = entity.concession.referenceNumber;
    //            this.bolConcession = entity;
    //            this.isLoading = false;
    //            this.canEdit = false;
    //            this.motivationEnabled = false;
    //        }, error => {
    //            this.errorMessage = <any>error;
    //            this.isLoading = false;
    //        });
    //    } else {
    //        this.isLoading = false;
    //    }
    //}

    //requestorApproveConcession() {
    //    this.isLoading = true;

    //    this.errorMessage = null;
    //    this.validationError = null;

    //    var bolConcession = this.getBolConcession(false);
    //    bolConcession.concession.status = ConcessionStatus.ApprovedWithChanges;
    //    bolConcession.concession.subStatus = ConcessionSubStatus.RequestorAcceptedChanges;
    //    bolConcession.concession.requestorId = this.bolConcession.currentUser.id;

    //    if (!bolConcession.concession.comments) {
    //        bolConcession.concession.comments = "Accepted Changes";
    //    }

    //    if (!this.validationError) {
    //        this.bolConcessionService.postUpdateBolData(bolConcession).subscribe(entity => {
    //            console.log("data saved");
    //            this.canApproveChanges = false;
    //            this.saveMessage = entity.concession.referenceNumber;
    //            this.isLoading = false;
    //            this.bolConcession = entity;
    //            this.canEdit = false;
    //        }, error => {
    //            this.errorMessage = <any>error;
    //            this.isLoading = false;
    //        });
    //    } else {
    //        this.isLoading = false;
    //    }
    //}

    //requestorDeclineConcession() {
    //    this.isLoading = true;

    //    this.errorMessage = null;
    //    this.validationError = null;

    //    var bolConcession = this.getBolConcession(false);
    //    bolConcession.concession.status = ConcessionStatus.Declined;
    //    bolConcession.concession.subStatus = ConcessionSubStatus.RequestorDeclinedChanges;
    //    bolConcession.concession.requestorId = this.bolConcession.currentUser.id;

    //    if (!bolConcession.concession.comments) {
    //        bolConcession.concession.comments = "Declined Changes";
    //    }

    //    if (!this.validationError) {
    //        this.bolConcessionService.postUpdateBolData(bolConcession).subscribe(entity => {
    //            console.log("data saved");
    //            this.canApproveChanges = false;
    //            this.saveMessage = entity.concession.referenceNumber;
    //            this.isLoading = false;
    //            this.bolConcession = entity;
    //            this.canEdit = false;
    //        }, error => {
    //            this.errorMessage = <any>error;
    //            this.isLoading = false;
    //        });
    //    } else {
    //        this.isLoading = false;
    //    }
    //}

    //archiveConcession() {
    //    if (confirm("Are you sure you want to archive this concession?")) {
    //        this.isLoading = true;
    //        this.errorMessage = null;

    //        this.userConcessionsService.deactivateConcession(this.concessionReferenceId).subscribe(entity => {
    //            this.warningMessage = "Concession has been archived.";

    //            this.isLoading = false;
    //            this.canBcmApprove = false;
    //            this.canPcmApprove = false;
    //            this.canExtend = false;
    //            this.canRenew = false;
    //            this.canRecall = false;
    //            this.isEditing = false;
    //            this.motivationEnabled = false;
    //            this.canEdit = false;
    //            this.isRecalling = false;
    //            this.canApproveChanges = false;
    //            this.canResubmit = false;
    //            this.canUpdate = false;
    //            this.canArchive = false;

    //        }, error => {
    //            this.errorMessage = <any>error;
    //            this.isLoading = false;
    //        });
    //    }
    //}

    setTwoNumberDecimal($event) {
        $event.target.value = this.formatDecimal($event.target.value);
    }

    formatDecimal(itemValue: number) {
        if (itemValue) {
            return new DecimalPipe('en-US').transform(itemValue, '1.2-2');
        }

        return null;
    }
}
