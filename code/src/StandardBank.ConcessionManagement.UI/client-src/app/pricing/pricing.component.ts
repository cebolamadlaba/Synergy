import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from "rxjs";
import { UserService } from "../user/user.service";
import { User } from "../models/user";
import { RiskGroupNameService } from "../risk-group-name/risk-group-name.service";

@Component({
    selector: 'app-pricing',
    templateUrl: './pricing.component.html',
    styleUrls: ['./pricing.component.css']
})
export class PricingComponent implements OnInit {
    observableLoggedInUser: Observable<User>;
    user: User;
    errorMessage: String;
    observableRiskGroupName: Observable<String>;
    riskGroupName: String;
    riskGroupNumber: number;
    foundRiskGroup = false;

    constructor( @Inject(UserService) private userService, @Inject(RiskGroupNameService) private riskGroupNameService ) { }

    ngOnInit() {
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
}
