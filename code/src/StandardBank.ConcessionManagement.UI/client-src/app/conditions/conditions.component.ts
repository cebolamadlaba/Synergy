import { Component, OnInit, Inject } from '@angular/core';
import { Subject } from 'rxjs/Rx';
import { Condition } from '../models/condition';
import { MyConditionService } from '../services/my-condition.service';
import { Observable } from "rxjs";
import { Period} from '../models/period';
import { LookupDataService } from "../services/lookup-data.service";

@Component({
  selector: 'app-conditions',
  templateUrl: './conditions.component.html',
  styleUrls: ['./conditions.component.css']
})
export class ConditionsComponent implements OnInit {
    dtOptions: DataTables.Settings = {};
    dtTrigger: Subject<Condition> = new Subject();
    observableConditions: Condition[];
    periods: Period[];
    errorMessage: String;
    periodType: string = "Standard";
    constructor(
        @Inject(MyConditionService) private conditionService,
        @Inject(LookupDataService) private lookupDataService) { }

  ngOnInit() {
      this.dtOptions = {
          pagingType: 'full_numbers',
          language: {
              emptyTable: "No records found!",
              search: "",
              searchPlaceholder: "Search"
          }
      };
    
      this.lookupDataService.getPeriods().subscribe(data => { this.periods = data; }, err => this.errorMessage = err);
      this.GetConditions("3 Months", this.periodType);
    }

  PeriodFilter(value:string) {
      this.GetConditions(value, this.periodType);
  }

  GetConditions(period:string, periodType:string) {
      this.conditionService.getMyConditions(period, periodType).subscribe(conditions => {
          this.observableConditions = conditions;
          this.dtTrigger.next();
      },
          error => this.errorMessage = <any>error);
  }

}
