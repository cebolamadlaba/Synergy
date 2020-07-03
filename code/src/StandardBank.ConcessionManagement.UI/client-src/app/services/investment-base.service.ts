import { Injectable } from '@angular/core';
import { ConditionType } from "../models/condition-type";

@Injectable()
export class InvestmentBaseService {
    validationError: String[];

    constructor() { }

    disableFieldBase(
        selectedConditionType: ConditionType,
        fieldName: string,
        canEdit: boolean,
        isSavedSubmitted: boolean,
        isEnabledExpiryDate: boolean,
        selectedInvestmentConcession: boolean
    ) {
        switch (fieldName) {
            case 'productType':
            case 'accountNumber':
            case 'balance':
            case 'loadedRate':
            case 'approvedRate':
                return (!isSavedSubmitted || canEdit) ? null : '';
            case 'noticeperiod':
                return !(!selectedInvestmentConcession && canEdit) || (selectedInvestmentConcession && !isSavedSubmitted) ? '' : null;
            case 'expiryDate':
                return (isEnabledExpiryDate && canEdit) || !isSavedSubmitted ? null : '';
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
