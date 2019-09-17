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
import { UserService } from "../services/user.service";
import { BaseComponentService } from '../services/base-component.service';
import { Http } from '@angular/http';

@Component({
    selector: 'app-pricing-cash',
    templateUrl: './pricing-cash.component.html',
    styleUrls: ['./pricing-cash.component.css']
})
export class PricingCashComponent extends BaseComponentService implements OnInit, OnDestroy {
    riskGroupNumber: number;
    sapbpid: number;

    subHeading: string;
    title: string;

    private sub: any;
    observableCashView: Observable<CashView>;
    cashView: CashView = new CashView();
    errorMessage: String;
    showHide = false;
    pageLoaded = false;
    isLoading = true;
    canRequest = false;


    constructor(
        public router: Router,
        private route: ActivatedRoute,
        private location: Location,
        @Inject(CashConcessionService) private cashConcessionService, public userService: UserService, public http: Http) {
        super(router, userService, http);
        this.cashView.riskGroup = new RiskGroup();
        this.cashView.cashConcessions = [new CashConcession()];
        this.cashView.cashConcessions[0].concession = new Concession();


    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
            this.sapbpid = +params['sapbpid'];

            if (this.riskGroupNumber || this.sapbpid) {
                this.observableCashView = this.cashConcessionService.getCashViewData(this.riskGroupNumber, this.sapbpid);
                this.observableCashView.subscribe(cashView => {
                    this.cashView = cashView;

                    if (this.riskGroupNumber || this.riskGroupNumber > 0) {
                        this.subHeading = this.cashView.riskGroup.name;
                        this.title = this.cashView.riskGroup.number.toString();
                    }
                    else {
                        this.subHeading = this.cashView.legalEntity.customerName;
                        this.title = this.cashView.legalEntity.customerNumber;
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
        //this.location.back();
        this.router.navigate(['/pricing', { riskGroupNumber: this.riskGroupNumber, sapbpid: this.sapbpid }]);
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

}
