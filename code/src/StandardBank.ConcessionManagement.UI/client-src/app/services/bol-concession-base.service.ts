import { ConditionType } from "../models/condition-type";
import { ConcessionRelationshipDetail } from "../models/concession-relationship-detail";



export class BolConcessionBaseService {

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


}

