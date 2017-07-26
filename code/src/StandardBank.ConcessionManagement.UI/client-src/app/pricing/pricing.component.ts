import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { UserService } from "../user/user.service";
import { User } from "../models/user";
import { RiskGroupNameService } from "../risk-group-name/risk-group-name.service";
import { ActivatedRoute } from '@angular/router';

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
    observableRiskGroupName: Observable<String>;
    riskGroupName: String;
    riskGroupNumber: number;
    foundRiskGroup = false;

    constructor(private route: ActivatedRoute,
        @Inject(UserService) private userService,
        @Inject(RiskGroupNameService) private riskGroupNameService) {
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
        this.observableRiskGroupName = this.riskGroupNameService.getData(riskGroupNumber);
        this.observableRiskGroupName.subscribe(riskGroupName => {
                this.riskGroupName = riskGroupName;
                this.foundRiskGroup = true;
            },
            error => this.errorMessage = <any>error);
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}
