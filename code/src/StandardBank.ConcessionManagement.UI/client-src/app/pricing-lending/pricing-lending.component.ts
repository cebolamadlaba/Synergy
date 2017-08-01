import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { LendingView } from "../models/lending-view";
import { LendingViewService } from "../lending-view/lending-view.service";
import { RiskGroup } from "../models/risk-group";
import { SourceSystemProduct } from "../models/source-system-product";
import { LendingConcession } from "../models/lending-concession";
import { Concession } from "../models/concession";

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
    showHide: true;

    constructor(private route: ActivatedRoute, @Inject(LendingViewService) private lendingViewService) {
        this.lendingView.riskGroup = new RiskGroup();
        this.lendingView
        this.lendingView.sourceSystemProducts = [new SourceSystemProduct()];
        this.lendingView.lendingConcessions = [new LendingConcession()];
        this.lendingView.lendingConcessions[0].concession = new Concession();
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];

            if (this.riskGroupNumber) {
                this.observableLendingView = this.lendingViewService.getData(this.riskGroupNumber);
                this.observableLendingView.subscribe(lendingView => this.lendingView = lendingView,
                    error => this.errorMessage = <any>error);
            }
        });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}
