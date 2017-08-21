import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-pricing-cash',
  templateUrl: './pricing-cash.component.html',
  styleUrls: ['./pricing-cash.component.css']
})
export class PricingCashComponent implements OnInit, OnDestroy {
    riskGroupNumber: number;
    private sub: any;

    constructor(private route: ActivatedRoute, private location: Location) {
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];

            if (this.riskGroupNumber) {
                //this.observableLendingView = this.lendingViewService.getData(this.riskGroupNumber);
                //this.observableLendingView.subscribe(lendingView => this.lendingView = lendingView,
                //    error => this.errorMessage = <any>error);
            }
        });
    }

    goBack() {
        this.location.back();
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

}
