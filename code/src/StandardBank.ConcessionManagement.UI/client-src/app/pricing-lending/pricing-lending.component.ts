import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { LendingView } from "../models/lending-view";
import { RiskGroup } from "../models/risk-group";
import { LendingConcession } from "../models/lending-concession";
import { Concession } from "../models/concession";
import { Location } from '@angular/common';
import { LendingService } from "../services/lending.service";
import { Router, RouterModule } from '@angular/router';
import { LendingFinancial } from "../models/lending-financial";
import { UserService} from "../services/user.service";
@Component({
    selector: 'app-pricing-lending',
    templateUrl: './pricing-lending.component.html',
    styleUrls: ['./pricing-lending.component.css']
})
export class PricingLendingComponent implements OnInit, OnDestroy {
    riskGroupNumber: number;
    private sub: any;
    observableLendingView: Observable<LendingView>;
    lendingView: LendingView = new LendingView();
    errorMessage: String;
    showHide = true;
    pageLoaded = false;
    canRequest = false;
    isLoading = true;

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private location: Location,
        @Inject(LendingService) private lendingService,
        private userService: UserService) {
        this.lendingView.riskGroup = new RiskGroup();
        this.lendingView.lendingFinancial = new LendingFinancial();
        this.lendingView.lendingConcessions = [new LendingConcession()];
        this.lendingView.lendingConcessions[0].concession = new Concession();
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];

            if (this.riskGroupNumber) {
                this.observableLendingView = this.lendingService.getLendingViewData(this.riskGroupNumber);
                this.observableLendingView.subscribe(lendingView => {                 
                    this.lendingView = lendingView;
                    this.pageLoaded = true;
                    this.isLoading = false;
                }, error => {
                    this.errorMessage = <any>error;
                    this.isLoading = false;
                });
            }
            this.userService.getData().subscribe(user => {
                this.canRequest = user.canRequest;
            });
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
