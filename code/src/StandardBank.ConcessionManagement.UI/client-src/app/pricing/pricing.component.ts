import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from "rxjs";

import { UserService } from "../user/user.service";
import { RiskGroupLegalEntitiesService } from "../risk-group-legal-entities/risk-group-legal-entities.service";

import { User } from "../models/user";
import { LegalEntity } from "../models/legal-entity";

@Component({
    selector: 'app-pricing',
    templateUrl: './pricing.component.html',
    styleUrls: ['./pricing.component.css']
})
export class PricingComponent implements OnInit {
    observableLoggedInUser: Observable<User>;
    user: User;

    observableLegalEntities: Observable<LegalEntity[]>;
    legalEntities: LegalEntity[];
    hasLegalEntities = false;
    errorMessage: String;

    constructor( @Inject(UserService) private userService, @Inject(RiskGroupLegalEntitiesService) private riskGroupLegalEntitiesService) { }

    ngOnInit() {
        this.observableLoggedInUser = this.userService.getData();
        this.observableLoggedInUser.subscribe(user => this.user = user,
            error => this.errorMessage = <any>error);
    }

    searchRiskGroupNumber(riskGroupNumber: number) {
        this.legalEntities = [];
        this.observableLegalEntities = this.riskGroupLegalEntitiesService.getData(riskGroupNumber);
        this.observableLegalEntities.subscribe(legalEntity => {
                this.legalEntities = legalEntity;
                this.hasLegalEntities = true;
            },
            error => this.errorMessage = <any>error);
    }
}
