import { Injectable } from '@angular/core';
import * as XLSX from 'xlsx';
import { XlsxModel } from '../models/XlsxModel';
import * as fileSaver from 'file-saver';
import { FileService } from '../services/file.service';
import { TransactionalConcessionDetail } from "../models/transactional-concession-detail";
import { TransactionalConcessionEnum } from "../models//transactional-concession-enum";

@Injectable()
export class TransactionalBaseService {

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
                    //case CashConcessionEnum.ANumber: -- might be used 
                    //    detail. = cell.v;
                    //    break;
                    //case CashConcessionEnum.RiskGroup:
                    //    detail. = cell.v;
                    //    break;
                    case TransactionalConcessionEnum.AccNumber:
                        detail.accountNumber = cell.v;
                        break;
                    case TransactionalConcessionEnum.TableNumber:
                        detail.approvedTableNumber = cell.v;
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

        return transactionalConcessionDetails;
    }

}
