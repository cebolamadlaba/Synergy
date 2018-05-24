import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { RiskGroup } from "../models/risk-group";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Location } from '@angular/common';
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
import { DecimalPipe } from '@angular/common';
import { ConcessionTypes } from '../constants/concession-types';

@Component({
    selector: 'app-cash-add-concession',
    templateUrl: './cash-add-concession.component.html',
    styleUrls: ['./cash-add-concession.component.css']
})
export class CashAddConcessionComponent implements OnInit, OnDestroy {
    private sub: any;
    errorMessage: String;
    validationError: String[];
    saveMessage: String;
    showHide = false;
    observableRiskGroup: Observable<RiskGroup>;
    riskGroup: RiskGroup;
    riskGroupNumber: number;
    public cashConcessionForm: FormGroup;
    selectedConditionTypes: ConditionType[];
    isLoading = true;
    observableLatestCrsOrMrs: Observable<number>;
    latestCrsOrMrs: number;

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

    observableTableNumbers: Observable<TableNumber[]>;
    tableNumbers: TableNumber[];

    constructor(private route: ActivatedRoute,
        private formBuilder: FormBuilder,
        private location: Location,
        @Inject(LookupDataService) private lookupDataService,
        @Inject(CashConcessionService) private cashConcessionService) {
        this.riskGroup = new RiskGroup();
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
                this.observableRiskGroup = this.lookupDataService.getRiskGroup(this.riskGroupNumber);
                this.observableRiskGroup.subscribe(riskGroup => this.riskGroup = riskGroup, error => this.errorMessage = <any>error);

                this.observableClientAccounts = this.lookupDataService.getClientAccounts(this.riskGroupNumber);
                this.observableClientAccounts.subscribe(clientAccounts => this.clientAccounts = clientAccounts, error => this.errorMessage = <any>error);

                this.observableLatestCrsOrMrs = this.cashConcessionService.getlatestCrsOrMrs(this.riskGroupNumber);
                this.observableLatestCrsOrMrs.subscribe(latestCrsOrMrs => this.latestCrsOrMrs = latestCrsOrMrs, error => this.errorMessage = <any>error);
            }
        });

        this.cashConcessionForm = this.formBuilder.group({
            concessionItemRows: this.formBuilder.array([this.initConcessionItemRows()]),
            conditionItemsRows: this.formBuilder.array([]),
            smtDealNumber: new FormControl(),
            motivation: new FormControl()
        });

        Observable.forkJoin([
            this.lookupDataService.getChannelTypes(),
            this.lookupDataService.getPeriods(),
            this.lookupDataService.getPeriodTypes(),
            this.lookupDataService.getConditionTypes(),
            this.lookupDataService.getAccrualTypes(),
            this.lookupDataService.getTableNumbers(ConcessionTypes.Cash),
            this.lookupDataService.getRiskGroup(this.riskGroupNumber),
            this.lookupDataService.getClientAccounts(this.riskGroupNumber),
            this.cashConcessionService.getlatestCrsOrMrs(this.riskGroupNumber)
        ]).subscribe(results => {
                this.channelTypes = <any>results[0];
                this.periods = <any>results[1];
                this.periodTypes = <any>results[2];
                this.conditionTypes = <any>results[3];
                this.accrualTypes = <any>results[4];
                this.tableNumbers = <any>results[5];
                this.riskGroup = <any>results[6];
                this.clientAccounts = <any>results[7];
                this.latestCrsOrMrs = <any>results[8];

                const control = <FormArray>this.cashConcessionForm.controls['concessionItemRows'];             

                if (this.channelTypes)
                    control.controls[0].get('channelType').setValue(this.channelTypes[0]);

                if (this.tableNumbers)
                    control.controls[0].get('tableNumber').setValue(this.tableNumbers[0]);

                if (this.clientAccounts)
                    control.controls[0].get('accountNumber').setValue(this.clientAccounts[0]);

                if (this.accrualTypes)
                    control.controls[0].get('accrualType').setValue(this.accrualTypes[0]);




                this.isLoading = false;
            },
            error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
    }

    initConcessionItemRows() {
        return this.formBuilder.group({
            channelType: [''],
            accountNumber: [''],
            tableNumber: [''],
            baseRate: [{ value: '', disabled: true }],
            adValorem: [{ value: '', disabled: true }],
            accrualType: [''],
            expiryDate: ['']
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
            expectedTurnoverValue: [''],
            periodType: [''],
            period: ['']
        });
    }

    addNewConcessionRow() {
        const control = <FormArray>this.cashConcessionForm.controls['concessionItemRows'];
        var newRow = this.initConcessionItemRows();      

        if (this.channelTypes)
            newRow.controls['channelType'].setValue(this.channelTypes[0]);

        if (this.tableNumbers)
            newRow.controls['tableNumber'].setValue(this.tableNumbers[0]);

        if (this.clientAccounts)
            newRow.controls['accountNumber'].setValue(this.clientAccounts[0]);

        if (this.accrualTypes)
            newRow.controls['accrualType'].setValue(this.accrualTypes[0]);

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

    copyConcessionRow(index: number) {

        const control = <FormArray>this.cashConcessionForm.controls['concessionItemRows'];              

        var newRow = this.initConcessionItemRows();

        //if (this.channelTypes)
        //    newRow.controls['channelType'].setValue(this.channelTypes[0]);

        //if (this.tableNumbers)
        //    newRow.controls['tableNumber'].setValue(this.tableNumbers[0]);

        //if (this.clientAccounts)
        //    newRow.controls['accountNumber'].setValue(this.clientAccounts[0]);

        //if (this.accrualTypes)
        //    newRow.controls['accrualType'].setValue(this.accrualTypes[0]);


        if (control.controls[index].get('channelType').value)
            newRow.controls['channelType'].setValue(control.controls[index].get('channelType').value);

        if (control.controls[index].get('accountNumber').value)
            newRow.controls['accountNumber'].setValue(control.controls[index].get('accountNumber').value);

        if (control.controls[index].get('accrualType').value)
            newRow.controls['accrualType'].setValue(control.controls[index].get('accrualType').value);

        if (control.controls[index].get('tableNumber').value)
            newRow.controls['tableNumber'].setValue(control.controls[index].get('tableNumber').value);

        if (control.controls[index].get('tableNumber').value.baseRate)
            newRow.controls['baseRate'].setValue(control.controls[index].get('tableNumber').value.baseRate.toFixed(2));
      
        if (control.controls[index].get('tableNumber').value.baseRate)
            newRow.controls['adValorem'].setValue(control.controls[index].get('tableNumber').value.adValorem.toFixed(3));
      
        if (control.controls[index].get('expiryDate').value)
            newRow.controls['expiryDate'].setValue(control.controls[index].get('expiryDate').value);

        control.insert(index + 1, newRow);
       
    }

    deleteConcessionRow(index: number) {
        if (confirm("Are you sure you want to remove this row?")) {
            const control = <FormArray>this.cashConcessionForm.controls['concessionItemRows'];
            control.removeAt(index);
        }
    }

    deleteConditionRow(index: number) {
        const control = <FormArray>this.cashConcessionForm.controls['conditionItemsRows'];
        control.removeAt(index);

        this.selectedConditionTypes.splice(index, 1);
    }

    conditionTypeChanged(rowIndex) {
        const control = <FormArray>this.cashConcessionForm.controls['conditionItemsRows'];
        this.selectedConditionTypes[rowIndex] = control.controls[rowIndex].get('conditionType').value;

        let currentCondition = control.controls[rowIndex];

        currentCondition.get('interestRate').setValue(null);
        currentCondition.get('volume').setValue(null);
        currentCondition.get('value').setValue(null);
        currentCondition.get('expectedTurnoverValue').setValue(null);
    }

    tableNumberChanged(rowIndex) {
        const control = <FormArray>this.cashConcessionForm.controls['concessionItemRows'];

        if (control.controls[rowIndex].get('tableNumber').value.baseRate)
            control.controls[rowIndex].get('baseRate').setValue(control.controls[rowIndex].get('tableNumber').value.baseRate.toFixed(2));
        else
            control.controls[rowIndex].get('baseRate').setValue(null);

        if (control.controls[rowIndex].get('tableNumber').value.adValorem)
            control.controls[rowIndex].get('adValorem').setValue(control.controls[rowIndex].get('tableNumber').value.adValorem.toFixed(3));
        else
            control.controls[rowIndex].get('adValorem').setValue(null);
    }

    addValidationError(validationDetail) {
        if (!this.validationError)
            this.validationError = [];

        this.validationError.push(validationDetail);
    }

    getCashConcession(): CashConcession {
        var cashConcession = new CashConcession();
        cashConcession.concession = new Concession();
        cashConcession.concession.riskGroupId = this.riskGroup.id;     

        if (this.cashConcessionForm.controls['smtDealNumber'].value)
            cashConcession.concession.smtDealNumber = this.cashConcessionForm.controls['smtDealNumber'].value;
        else
            this.addValidationError("SMT Deal Number not captured");

        if (this.cashConcessionForm.controls['motivation'].value)
            cashConcession.concession.motivation = this.cashConcessionForm.controls['motivation'].value;
        else
            this.addValidationError("Motivation not captured");

        const concessions = <FormArray>this.cashConcessionForm.controls['concessionItemRows'];

        for (let concessionFormItem of concessions.controls) {
            if (!cashConcession.cashConcessionDetails)
                cashConcession.cashConcessionDetails = [];

            let cashConcessionDetail = new CashConcessionDetail();

            if (concessionFormItem.get('channelType').value) {
                cashConcessionDetail.channelTypeId = concessionFormItem.get('channelType').value.id;
            } else {
                this.addValidationError("Channel type not selected");
            }

         

            if (concessionFormItem.get('accountNumber').value && concessionFormItem.get('accountNumber').value.legalEntityId) {
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

    onSubmit() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;
      

        var cashConcession = this.getCashConcession();
        
        cashConcession.concession.concessionType = ConcessionTypes.Cash;
        cashConcession.concession.type = "New";

        if (!this.validationError) {
            this.cashConcessionService.postNewCashData(cashConcession).subscribe(entity => {
                console.log("data saved");
                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;
            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        } else {
            this.isLoading = false;
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

    goBack() {
        this.location.back();
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}
