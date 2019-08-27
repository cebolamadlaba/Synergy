import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { RiskGroup } from "../models/risk-group";

import { InvestmentConcession } from "../models/investment-concession";
import { InvestmentView } from "../models/investment-view";

import { InvestmentConcessionService } from "../services/investment-concession.service";

import { Concession } from "../models/concession";
import { UserService } from "../services/user.service";

@Component({
    selector: 'app-pricing-investments',
    templateUrl: './pricing-investments.component.html',
    styleUrls: ['./pricing-investments.component.css']
})
export class PricingInvestmentsComponent implements OnInit, OnDestroy {
    riskGroupNumber: number;
    sapbpid: number;
    private sub: any;

    entityName: string;
    entityNumber: string;

    observableInvestmentView: Observable<InvestmentView>;
    investmentView: InvestmentView = new InvestmentView();
    errorMessage: String;
    showHide = false;
    pageLoaded = false;
    isLoading = true;
    canRequest = false;

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private location: Location,
        @Inject(InvestmentConcessionService) private investmentConcessionService, private userService: UserService
    ) {
        this.investmentView.riskGroup = new RiskGroup();
        this.investmentView.investmentConcessions = [new InvestmentConcession()];
        this.investmentView.investmentConcessions[0].concession = new Concession();
    }


    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
            this.sapbpid = +params['sapbpid'];

            if (this.riskGroupNumber || this.sapbpid) {

                this.observableInvestmentView = this.investmentConcessionService.getInvestmentViewData(this.riskGroupNumber, this.sapbpid);
                this.observableInvestmentView.subscribe(investmentView => {
                    this.investmentView = investmentView;

                    if (this.riskGroupNumber || this.riskGroupNumber > 0) {
                        this.entityName = this.investmentView.riskGroup.name;
                        this.entityNumber = this.investmentView.riskGroup.number.toString();
                    }
                    else {
                        this.entityName = this.investmentView.legalEntity.customerName;
                        this.entityNumber = this.investmentView.legalEntity.customerNumber;
                    }

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
        this.router.navigate(['/pricing', { riskGroupNumber: this.riskGroupNumber, sapbpid: this.sapbpid }]);
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}
