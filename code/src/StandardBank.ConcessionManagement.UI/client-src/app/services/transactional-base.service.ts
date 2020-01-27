import { Injectable } from '@angular/core';
import * as XLSX from 'xlsx';
import { XlsxModel } from '../models/XlsxModel';
import * as fileSaver from 'file-saver';
import { FileService } from '../services/file.service';
import { TransactionalConcessionDetail } from "../models/transactional-concession-detail";
import { TransactionalConcessionEnum } from "../models//transactional-concession-enum";
import { TransactionalConcession } from '../models/transactional-concession';
import { FormGroup } from '@angular/forms';
import { ConditionType } from '../models/condition-type';
import { ConcessionRelationshipDetail } from '../models/concession-relationship-detail';

@Injectable()
export class TransactionalBaseService {
    showHide = false;
    errorMessage: String;
    saveMessage: String;
    warningMessage: String;
    isLoading = true;
    canEdit = false;
    isRecalling = false;
    canBcmApprove = false;
    canPcmApprove = false;
    canApproveChanges: boolean;
    motivationEnabled = false;
    isEditing = false;
    canRecall = false;
    public transactionalConcessionForm: FormGroup;
    selectedConditionTypes: ConditionType[];
    transactionalConcession: TransactionalConcession;
    concessionReferenceId: string;

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
                        detail.expiryDate = new Date(cell.w);
                        break;
                }

            }
        }

        return transactionalConcessionDetails.filter(value => JSON.stringify(value) !== '{}');;
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

    compressClick() {
        this.showHide = !this.showHide;
    }

    disableFieldBase(fieldname: string, index: number = null, concessionRelationship: ConcessionRelationshipDetail = null) {
        switch (fieldname) {
            case 'errorMessage':
                return (this.errorMessage) && !this.isLoading;
            case 'validationError':
                return (this.validationError) && !this.isLoading;
            case 'saveMessage':
                return this.saveMessage && !this.isLoading;
            case 'warningMessage':
                return this.warningMessage && !this.isLoading;
            case 'viewSMTDealNumber':
                return (this.isRecalling || this.canEdit) ? null : '';
            case 'viewMotivation':
                return this.motivationEnabled ? null : '';
            case 'viewComments':
                return this.canBcmApprove || this.canPcmApprove || this.canApproveChanges;
            case 'addSMTDealNumberAndMotivation':
                return this.saveMessage ? '' : null;
            case 'viewConcessions':
                return this.canPcmApprove || this.isEditing || this.isRecalling;
            case 'viewConcessionFeildDisable':
                return this.canEdit ? null : '';
            case 'addConcessionFeildDisable':
                return this.saveMessage ? '' : null;
            case 'deleteConcessionCol':
                return this.canBcmApprove || this.canPcmApprove || this.isEditing || this.isRecalling;
            case 'concessionItemRowsDelete':
                return this.transactionalConcessionForm.controls.concessionItemRows.value.length > 1 && !this.saveMessage;
            case 'hasNoConditions':
                return this.transactionalConcessionForm.controls.conditionItemsRows.value.length == 0;
            case 'hasConditions':
                return this.transactionalConcessionForm.controls.conditionItemsRows.value.length > 0;
            case 'canArchiveDelete':
                return this.transactionalConcessionForm.controls.concessionItemRows.value.length > 1;
            case 'noCommentsAdded':
                return !this.transactionalConcession.concession.concessionComments || this.transactionalConcession.concession.concessionComments.length == 0;
            case 'noRelatedConcessions':
                return !this.transactionalConcession.concession.concessionRelationshipDetails || this.transactionalConcession.concession.concessionRelationshipDetails.length == 0;
            case 'productType':
                return this.selectedConditionTypes[index] != null;
            case 'interestRateDisable':
                return this.selectedConditionTypes[index] != null && this.selectedConditionTypes[index].enableInterestRate ? null : '';
            case 'volumeDisable':
                return this.selectedConditionTypes[index] != null && this.selectedConditionTypes[index].enableConditionVolume ? null : '';
            case 'valueDisable':
                return this.selectedConditionTypes[index] != null && this.selectedConditionTypes[index].enableConditionValue ? null : '';
            case 'parentReferenceCheck':
                return concessionRelationship.parentConcessionReference == this.concessionReferenceId;
            case 'childReferenceCheck':
                return concessionRelationship.childConcessionReference == this.concessionReferenceId;
            case 'recall':
                return this.canRecall && !this.isRecalling;
            default:
                break;
        }
    }
}
