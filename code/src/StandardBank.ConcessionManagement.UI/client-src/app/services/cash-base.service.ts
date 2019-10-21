import { Injectable } from '@angular/core';
import * as XLSX from 'xlsx';
import { XlsxModel } from '../models/XlsxModel';
import * as fileSaver from 'file-saver';
import { FileService } from '../services/file.service';
import { CashConcessionDetail } from "../models/cash-concession-detail";
import { CashConcessionEnum } from "../models/cash-concession-enum";

@Injectable()
export class CashBaseService {

    constructor() { }


    public processFileContent(xlsxModel: XlsxModel): CashConcessionDetail[] {

        var self = this;

        let cashConcessionDetails = [ new CashConcessionDetail()];
         
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
             cashConcessionDetails.push(detail);

            for (let colNum = range.s.c; colNum <= colCount; colNum++) {

                // get the cell value.
                cell = sheet[XLSX.utils.encode_cell({ r: rowNum, c: colNum })];

                // ignore null cells.
                if (cell == null) { continue; }

                switch (colNum) {
                    //case CashConcessionEnum.ANumber:
                    //    detail. = cell.v;
                    //    break;
                    //case CashConcessionEnum.RiskGroup:
                    //    detail. = cell.v;
                    //    break;
                    case CashConcessionEnum.AccNumber:
                        detail.accountNumber = cell.v;
                        break;
                    case CashConcessionEnum.Channel:
                        detail.channel = cell.v;
                        break;
                    case CashConcessionEnum.TableNumber:
                        detail.approvedTableNumber = cell.v;
                        break;
                    case CashConcessionEnum.Accrual:
                        detail.accrualTypeId = cell.v;
                        break;
                    case CashConcessionEnum.ExpiryDate:
                        detail.expiryDate = new Date(cell.w);
                        break;
                }
               
            }
        }

        return cashConcessionDetails;
    }

}
