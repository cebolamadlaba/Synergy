import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { RiskGroup } from "../models/risk-group";
import { UserService } from "../services/user.service";

@Component({
    selector: 'app-pricing-trade',
    templateUrl: './pricing-trade.component.html',
    styleUrls: ['./pricing-trade.component.css']
})
export class PricingTradeComponent implements OnInit, OnDestroy {
    riskGroupNumber: number;
    private sub: any;
    canRequest = false;


    constructor(
        private router: Router,
        private route: ActivatedRoute, private userService : UserService) { }


    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
        });
        this.userService.getData().subscribe(user => {
            this.canRequest = user.canRequest;

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
