import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-pricing-investments',
  templateUrl: './pricing-investments.component.html',
  styleUrls: ['./pricing-investments.component.css']
})
export class PricingInvestmentsComponent implements OnInit {

    constructor(private location: Location) { }

  ngOnInit() {
  }

  goBack() {
      this.location.back();
  }

}
