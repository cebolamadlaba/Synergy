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
import { UserService } from "../services/user.service";
import { BaseComponentService } from '../services/base-component.service';
import { Http } from '@angular/http';
import { LendingConcessionTieredRate } from '../models/lending-concession-tiered-rate';
import { ProductTypeEnum } from '../models/product-type-enum';

@Component({
    selector: 'app-pricing-lending',
    templateUrl: './pricing-lending.component.html',
    styleUrls: ['./pricing-lending.component.css']
})
export class PricingLendingComponent extends BaseComponentService implements OnInit, OnDestroy {
    riskGroupNumber: number;
    sapbpid: number;
    private sub: any;
    observableLendingView: Observable<LendingView>;
    lendingView: LendingView = new LendingView();
    errorMessage: String;
    showHide = false;
    pageLoaded = false;
    canRequest = false;
    isLoading = true;

    entityName: string;
    entityNumber: string;

    subHeading: string = "n/a";
    title: number = 0;

    selectedLineItemTieredRates: LendingConcessionTieredRate[] = [];

    constructor(
        public router: Router,
        private route: ActivatedRoute,
        private location: Location,
        @Inject(LendingService) private lendingService,
        public userService: UserService) {
        super(router, userService);
        this.lendingView.riskGroup = new RiskGroup();
        this.lendingView.lendingFinancial = new LendingFinancial();
        this.lendingView.lendingConcessions = [new LendingConcession()];
        this.lendingView.lendingConcessions[0].concession = new Concession();
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
            this.sapbpid = +params['sapbpid'];

            if (this.riskGroupNumber != null && this.riskGroupNumber != 0) {
                this.observableLendingView = this.lendingService.getLendingViewData(this.riskGroupNumber);
                this.observableLendingView.subscribe(lendingView => {
                    this.lendingView = lendingView;
                    this.entityName = this.lendingView.riskGroup.name;
                    this.entityNumber = this.lendingView.riskGroup.number.toString();
                    this.pageLoaded = true;
                    this.isLoading = false;
                }, error => {
                    this.errorMessage = <any>error;
                    this.isLoading = false;
                });
            } else if (this.sapbpid != null && this.sapbpid != 0) {
                this.observableLendingView = this.lendingService.getLendingViewDataBySAPBPID(this.sapbpid);
                this.observableLendingView.subscribe(lendingView => {
                    this.lendingView = lendingView;
                    this.entityName = this.lendingView.legalEntity.customerName;
                    this.entityNumber = this.lendingView.legalEntity.customerNumber;
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

    openManageTier(x, tier) {
        this.selectedLineItemTieredRates = tier.lendingConcessionDetailTieredRates;
    }

    showTieredRateButton(rowIndex: number, concession) {
        // Is the product Overdraft or Temporary Overdraft?
        if (concession.productType == ProductTypeEnum.Overdraft || concession.productType == ProductTypeEnum.TemporaryOverdraft) {
            return true;
        }
        else {
            return false;
        }
    }

    goBack() {     
        this.router.navigate(['/pricing', { riskGroupNumber: this.riskGroupNumber, sapbpid: this.sapbpid }]);
    }
    
    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}
