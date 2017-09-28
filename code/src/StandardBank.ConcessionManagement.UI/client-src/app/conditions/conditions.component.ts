import { Component, OnInit, Inject } from '@angular/core';
import { Subject } from 'rxjs/Rx';
import { MyConditionService } from '../services/my-condition.service';
import { Observable } from "rxjs";
import { Period } from '../models/period';
import { LookupDataService } from "../services/lookup-data.service";
import { ConditionCounts } from "../models/condition-counts";
import { ConcessionCondition } from "../models/concession-condition";
import { Router, RouterModule } from '@angular/router';

@Component({
    selector: 'app-conditions',
    templateUrl: './conditions.component.html',
    styleUrls: ['./conditions.component.css']
})
export class ConditionsComponent implements OnInit {
    observableConditions: ConcessionCondition[];
    periods: Period[];

    observableConditionCounts: Observable<ConditionCounts>;
    conditionCounts: ConditionCounts;

    errorMessage: String;
    validationError: String[];
    saveMessage: String;
    warningMessage: String;
    isLoading = true;

    periodType: string = "Standard";
    period: string = "3 Months"

    standardClass: string = "activeWidget";
    ongoingClass: string = "";

    constructor(
        @Inject(MyConditionService) private conditionService,
        @Inject(LookupDataService) private lookupDataService,
        private router: Router) { }

    ngOnInit() {
        this.lookupDataService.getPeriods().subscribe(data => { this.periods = data; }, err => this.errorMessage = err);

        this.loadAll();
    }

    loadAll() {
        this.observableConditionCounts = this.conditionService.getConditionCounts();
        this.observableConditionCounts.subscribe(conditionCounts => this.conditionCounts = conditionCounts, error => this.errorMessage = <any>error);

        this.getConditions();
    }

    periodFilter(value: string) {
        this.period = value;
        this.getConditions();
    }

    getConditions() {
        this.conditionService.getMyConditions(this.period, this.periodType).subscribe(conditions => {
            this.observableConditions = conditions;
            this.isLoading = false;
        }, error => this.errorMessage = <any>error);
    }

    showStandard() {
        this.standardClass = "activeWidget";
        this.ongoingClass = "";
        this.periodType = "Standard";

        this.getConditions();
    }

    showOngoing() {
        this.standardClass = "";
        this.ongoingClass = "activeWidget";
        this.periodType = "Ongoing";

        this.getConditions();
    }

    conditionNotMet() {
        if (confirm("Are you sure this condition has not been met?")) {

        }
    }

    conditionMet() {
        if (confirm("Are you sure this condition has been met?")) {

        }
    }

    openConcessionView(concessionCondition: ConcessionCondition) {
        switch (concessionCondition.concessionType) {
            case "Lending":
                this.router.navigate(['/lending-view-concession', concessionCondition.riskGroupNumber, concessionCondition.concessionReferenceNumber]);
                break;
            case "Cash":
                this.router.navigate(['/cash-view-concession', concessionCondition.riskGroupNumber, concessionCondition.concessionReferenceNumber]);
                break;
            case "Transactional":
                this.router.navigate(['/transactional-view-concession', concessionCondition.riskGroupNumber, concessionCondition.concessionReferenceNumber]);
                break;
        }
    }
}
