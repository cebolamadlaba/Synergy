import { Injectable } from '@angular/core';
import { ConditionType } from "../models/condition-type";
import { LendingConcessionDetail } from "../models/lending-concession-detail";

@Injectable()
export class LendingBaseService {
    validationError: String[];

    constructor() { }

    disableFieldBase(
        selectedConditionType: ConditionType,
        lendingConcessionDetail: LendingConcessionDetail,
        fieldname: string,
        canEdit: boolean,
        canSaveMessage: boolean
    ) {
        switch (fieldname) {
            case 'productType':
            case 'accountNumber':
            case 'limit':
            case 'term':
            case 'marginAgainstPrime':
            case 'initiationFee':
            case 'mrsEri':
                return (!canSaveMessage || canEdit) ? null : '';
            case 'reviewFeeType':
                return (lendingConcessionDetail.show_reviewFeeType && canEdit) ? null : '';
            case 'reviewFee':
                return (lendingConcessionDetail.show_reviewFee && canEdit) ? null : '';
            case 'uffFee':
                return (lendingConcessionDetail.show_uffFee && canEdit) ? null : '';
            case 'frequency':
                return (lendingConcessionDetail.show_frequency && canEdit) ? null : '';
            case 'serviceFee':
                return (lendingConcessionDetail.show_serviceFee && canEdit) ? null : '';
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

        this.validationError.push(validationDetail);
    }
}
