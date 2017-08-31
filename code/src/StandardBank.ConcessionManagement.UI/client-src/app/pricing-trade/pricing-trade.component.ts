import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { RiskGroup } from "../models/risk-group";


@Component({
    selector: 'app-pricing-trade',
    templateUrl: './pricing-trade.component.html',
    styleUrls: ['./pricing-trade.component.css']
})
export class PricingTradeComponent implements OnInit, OnDestroy {
    riskGroupNumber: number;
    private sub: any;


    constructor(
        private router: Router,
        private route: ActivatedRoute) { }


    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
        });
    }

    goBack() {

        //this.location.back();
        this.router.navigate(['/pricing', this.riskGroupNumber]);
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

    


}
