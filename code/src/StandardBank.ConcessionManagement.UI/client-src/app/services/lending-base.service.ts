import { Injectable } from '@angular/core';
import { ConditionType } from "../models/condition-type";

@Injectable()
export class LendingBaseService {
    validationError: String[];

    constructor() { }

    disableFieldBase(
        selectedConditionTypes: ConditionType[],
        index: number,
        fieldname: string
    ) {
        switch (fieldname) {
            case 'interestRate':
                return selectedConditionTypes[index] != null && selectedConditionTypes[index].enableInterestRate ? null : '';
            case 'volume':
                return selectedConditionTypes[index] != null && selectedConditionTypes[index].enableConditionVolume ? null : '';
            case 'value':
                return selectedConditionTypes[index] != null && selectedConditionTypes[index].enableConditionValue ? null : '';
        }
    }

    addValidationError(validationDetail) {
        if (!this.validationError)
            this.validationError = [];

        this.validationError.push(validationDetail);
    }
}
