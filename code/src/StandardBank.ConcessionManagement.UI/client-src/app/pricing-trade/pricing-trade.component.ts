import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { RiskGroup } from "../models/risk-group";

import { TradeConcession } from "../models/trade-concession";
import { TradeView } from "../models/trade-view";

import { TradeConcessionService } from "../services/trade-concession.service";

import { Concession } from "../models/concession";
import { UserService } from "../services/user.service";

@Component({
    selector: 'app-pricing-trade',
    templateUrl: './pricing-trade.component.html',
    styleUrls: ['./pricing-trade.component.css']
})
export class PricingTradeComponent implements OnInit, OnDestroy {
    riskGroupNumber: number;
    private sub: any;

    observableTradeView: Observable<TradeView>;
    tradeView: TradeView = new TradeView();
    errorMessage: String;
    showHide = false;
    pageLoaded = false;
    isLoading = true;
    canRequest = false;

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private location: Location,
         @Inject(TradeConcessionService) private tradeConcessionService, private userService: UserService
    ) {
        this.tradeView.riskGroup = new RiskGroup();
        this.tradeView.tradeConcessions = [new TradeConcession()];
        this.tradeView.tradeConcessions[0].concession = new Concession();
    }


    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];

            if (this.riskGroupNumber) {
                this.observableTradeView = this.tradeConcessionService.getTradeViewData(this.riskGroupNumber);
                this.observableTradeView.subscribe(tradeView => {


                    this.tradeView = tradeView;
                    this.pageLoaded = true;
                    this.isLoading = false;
                }, error => {
                    this.errorMessage = <any>error;
                    this.isLoading = false;
                });
            }
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
