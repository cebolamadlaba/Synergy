import { Injectable } from '@angular/core';
import { ConditionType } from "../models/condition-type";

@Injectable()
export class InvestmentBaseService{
    constructor() { }

    disableFieldBase(selectedConditionTypes: ConditionType[], index: number, fieldname: string) {
        switch (fieldname) {
            case 'interestRate':
                return selectedConditionTypes[index] != null && selectedConditionTypes[index].enableInterestRate ? null : '';
            case 'volume':
                return selectedConditionTypes[index] != null && selectedConditionTypes[index].enableConditionVolume ? null : '';
            case 'value':
                return selectedConditionTypes[index] != null && selectedConditionTypes[index].enableConditionValue ? null : '';
        }
    }
}
