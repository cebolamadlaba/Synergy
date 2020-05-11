import { Injectable } from '@angular/core';
import * as XLSX from 'xlsx';
import { XlsxModel } from '../models/XlsxModel';
import * as fileSaver from 'file-saver';
import { FileService } from '../services/file.service';
import { TransactionalConcessionDetail } from "../models/transactional-concession-detail";
import { TransactionalConcessionEnum } from "../models//transactional-concession-enum";
import { ConditionType } from "../models/condition-type";
import { TransactionalConcession } from '../models/transactional-concession';

@Injectable()
export class TransactionalBaseService {
    validationError: String[];

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
                        var expiryDate = cell.w.split('/')
                        var newDate = expiryDate[1] + '/' + expiryDate[0] + '/' + expiryDate[2];

                        detail.expiryDate = new Date(newDate);
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
}
