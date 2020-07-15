import { Injectable } from '@angular/core';
import * as XLSX from 'xlsx';
import { XlsxModel } from '../models/XlsxModel';
import * as fileSaver from 'file-saver';
import { FileService } from '../services/file.service';
import { TransactionalConcessionDetail } from "../models/transactional-concession-detail";
import { TransactionalConcessionEnum } from "../models//transactional-concession-enum";
import { ConditionType } from "../models/condition-type";
import { TransactionalConcession } from '../models/transactional-concession';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { TransactionType } from "../models/transaction-type";
import { ClientAccount } from "../models/client-account";
import { Location, DatePipe } from '@angular/common';

@Injectable()
export class TransactionalBaseService {
    validationError: String[];
    fileService: FileService;
    xlsxModel = new XlsxModel();
    transactionalConcessionDetail: TransactionalConcessionDetail[];
    public transactionalConcessionForm: FormGroup;
    transactionTypes: TransactionType[];
    selectedTransactionTypes: TransactionType[];
    clientAccounts: ClientAccount[];
    datepipe: DatePipe;
    formBuilder: FormBuilder;

    constructor() { }

    public processFileContent(xlsxModel: XlsxModel): TransactionalConcessionDetail[] {

        var self = this;

        let transactionalConcessionDetails = [new TransactionalConcessionDetail()];

        let workbook = XLSX.read(xlsxModel.fileContent, { type: "binary" });
        let sheetName = workbook.SheetNames[0];
        let sheet = workbook.Sheets[sheetName];

        // range.s refers to starting point.
        // range.e refers to ending point.
        var range = XLSX.utils.decode_range(sheet['!ref']);

        let rowCount: number = range.e.r;
        let colCount: number = range.e.c + 1;

        let cell: any;

        for (let rowNum = range.s.r; rowNum <= rowCount; rowNum++) {
            if (rowNum == 0)
                continue;

            let detail = new TransactionalConcessionDetail();
            transactionalConcessionDetails.push(detail);

            for (let colNum = range.s.c; colNum <= colCount; colNum++) {

                // get the cell value.
                cell = sheet[XLSX.utils.encode_cell({ r: rowNum, c: colNum })];

                // ignore null cells.
                if (cell == null) { continue; }

                switch (colNum) {
                    case TransactionalConcessionEnum.AccNumber:
                        detail.accountNumber = cell.v;
                        break;
                    case TransactionalConcessionEnum.TableNumber:
                        detail.approvedTransactionTableNumberId = cell.v;
                        break;
                    case TransactionalConcessionEnum.TransType:
                        detail.transactionType = cell.v;
                        break;
                    case TransactionalConcessionEnum.ExpiryDate:
                        detail.expiryDate = new Date(cell.w);
                        break;
                }

            }
        }

        return transactionalConcessionDetails.filter(value => JSON.stringify(value) !== '{}');;
    }

    disableFieldBase(fieldname: string, canEdit: boolean, index: number = null, selectedConditionTypes: ConditionType[], isRecalling: boolean = null, motivationEnabled: boolean = null) {
        switch (fieldname) {
            case 'smtDealNumber':
                if (isRecalling == null) {
                    return canEdit ? null : '';
                } else {
                    return (isRecalling || canEdit) ? null : '';
                }
            case 'motivation':
                if (motivationEnabled == null) {
                    return canEdit ? null : '';
                } else {
                    return motivationEnabled ? null : '';
                }
            case 'transactionType':
            case 'accountNumber':
            case 'transactionTableNumber':
            case 'expiryDate':
                return canEdit ? null : '';
            case 'interestRate':
                return selectedConditionTypes[index] != null && selectedConditionTypes[index].enableInterestRate ? null : '';
            case 'volume':
                return selectedConditionTypes[index] != null && selectedConditionTypes[index].enableConditionVolume ? null : '';
            case 'value':
                return selectedConditionTypes[index] != null && selectedConditionTypes[index].enableConditionValue ? null : '';
            default:
                break;
        }
    }

    addValidationError(validationDetail) {
        if (!this.validationError)
            this.validationError = [];

        if (!this.validationError.includes(validationDetail)) {
            this.validationError.push(validationDetail);
        }
    }

    checkConcessionExpiryDate(transactionalConcession: TransactionalConcession) {
        if (transactionalConcession.transactionalConcessionDetails.length > 1) {
            var firstDate;
            transactionalConcession.transactionalConcessionDetails.forEach(concession => {
                if (!firstDate) {
                    firstDate = concession.expiryDate;
                } else if (firstDate.getTime() != concession.expiryDate.getTime()) {
                    this.addValidationError("All concessions must have the same expiry date.");
                }
            });
        }
    }

    downloadFile(name) {
        this.fileService.downloadFile(name).subscribe(response => {
            window.location.href = response.url;
        }), error => console.log('Error downloading the file'),
            () => console.info('File downloaded successfully');
    }

    onFileSelected(event, isNewConcession: boolean) {

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
            self.populateTransactionalConcessionByFile(isNewConcession);
        }

        // execute reading of the file
        fileReader.readAsBinaryString(file);
    }

    populateTransactionalConcessionByFile(isNewConcession: boolean) {

        let rowIndex = 0;

        for (let transactionalConcessionDetail of this.transactionalConcessionDetail) {

            if (rowIndex != 0) {
                this.addNewConcessionRow(isNewConcession, false);
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
                    if (selectedAccountNo.length > 0) {
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

    addNewConcessionRow(isNewConcession: boolean, isClickEvent: boolean = false) {
        const control = <FormArray>this.transactionalConcessionForm.controls['concessionItemRows'];
        var newRow;

        if (isNewConcession) {
            newRow = this.initConcessionItemRowsAdd();
        } else {
            newRow = this.initConcessionItemRowsUpdate();
        }

        var length = control.controls.length;

        if (this.transactionTypes)
            newRow.controls['transactionType'].setValue(this.transactionTypes[0]);

        if (this.clientAccounts)
            newRow.controls['accountNumber'].setValue(this.clientAccounts[0]);

        this.selectedTransactionTypes[length] = this.transactionTypes[0];

        if (this.transactionTypes && this.transactionTypes[0].transactionTableNumbers)
            newRow.controls['transactionTableNumber'].setValue(this.transactionTypes[0].transactionTableNumbers[0]);

        if (isClickEvent) {
            if (control != null && control.length > 0) {
                let expiryDate = control.controls[0].get('expiryDate').value;
                if (expiryDate != null) {
                    newRow.controls['expiryDate'].setValue(expiryDate);
                }
            }
        }

        control.push(newRow);

        this.transactionTableNumberChanged(length);
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

    initConcessionItemRowsAdd() {
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

    initConcessionItemRowsUpdate() {
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
}
