import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DecimalPipe } from '@angular/common';

import { LendingConcessionTieredRate } from '../models/lending-concession-tiered-rate';

import { BaseComponentService } from '../services/base-component.service';

@Component({
    selector: 'app-lending-tier-rate-modal',
    templateUrl: './lending-tier-rate-modal.component.html',
    styleUrls: ['./lending-tier-rate-modal.component.css']
})
export class LendingTierRateModalComponent implements OnInit {

    @Input() selectedLineItemTieredRates: LendingConcessionTieredRate[];
    @Output() newItemEvent = new EventEmitter<LendingConcessionTieredRate[]>();


    constructor(private baseComponentService: BaseComponentService) {

    }

    ngOnInit() {
        if (this.selectedLineItemTieredRates == null)
            this.selectedLineItemTieredRates = [];
    }

    addNewTieredRateRow() {
        if (this.selectedLineItemTieredRates == null) {
            this.selectedLineItemTieredRates = [];
        }
        this.selectedLineItemTieredRates.push(new LendingConcessionTieredRate());
    }

    deleteTieredRateRow(rowIndex: number) {
        this.selectedLineItemTieredRates = this.selectedLineItemTieredRates.filter((item) => {
            return item != this.selectedLineItemTieredRates[rowIndex];
        });
    }

    saveTieredRates() {
        this.newItemEvent.emit(this.selectedLineItemTieredRates);
        this.selectedLineItemTieredRates = [];
    }

    setTwoNumberDecimal($event) {
        $event.target.value = this.baseComponentService.formatDecimal($event.target.value);
    }

    setTwoNumberDecimalMAP($event) {

        //check that it is a valid number
        if (((isNaN($event.target.value)).valueOf()) == true) {

            alert("Not a valid number for 'Prime -/+'");
            $event.target.value = 0;
        }
        else {

            $event.target.value = new DecimalPipe('en-US').transform($event.target.value, '1.3-3');
        }
    }
}
