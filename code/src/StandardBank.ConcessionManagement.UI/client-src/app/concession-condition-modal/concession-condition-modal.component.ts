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
        //this.validationError = null;

        //let selectedPeriodType = itemrow.controls.periodType.value.description;

        //let selectedPeriod = itemrow.controls.period.value.description;

        //if (selectedPeriodType == 'Once-off' && selectedPeriod == 'Monthly') {
        //    this.addValidationError("Conditions: The Period 'Monthly' cannot be selected for Period Type 'Once-off'");
        //}
    }

    deleteConditionRow(index: number) {
        const control = <FormArray>this.concessionForm.controls['conditionItemsRows'];
        control.removeAt(index);

        this.selectedConditionTypes.splice(index, 1);

    }

    disableField(fieldName: string, rowIndex: number) {
        switch (fieldName) {
            case "interestRate":
                return this.selectedConditionTypes[rowIndex] != null && this.selectedConditionTypes[rowIndex].enableInterestRate ? null : '';
            case "volume":
                return this.selectedConditionTypes[rowIndex] != null && this.selectedConditionTypes[rowIndex].enableConditionVolume ? null : '';
            case "value":
                return this.selectedConditionTypes[rowIndex] != null && this.selectedConditionTypes[rowIndex].enableConditionValue ? null : '';
            default:
                return null;
        }
    }
}
