import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
selector: 'app-pricing-bol',
templateUrl: './pricing-bol.component.html',
styleUrls: ['./pricing-bol.component.css']
})
export class PricingBolComponent implements OnInit {

    constructor(private location: Location) { }

ngOnInit() {
}

goBack() {
    this.location.back();
}

}
