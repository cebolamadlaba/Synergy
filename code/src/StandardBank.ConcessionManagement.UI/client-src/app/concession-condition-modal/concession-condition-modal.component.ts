import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';

import { ConditionType } from "../models/condition-type";
import { PeriodType } from '../models/period-type';
import { Period } from '../models/period';

@Component({
    selector: 'app-concession-condition-modal',
    templateUrl: './concession-condition-modal.component.html',
    styleUrls: ['./concession-condition-modal.component.css']
})
export class ConcessionConditionModalComponent implements OnInit {

    @Input() selectedConditionTypes: ConditionType[];
    @Input() concessionForm: FormGroup;
    @Input() conditionTypes: ConditionType[];
    @Input() periodTypes: PeriodType[];
    @Input() periods: Period[];

    @Output() parentValidatePeriod = new EventEmitter<any>();

    showComment: boolean = false;

    constructor(
        private formBuilder: FormBuilder
    ) { }

    ngOnInit() {
    }

    addNewConditionRow() {
        const control = <FormArray>this.concessionForm.controls['conditionItemsRows'];
        control.push(this.initConditionItemRows());
    }

    initConditionItemRows() {
        this.selectedConditionTypes.push(new ConditionType());

        return this.formBuilder.group({
            conditionType: [''],
            conditionProduct: [''],
            interestRate: [''],
            volume: [''],
            value: [''],
            conditionComment: [''],
            periodType: [''],
            period: ['']
        });
    }

    conditionTypeChanged(rowIndex) {
        const control = <FormArray>this.concessionForm.controls['conditionItemsRows'];
        this.selectedConditionTypes[rowIndex] = control.controls[rowIndex].get('conditionType').value;

        let currentCondition = control.controls[rowIndex];

        currentCondition.get('conditionProduct').setValue(null);
        currentCondition.get('interestRate').setValue(null);
        currentCondition.get('volume').setValue(null);
        currentCondition.get('value').setValue(null);
    }

    validatePeriod(itemrow) {
        this.parentValidatePeriod.emit(itemrow);
    }

    deleteConditionRow(index: number) {
        const control = <FormArray>this.concessionForm.controls['conditionItemsRows'];
        control.removeAt(index);

        this.selectedConditionTypes.splice(index, 1);

    }

    disableField(fieldName: string, rowIndex: number) {
        if (this.selectedConditionTypes[rowIndex] == null)
            return '';

        this.setShowComment(rowIndex);

        switch (fieldName) {
            case "interestRate":
                return this.selectedConditionTypes[rowIndex].enableInterestRate ? null : '';
            case "volume":
                return this.selectedConditionTypes[rowIndex].enableConditionVolume ? null : '';
            case "value":
                return this.selectedConditionTypes[rowIndex].enableConditionValue ? null : '';
            case "conditionComment":
                return this.showComment ? null : '';
            default:
                return null;
        }
    }

    setShowComment(rowIndex) {
        if (!this.selectedConditionTypes[rowIndex].enableInterestRate &&
            !this.selectedConditionTypes[rowIndex].enableConditionVolume &&
            !this.selectedConditionTypes[rowIndex].enableConditionValue)
            this.showComment = true;
        else
            this.showComment = false;
    }
}
