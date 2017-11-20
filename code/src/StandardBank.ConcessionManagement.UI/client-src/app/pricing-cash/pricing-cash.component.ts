import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { CashConcession } from "../models/cash-concession";
import { CashConcessionService } from "../services/cash-concession.service";
import { CashView } from "../models/cash-view";
import { RiskGroup } from "../models/risk-group";
import { Concession } from "../models/concession";
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-pricing-cash',
  templateUrl: './pricing-cash.component.html',
  styleUrls: ['./pricing-cash.component.css']
})
export class PricingCashComponent implements OnInit, OnDestroy {
    riskGroupNumber: number;
    private sub: any;
    observableCashView: Observable<CashView>;
    cashView: CashView = new CashView();
    errorMessage: String;
    showHide = false;
    pageLoaded = false;
    isLoading = true;

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private location: Location,
        @Inject(CashConcessionService) private cashConcessionService) {
        this.cashView.riskGroup = new RiskGroup();
        this.cashView.cashConcessions = [new CashConcession()];
        this.cashView.cashConcessions[0].concession = new Concession();
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];

            if (this.riskGroupNumber) {
                this.observableCashView = this.cashConcessionService.getCashViewData(this.riskGroupNumber);
                this.observableCashView.subscribe(cashView => {
                    this.cashView = cashView;
                    this.pageLoaded = true;
                    this.isLoading = false;
                }, error => {
                    this.errorMessage = <any>error;
                    this.isLoading = false;
                });
            }
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
