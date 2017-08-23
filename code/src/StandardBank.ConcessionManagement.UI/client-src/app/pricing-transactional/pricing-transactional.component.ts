import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
    selector: 'app-pricing-transactional',
    templateUrl: './pricing-transactional.component.html',
    styleUrls: ['./pricing-transactional.component.css']
})
export class PricingTransactionalComponent implements OnInit {
    showHide: true;

    constructor(private location: Location) { }

    ngOnInit() {
    }

    goBack() {
        this.location.back();
    }

}
