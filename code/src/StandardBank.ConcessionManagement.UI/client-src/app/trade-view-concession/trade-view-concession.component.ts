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

import { LegalEntityGBBNumber } from "../models/legal-entity-gbb-number";

import { TradeProduct } from "../models/trade-product";
import { TradeProductType } from "../models/trade-product-type";

import { TradeConcession } from "../models/trade-concession";
import { TradeConcessionDetail } from "../models/trade-concession-detail";
import { TradeConcessionService } from "../services/trade-concession.service";

import { TradeView } from "../models/trade-view";
import { LegalEntity } from "../models/legal-entity";

import { BaseComponentService } from '../services/base-component.service';
import * as moment from 'moment';
import { MOnthEnum } from '../models/month-enum';

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
    notificationMessage: string;

    observableRiskGroup: Observable<RiskGroup>;
    observableTradeView: Observable<TradeView>;
    tradeView: TradeView = new TradeView();

    riskGroup: RiskGroup;
    riskGroupNumber: number;

    legalEntity: LegalEntity;
    sapbpid: number;

    entityName: string;
    entityNumber: string;

    public tradeConcessionForm: FormGroup;

    isLoading = true;
    isProductLocalGuarantee: boolean;
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

    observableLegalEntityGBbNumbers: Observable<LegalEntityGBBNumber[]>;
    legalentitygbbnumbers: LegalEntityGBBNumber[];

    selectedConditionTypes: ConditionType[];
    selectedProductTypes: TradeProductType[];
    selectedTradeConcession: boolean[];

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
        @Inject(TradeConcessionService) private tradeConcessionService,
        private baseComponentService: BaseComponentService) {

        this.riskGroup = new RiskGroup();
        this.tradeproducttypes = [new TradeProductType()];
        this.tradeproducts = [new TradeProduct()];
        this.periods = [new Period()];
        this.periodTypes = [new PeriodType()];

        this.legalentitygbbnumbers = [new LegalEntityGBBNumber()];

        this.conditionTypes = [new ConditionType()];
        this.selectedConditionTypes = [new ConditionType()];
        this.selectedProductTypes = [new TradeProductType()];
        this.selectedTradeConcession = [false];

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
            this.sapbpid = +params['sapbpid'];
            this.concessionReferenceId = params['concessionReferenceId'];

            this.observableClientAccounts = this.lookupDataService.getClientAccountsConcessionType(this.riskGroupNumber, this.sapbpid, ConcessionTypes.Trade);
            this.observableClientAccounts.subscribe(clientAccounts => this.clientAccounts = clientAccounts, error => this.errorMessage = <any>error);

            this.observableTradeView = this.tradeConcessionService.getTradeViewData(this.riskGroupNumber, this.sapbpid);
            this.observableTradeView.subscribe(tradeView => {
                this.tradeView = tradeView;
            }, error => {
                this.errorMessage = <any>error;
            });

        });

        this.tradeConcessionForm = this.formBuilder.group({
            concessionItemRows: this.formBuilder.array([this.initConcessionItemRows()]),
            conditionItemsRows: this.formBuilder.array([]),
            smtDealNumber: new FormControl(),
            motivation: new FormControl(),
            comments: new FormControl()
        });

        this.getInitialData();

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

    getInitialData() {
        if (this.riskGroupNumber != null && this.riskGroupNumber != 0) {
            Observable.forkJoin([
                this.lookupDataService.getConditionTypes(),
                this.lookupDataService.getTradeProductTypes(),
                this.lookupDataService.getTradeProducts(),
                this.lookupDataService.getPeriods(),
                this.lookupDataService.getPeriodTypes(),
                this.lookupDataService.getLegalEntityGBBNumbers(this.riskGroupNumber),
                this.lookupDataService.getRiskGroup(this.riskGroupNumber)
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
                this.lookupDataService.getTradeProductTypes(),
                this.lookupDataService.getTradeProducts(),
                this.lookupDataService.getPeriods(),
                this.lookupDataService.getPeriodTypes(),
                this.lookupDataService.getLegalEntityGBBNumbersBySAPBPID(this.sapbpid),
                this.lookupDataService.getLegalEntity(this.sapbpid)
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
        this.tradeproducttypes = <any>results[1];
        this.tradeproducts = <any>results[2];
        this.periods = <any>results[3];
        this.periodTypes = <any>results[4];
        this.legalentitygbbnumbers = <any>results[5];

        this.populateForm();
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

                    // Removed as per SBSA.Anthony's request - 2019-07-15
                    //if (!tradeConcession.concession.isInProgressExtension) {
                    this.canEdit = tradeConcession.currentUser.canPcmApprove;
                    //}
                }

                //if it's still pending and the user is a requestor then they can recall it
                if (tradeConcession.concession.status == ConcessionStatus.Pending && tradeConcession.concession.subStatus == ConcessionSubStatus.BCMPending) {
                    this.canRecall = tradeConcession.currentUser.canRequest && tradeConcession.concession.isAENumberLinkedAccountExecutiveOrAssistant;
                }

                if (tradeConcession.concession.status == ConcessionStatus.Pending &&
                    (tradeConcession.concession.subStatus == ConcessionSubStatus.PCMApprovedWithChanges || tradeConcession.concession.subStatus == ConcessionSubStatus.HOApprovedWithChanges)) {
                    this.canApproveChanges = tradeConcession.currentUser.canRequest && tradeConcession.concession.isAENumberLinkedAccountExecutiveOrAssistant;
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

                this.tradeConcessionForm.controls['motivation'].setValue(this.tradeConcession.concession.motivation);

                let rowIndex = 0;

                for (let tradeConcessionDetail of this.tradeConcession.tradeConcessionDetails) {

                    if (rowIndex != 0) {
                        this.addNewConcessionRow(false);
                    }

                    const concessions = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];
                    let currentConcession = concessions.controls[concessions.length - 1];

                    currentConcession.get('tradeConcessionDetailId').setValue(tradeConcessionDetail.tradeConcessionDetailId);
                    currentConcession.get('concessionDetailId').setValue(tradeConcessionDetail.concessionDetailId);

                    if (this.clientAccounts) {
                        let selectedAccountNo = this.clientAccounts.filter(_ => _.legalEntityAccountId == tradeConcessionDetail.legalEntityAccountId);
                        currentConcession.get('accountNumber').setValue(selectedAccountNo[0]);
                    }

                    if (this.tradeproducts) {

                        let currentproduct = this.tradeproducts.filter(_ => _.tradeProductId == tradeConcessionDetail.fkTradeProductId);
                        currentConcession.get('product').setValue(currentproduct[0]);


                        if (currentproduct[0] && currentproduct[0].tradeProductTypeId) {
                            let selectedproducttype = this.tradeproducttypes.filter(re => re.tradeProductTypeID == currentproduct[0].tradeProductTypeId);

                            this.selectedProductTypes[rowIndex].products = this.tradeproducts.filter(re => re.tradeProductTypeId == selectedproducttype[0].tradeProductTypeID);

                            currentConcession.get('producttype').setValue(selectedproducttype[0]);

                            switch (selectedproducttype[0].tradeProductType) {
                                case "Local guarantee":
                                    this.selectedTradeConcession[rowIndex] = true;
                                    currentConcession.get('disablecontrolset').setValue(true);
                                    break;
                                default:
                                    this.selectedTradeConcession[rowIndex] = false;
                                    currentConcession.get('disablecontrolset').setValue(false);
                                    break;
                            }

                            //if (selectedproducttype[0].tradeProductType == "Local guarantee") {

                            //    this.isProductLocalGuarantee = true;
                            //    this.selectedTradeConcession[rowIndex] = true;
                            //    currentConcession.get('disablecontrolset').setValue(true);

                            //}
                            //else {
                            //    this.isProductLocalGuarantee = false;
                            //    this.selectedTradeConcession[rowIndex] = false;
                            //    currentConcession.get('disablecontrolset').setValue(false);
                            //}

                            //if (selectedproducttype[0].tradeProductType == "Outward TT") {
                            //    this.isNotProductOutwardTT = false;
                            //}
                            //else {
                            //    this.isNotProductOutwardTT = true;
                            //}
                        }
                    }

                    if (tradeConcessionDetail.adValorem || tradeConcessionDetail.adValorem == 0) {
                        currentConcession.get('advalorem').setValue(tradeConcessionDetail.adValorem);

                        tradeConcessionDetail.show_advalorem = true;

                    }

                    if (tradeConcessionDetail.communication) {
                        let communication = this.baseComponentService.formatDecimal(+tradeConcessionDetail.communication);
                        currentConcession.get('communication').setValue(communication);

                        tradeConcessionDetail.show_communication = true;

                    }
                    else {

                        tradeConcessionDetail.show_communication = true;
                    }

                    if (tradeConcessionDetail.flatFee || tradeConcessionDetail.flatFee == 0) {
                        currentConcession.get('flatfee').setValue(tradeConcessionDetail.flatFee);
                        tradeConcessionDetail.show_flatfee = true;
                    }

                    if (tradeConcessionDetail.currency) {
                        currentConcession.get('currency').setValue(tradeConcessionDetail.currency);

                    }

                    if (tradeConcessionDetail.gbbNumber) {

                        let selectedGBBNo = this.legalentitygbbnumbers.filter(_ => _.pkLegalEntityGBBNumber == tradeConcessionDetail.fkLegalEntityGBBNumber);
                        currentConcession.get('gbbnumber').setValue(selectedGBBNo[0]);
                        currentConcession.get('gbbnumberText').setValue(tradeConcessionDetail.gbbNumber);
                        currentConcession.get('accountNumber').setValue(null);
                    }

                    if (tradeConcessionDetail.term) {
                        currentConcession.get('term').setValue(tradeConcessionDetail.term);
                        tradeConcessionDetail.show_term = true;
                    }

                    if (tradeConcessionDetail.establishmentFee)
                        currentConcession.get('estfee').setValue(tradeConcessionDetail.establishmentFee);

                    if (tradeConcessionDetail.approvedRate)
                        currentConcession.get('approvedRate').setValue(tradeConcessionDetail.approvedRate);

                    if (tradeConcessionDetail.rate)
                        currentConcession.get('rate').setValue(tradeConcessionDetail.rate);

                    if (tradeConcessionDetail.min || tradeConcessionDetail.min == 0) {
                        let min = this.baseComponentService.formatDecimal(+tradeConcessionDetail.min);
                        currentConcession.get('min').setValue(min);
                        tradeConcessionDetail.show_min = true;
                    }

                    if (tradeConcessionDetail.max || tradeConcessionDetail.max == 0) {
                        let max = this.baseComponentService.formatDecimal(+tradeConcessionDetail.max);
                        currentConcession.get('max').setValue(max);
                        tradeConcessionDetail.show_max = true;
                    }

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

                    let selectedPeriodType = this.periodTypes.filter(_ => _.id == concessionCondition.periodTypeId);
                    currentCondition.get('periodType').setValue(selectedPeriodType[0]);

                    let selectedPeriod = this.periods.filter(_ => _.id == concessionCondition.periodId);
                    currentCondition.get('period').setValue(selectedPeriod[0]);

                    rowIndex++;
                }
                this.changearray = this.lookupDataService.checkforLC(this.tradeConcession.concession.status, this.tradeConcession.concession.subStatus, tradeConcession.concession.concessionComments);

                this.isLoading = false;
            }, error => {
                this.isLoading = false;
                this.errorMessage = <any>error;
            });
        }
    }

    initConcessionItemRows() {



        this.selectedProductTypes.push(new TradeProductType());
        this.selectedTradeConcession.push(false)

        return this.formBuilder.group({

            disablecontrolset: [''],
            tradeConcessionDetailId: [''],
            concessionDetailId: [''],
            product: [{ value: '', disabled: true }],
            producttype: [''],
            accountnumber: [''],
            gbbnumber: [''],
            gbbnumberText: [''],
            accountNumber: [''],
            term: [''],
            advalorem: [''],
            min: [''],
            max: [''],
            communication: [''],
            flatfee: [''],
            currency: [''],
            estfee: [''],
            rate: [''],
            approvedRate: [''],
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
            periodType: [''],
            period: ['']
        });
    }

    addNewConcessionRow(add: boolean) {
        const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];
        var newRow = this.initConcessionItemRows();
        control.push(newRow);

        if (add == true) {

            let newconcession = new TradeConcessionDetail();
            newconcession.show_advalorem = true;
            newconcession.show_communication = true;
            newconcession.show_max = true;
            newconcession.show_flatfee = true;
            newconcession.show_min = true;
            newconcession.show_term = true;

            this.tradeConcession.tradeConcessionDetails.push(newconcession);
        }
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
        if (confirm("Please note that the account will be put back to standard pricing. Are you sure you want to delete this concession?")) {
            const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];
            control.removeAt(index);

            this.selectedProductTypes.splice(index, 1);
            this.selectedTradeConcession.splice(index, 1);
        }
    }

    onExpiryDateChanged(rowIndex) {
        this.errorMessage = null;
        this.validationError = null;

        const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];
        let selectedExpiryDate = control.controls[rowIndex].get('expiryDate').value;

        var currentMonth = moment().month()
        var selectedExpiryDateMonth = moment(selectedExpiryDate).month();
        let monthsDifference = currentMonth - selectedExpiryDateMonth;

        if (monthsDifference < MOnthEnum.ThreeMonths) {
            this.addValidationError("Concession expiry date must be 3 months or more");
        };
    }

    onTermValueChange(rowIndex) {
        this.errorMessage = null;
        this.validationError = null;

        const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];
        let term = control.controls[rowIndex].get('term').value;

        if (term < MOnthEnum.ThreeMonths) {
            this.addValidationError("Minimum term captured should be 3 months");
        };
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

        currentCondition.get('conditionProduct').setValue(null);
        currentCondition.get('interestRate').setValue(null);
        currentCondition.get('volume').setValue(null);
        currentCondition.get('value').setValue(null);
    }

    productTypeChanged(rowIndex) {
        this.notificationMessage = null;

        const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];

        this.selectedProductTypes[rowIndex] = control.controls[rowIndex].get('producttype').value;

        let currentProduct = control.controls[rowIndex];
        var selectedproducttype = currentProduct.get('producttype').value;

        this.selectedProductTypes[rowIndex].products = this.tradeproducts.filter(re => re.tradeProductTypeId == selectedproducttype.tradeProductTypeID);

        currentProduct.get('product').setValue(this.selectedProductTypes[rowIndex].products[0]);

        if (selectedproducttype.tradeProductType == "Local guarantee") {
            this.notificationMessage = "Please note there is no system integration for GBBs, therefore collateral Centres need to load fees/ rates.";

            this.selectedTradeConcession[rowIndex] = true;

            currentProduct.get('disablecontrolset').setValue(true);
            currentProduct.get('accountNumber').setValue(null);
            currentProduct.get('advalorem').setValue(null);
            currentProduct.get('min').setValue(null);
            currentProduct.get('max').setValue(null);

            currentProduct.get('communication').setValue(null);
            currentProduct.get('flatfee').setValue(null);
            currentProduct.get('currency').setValue(null);
            currentProduct.get('expiryDate').setValue('');

            this.tradeConcession.tradeConcessionDetails[rowIndex].show_advalorem = false;
            this.tradeConcession.tradeConcessionDetails[rowIndex].show_min = false;
            this.tradeConcession.tradeConcessionDetails[rowIndex].show_max = false;


            this.tradeConcession.tradeConcessionDetails[rowIndex].show_communication = false;
            this.tradeConcession.tradeConcessionDetails[rowIndex].show_flatfee = false;
            this.tradeConcession.tradeConcessionDetails[rowIndex].show_term = true;


        }
        else {

            this.selectedTradeConcession[rowIndex] = false;
            currentProduct.get('disablecontrolset').setValue(false);
            currentProduct.get('gbbnumber').setValue(null);

            currentProduct.get('term').setValue(null);
            currentProduct.get('estfee').setValue(null);
            currentProduct.get('rate').setValue(null);


            this.tradeConcession.tradeConcessionDetails[rowIndex].show_advalorem = true;
            this.tradeConcession.tradeConcessionDetails[rowIndex].show_min = true;
            this.tradeConcession.tradeConcessionDetails[rowIndex].show_max = true;


            this.tradeConcession.tradeConcessionDetails[rowIndex].show_communication = true;
            this.tradeConcession.tradeConcessionDetails[rowIndex].show_flatfee = true;

            this.tradeConcession.tradeConcessionDetails[rowIndex].show_term = false;

        }

        if (selectedproducttype.tradeProductType != "Outward TT") {
            currentProduct.get('communication').setValue(null);
        }
    }

    addValidationError(validationDetail) {
        if (!this.validationError)
            this.validationError = [];

        this.validationError.push(validationDetail);
    }

    showGbbDeclaimer() {
        this.notificationMessage = "For New GBB, insert C/A number and update once the M-number is issued. For existing GBB use existing M- number.";
    }

    clearMessages() {
        this.errorMessage = null;
        this.validationError = null;
        this.notificationMessage = null;
        this.saveMessage = null;
    }

    disableCommunicationFee(rowIndex) {
        const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];
        let currentrow = control.controls[rowIndex];

        let productype = currentrow.get('producttype').value;

        if (productype != null && productype.tradeProductType != "" && productype.tradeProductType != "Outward TT") {
            currentrow.get('communication').disable();
            currentrow.get('communication').setValue(null);
            return true;
        }
        else {
            if (this.canEdit) {
                currentrow.get('communication').enable();
                return false;
            }
            else {
                currentrow.get('communication').disable();
                return true;
            }
        }
    }

    getTradeConcession(isNew: boolean): TradeConcession {
        var tradeConcession = new TradeConcession();
        tradeConcession.concession = new Concession();

        if (this.riskGroup)
            tradeConcession.concession.riskGroupId = this.riskGroup.id;
        if (this.legalEntity)
            tradeConcession.concession.legalEntityId = this.legalEntity.id;

        tradeConcession.concession.referenceNumber = this.concessionReferenceId;
        tradeConcession.concession.concessionType = ConcessionTypes.Trade;

        if (this.tradeConcessionForm.controls['comments'].value)
            tradeConcession.concession.comments = this.tradeConcessionForm.controls['comments'].value;

        if (this.tradeConcessionForm.controls['motivation'].value)
            tradeConcession.concession.motivation = this.tradeConcessionForm.controls['motivation'].value;
        else
            tradeConcession.concession.motivation = '.';

        const concessions = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];

        let hasTypeId: boolean = false;
        let hasLegalEntityId: boolean = false;
        let hasLegalEntityAccountId: boolean = false;

        for (let concessionFormItem of concessions.controls) {
            if (!tradeConcession.tradeConcessionDetails)
                tradeConcession.tradeConcessionDetails = [];

            let tradeConcessionDetail = new TradeConcessionDetail();
            let applyexpirydate = true;

            if (!isNew && concessionFormItem.get('tradeConcessionDetailId').value)
                tradeConcessionDetail.tradeConcessionDetailId = concessionFormItem.get('tradeConcessionDetailId').value;

            if (!isNew && concessionFormItem.get('concessionDetailId').value)
                tradeConcessionDetail.concessionDetailId = concessionFormItem.get('concessionDetailId').value;

            tradeConcessionDetail.disablecontrolset = concessionFormItem.get('disablecontrolset').value;

            if (concessionFormItem.get('product').value) {
                tradeConcessionDetail.fkTradeProductId = concessionFormItem.get('product').value.tradeProductId;

            } else {
                this.addValidationError("Product not selected");
            }


            if (concessionFormItem.get('producttype').value) {
                tradeConcessionDetail.tradeProductTypeID = concessionFormItem.get('producttype').value.tradeProductTypeID;

                if (concessionFormItem.get('producttype').value.tradeProductType == "Local guarantee") {
                    applyexpirydate = false;
                }
                hasTypeId = true;
            } else {
                this.addValidationError("Product Type code not selected");
            }


            if ((concessionFormItem.get('accountNumber').value && concessionFormItem.get('accountNumber').value.legalEntityId)) {
                tradeConcessionDetail.legalEntityId = concessionFormItem.get('accountNumber').value.legalEntityId;
                tradeConcessionDetail.legalEntityAccountId = concessionFormItem.get('accountNumber').value.legalEntityAccountId;
                hasLegalEntityId = true;
                hasLegalEntityAccountId = true;
            } else {
                hasLegalEntityId = false;
                hasLegalEntityAccountId = false;
                if (!tradeConcessionDetail.disablecontrolset) {

                    this.addValidationError("Client account not selected");
                }
            }

            let hasValueGbbNumber = (concessionFormItem.get('gbbnumberText').value);

            //if (concessionFormItem.get('gbbnumber').value) {
            if (hasValueGbbNumber) {
                tradeConcessionDetail.gbbNumber = concessionFormItem.get('gbbnumberText').value;
            } else {
                if (tradeConcessionDetail.disablecontrolset) {
                    this.addValidationError("GBB Number not entered");
                }
            }


            if (concessionFormItem.get('term').value) {
                tradeConcessionDetail.term = concessionFormItem.get('term').value;
            } else {
                if (tradeConcessionDetail.disablecontrolset) {
                    this.addValidationError("Term not entered");
                }
            }

            let advaloremfound = false;

            if ((concessionFormItem.get('advalorem').value != null && concessionFormItem.get('advalorem').value != "") ||
                concessionFormItem.get('advalorem').value === 0) {
                advaloremfound = true;
                tradeConcessionDetail.adValorem = concessionFormItem.get('advalorem').value;
            } else {
                // if (!tradeConcessionDetail.disablecontrolset) {
                // this.addValidationError("AdValorem value not entered");
                //}
            }

            if (concessionFormItem.get('min').value && advaloremfound) {
                tradeConcessionDetail.min = concessionFormItem.get('min').value;
            } else {
                if (!tradeConcessionDetail.disablecontrolset && advaloremfound) {
                    this.addValidationError("Min value not entered");
                }
            }

            if (concessionFormItem.get('max').value && advaloremfound) {
                tradeConcessionDetail.max = concessionFormItem.get('max').value;
            } else {
                if (!tradeConcessionDetail.disablecontrolset && advaloremfound) {
                    this.addValidationError("Max value not entered");
                }
            }
            ///---
            //if (!concessionFormItem.get('communication').disabled) {
            if (concessionFormItem.get('communication').value != null && concessionFormItem.get('communication').value > -1) {
                let communicationVal = this.baseComponentService.unformat(concessionFormItem.get('communication').value);
                if (communicationVal != null || communicationVal == 0) {
                    tradeConcessionDetail.communication = communicationVal;
                } else {
                    if (!tradeConcessionDetail.disablecontrolset) {
                        this.addValidationError("Communication not entered");
                    }
                }
            }


            let flatfeefound = false;
            if ((concessionFormItem.get('flatfee').value != null && concessionFormItem.get('flatfee').value != "") ||
                concessionFormItem.get('flatfee').value === 0) {
                flatfeefound = true;
                tradeConcessionDetail.flatFee = concessionFormItem.get('flatfee').value;
            } else {
                if (!tradeConcessionDetail.disablecontrolset && !advaloremfound) {
                    // this.addValidationError("Flat fee not entered");
                }
            }


            if ((flatfeefound == false) && (advaloremfound == false)) {

                if (applyexpirydate) {
                    this.addValidationError("Either AdValorem or Flat fee must be entered");
                }
            }

            if (concessionFormItem.get('currency').value) {
                tradeConcessionDetail.currency = concessionFormItem.get('currency').value;
            } else {
                if (!tradeConcessionDetail.disablecontrolset) {
                    this.addValidationError("Currency not selected");
                }
            }

            if (concessionFormItem.get('estfee').value) {
                tradeConcessionDetail.establishmentFee = concessionFormItem.get('estfee').value;
            } else {
                if (tradeConcessionDetail.disablecontrolset) {
                    this.addValidationError("Est. fee not entered");
                }
            }

            if (concessionFormItem.get('rate').value) {
                tradeConcessionDetail.rate = concessionFormItem.get('rate').value;
            } else {
                if (tradeConcessionDetail.disablecontrolset) {
                    this.addValidationError("Rate not entered");
                }
            }
            if (concessionFormItem.get('expiryDate').value && concessionFormItem.get('expiryDate').value != "") {
                tradeConcessionDetail.expiryDate = new Date(concessionFormItem.get('expiryDate').value);
            }
            else {
                if (applyexpirydate) {
                    this.addValidationError("Expiry date not selected");
                }
            }

            tradeConcession.tradeConcessionDetails.push(tradeConcessionDetail);

            if (hasTypeId && hasLegalEntityId && hasLegalEntityAccountId) {
                let hasDuplicates = this.baseComponentService.HasDuplicateConcessionAccountTradeProduct(
                    tradeConcession.tradeConcessionDetails,
                    concessionFormItem.get('producttype').value.tradeProductTypeID,
                    concessionFormItem.get('accountNumber').value.legalEntityId,
                    concessionFormItem.get('accountNumber').value.legalEntityAccountId);

                if (hasDuplicates) {
                    this.addValidationError("Duplicate Account / Trade Product pricing found. Please select different account.");

                    break;
                }
            }
        }

        const conditions = <FormArray>this.tradeConcessionForm.controls['conditionItemsRows'];

        for (let conditionFormItem of conditions.controls) {
            if (!tradeConcession.concessionConditions)
                tradeConcession.concessionConditions = [];

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

            tradeConcession.concessionConditions.push(concessionCondition);
        }

        return tradeConcession;
    }


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

    bcmApproveConcession() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;

        var tradeConcession = this.getTradeConcession(false);
        tradeConcession.concession.subStatus = ConcessionSubStatus.PCMPending;
        tradeConcession.concession.bcmUserId = this.tradeConcession.currentUser.id;

        if (!tradeConcession.concession.comments) {
            tradeConcession.concession.comments = "Forwarded";
        }

        if (!this.validationError) {
            this.tradeConcessionService.postUpdateTradeData(tradeConcession).subscribe(entity => {
                this.canBcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
                this.tradeConcession = entity;
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

        const concessions = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];

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

    bcmDeclineConcession() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;

        var tradeConcession = this.getTradeConcession(false);
        tradeConcession.concession.status = ConcessionStatus.Declined;
        tradeConcession.concession.subStatus = ConcessionSubStatus.BCMDeclined;
        tradeConcession.concession.bcmUserId = this.tradeConcession.currentUser.id;

        if (!tradeConcession.concession.comments) {
            tradeConcession.concession.comments = ConcessionStatus.Declined;
        }

        if (!this.validationError) {
            this.tradeConcessionService.postUpdateTradeData(tradeConcession).subscribe(entity => {
                console.log("data saved");
                this.canBcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
                this.tradeConcession = entity;
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

        var tradeConcession = this.getTradeConcession(false);

        if (this.hasChanges) {
            tradeConcession.concession.status = ConcessionStatus.Pending;

            if (this.tradeConcession.currentUser.isHO) {
                tradeConcession.concession.subStatus = ConcessionSubStatus.HOApprovedWithChanges;
                tradeConcession.concession.hoUserId = this.tradeConcession.currentUser.id;
            } else {
                tradeConcession.concession.subStatus = ConcessionSubStatus.PCMApprovedWithChanges;
                tradeConcession.concession.pcmUserId = this.tradeConcession.currentUser.id;
            }

            if (!tradeConcession.concession.comments) {
                tradeConcession.concession.comments = ConcessionStatus.ApprovedWithChanges;
            }

            tradeConcession.concession.concessionComments = this.GetChanges(tradeConcession.concession.id);

        } else {
            tradeConcession.concession.status = ConcessionStatus.Approved;

            if (this.tradeConcession.currentUser.isHO) {
                tradeConcession.concession.subStatus = ConcessionSubStatus.HOApproved;
                tradeConcession.concession.hoUserId = this.tradeConcession.currentUser.id;
            } else {
                tradeConcession.concession.subStatus = ConcessionSubStatus.PCMApproved;
                tradeConcession.concession.pcmUserId = this.tradeConcession.currentUser.id;
            }

            if (!tradeConcession.concession.comments) {
                tradeConcession.concession.comments = ConcessionStatus.Approved;
            }
        }

        if (!this.validationError) {
            this.tradeConcessionService.postUpdateTradeData(tradeConcession).subscribe(entity => {
                console.log("data saved");
                this.canPcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
                this.tradeConcession = entity;
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

        var tradeConcession = this.getTradeConcession(false);

        tradeConcession.concession.status = ConcessionStatus.Declined;

        if (this.tradeConcession.currentUser.isHO) {
            tradeConcession.concession.subStatus = ConcessionSubStatus.HODeclined;
            tradeConcession.concession.hoUserId = this.tradeConcession.currentUser.id;
        } else {
            tradeConcession.concession.subStatus = ConcessionSubStatus.PCMDeclined;
            tradeConcession.concession.pcmUserId = this.tradeConcession.currentUser.id;
        }

        if (!tradeConcession.concession.comments) {
            tradeConcession.concession.comments = ConcessionStatus.Declined;
        }

        if (!this.validationError) {
            this.tradeConcessionService.postUpdateTradeData(tradeConcession).subscribe(entity => {
                console.log("data saved");
                this.canPcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
                this.tradeConcession = entity;
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

            this.tradeConcessionService.postExtendConcession(this.concessionReferenceId).subscribe(entity => {
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
                this.tradeConcession = entity;
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

        this.tradeConcessionForm.controls['motivation'].setValue('');
    }

    saveConcession() {
        this.isLoading = true;

        this.clearMessages();

        var tradeConcession = this.getTradeConcession(true);

        tradeConcession.concession.status = ConcessionStatus.Pending;
        tradeConcession.concession.subStatus = ConcessionSubStatus.BCMPending;
        tradeConcession.concession.type = "Existing";
        tradeConcession.concession.referenceNumber = this.concessionReferenceId;

        if (!this.validationError) {
            this.tradeConcessionService.postChildConcession(tradeConcession, this.editType).subscribe(entity => {
                console.log("data saved");
                this.isEditing = false;
                this.saveMessage = entity.concession.childReferenceNumber;
                this.tradeConcession = entity;
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

        this.clearMessages();

        var tradeConcession = this.getTradeConcession(true);

        tradeConcession.concession.type = "Existing";
        tradeConcession.concession.referenceNumber = this.concessionReferenceId;

        if (!this.validationError) {
            this.tradeConcessionService.postUpdateTradeData(tradeConcession, this.editType).subscribe(entity => {
                console.log("data saved");
                this.isEditing = false;
                this.saveMessage = entity.concession.childReferenceNumber;
                this.tradeConcession = entity;
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

        var tradeConcession = this.getTradeConcession(false);

        tradeConcession.concession.status = ConcessionStatus.Pending;
        tradeConcession.concession.subStatus = ConcessionSubStatus.BCMPending;
        tradeConcession.concession.referenceNumber = this.concessionReferenceId;

        if (!this.validationError) {
            this.tradeConcessionService.postRecallTradeData(tradeConcession).subscribe(entity => {
                console.log("data saved");
                this.isRecalling = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.tradeConcession = entity;
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

        var tradeConcession = this.getTradeConcession(false);
        tradeConcession.concession.status = ConcessionStatus.ApprovedWithChanges;
        tradeConcession.concession.subStatus = ConcessionSubStatus.RequestorAcceptedChanges;
        tradeConcession.concession.requestorId = this.tradeConcession.currentUser.id;

        if (!tradeConcession.concession.comments) {
            tradeConcession.concession.comments = "Accepted Changes";
        }

        if (!this.validationError) {
            this.tradeConcessionService.postUpdateTradeData(tradeConcession).subscribe(entity => {
                console.log("data saved");
                this.canApproveChanges = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
                this.tradeConcession = entity;
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


        var tradeConcession = this.getTradeConcession(false);
        tradeConcession.concession.status = ConcessionStatus.Declined;
        tradeConcession.concession.subStatus = ConcessionSubStatus.RequestorDeclinedChanges;
        tradeConcession.concession.requestorId = this.tradeConcession.currentUser.id;

        if (!tradeConcession.concession.comments) {
            tradeConcession.concession.comments = "Declined Changes";
        }

        if (!this.validationError) {
            this.tradeConcessionService.postUpdateTradeData(tradeConcession).subscribe(entity => {
                console.log("data saved");
                this.canApproveChanges = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
                this.tradeConcession = entity;
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


    setadvalorem($event, rowIndex, controlname) {

        if (event.target != null && $event.target.value != "") {

            const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];
            let currentrow = control.controls[rowIndex];

            if (controlname == "advalorem") {

                currentrow.get('flatfee').disable();
                currentrow.get('flatfee').setValue(null);
            }
            else {
                currentrow.get('advalorem').disable();
                currentrow.get('min').disable();
                currentrow.get('max').disable();

                currentrow.get('advalorem').setValue(null);
                currentrow.get('min').setValue(null);
                currentrow.get('max').setValue(null);

            }
        }
        else {
            const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];
            let currentrow = control.controls[rowIndex];

            if (controlname == "advalorem") {

                currentrow.get('flatfee').enable();
            }
            else {

                currentrow.get('advalorem').enable();
                currentrow.get('min').enable();
                currentrow.get('max').enable();


            }
        }


        //$event.target.value = this.formatDecimal($event.target.value);
    }

    setTwoNumberDecimal($event) {
        $event.target.value = this.baseComponentService.formatDecimal($event.target.value);
        //$event.target.value = this.formatDecimal($event.target.value);
    }

    setThreeNumberDecimal($event) {

        if ($event.target.value) {
            $event.target.value = new DecimalPipe('en-US').transform($event.target.value, '1.3-3');
        }
        else {

            $event.target.value = null;
        }
    }

    setFlatFee($event, rowIndex, controlname) {

        const control = <FormArray>this.tradeConcessionForm.controls['concessionItemRows'];
        let currentrow = control.controls[rowIndex];

        if (event.target != null && $event.target.value != "") {

            if (controlname == "flatfee") {
                currentrow.get('advalorem').disable();
                currentrow.get('advalorem').setValue(null);
                currentrow.get('min').disable();
                currentrow.get('max').disable();
                currentrow.get('min').setValue(null);
                currentrow.get('max').setValue(null);
            }
        }
        else {
            currentrow.get('advalorem').enable();
            currentrow.get('min').enable();
            currentrow.get('max').enable();
            currentrow.get('advalorem').setValue(null);
            currentrow.get('min').setValue(null);
            currentrow.get('max').setValue(null);
        }
    }

    formatDecimal(itemValue: number) {
        if (itemValue) {
            return new DecimalPipe('en-US').transform(itemValue, '1.2-2');
        }

        return null;
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
