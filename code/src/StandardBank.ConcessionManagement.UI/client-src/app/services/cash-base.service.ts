import { DatePipe } from '@angular/common';
import { Injectable } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import * as XLSX from 'xlsx';
import { AccrualType } from "../models/accrual-type";
import { CashConcession } from '../models/cash-concession';
import { CashConcessionDetail } from "../models/cash-concession-detail";
import { CashConcessionEnum } from "../models/cash-concession-enum";
import { ChannelType } from "../models/channel-type";
import { ClientAccount } from "../models/client-account";
import { ConditionType } from "../models/condition-type";
import { TableNumber } from "../models/table-number";
import { XlsxModel } from '../models/XlsxModel';
import { FileService } from '../services/file.service';

@Injectable()
export class CashBaseService {
    validationError: String[];
    xlsxModel = new XlsxModel();
    cashConcessionDetail = [new CashConcessionDetail()];
    public cashConcessionForm: FormGroup;
    channelTypes: ChannelType[];
    clientAccounts: ClientAccount[];
    tableNumbers: TableNumber[];
    accrualTypes: AccrualType[];
    datepipe: DatePipe;
    formBuilder: FormBuilder;
    fileService: FileService;

    constructor() {}

    public processFileContent(xlsxModel: XlsxModel): CashConcessionDetail[] {
        var self = this;

        let cashConcessionDetails = [new CashConcessionDetail()];

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

            let detail = new CashConcessionDetail();
            if (detail != null) {
                cashConcessionDetails.push(detail);
            }

            for (let colNum = range.s.c; colNum <= colCount; colNum++) {
                // get the cell value.
                cell = sheet[XLSX.utils.encode_cell({ r: rowNum, c: colNum })];

                // ignore null cells.
                if (cell == null) { continue; }

                switch (colNum) {
                    case CashConcessionEnum.AccNumber:
                        detail.accountNumber = cell.v;
                        break;
                    case CashConcessionEnum.Channel:
                        detail.channel = cell.v;
                        break;
                    case CashConcessionEnum.TableNumber:
                        detail.tableNumberId = cell.v;
                        break;
                    case CashConcessionEnum.Accrual:
                        detail.accrualType = cell.v;
                        break;
                    case CashConcessionEnum.ExpiryDate:
                        detail.expiryDate = new Date(cell.w);
                        break;
                }
            }
        }

        return cashConcessionDetails.filter(value => JSON.stringify(value) !== '{}');
    }

    disableFieldBase(
        selectedConditionType: ConditionType,
        fieldName: string,
        canEdit: boolean,
        canSaveMessage: boolean) {
        switch (fieldName) {
            case 'channelType':
            case 'accountNumber':
            case 'tableNumber':
            case 'accrualType':
            case 'expiryDate':
                return (!canSaveMessage || canEdit) ? null : '';
            case 'interestRate':
                return selectedConditionType != null && selectedConditionType.enableInterestRate ? null : '';
            case 'volume':
                return selectedConditionType != null && selectedConditionType.enableConditionVolume ? null : '';
            case 'value':
                return selectedConditionType != null && selectedConditionType.enableConditionValue ? null : '';
        }
    }
    addValidationError(validationDetail) {
        if (!this.validationError)
            this.validationError = [];

        if (!this.validationError.includes(validationDetail)) {
            this.validationError.push(validationDetail);
        }
    }

    checkConcessionExpiryDate(cashConcession: CashConcession) {
        if (cashConcession.cashConcessionDetails.length > 1) {
            var firstDate;
            cashConcession.cashConcessionDetails.forEach(concession => {
                if (!firstDate) {
                    firstDate = concession.expiryDate;
                } else if (firstDate.getTime() != concession.expiryDate.getTime()) {
                    this.addValidationError("All concessions must have the same expiry date.");
                }
            });
        }
    }

    onFileSelected(event, add) {
        var file: File = event.target.files[0];
        var fileReader: FileReader = new FileReader();

        this.xlsxModel = new XlsxModel();
        this.cashConcessionDetail = [new CashConcessionDetail()];

        var self = this;
        fileReader.onload = function (e) {
            // set initial properties in order to process the file
            self.xlsxModel.fileContent = fileReader.result;
            self.xlsxModel.selectedFileName = file.name;

            self.cashConcessionDetail = self.processFileContent(self.xlsxModel);
            self.populateCashConcessionByFile(add);
        }

        fileReader.readAsBinaryString(file);
    }

    populateCashConcessionByFile(add) {

        let rowIndex = 0;

        for (let cashConcessionDetail of this.cashConcessionDetail) {

            if (rowIndex != 0) {
                this.addNewConcessionRow(add);
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
                        this.addValidationError('AccountNumber does not belong to selected risk group');
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


    addNewConcessionRow(add) {
        const control = <FormArray>this.cashConcessionForm.controls['concessionItemRows'];
        var newRow;

        if (add) {
            newRow = this.initConcessionItemRowsAdd();
        } else {
            newRow = this.initConcessionItemRowsUpdate();
        }

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

    initConcessionItemRowsAdd() {
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

    initConcessionItemRowsUpdate() {
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

    downloadFile(name: string) {
        this.fileService.downloadFile(name).subscribe(response => {
            window.location.href = response.url;
        }), error => console.log('Error downloading the file'),
            () => console.info('File downloaded successfully');
    }

}
