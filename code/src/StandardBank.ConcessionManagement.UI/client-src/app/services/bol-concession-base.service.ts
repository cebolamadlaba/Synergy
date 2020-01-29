import { ConditionType } from "../models/condition-type";

export class BolConcessionBaseService {

    validationError: String[];

    constructor() { }

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
            case 'product':
            case 'userid':
            case 'chargecode':
            case 'unitcharge':
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

        this.validationError.push(validationDetail);
    }
}

