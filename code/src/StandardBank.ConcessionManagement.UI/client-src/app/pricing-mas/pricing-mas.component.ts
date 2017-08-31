import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { RiskGroup } from "../models/risk-group";

@Component({
    selector: 'app-pricing-mas',
    templateUrl: './pricing-mas.component.html',
    styleUrls: ['./pricing-mas.component.css']
})
export class PricingMasComponent implements OnInit, OnDestroy {
    riskGroupNumber: number;
    private sub: any;

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private location: Location) { }

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
