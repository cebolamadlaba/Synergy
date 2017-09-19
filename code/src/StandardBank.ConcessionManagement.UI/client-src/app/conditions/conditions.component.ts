import { Component, OnInit, Inject } from '@angular/core';
import { Subject } from 'rxjs/Rx';
import { Condition } from '../models/condition';
import { MyConditionService } from '../services/my-condition.service';
import { Observable } from "rxjs";
import { Period } from '../models/period';
import { LookupDataService } from "../services/lookup-data.service";
import { ConditionCounts } from "../models/condition-counts";

@Component({
    selector: 'app-conditions',
    templateUrl: './conditions.component.html',
    styleUrls: ['./conditions.component.css']
})
export class ConditionsComponent implements OnInit {
    observableConditions: Condition[];
    periods: Period[];

    observableConditionCounts: Observable<ConditionCounts>;
    conditionCounts: ConditionCounts;

    errorMessage: String;
    validationError: String[];
    saveMessage: String;
    warningMessage: String;
    isLoading = false;

    periodType: string = "Standard";
    period: string = "3 Months"

    standardClass: string = "activeWidget";
    ongoingClass: string = "";

    constructor(
        @Inject(MyConditionService) private conditionService,
        @Inject(LookupDataService) private lookupDataService) { }

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

    }

    conditionMet() {

    }
}
