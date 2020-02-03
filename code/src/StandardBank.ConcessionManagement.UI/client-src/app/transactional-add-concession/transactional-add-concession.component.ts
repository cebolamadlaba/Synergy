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
import { DecimalPipe } from '@angular/common';
import { ConcessionTypes } from '../constants/concession-types';
import { BaseComponentService } from '../services/base-component.service';
import { LegalEntity } from "../models/legal-entity";
import * as moment from 'moment';
import { MOnthEnum } from '../models/month-enum';
import { ConcessionConditionReturnObject } from '../models/concession-condition-return-object';
import * as fileSaver from 'file-saver';
import { FileService } from '../services/file.service';
import * as XLSX from 'xlsx';
import { XlsxModel } from '../models/XlsxModel';
import { TransactionalBaseService } from '../services/transactional-base.service';

@Component({
    selector: 'app-transactional-add-concession',
    templateUrl: './transactional-add-concession.component.html',
    styleUrls: ['./transactional-add-concession.component.css'],
    providers: [DatePipe]
})
export class TransactionalAddConcessionComponent extends TransactionalBaseService implements OnInit, OnDestroy {
    public transactionalConcessionForm: FormGroup;
    private sub: any;
    errorMessage: String;
    saveMessage: String;
    observableRiskGroup: Observable<RiskGroup>;
    riskGroup: RiskGroup;
    riskGroupNumber: number;
    legalEntity: LegalEntity;
    sapbpid: number;
    transactionalConcessionDetail: TransactionalConcessionDetail[];

    entityName: string;
    entityNumber: string;

    selectedConditionTypes: ConditionType[];
    selectedTransactionTypes: TransactionType[];
    isLoading = true;
    observableLatestCrsOrMrs: Observable<number>;
    latestCrsOrMrs: number;
    showHide = false;

    xlsxModel = new XlsxModel();

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

    constructor(private route: ActivatedRoute,
        private formBuilder: FormBuilder,
        private location: Location,
        private datepipe: DatePipe,
        @Inject(LookupDataService) private lookupDataService,
        @Inject(TransactionalConcessionService) private transactionalConcessionService,
        private fileService: FileService,
        private baseComponentService: BaseComponentService) {
        super();
        this.riskGroup = new RiskGroup();
        this.periods = [new Period()];
        this.periodTypes = [new PeriodType()];
        this.conditionTypes = [new ConditionType()];
        this.selectedConditionTypes = [new ConditionType()];
        this.selectedTransactionTypes = [new TransactionType()];
        this.clientAccounts = [new ClientAccount()];
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
            this.sapbpid = +params['sapbpid'];
        });

        this.transactionalConcessionForm = this.formBuilder.group({
            concessionItemRows: this.formBuilder.array([this.initConcessionItemRows()]),
            conditionItemsRows: this.formBuilder.array([]),
            mrsCrs: new FormControl(),
            smtDealNumber: new FormControl(),
            motivation: new FormControl()
        });

        this.getInitialData();
    }

    getInitialData() {
        if (this.riskGroupNumber != null && this.riskGroupNumber != 0) {
            Observable.forkJoin([
                this.lookupDataService.getPeriods(),
                this.lookupDataService.getPeriodTypes(),
                this.lookupDataService.getConditionTypes(),
                this.lookupDataService.getTransactionTypes(ConcessionTypes.Transactional),
                this.lookupDataService.getRiskGroup(this.riskGroupNumber),
                this.lookupDataService.getClientAccountsConcessionType(this.riskGroupNumber, this.sapbpid, ConcessionTypes.Transactional),
                this.transactionalConcessionService.getlatestCrsOrMrs(this.riskGroupNumber)
            ]).subscribe(results => {

                this.setInitialData(results, true);

            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        }
        else if (this.sapbpid != null && this.sapbpid != 0) {
            Observable.forkJoin([
                this.lookupDataService.getPeriods(),
                this.lookupDataService.getPeriodTypes(),
                this.lookupDataService.getConditionTypes(),
                this.lookupDataService.getTransactionTypes(ConcessionTypes.Transactional),
                this.lookupDataService.getLegalEntity(this.sapbpid),
                this.lookupDataService.getClientAccountsConcessionType(this.riskGroupNumber, this.sapbpid, ConcessionTypes.Transactional),

            ]).subscribe(results => {

                this.setInitialData(results, false);

            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        }
    }

    setInitialData(results: {}[], isForRiskGroup: boolean) {
        this.periods = <any>results[0];
        this.periodTypes = <any>results[1];
        this.conditionTypes = <any>results[2];
        this.transactionTypes = <any>results[3];

        if (isForRiskGroup) {
            this.riskGroup = <any>results[4];
            this.latestCrsOrMrs = <any>results[6];
            this.entityName = this.riskGroup.name;
            this.entityNumber = this.riskGroup.number.toString();
        }
        else {
            this.legalEntity = <any>results[4];
            this.latestCrsOrMrs = 0;
            this.entityName = this.legalEntity.customerName;
            this.entityNumber = this.legalEntity.customerNumber;
        }

        this.clientAccounts = <any>results[5];

        const control = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];

        if (this.transactionTypes)
            control.controls[0].get('transactionType').setValue(this.transactionTypes[0]);

        if (this.clientAccounts)
            control.controls[0].get('accountNumber').setValue(this.clientAccounts[0]);

        this.selectedTransactionTypes[0] = this.transactionTypes[0];
        let currentTransactionType = control.controls[0];
        currentTransactionType.get('adValorem').setValue(null);
        currentTransactionType.get('flatFeeOrRate').setValue(null);

        if (this.selectedTransactionTypes[0].transactionTableNumbers)
            control.controls[0].get('transactionTableNumber').setValue(this.selectedTransactionTypes[0].transactionTableNumbers[0]);

        this.transactionTableNumberChanged(0);

        this.isLoading = false;
    }

    initConcessionItemRows() {
        this.selectedTransactionTypes.push(new TransactionType());

        return this.formBuilder.group({
            transactionType: [''],
            accountNumber: [''],
            transactionTableNumber: [''],
            flatFeeOrRate: [{ value: '', disabled: true }],
            adValorem: [{ value: '', disabled: true }],
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
            conditionComment: [''],
            periodType: [''],
            period: ['']
        });
    }

    downloadFile(name) {
        this.fileService.downloadFile(name).subscribe(response => {
            window.location.href = response.url;
        }), error => console.log('Error downloading the file'),
            () => console.info('File downloaded successfully');
    }

    onFileSelected(event) {

        var file: File = event.target.files[0];
        var fileReader: FileReader = new FileReader();

        this.xlsxModel = new XlsxModel();
        this.transactionalConcessionDetail = [new TransactionalConcessionDetail()];

        var self = this;
        fileReader.onload = function (e) {
            // set initial properties in order to process the file
            self.xlsxModel.fileContent = fileReader.result;
            self.xlsxModel.selectedFileName = file.name;

            self.transactionalConcessionDetail = self.processFileContent(self.xlsxModel);
            self.populateTransactionalConcessionByFile();
        }

        // execute reading of the file
        fileReader.readAsBinaryString(file);
    }

    populateTransactionalConcessionByFile() {

        let rowIndex = 0;

        for (let transactionalConcessionDetail of this.transactionalConcessionDetail) {

            if (rowIndex != 0) {
                this.addNewConcessionRow();
            }

            const concessions = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];
            let currentConcession = concessions.controls[concessions.length - 1];

            if (transactionalConcessionDetail.transactionType) {
                let selectedTransactionType = this.transactionTypes.filter(_ => _.description === transactionalConcessionDetail.transactionType);
                if (selectedTransactionType.length > 0) {
                    currentConcession.get('transactionType').setValue(selectedTransactionType[0]);

                    this.selectedTransactionTypes[concessions.length - 1] = selectedTransactionType[0];
                    if (transactionalConcessionDetail.approvedTransactionTableNumberId && selectedTransactionType.length > 0) {

                        let selectedTransactionTableNumber = selectedTransactionType[0].transactionTableNumbers.filter(_ => _.tariffTable == transactionalConcessionDetail.approvedTransactionTableNumberId);
                        if (selectedTransactionTableNumber.length > 0) {

                            currentConcession.get('transactionTableNumber').setValue(selectedTransactionTableNumber[0]);
                            this.transactionTableNumberChanged(concessions.length - 1);

                        } else {
                            this.addValidationError('Table number is not linked to transactional type.');
                        }
                    }

                } else {

                    this.addValidationError('Transactional type added does not exist.');
                    currentConcession.get('transactionType').setValue(null);
                    currentConcession.get('transactionTableNumber').setValue(null);
                    currentConcession.get('flatFeeOrRate').setValue(null);
                }
            }

            if (transactionalConcessionDetail.accountNumber) {
                if (this.clientAccounts) {
                    let selectedAccountNo = this.clientAccounts.filter(_ => _.accountNumber == transactionalConcessionDetail.accountNumber);
                    if (selectedAccountNo != null) {
                        currentConcession.get('accountNumber').setValue(selectedAccountNo[0]);
                    } else {
                        this.addValidationError('AccountNumber doesnt belong to selected risk group');
                    }
                }
            }

            if (transactionalConcessionDetail.expiryDate) {
                var formattedExpiryDate = this.datepipe.transform(transactionalConcessionDetail.expiryDate, 'yyyy-MM-dd');
                currentConcession.get('expiryDate').setValue(formattedExpiryDate);
            }

            rowIndex++;
        }
    }

    addNewConcessionRow() {
        const control = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];

        var newRow = this.initConcessionItemRows();

        var length = control.controls.length;

        if (this.transactionTypes)
            newRow.controls['transactionType'].setValue(this.transactionTypes[0]);

        if (this.clientAccounts)
            newRow.controls['accountNumber'].setValue(this.clientAccounts[0]);

        this.selectedTransactionTypes[length] = this.transactionTypes[0];

        if (this.transactionTypes && this.transactionTypes[0].transactionTableNumbers)
            newRow.controls['transactionTableNumber'].setValue(this.transactionTypes[0].transactionTableNumbers[0]);

        control.push(newRow);

        this.transactionTableNumberChanged(length);
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
        if (confirm("Are you sure you want to remove this row?")) {
            const control = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];
            control.removeAt(index);

            this.selectedTransactionTypes.splice(index, 1);
        }
    }

    deleteConditionRow(index: number) {
        const control = <FormArray>this.transactionalConcessionForm.controls['conditionItemsRows'];
        control.removeAt(index);

        this.selectedConditionTypes.splice(index, 1);
    }

    conditionTypeChanged(rowIndex) {
        const control = <FormArray>this.transactionalConcessionForm.controls['conditionItemsRows'];
        this.selectedConditionTypes[rowIndex] = control.controls[rowIndex].get('conditionType').value;

        let currentCondition = control.controls[rowIndex];

        currentCondition.get('conditionProduct').setValue(null);
        currentCondition.get('interestRate').setValue(null);
        currentCondition.get('volume').setValue(null);
        currentCondition.get('value').setValue(null);
    }

    transactionTypeChanged(rowIndex) {
        const control = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];
        this.selectedTransactionTypes[rowIndex] = control.controls[rowIndex].get('transactionType').value;

        let currentTransactionType = control.controls[rowIndex];
        currentTransactionType.get('adValorem').setValue(null);
        currentTransactionType.get('flatFeeOrRate').setValue(null);

        control.controls[rowIndex].get('transactionTableNumber').setValue(this.selectedTransactionTypes[rowIndex].transactionTableNumbers[0]);
    }

    onExpiryDateChanged(itemrow) {
        var validationErrorMessage = this.baseComponentService.expiringDateDifferenceValidation(itemrow.controls['expiryDate'].value);

        if (validationErrorMessage != null) {
            this.addValidationError(validationErrorMessage);
        }
    }

    transactionTableNumberChanged(rowIndex) {
        const control = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];

        if (control.controls[rowIndex].get('transactionTableNumber').value.fee)
            control.controls[rowIndex].get('flatFeeOrRate').setValue(control.controls[rowIndex].get('transactionTableNumber').value.fee.toFixed(2));
        else
            control.controls[rowIndex].get('flatFeeOrRate').setValue(null);

        if (control.controls[rowIndex].get('transactionTableNumber').value.adValorem)
            control.controls[rowIndex].get('adValorem').setValue(control.controls[rowIndex].get('transactionTableNumber').value.adValorem.toFixed(3));
        else
            control.controls[rowIndex].get('adValorem').setValue(null);
    }

    getTransactionalConcession(): TransactionalConcession {
        var transactionalConcession = new TransactionalConcession();
        transactionalConcession.concession = new Concession();

        if (this.riskGroup)
            transactionalConcession.concession.riskGroupId = this.riskGroup.id;
        if (this.legalEntity)
            transactionalConcession.concession.legalEntityId = this.legalEntity.id;

        if (this.transactionalConcessionForm.controls['smtDealNumber'].value)
            transactionalConcession.concession.smtDealNumber = this.transactionalConcessionForm.controls['smtDealNumber'].value;
        else
            this.addValidationError("SMT Deal Number not captured");

        if (this.transactionalConcessionForm.controls['motivation'].value)
            transactionalConcession.concession.motivation = this.transactionalConcessionForm.controls['motivation'].value;
        else
            transactionalConcession.concession.motivation = '.';

        const concessions = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];

        let hasTypeId: boolean = false;
        let hasLegalEntityId: boolean = false;
        let hasLegalEntityAccountId: boolean = false;

        for (let concessionFormItem of concessions.controls) {
            if (!transactionalConcession.transactionalConcessionDetails)
                transactionalConcession.transactionalConcessionDetails = [];

            let transactionalConcessionDetail = new TransactionalConcessionDetail();

            if (concessionFormItem.get('transactionType').value) {
                transactionalConcessionDetail.transactionTypeId = concessionFormItem.get('transactionType').value.id;
                hasTypeId = true;
            }
            else
                this.addValidationError("Transaction type not selected");

            if (concessionFormItem.get('accountNumber').value) {
                transactionalConcessionDetail.legalEntityId = concessionFormItem.get('accountNumber').value.legalEntityId;
                transactionalConcessionDetail.legalEntityAccountId = concessionFormItem.get('accountNumber').value.legalEntityAccountId;
                hasLegalEntityId = true;
                hasLegalEntityAccountId = true;
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

            if (concessionFormItem.get('expiryDate').value && concessionFormItem.get('expiryDate').value != "") {
                this.onExpiryDateChanged(concessionFormItem);
                transactionalConcessionDetail.expiryDate = new Date(concessionFormItem.get('expiryDate').value);
            }
            else {
                this.addValidationError("Expiry date not selected");
            }

            transactionalConcession.transactionalConcessionDetails.push(transactionalConcessionDetail);

            if (hasTypeId && hasLegalEntityId && hasLegalEntityAccountId) {
                let hasDuplicates = this.baseComponentService.HasDuplicateConcessionAccountTransaction(
                    transactionalConcession.transactionalConcessionDetails,
                    concessionFormItem.get('transactionType').value.id,
                    concessionFormItem.get('accountNumber').value.legalEntityId,
                    concessionFormItem.get('accountNumber').value.legalEntityAccountId);

                if (hasDuplicates) {
                    this.addValidationError("Duplicate Account / Transaction pricing found. Please select different account.");

                    break;
                }
            }
        }

        const conditions = <FormArray>this.transactionalConcessionForm.controls['conditionItemsRows'];

        let concessionConditionReturnObject = this.baseComponentService.getConsessionConditionData(conditions, transactionalConcession.concessionConditions, this.validationError);
        transactionalConcession.concessionConditions = concessionConditionReturnObject.concessionConditions;
        this.validationError = concessionConditionReturnObject.validationError;

        return transactionalConcession;
    }

    onSubmit() {
        this.isLoading = true;

        this.errorMessage = null;
        this.validationError = null;

        var transactionalConcession = this.getTransactionalConcession();

        transactionalConcession.concession.concessionType = ConcessionTypes.Transactional;
        transactionalConcession.concession.type = "New";
        transactionalConcession.concession.comments = "Created";

        if (!this.validationError) {
            this.transactionalConcessionService.postNewTransactionalData(transactionalConcession).subscribe(entity => {
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
            this.addValidationError("Conditions: The Period 'Monthly' cannot be selected for Period Type 'Once-off'");
        }
    }

    disableField(fieldname: string, index: number = null) {
        return this.disableFieldBase(fieldname, this.saveMessage == null, index, this.selectedConditionTypes, null, null)
    }
}
