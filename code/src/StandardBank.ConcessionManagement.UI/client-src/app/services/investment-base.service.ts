import { Injectable } from '@angular/core';
import { ConditionType } from "../models/condition-type";

@Injectable()
export class InvestmentBaseService {

    constructor() { }

    disableFieldBase(
        selectedConditionType: ConditionType,
        fieldName: string,
        canEdit: boolean,
        canSaveMessage: boolean,
        isEnabledExpiryDate: boolean,
        selectedInvestmentConcession: boolean
    ) {
        switch (fieldName) {
            case 'productType':
            case 'accountNumber':
            case 'balance':
            case 'loadedRate':
            case 'approvedRate':
                return (!canSaveMessage || canEdit) ? null : '';
            case 'noticeperiod':
                return (selectedInvestmentConcession && canEdit) || !(selectedInvestmentConcession || canSaveMessage) ? null : '';
            case 'expiryDate':
                return (isEnabledExpiryDate && canEdit) || !canSaveMessage ? null : '';
            case 'interestRate':
                return selectedConditionType != null && selectedConditionType.enableInterestRate ? null : '';
            case 'volume':
                return selectedConditionType != null && selectedConditionType.enableConditionVolume ? null : '';
            case 'value':
                return selectedConditionType != null && selectedConditionType.enableConditionValue ? null : '';
        }
    }
}
