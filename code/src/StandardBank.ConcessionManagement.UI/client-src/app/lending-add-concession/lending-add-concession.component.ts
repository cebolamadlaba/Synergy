import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { RiskGroupService } from "../risk-group/risk-group.service";
import { RiskGroup } from "../models/risk-group";

@Component({
    selector: 'app-lending-add-concession',
    templateUrl: './lending-add-concession.component.html',
    styleUrls: ['./lending-add-concession.component.css']
})
export class LendingAddConcessionComponent implements OnInit, OnDestroy {
    private sub: any;
    errorMessage: String;
    observableRiskGroup: Observable<RiskGroup>;
    riskGroup: RiskGroup;
    riskGroupNumber: number;

    constructor(private route: ActivatedRoute, @Inject(RiskGroupService) private riskGroupService) {
        this.riskGroup = new RiskGroup();
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];

            if (this.riskGroupNumber) {
                this.observableRiskGroup = this.riskGroupService.getData(this.riskGroupNumber);
                this.observableRiskGroup.subscribe(riskGroup => this.riskGroup = riskGroup, error => this.errorMessage = <any>error);
            }
        });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}
