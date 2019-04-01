import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { UserService } from "../services/user.service";
import { User } from "../models/user";
import { ActivatedRoute } from '@angular/router';
import { RiskGroup } from "../models/risk-group";
import { LookupDataService } from "../services/lookup-data.service";

@Component({
    selector: 'app-pricing',
    templateUrl: './pricing.component.html',
    styleUrls: ['./pricing.component.css']
})
export class PricingComponent implements OnInit, OnDestroy {
    private sub: any;
    observableLoggedInUser: Observable<User>;
    user: User;
    errorMessage: String;
    observableRiskGroup: Observable<RiskGroup>;
    riskGroup: RiskGroup;
    riskGroupNumber: number;
    foundRiskGroup = false;
    activePricingProducts: number[] = [];
    isLoading = false;

    constructor(private route: ActivatedRoute,
        @Inject(UserService) private userService,
        @Inject(LookupDataService) private lookupDataService) {
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];

            if (this.riskGroupNumber)
                this.searchRiskGroupNumber(this.riskGroupNumber);
        });

        this.observableLoggedInUser = this.userService.getData();
        this.observableLoggedInUser.subscribe(user => this.user = user,
            error => this.errorMessage = <any>error);
    }

    searchRiskGroupNumber(riskGroupNumber: number) {
        this.errorMessage = null;
        this.isLoading = true;
        this.foundRiskGroup = false;
        this.riskGroupNumber = riskGroupNumber;
        this.observableRiskGroup = this.lookupDataService.getRiskGroup(riskGroupNumber);
        this.observableRiskGroup.subscribe(riskGroup => {
            this.riskGroup = riskGroup;

            this.lookupDataService.getActivePricingProducts().subscribe(activePricingProducts => {

                if (activePricingProducts === null) {
                    this.errorMessage = "No Pricing Products have been set active. Please contact the administrator.";
                }
                else {
                    this.activePricingProducts = activePricingProducts;
                }
            });

            this.foundRiskGroup = true;
            this.isLoading = false;
        },
            error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

    isVisiblePricingProduct(pricingProductNumber: number) {
        if (this.activePricingProducts === null) {
            this.errorMessage = "No Pricing Products have been set active. Please contact the administrator.";
        }
        else {
            let results = this.activePricingProducts.filter(item => {
                return item === pricingProductNumber;
            });

            if (results === null || results.length === 0) {
                return false;
            }
            else {
                return true;
            }
        }
    }
}
