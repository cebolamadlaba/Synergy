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
        this.foundRiskGroup = false;
        this.riskGroupNumber = riskGroupNumber;
        this.observableRiskGroup = this.lookupDataService.getRiskGroup(riskGroupNumber);
        this.observableRiskGroup.subscribe(riskGroup => {
                this.riskGroup = riskGroup;
                this.foundRiskGroup = true;
            },
            error => this.errorMessage = <any>error);
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}
