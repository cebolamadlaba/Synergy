import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { UserService } from "../services/user.service";
import { User } from "../models/user";
import { ActivatedRoute } from '@angular/router';
import { RiskGroup } from "../models/risk-group";
import { LegalEntity } from "../models/legal-entity";
import { PricingView } from "../models/pricing-view";
import { LookupDataService } from "../services/lookup-data.service";

import { SubRoleEnum } from "../models/subrole-enum";

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
    observablePricingView: Observable<PricingView>;
    riskGroup: RiskGroup;
    legalEntity: LegalEntity;
    riskGroupNumber: number;
    sapbpid: number;
    foundRiskGroup = false;
    foundSAPBPID = false;
    activePricingProducts: number[] = [];
    isLoading = false;

    marketSegment: string = null;
    entityName: string = null;
    entityNumber: number = null;

    constructor(private route: ActivatedRoute,
        @Inject(UserService) private userService,
        @Inject(LookupDataService) private lookupDataService) {
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
            this.sapbpid = +params['sapbpid'];

            if (this.riskGroupNumber)
                this.searchRiskGroupNumber(this.riskGroupNumber);
            if (this.sapbpid)
                this.searchSAPBPID(this.sapbpid);
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
        this.sapbpid = 0;
        this.observableRiskGroup = this.lookupDataService.getRiskGroup(riskGroupNumber);
        this.observableRiskGroup.subscribe(riskGroup => {
            this.riskGroup = riskGroup;

            if (this.riskGroup != null) {
                this.marketSegment = this.riskGroup.marketSegment;
                this.entityName = this.riskGroup.name;
                this.entityNumber = this.riskGroup.number;
            }
            else {
                this.marketSegment = null;
                this.entityName = null;
                this.entityNumber = null;
            }

            this.getActivePricingProducts();

            this.foundRiskGroup = true;
            this.isLoading = false;
        },
            error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
    }

    searchSAPBPID(sapbpid: number) {
        this.errorMessage = null;
        this.isLoading = true;
        this.foundRiskGroup = false;
        this.foundSAPBPID = false;
        this.riskGroupNumber = 0;
        this.sapbpid = sapbpid;
        this.observablePricingView = this.lookupDataService.getRiskGroupBySAPBPID(sapbpid);
        this.observablePricingView.subscribe(pricingview => {

            this.riskGroup = pricingview.riskGroup;
            this.legalEntity = pricingview.legalEntity

            if (this.legalEntity != null) {
                this.marketSegment = this.legalEntity.marketSegment;
                this.entityName = this.legalEntity.customerName;
                this.entityNumber = Number(this.legalEntity.customerNumber);
            }
            else {
                this.marketSegment = null;
                this.entityName = null;
                this.entityNumber = null;
            }

            if (this.riskGroup != null) {
                this.riskGroupNumber = this.riskGroup.number;
                this.foundRiskGroup = true;
            }
            else if (this.legalEntity != null) {
                this.foundSAPBPID = true;
            }

            this.lookupDataService.getActivePricingProducts().subscribe(activePricingProducts => {

                if (activePricingProducts === null) {
                    this.errorMessage = "No Pricing Products have been set active. Please contact the administrator.";
                }
                else {
                    this.activePricingProducts = activePricingProducts;
                }
            });


            this.isLoading = false;
        },
            error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
    }

    private getActivePricingProducts() {
        this.lookupDataService.getActivePricingProducts().subscribe(activePricingProducts => {

            if (activePricingProducts === null) {
                this.errorMessage = "No Pricing Products have been set active. Please contact the administrator.";
            }
            else {
                this.activePricingProducts = activePricingProducts;
            }
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
                // for BOL COnsultants, Trade Bankers and PCM S&I
                if (this.user.subRoleId != null) {
                    // BOL Concessions
                    if (this.user.subRoleId == SubRoleEnum.BOLConsultant && pricingProductNumber == 4) {
                        return true;
                    }
                    // Trade Concessions
                    else if (this.user.subRoleId == SubRoleEnum.TradeBanker && pricingProductNumber == 5) {
                        return true;
                    }
                    // PCM S & I
                    else if (this.user.subRoleId == SubRoleEnum.PCMSnI && pricingProductNumber == 6) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }

                if (this.user.subRoleId == null && pricingProductNumber == 6) {
                    return false;
                }

                // for all other users
                return true;
            }
        }

        //let canView: boolean = true;
        //if (this.user.subRoleId != null && this.user.subRoleId > 0) {
        //    switch (this.user.subRoleId) {
        //        case SubRoleEnum.
        //    }
        //}

    }
}
