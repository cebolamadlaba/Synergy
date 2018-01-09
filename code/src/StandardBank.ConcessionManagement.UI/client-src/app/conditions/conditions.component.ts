import { Component, OnInit, Inject } from '@angular/core';
import { Subject } from 'rxjs/Rx';
import { MyConditionService } from '../services/my-condition.service';
import { Observable } from "rxjs";
import { Period } from '../models/period';
import { LookupDataService } from "../services/lookup-data.service";
import { ConditionCounts } from "../models/condition-counts";
import { ConcessionCondition } from "../models/concession-condition";
import { Router, RouterModule } from '@angular/router';
import { ConcessionTypes } from '../constants/concession-types';

@Component({
    selector: 'app-conditions',
    templateUrl: './conditions.component.html',
    styleUrls: ['./conditions.component.css']
})
export class ConditionsComponent implements OnInit {
    observableConditions: ConcessionCondition[];
    periods: Period[];

	observableCondition: Observable<ConcessionCondition>;
	condition: ConcessionCondition;

    observableConditionCounts: Observable<ConditionCounts>;
    conditionCounts: ConditionCounts;

    errorMessage: String;
    validationError: String[];
    saveMessage: String;
    warningMessage: String;
    isLoading = true;

    periodType: string = "Standard";
	period: Period;

    standardClass: string = "activeWidget";
    ongoingClass: string = "";

    constructor(
        @Inject(MyConditionService) private conditionService,
        @Inject(LookupDataService) private lookupDataService,
        private router: Router) { }

    ngOnInit() {
		this.lookupDataService.getPeriods().subscribe(data => {
			this.periods = data;
			this.period = this.periods[0];

			this.loadAll();
		}, err => this.errorMessage = err);
    }

    loadAll() {
        this.observableConditionCounts = this.conditionService.getConditionCounts();
        this.observableConditionCounts.subscribe(conditionCounts => this.conditionCounts = conditionCounts, error => this.errorMessage = <any>error);

		if (this.period) {
			this.getConditions();
		}
    }

    periodFilter(value: string) {
		//this.period = value;
		//this.selectedPeriod = value;
        this.getConditions();
    }

    getConditions() {
        this.conditionService.getMyConditions(this.period.description, this.periodType).subscribe(conditions => {
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

	conditionNotMet(concessionCondition: ConcessionCondition) {
		if (confirm("Are you sure this condition has not been met?")) {
			this.isLoading = true;
			concessionCondition.conditionMet = false;

			this.observableCondition = this.conditionService.updateCondition(concessionCondition);
			this.observableCondition.subscribe(
				condition => {
					this.condition = condition;

					//open the concession
					this.openConcessionView(concessionCondition);
				},
				error => this.errorMessage = <any>error);
        }
    }

	conditionMet(concessionCondition: ConcessionCondition) {
		if (confirm("Are you sure this condition has been met?")) {
			this.isLoading = true;

            //update the condition in the database
			concessionCondition.conditionMet = true;

			this.observableCondition = this.conditionService.updateCondition(concessionCondition);
			this.observableCondition.subscribe(
				condition => {
					this.condition = condition;
					this.loadAll();
				},
				error => this.errorMessage = <any>error);
        }
    }

    openConcessionView(concessionCondition: ConcessionCondition) {
        switch (concessionCondition.concessionType) {
            case ConcessionTypes.Lending:
                this.router.navigate(['/lending-view-concession', concessionCondition.riskGroupNumber, concessionCondition.concessionReferenceNumber]);
                break;
            case ConcessionTypes.Cash:
                this.router.navigate(['/cash-view-concession', concessionCondition.riskGroupNumber, concessionCondition.concessionReferenceNumber]);
                break;
            case ConcessionTypes.Transactional:
                this.router.navigate(['/transactional-view-concession', concessionCondition.riskGroupNumber, concessionCondition.concessionReferenceNumber]);
                break;
        }
    }
}
