import { Component, OnInit } from '@angular/core';
import { InboxConcessionCountService } from "../inbox-concession-count/inbox-concession-count.service";
import { Observable } from "rxjs";
import { ConcessionsSummary } from "../models/concessions-summary";

@Component({
  selector: 'app-inbox-header',
  templateUrl: './inbox-header.component.html',
  styleUrls: ['./inbox-header.component.css'],
  providers: [InboxConcessionCountService]
})
export class InboxHeaderComponent implements OnInit {
  observableConcessionsSummary: Observable<ConcessionsSummary>
  concessionsSummary: ConcessionsSummary;
  errorMessage: String;

  constructor(private _inboxConcessionCountService: InboxConcessionCountService) { }

  ngOnInit() {
    this.observableConcessionsSummary = this._inboxConcessionCountService.getData();
    this.observableConcessionsSummary.subscribe(concessionsSummary => this.concessionsSummary = concessionsSummary,
      error => this.errorMessage = <any>error);
  }

}
