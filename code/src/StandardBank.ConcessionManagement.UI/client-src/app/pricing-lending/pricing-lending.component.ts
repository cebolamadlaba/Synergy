import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { LendingView } from "../models/lending-view";
import { RiskGroup } from "../models/risk-group";
import { SourceSystemProduct } from "../models/source-system-product";
import { LendingConcession } from "../models/lending-concession";
import { Concession } from "../models/concession";
import { Location } from '@angular/common';
import { LendingService } from "../services/lending.service";
import { Router, RouterModule } from '@angular/router';

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

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private location: Location,
        @Inject(LendingService) private lendingService) {
        this.lendingView.riskGroup = new RiskGroup();
        this.lendingView.sourceSystemProducts = [new SourceSystemProduct()];
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
                }, error => this.errorMessage = <any>error);
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
