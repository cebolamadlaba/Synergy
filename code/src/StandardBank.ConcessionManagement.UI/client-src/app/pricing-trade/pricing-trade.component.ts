import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { RiskGroup } from "../models/risk-group";
import { LegalEntity } from "../models/legal-entity";

import { TradeConcession } from "../models/trade-concession";
import { TradeView } from "../models/trade-view";

import { TradeConcessionService } from "../services/trade-concession.service";

import { Concession } from "../models/concession";
import { UserService } from "../services/user.service";
import { BaseComponentService } from '../services/base-component.service';
import { Http } from '@angular/http';

@Component({
    selector: 'app-pricing-trade',
    templateUrl: './pricing-trade.component.html',
    styleUrls: ['./pricing-trade.component.css']
})
export class PricingTradeComponent extends BaseComponentService implements OnInit, OnDestroy {
    riskGroupNumber: number;
    sapbpid: number;
    private sub: any;

    observableTradeView: Observable<TradeView>;
    tradeView: TradeView = new TradeView();
    errorMessage: String;
    showHide = false;
    pageLoaded = false;
    isLoading = true;
    canRequest = false;
    entityName: string;
    entityNumber: string;

    constructor(
        public router: Router,
        private route: ActivatedRoute,
        private location: Location,
        @Inject(TradeConcessionService) private tradeConcessionService, public userService: UserService, public http: Http
    ) {
        super(router, userService, http);
        this.tradeView.riskGroup = new RiskGroup();
        this.tradeView.tradeConcessions = [new TradeConcession()];
        this.tradeView.tradeConcessions[0].concession = new Concession();
    }


    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
            this.sapbpid = +params['sapbpid'];

            if (this.riskGroupNumber || this.sapbpid) {

                this.observableTradeView = this.tradeConcessionService.getTradeViewData(this.riskGroupNumber, this.sapbpid);
                this.observableTradeView.subscribe(tradeView => {

                    this.tradeView = tradeView;

                    if (this.riskGroupNumber || this.riskGroupNumber > 0) {
                        this.entityName = this.tradeView.riskGroup.name;
                        this.entityNumber = this.tradeView.riskGroup.number.toString();
                    }
                    else {
                        this.entityName = this.tradeView.legalEntity.customerName;
                        this.entityNumber = this.tradeView.legalEntity.customerNumber;
                    }

                    this.pageLoaded = true;
                    this.isLoading = false;
                }, error => {
                    this.errorMessage = <any>error;
                    this.isLoading = false;
                });


            }

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
        this.router.navigate(['/pricing', { riskGroupNumber: this.riskGroupNumber, sapbpid: this.sapbpid }]);
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

}
