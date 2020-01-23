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
import { LegalEntity } from "../models/legal-entity";
import { DecimalPipe } from '@angular/common';
import { ConcessionTypes } from '../constants/concession-types';
import * as moment from 'moment';
import { MOnthEnum } from '../models/month-enum';
import * as fileSaver from 'file-saver';
import { FileService } from '../services/file.service';
import * as XLSX from 'xlsx';
import { XlsxModel } from '../models/XlsxModel';
import { CashBaseService } from '../services/cash-base.service';

import { BaseComponentService } from '../services/base-component.service';

@Component({
    selector: 'app-cash-add-concession',
    templateUrl: './cash-add-concession.component.html',
    styleUrls: ['./cash-add-concession.component.css'],
    providers: [DatePipe]
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
    legalEntity: LegalEntity;
    sapbpid: number;
    cashConcessionDetail = [new CashConcessionDetail()];
    xlsxModel = new XlsxModel();

    subHeading: string;
    title: string;

    public cashConcessionForm: FormGroup;
    selectedConditionTypes: ConditionType[];
    isLoading = true;
    observableLatestCrsOrMrs: Observable<number>;
    latestCrsOrMrs: number = 0;

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
        @Inject(CashConcessionService) private cashConcessionService,
        @Inject(CashBaseService) private cashBaseService,
        private fileService: FileService,
        private datepipe: DatePipe,
        private baseComponentService: BaseComponentService) {
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
            this.sapbpid = +params['sapbpid'];
        });

        this.cashConcessionForm = this.formBuilder.group({
            concessionItemRows: this.formBuilder.array([this.initConcessionItemRows()]),
            conditionItemsRows: this.formBuilder.array([]),
            smtDealNumber: new FormControl(),
            motivation: new FormControl()
        });

        this.getInitialData();

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
            periodType: [''],
            period: ['']
        });
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
                this.cashConcessionService.getlatestCrsOrMrs(this.riskGroupNumber)
            ]).subscribe(results => {
                this.setInitialData(results, true);
            },
                error => {
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
                this.lookupDataService.getClientAccountsConcessionType(this.riskGroupNumber, this.sapbpid, ConcessionTypes.Cash)
            ]).subscribe(results => {
                this.setInitialData(results, false);
            },
                error => {
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
            this.latestCrsOrMrs = <any>results[8];
            this.subHeading = this.riskGroup.name;
            this.title = this.riskGroup.number.toString();
        }
        else {
            this.legalEntity = <any>results[6];
            this.subHeading = this.legalEntity.customerName;
            this.title = this.legalEntity.customerNumber;
        }

        this.clientAccounts = <any>results[7];


        const control = <FormArray>this.cashConcessionForm.controls['concessionItemRows'];

        if (this.channelTypes)
            control.controls[0].get('channelType').setValue(this.channelTypes[0]);


        if (this.clientAccounts)
            control.controls[0].get('accountNumber').setValue(this.clientAccounts[0]);

        if (this.accrualTypes)
            control.controls[0].get('accrualType').setValue(this.accrualTypes[0]);

        if (this.tableNumbers) {
            control.controls[0].get('tableNumber').setValue(this.tableNumbers[0]);

            this.tableNumberChanged(0);

        }

        this.isLoading = false;
    }

    downloadFile(name: string) {
        this.fileService.downloadFile(name).subscribe(response => {
            window.location.href = response.url;
        }), error => console.log('Error downloading the file'),
            () => console.info('File downloaded successfully');
    }

    onFileSelected(event) {
        var file: File = event.target.files[0];
        var fileReader: FileReader = new FileReader();

        this.xlsxModel = new XlsxModel();
        this.cashConcessionDetail = [new CashConcessionDetail()];

        var self = this;
        fileReader.onload = function (e) {
            // set initial properties in order to process the file
            self.xlsxModel.fileContent = fileReader.result;
            self.xlsxModel.selectedFileName = file.name;

            self.cashConcessionDetail = self.cashBaseService.processFileContent(self.xlsxModel);
            self.populateCashConcessionByFile();
        }

        fileReader.readAsBinaryString(file);
    }

    populateCashConcessionByFile() {

        let rowIndex = 0;

        for (let cashConcessionDetail of this.cashConcessionDetail) {

            if (rowIndex != 0) {
                this.addNewConcessionRow();
            }

            const concessions = <FormArray>this.cashConcessionForm.controls['concessionItemRows'];
            let currentConcession = concessions.controls[concessions.length - 1];

            if (cashConcessionDetail.channel) {
                let selectedChannelType = this.channelTypes.filter(_ => _.description == cashConcessionDetail.channel);
                currentConcession.get('channelType').setValue(selectedChannelType[0]);
            }

            if (cashConcessionDetail.accountNumber) {

                if (this.clientAccounts) {

                    let selectedAccountNo = this.clientAccounts.filter(_ => _.accountNumber == cashConcessionDetail.accountNumber);
                    if (selectedAccountNo.length > 0) {
                        currentConcession.get('accountNumber').setValue(selectedAccountNo[0]);
                    } else {
                        this.cashBaseService.addValidationError('AccountNumber does not belong to selected risk group');
                    }  
                }  
            }

            if (cashConcessionDetail.tableNumberId) {
                let selectedTableNumber = this.tableNumbers.filter(_ => _.tariffTable == cashConcessionDetail.tableNumberId);
                currentConcession.get('tableNumber').setValue(selectedTableNumber[0]);
                this.tableNumberChanged(concessions.length - 1);
            }

            if (cashConcessionDetail.accrualType) {
                let selectedAccrualType = this.accrualTypes.filter(_ => _.description == cashConcessionDetail.accrualType);
                currentConcession.get('accrualType').setValue(selectedAccrualType[0]);
            }

            if (cashConcessionDetail.expiryDate) {
                var formattedExpiryDate = this.datepipe.transform(cashConcessionDetail.expiryDate, 'yyyy-MM-dd');
                currentConcession.get('expiryDate').setValue(formattedExpiryDate);
            }

            rowIndex++;
        }
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

        if (control.controls[index].get('tableNumber').value.adValorem)
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

        currentCondition.get('conditionProduct').setValue(null);
        currentCondition.get('interestRate').setValue(null);
        currentCondition.get('volume').setValue(null);
        currentCondition.get('value').setValue(null);
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

    onExpiryDateChanged(itemrow) {
        
        var validationErrorMessage = this.baseComponentService.expiringDateDifferenceValidation(itemrow.controls['expiryDate'].value);
        if (validationErrorMessage != null) {
            this.cashBaseService.addValidationError(validationErrorMessage);
        }
    }

    getCashConcession(): CashConcession {
        var cashConcession = new CashConcession();
        cashConcession.concession = new Concession();

        if (this.riskGroup)
            cashConcession.concession.riskGroupId = this.riskGroup.id;
        if (this.legalEntity)
            cashConcession.concession.legalEntityId = this.legalEntity.id;

        if (this.cashConcessionForm.controls['smtDealNumber'].value)
            cashConcession.concession.smtDealNumber = this.cashConcessionForm.controls['smtDealNumber'].value;
        else
            this.cashBaseService.addValidationError("SMT Deal Number not captured");

        if (this.cashConcessionForm.controls['motivation'].value)
            cashConcession.concession.motivation = this.cashConcessionForm.controls['motivation'].value;
        else
            cashConcession.concession.motivation = '.';

        const concessions = <FormArray>this.cashConcessionForm.controls['concessionItemRows'];

        for (let concessionFormItem of concessions.controls) {
            if (!cashConcession.cashConcessionDetails)
                cashConcession.cashConcessionDetails = [];

            let cashConcessionDetail = new CashConcessionDetail();

            if (concessionFormItem.get('channelType').value) {
                cashConcessionDetail.channelTypeId = concessionFormItem.get('channelType').value.id;
            } else {
                this.cashBaseService.addValidationError("Channel type not selected");
            }

            if (concessionFormItem.get('expiryDate').value && concessionFormItem.get('expiryDate').value != "") {
                this.onExpiryDateChanged(concessionFormItem);
                cashConcessionDetail.expiryDate = new Date(concessionFormItem.get('expiryDate').value);
            }
            else {
                this.cashBaseService.addValidationError("Expiry date not selected");
            }

            if (concessionFormItem.get('accountNumber').value && concessionFormItem.get('accountNumber').value.legalEntityId) {
                cashConcessionDetail.legalEntityId = concessionFormItem.get('accountNumber').value.legalEntityId;
                cashConcessionDetail.legalEntityAccountId = concessionFormItem.get('accountNumber').value.legalEntityAccountId;
            } else {
                this.cashBaseService.addValidationError("Client account not selected");
            }

            if (concessionFormItem.get('tableNumber').value) {
                cashConcessionDetail.tableNumberId = concessionFormItem.get('tableNumber').value.id;
                cashConcessionDetail.adValorem = concessionFormItem.get('tableNumber').value.adValorem;

                if (concessionFormItem.get('tableNumber').value.baseRate)
                    cashConcessionDetail.baseRate = concessionFormItem.get('tableNumber').value.baseRate;
            } else {
                this.cashBaseService.addValidationError("Table Number not selected");
            }

            if (concessionFormItem.get('accrualType').value) {
                cashConcessionDetail.accrualTypeId = concessionFormItem.get('accrualType').value.id;
            } else {
                this.cashBaseService.addValidationError("Accrual type not selected");
            }

            cashConcession.cashConcessionDetails.push(cashConcessionDetail);

            let hasDuplicates = this.baseComponentService.HasDuplicateConcessionAccountChannel(
                cashConcession.cashConcessionDetails,
                concessionFormItem.get('channelType').value.id,
                concessionFormItem.get('accountNumber').value.legalEntityId,
                concessionFormItem.get('accountNumber').value.legalEntityAccountId);

            if (hasDuplicates) {
                this.cashBaseService.addValidationError("Duplicate Account / Channel pricing found. Please select different account.");

                break;
            }
        }

        const conditions = <FormArray>this.cashConcessionForm.controls['conditionItemsRows'];

        for (let conditionFormItem of conditions.controls) {
            if (!cashConcession.concessionConditions)
                cashConcession.concessionConditions = [];

            let concessionCondition = new ConcessionCondition();

            if (conditionFormItem.get('conditionType').value)
                concessionCondition.conditionTypeId = conditionFormItem.get('conditionType').value.id;
            else
                this.cashBaseService.addValidationError("Condition type not selected");

            if (conditionFormItem.get('conditionProduct').value)
                concessionCondition.conditionProductId = conditionFormItem.get('conditionProduct').value.id;
            else
                this.cashBaseService.addValidationError("Condition product not selected");

            if (conditionFormItem.get('interestRate').value)
                concessionCondition.interestRate = conditionFormItem.get('interestRate').value;

            if (conditionFormItem.get('volume').value)
                concessionCondition.conditionVolume = conditionFormItem.get('volume').value;

            if (conditionFormItem.get('value').value == null || (<string>conditionFormItem.get('value').value).length < 1) {
                var value = conditionFormItem.get('conditionType').value;
                if (value != null && value.enableConditionValue == true)
                    this.cashBaseService.addValidationError("Conditions: 'Value' is a mandatory field");
            }
            else if (conditionFormItem.get('value').value)
                concessionCondition.conditionValue = conditionFormItem.get('value').value;

            if (conditionFormItem.get('periodType').value) {
                concessionCondition.periodTypeId = conditionFormItem.get('periodType').value.id;
            } else {
                this.cashBaseService.addValidationError("Period type not selected");
            }

            if (conditionFormItem.get('period').value) {
                concessionCondition.periodId = conditionFormItem.get('period').value.id;
            } else {
                this.cashBaseService.addValidationError("Period not selected");
            }

            if (conditionFormItem.get('periodType').value.description == 'Once-off' && conditionFormItem.get('period').value.description == 'Monthly') {
                this.cashBaseService.addValidationError("Conditions: The Period 'Monthly' cannot be selected for Period Type 'Once-off'");
            }

            cashConcession.concessionConditions.push(concessionCondition);
        }

        this.cashBaseService.checkConcessionExpiryDate(cashConcession);

        return cashConcession;
    }

    onSubmit() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;


        var cashConcession = this.getCashConcession();

        cashConcession.concession.concessionType = ConcessionTypes.Cash;
        cashConcession.concession.type = "New";
        cashConcession.concession.comments = "Created";

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
        $event.target.value = this.baseComponentService.formatDecimal($event.target.value);

    }

    disableField(index: number, fieldname: string) {
        return this.cashBaseService.disableFieldBase(
            this.selectedConditionTypes[index],
            fieldname,
            this.saveMessage == null,
            this.saveMessage != null
        );
    }

    goBack() {
        this.location.back();
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

    validatePeriod(itemrow) {
        this.validationError = null;

        let selectedPeriodType = itemrow.controls.periodType.value.description;

        let selectedPeriod = itemrow.controls.period.value.description;

        if (selectedPeriodType == 'Once-off' && selectedPeriod == 'Monthly') {
            this.cashBaseService.addValidationError("Conditions: The Period 'Monthly' cannot be selected for Period Type 'Once-off'");
        }
    }
}
