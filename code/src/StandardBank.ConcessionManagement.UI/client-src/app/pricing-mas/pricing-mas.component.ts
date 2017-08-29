import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-pricing-mas',
  templateUrl: './pricing-mas.component.html',
  styleUrls: ['./pricing-mas.component.css']
})
export class PricingMasComponent implements OnInit {

  constructor(private location: Location) { }

  ngOnInit() {
  }

  goBack() {
      this.location.back();
  }

}
