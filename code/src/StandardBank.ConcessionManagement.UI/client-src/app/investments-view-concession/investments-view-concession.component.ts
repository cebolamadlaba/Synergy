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

import { InvestmentProduct } from "../models/investment-product";
import { ProductType } from "../models/product-type";

import { InvestmentConcession } from "../models/investment-concession";
import { InvestmentConcessionDetail } from "../models/investment-concession-detail";
import { InvestmentConcessionService } from "../services/investment-concession.service";

import { InvestmentView } from "../models/investment-view";


@Component({
    selector: 'app-investments-view-concession',
    templateUrl: './investments-view-concession.component.html',
    styleUrls: ['./investments-view-concession.component.css'],
    providers: [DatePipe]
})
export class InvestmentsViewConcessionComponent implements OnInit, OnDestroy {

    concessionReferenceId: string;
    private sub: any;
    errorMessage: String;
    validationError: String[];
    saveMessage: String;
    warningMessage: String;  

    observableRiskGroup: Observable<RiskGroup>;
    observableInvestmentView: Observable<InvestmentView>;
    investmentView: InvestmentView = new InvestmentView();

    riskGroup: RiskGroup;
    riskGroupNumber: number;

    primeRate = "0.00";
    today: string;   

    public investmentConcessionForm: FormGroup;   
   
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

    productTypes: ProductType[];   

    observableInvestmentProducts: Observable<InvestmentProduct[]>;
    investmentproducts: InvestmentProduct[];

    observableLegalEntityGBbNumbers: Observable<LegalEntityGBBNumber[]>;
    legalentitygbbnumbers: LegalEntityGBBNumber[];

    selectedConditionTypes: ConditionType[];  
    selectedInvestmentConcession: boolean[];

    observableConditionTypes: Observable<ConditionType[]>;
    conditionTypes: ConditionType[];

    observableClientAccounts: Observable<ClientAccount[]>;
    clientAccounts: ClientAccount[];

    observableInvestmentConcession: Observable<InvestmentConcession>;
    investmentConcession: InvestmentConcession;


    constructor(private route: ActivatedRoute,
        private formBuilder: FormBuilder,
        private location: Location,
        private datepipe: DatePipe,
        @Inject(LookupDataService) private lookupDataService,
        @Inject(UserConcessionsService) private userConcessionsService,
        @Inject(InvestmentConcessionService) private investmentConcessionService) {

        this.riskGroup = new RiskGroup();
        //this.investmentproducttypes = [new InvestmentProductType()];
        //this.investmentproducts = [new InvestmentProduct()];

        this.productTypes = [new ProductType()];
        this.periods = [new Period()];
        this.periodTypes = [new PeriodType()];

        this.legalentitygbbnumbers = [new LegalEntityGBBNumber()];

        this.conditionTypes = [new ConditionType()];
        this.selectedConditionTypes = [new ConditionType()];
        //this.selectedProductTypes = [new InvestmentProductType()];

        this.selectedInvestmentConcession = [false];

        this.clientAccounts = [new ClientAccount()];

        this.investmentView.riskGroup = new RiskGroup();
        this.investmentView.investmentConcessions = [new InvestmentConcession()];
        this.investmentView.investmentConcessions[0].concession = new Concession();

        this.investmentConcession = new InvestmentConcession();
        this.investmentConcession.concession = new Concession();
        this.investmentConcession.concession.concessionComments = [new ConcessionComment()];

    }

    ngOnInit() {

        this.today = new Date().toISOString().split('T')[0];

        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
            this.concessionReferenceId = params['concessionReferenceId'];
        });

        if (this.riskGroupNumber) {

            this.observableRiskGroup = this.lookupDataService.getRiskGroup(this.riskGroupNumber);
            this.observableRiskGroup.subscribe(riskGroup => this.riskGroup = riskGroup, error => this.errorMessage = <any>error);

            this.observableClientAccounts = this.lookupDataService.getClientAccountsConcessionType(this.riskGroupNumber, ConcessionTypes.Investment);
            this.observableClientAccounts.subscribe(clientAccounts => this.clientAccounts = clientAccounts, error => this.errorMessage = <any>error);
        }

        if (this.riskGroupNumber) {
            this.observableInvestmentView = this.investmentConcessionService.getInvestmentViewData(this.riskGroupNumber);
            this.observableInvestmentView.subscribe(tempView => {
                this.investmentView = tempView;

                this.isLoading = false;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        }

        //if (this.riskGroupNumber) {
        //    this.observableInvestmentView = this.investmentConcessionService.getInvestmentViewData(this.riskGroupNumber);
        //    this.observableInvestmentView.subscribe(investmentView => {
        //        this.investmentView = investmentView;
        //    }, error => {
        //        this.errorMessage = <any>error;
        //    });
        //}

        this.investmentConcessionForm = this.formBuilder.group({
            concessionItemRows: this.formBuilder.array([this.initConcessionItemRows()]),
            conditionItemsRows: this.formBuilder.array([]),
            smtDealNumber: new FormControl(),
            motivation: new FormControl(),
            comments: new FormControl()
        });

        Observable.forkJoin([

            this.lookupDataService.getProductTypes(ConcessionTypes.Investment),
            this.lookupDataService.getPeriods(),
            this.lookupDataService.getPeriodTypes(),
            this.lookupDataService.getConditionTypes(),
            this.lookupDataService.getRiskGroup(this.riskGroupNumber),
            this.lookupDataService.getClientAccountsConcessionType(this.riskGroupNumber, ConcessionTypes.Investment)


        ]).subscribe(results => {

            this.productTypes = <any>results[0];
            this.periods = <any>results[1];
            this.periodTypes = <any>results[2];
            this.conditionTypes = <any>results[3];
            this.riskGroup = <any>results[4];
            this.clientAccounts = <any>results[5];
            this.primeRate = <string>results[6];

            //this.conditionTypes = <any>results[0];
            //this.productTypes = <any>results[1];
            //this.investmentproducts = <any>results[2];

            //this.periods = <any>results[3];
            //this.periodTypes = <any>results[4];
            //this.legalentitygbbnumbers = <any>results[5];

            this.populateForm();
        },
            error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });

        this.investmentConcessionForm.valueChanges.subscribe((value: any) => {
            if (this.investmentConcessionForm.dirty) {
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
            this.observableInvestmentConcession = this.investmentConcessionService.getInvestmentConcessionData(this.concessionReferenceId);
            this.observableInvestmentConcession.subscribe(investmentConcession => {
                this.investmentConcession = investmentConcession;

                if (investmentConcession.concession.status == ConcessionStatus.Pending && investmentConcession.concession.subStatus == ConcessionSubStatus.BCMPending) {
                    this.canBcmApprove = investmentConcession.currentUser.canBcmApprove;
                }

                if (investmentConcession.concession.status == ConcessionStatus.Pending && investmentConcession.concession.subStatus == ConcessionSubStatus.PCMPending) {
                    if (this.investmentConcession.currentUser.isHO) {
                        this.canPcmApprove = investmentConcession.currentUser.canPcmApprove
                    } else {
                        this.canPcmApprove = investmentConcession.currentUser.canPcmApprove && investmentConcession.currentUser.canApprove;
                    }

                    if (!investmentConcession.concession.isInProgressExtension) {
                        this.canEdit = investmentConcession.currentUser.canPcmApprove;
                    }
                }


                if (investmentConcession.primeRate) {

                    this.primeRate = investmentConcession.primeRate;
                }

                //if it's still pending and the user is a requestor then they can recall it
                if (investmentConcession.concession.status == ConcessionStatus.Pending && investmentConcession.concession.subStatus == ConcessionSubStatus.BCMPending) {
                    this.canRecall = investmentConcession.currentUser.canRequest;
                }

                if (investmentConcession.concession.status == ConcessionStatus.Pending &&
                    (investmentConcession.concession.subStatus == ConcessionSubStatus.PCMApprovedWithChanges || investmentConcession.concession.subStatus == ConcessionSubStatus.HOApprovedWithChanges)) {
                    this.canApproveChanges = investmentConcession.currentUser.canRequest;
                }

                if (investmentConcession.concession.status === ConcessionStatus.Approved ||
                    investmentConcession.concession.status === ConcessionStatus.ApprovedWithChanges) {
                    this.isApproved = true;
                }

                //if the concession is set to can extend and the user is a requestor, then they can extend or renew it
                this.canExtend = investmentConcession.concession.canExtend && investmentConcession.currentUser.canRequest;
                this.canRenew = investmentConcession.concession.canRenew && investmentConcession.currentUser.canRequest;

                //set the resubmit and update permissions
                this.canResubmit = investmentConcession.concession.canResubmit && investmentConcession.currentUser.canRequest;
                this.canUpdate = investmentConcession.concession.canUpdate && investmentConcession.currentUser.canRequest;

                this.canArchive = investmentConcession.concession.canArchive && investmentConcession.currentUser.canRequest;
                this.isInProgressExtension = investmentConcession.concession.isInProgressExtension;
                this.isInProgressRenewal = investmentConcession.concession.isInProgressRenewal;
              
                this.investmentConcessionForm.controls['motivation'].setValue(this.investmentConcession.concession.motivation);               
                this.investmentConcessionForm.controls['smtDealNumber'].setValue(this.investmentConcession.concession.smtDealNumber);

                let rowIndex = 0;

                for (let investmentConcessionDetail of this.investmentConcession.investmentConcessionDetails) {

                    if (rowIndex != 0) {
                        this.addNewConcessionRow();
                    }

                    const concessions = <FormArray>this.investmentConcessionForm.controls['concessionItemRows'];
                    let currentConcession = concessions.controls[concessions.length - 1];

                    currentConcession.get('investmentConcessionDetailId').setValue(investmentConcessionDetail.investmentConcessionDetailId);
                    currentConcession.get('concessionDetailId').setValue(investmentConcessionDetail.concessionDetailId);

                 
                    if (this.clientAccounts) {
                        let selectedAccountNo = this.clientAccounts.filter(_ => _.legalEntityAccountId == investmentConcessionDetail.legalEntityAccountId);
                        currentConcession.get('accountNumber').setValue(selectedAccountNo[0]);
                    }


                    if (this.productTypes) {                                           

                        let selectedProductType = this.productTypes.filter(_ => _.id === investmentConcessionDetail.productTypeId);
                        currentConcession.get('productType').setValue(selectedProductType[0]);                      
                    }

                    if (investmentConcessionDetail.balance)
                        currentConcession.get('balance').setValue(investmentConcessionDetail.balance);

                    if (investmentConcessionDetail.approvedRate)
                        currentConcession.get('approvedRate').setValue(investmentConcessionDetail.approvedRate);

                    if (investmentConcessionDetail.loadedRate)
                        currentConcession.get('loadedRate').setValue(investmentConcessionDetail.loadedRate);

                    if (investmentConcessionDetail.term)
                        currentConcession.get('noticeperiod').setValue(investmentConcessionDetail.term);                                

                    if (investmentConcessionDetail.expiryDate) {
                        var formattedExpiryDate = this.datepipe.transform(investmentConcessionDetail.expiryDate, 'yyyy-MM-dd');
                        currentConcession.get('expiryDate').setValue(formattedExpiryDate);
                    }

                    if (investmentConcessionDetail.dateApproved) {
                        var formattedDateApproved = this.datepipe.transform(investmentConcessionDetail.dateApproved, 'yyyy-MM-dd');
                        currentConcession.get('dateApproved').setValue(formattedDateApproved);
                    }                  

                    currentConcession.get('isExpired').setValue(investmentConcessionDetail.isExpired);
                    currentConcession.get('isExpiring').setValue(investmentConcessionDetail.isExpiring);

                    rowIndex++;
                }

                rowIndex = 0;

                for (let concessionCondition of this.investmentConcession.concessionConditions) {
                    this.addNewConditionRow();

                    const conditions = <FormArray>this.investmentConcessionForm.controls['conditionItemsRows'];
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

                this.changearray = this.lookupDataService.checkforLC(this.investmentConcession.concession.status, this.investmentConcession.concession.subStatus, investmentConcession.concession.concessionComments);

                this.isLoading = false;
            }, error => {
                this.isLoading = false;
                this.errorMessage = <any>error;
            });
        }
    }  
        

    initConcessionItemRows() {
     
        this.selectedInvestmentConcession.push(false)

        return this.formBuilder.group({
           
            disablecontrolset: [''],
            investmentConcessionDetailId: [''],
            concessionDetailId: [''],
            product: [{ value: '', disabled: true }],
            productType: [''],
            accountNumber: [''],
            balance: [''],
            noticeperiod: [''],          
            approvedRate: [''],
            loadedRate: [''],        
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
        const control = <FormArray>this.investmentConcessionForm.controls['concessionItemRows'];
        var newRow = this.initConcessionItemRows();
        control.push(newRow);
    }

    addNewConditionRow() {
        const control = <FormArray>this.investmentConcessionForm.controls['conditionItemsRows'];
        control.push(this.initConditionItemRows());
    }

    addNewConditionRowIfNone() {
        const control = <FormArray>this.investmentConcessionForm.controls['conditionItemsRows'];
        if (control.length == 0)
            control.push(this.initConditionItemRows());
    }

    deleteConcessionRow(index: number) {
        if (confirm("Are you sure you want to remove this row?")) {
            const control = <FormArray>this.investmentConcessionForm.controls['concessionItemRows'];
            control.removeAt(index);
        
            this.selectedInvestmentConcession.splice(index, 1);
        }
    }

    deleteConditionRow(index: number) {
        const control = <FormArray>this.investmentConcessionForm.controls['conditionItemsRows'];
        control.removeAt(index);

        this.selectedConditionTypes.splice(index, 1);
    }

    conditionTypeChanged(rowIndex) {
        const control = <FormArray>this.investmentConcessionForm.controls['conditionItemsRows'];
        this.selectedConditionTypes[rowIndex] = control.controls[rowIndex].get('conditionType').value;

        let currentCondition = control.controls[rowIndex];

        currentCondition.get('conditionProduct').setValue(null);
        currentCondition.get('interestRate').setValue(null);
        currentCondition.get('volume').setValue(null);
        currentCondition.get('value').setValue(null);
        currentCondition.get('expectedTurnoverValue').setValue(null);
    }

    productTypeChanged(rowIndex) {

        const control = <FormArray>this.investmentConcessionForm.controls['concessionItemRows'];      

        let currentProduct = control.controls[rowIndex];
        var selectedproducttype = currentProduct.get('producttype').value;

        //this.selectedProductTypes[rowIndex].products = this.investmentproducts.filter(re => re.investmentProductTypeId == selectedproducttype.investmentProductTypeID);

       // currentProduct.get('product').setValue(this.selectedProductTypes[rowIndex].products[0]);

        //if (selectedproducttype.investmentProductType == "Local guarantee") {

        //    this.selectedInvestmentConcession[rowIndex] = true;

        //    currentProduct.get('disablecontrolset').setValue(true);
            
        //    currentProduct.get('advalorem').setValue(null);
        //    currentProduct.get('min').setValue(null);
        //    currentProduct.get('max').setValue(null);

        //    currentProduct.get('communication').setValue(null);
        //    currentProduct.get('flatfee').setValue(null);
        //    currentProduct.get('currency').setValue(null);

        //}
        //else {

        //    this.selectedInvestmentConcession[rowIndex] = false;

        //    currentProduct.get('disablecontrolset').setValue(false);

        //    currentProduct.get('gbbnumber').setValue(null);
        //    currentProduct.get('term').setValue(null);
        //    currentProduct.get('estfee').setValue(null);
        //    currentProduct.get('loadedRate').setValue(null);
        //}
    }

    addValidationError(validationDetail) {
        if (!this.validationError)
            this.validationError = [];

        this.validationError.push(validationDetail);
    }  
  
    getInvestmentConcession(isNew: boolean): InvestmentConcession {
        var investmentConcession = new InvestmentConcession();
        investmentConcession.concession = new Concession();      
        investmentConcession.concession.riskGroupId = this.riskGroup.id;
        investmentConcession.concession.referenceNumber = this.concessionReferenceId;
        investmentConcession.concession.concessionType = ConcessionTypes.Investment;     

        if (this.investmentConcessionForm.controls['comments'].value)
            investmentConcession.concession.comments = this.investmentConcessionForm.controls['comments'].value;

        if (this.investmentConcessionForm.controls['motivation'].value)
            investmentConcession.concession.motivation = this.investmentConcessionForm.controls['motivation'].value;
        else
            investmentConcession.concession.motivation = '.';

        const concessions = <FormArray>this.investmentConcessionForm.controls['concessionItemRows'];

        for (let concessionFormItem of concessions.controls) {
            if (!investmentConcession.investmentConcessionDetails)
                investmentConcession.investmentConcessionDetails = [];

            let investmentConcessionDetail = new InvestmentConcessionDetail();

            investmentConcessionDetail.disablecontrolset = concessionFormItem.get('disablecontrolset').value;

            if (!isNew && concessionFormItem.get('investmentConcessionDetailId').value)
                investmentConcessionDetail.investmentConcessionDetailId = concessionFormItem.get('investmentConcessionDetailId').value;

            if (!isNew && concessionFormItem.get('concessionDetailId').value)
                investmentConcessionDetail.concessionDetailId = concessionFormItem.get('concessionDetailId').value;


            if (concessionFormItem.get('productType').value)
                investmentConcessionDetail.productTypeId = concessionFormItem.get('productType').value.id;
            else
                this.addValidationError("Product not selected");


            if ((concessionFormItem.get('accountNumber').value && concessionFormItem.get('accountNumber').value.legalEntityId)) {
                investmentConcessionDetail.legalEntityId = concessionFormItem.get('accountNumber').value.legalEntityId;
                investmentConcessionDetail.legalEntityAccountId = concessionFormItem.get('accountNumber').value.legalEntityAccountId;
            } else {

                this.addValidationError("Client account not selected");

            }

            if (concessionFormItem.get('balance').value) {
                investmentConcessionDetail.balance = concessionFormItem.get('balance').value;
            } else {

                this.addValidationError("Balance not entered");

            }

            if (concessionFormItem.get('noticeperiod').value) {
                investmentConcessionDetail.term = concessionFormItem.get('noticeperiod').value;
            } else {

                this.addValidationError("Notice period value not entered");

            }

            if (concessionFormItem.get('loadedRate').value) {
                investmentConcessionDetail.loadedRate = concessionFormItem.get('loadedRate').value;
            } else {

                this.addValidationError("Rate value not entered");

            }

            investmentConcession.investmentConcessionDetails.push(investmentConcessionDetail);
        }      

        const conditions = <FormArray>this.investmentConcessionForm.controls['conditionItemsRows'];

        for (let conditionFormItem of conditions.controls) {
            if (!investmentConcession.concessionConditions)
                investmentConcession.concessionConditions = [];

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

            investmentConcession.concessionConditions.push(concessionCondition);
        }

        return investmentConcession;
    }
    

    getBackgroundColour(rowIndex: number) {
        const control = <FormArray>this.investmentConcessionForm.controls['concessionItemRows'];

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

        var investmentConcession = this.getInvestmentConcession(false);
        investmentConcession.concession.subStatus = ConcessionSubStatus.PCMPending;
        investmentConcession.concession.bcmUserId = this.investmentConcession.currentUser.id;

        if (!investmentConcession.concession.comments) {
            investmentConcession.concession.comments = "Forwarded";
        }

        if (!this.validationError) {
            this.investmentConcessionService.postUpdateInvestmentData(investmentConcession).subscribe(entity => {
                this.canBcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
                this.investmentConcession = entity;
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

        var investmentConcession = this.getInvestmentConcession(false);
        investmentConcession.concession.status = ConcessionStatus.Declined;
        investmentConcession.concession.subStatus = ConcessionSubStatus.BCMDeclined;
        investmentConcession.concession.bcmUserId = this.investmentConcession.currentUser.id;

        if (!investmentConcession.concession.comments) {
            investmentConcession.concession.comments = ConcessionStatus.Declined;
        }

        if (!this.validationError) {
            this.investmentConcessionService.postUpdateInvestmentData(investmentConcession).subscribe(entity => {
                console.log("data saved");
                this.canBcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
                this.investmentConcession = entity;
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

        var investmentConcession = this.getInvestmentConcession(false);

        if (this.hasChanges) {

            investmentConcession.concession.status = ConcessionStatus.Pending;

            if (this.investmentConcession.currentUser.isHO) {
                investmentConcession.concession.subStatus = ConcessionSubStatus.HOApprovedWithChanges;
                investmentConcession.concession.hoUserId = this.investmentConcession.currentUser.id;
            } else {
                investmentConcession.concession.subStatus = ConcessionSubStatus.PCMApprovedWithChanges;
                investmentConcession.concession.pcmUserId = this.investmentConcession.currentUser.id;
            }

            if (!investmentConcession.concession.comments) {
                investmentConcession.concession.comments = ConcessionStatus.ApprovedWithChanges;
            }          
           
            investmentConcession.concession.concessionComments = this.GetChanges(investmentConcession.concession.id);

        } else {
            investmentConcession.concession.status = ConcessionStatus.Approved;

            if (this.investmentConcession.currentUser.isHO) {
                investmentConcession.concession.subStatus = ConcessionSubStatus.HOApproved;
                investmentConcession.concession.hoUserId = this.investmentConcession.currentUser.id;
            } else {
                investmentConcession.concession.subStatus = ConcessionSubStatus.PCMApproved;
                investmentConcession.concession.pcmUserId = this.investmentConcession.currentUser.id;
            }

            if (!investmentConcession.concession.comments) {
                investmentConcession.concession.comments = ConcessionStatus.Approved;
            }
        }

        if (!this.validationError) {
            this.investmentConcessionService.postUpdateInvestmentData(investmentConcession).subscribe(entity => {
                console.log("data saved");
                this.canPcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
                this.investmentConcession = entity;
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

        const concessions = <FormArray>this.investmentConcessionForm.controls['concessionItemRows'];

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

        var investmentConcession = this.getInvestmentConcession(false);

        investmentConcession.concession.status = ConcessionStatus.Declined;

        if (this.investmentConcession.currentUser.isHO) {
            investmentConcession.concession.subStatus = ConcessionSubStatus.HODeclined;
            investmentConcession.concession.hoUserId = this.investmentConcession.currentUser.id;
        } else {
            investmentConcession.concession.subStatus = ConcessionSubStatus.PCMDeclined;
            investmentConcession.concession.pcmUserId = this.investmentConcession.currentUser.id;
        }

        if (!investmentConcession.concession.comments) {
            investmentConcession.concession.comments = ConcessionStatus.Declined;
        }

        if (!this.validationError) {
            this.investmentConcessionService.postUpdateInvestmentData(investmentConcession).subscribe(entity => {
                console.log("data saved");
                this.canPcmApprove = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
                this.investmentConcession = entity;
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

            this.investmentConcessionService.postExtendConcession(this.concessionReferenceId).subscribe(entity => {
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
                this.investmentConcession = entity;
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

        this.investmentConcessionForm.controls['motivation'].setValue('');
    }

    saveConcession() {
        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;

        var investmentConcession = this.getInvestmentConcession(true);

        investmentConcession.concession.status = ConcessionStatus.Pending;
        investmentConcession.concession.subStatus = ConcessionSubStatus.BCMPending;
        investmentConcession.concession.type = "Existing";
        investmentConcession.concession.referenceNumber = this.concessionReferenceId;

        if (!this.validationError) {
            this.investmentConcessionService.postChildConcession(investmentConcession, this.editType).subscribe(entity => {
                console.log("data saved");
                this.isEditing = false;
                this.saveMessage = entity.concession.childReferenceNumber;
                this.investmentConcession = entity;
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

        var investmentConcession = this.getInvestmentConcession(false);

        investmentConcession.concession.status = ConcessionStatus.Pending;
        investmentConcession.concession.subStatus = ConcessionSubStatus.BCMPending;
        investmentConcession.concession.referenceNumber = this.concessionReferenceId;

        if (!this.validationError) {
            this.investmentConcessionService.postRecallInvestmentData(investmentConcession).subscribe(entity => {
                console.log("data saved");
                this.isRecalling = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.investmentConcession = entity;
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

        var investmentConcession = this.getInvestmentConcession(false);
        investmentConcession.concession.status = ConcessionStatus.ApprovedWithChanges;
        investmentConcession.concession.subStatus = ConcessionSubStatus.RequestorAcceptedChanges;
        investmentConcession.concession.requestorId = this.investmentConcession.currentUser.id;

        if (!investmentConcession.concession.comments) {
            investmentConcession.concession.comments = "Accepted Changes";
        }

        if (!this.validationError) {
            this.investmentConcessionService.postUpdateInvestmentData(investmentConcession).subscribe(entity => {
                console.log("data saved");
                this.canApproveChanges = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
                this.investmentConcession = entity;
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

      
        var investmentConcession = this.getInvestmentConcession(false);
        investmentConcession.concession.status = ConcessionStatus.Declined;
        investmentConcession.concession.subStatus = ConcessionSubStatus.RequestorDeclinedChanges;
        investmentConcession.concession.requestorId = this.investmentConcession.currentUser.id;

        if (!investmentConcession.concession.comments) {
            investmentConcession.concession.comments = "Declined Changes";
        }

        if (!this.validationError) {
            this.investmentConcessionService.postUpdateInvestmentData(investmentConcession).subscribe(entity => {
                console.log("data saved");
                this.canApproveChanges = false;
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
                this.investmentConcession = entity;
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

        if (confirm("Are you sure you want to delete the concession item ?")) {
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
        if (confirm("Are you sure you want to delete this concession ?")) {
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
        $event.target.value = this.formatDecimal($event.target.value);
    }

    formatDecimal(itemValue: number) {
        if (itemValue) {
            return new DecimalPipe('en-US').transform(itemValue, '1.2-2');
        }

        return null;
    }
}
