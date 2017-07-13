import { Component, OnInit, Inject } from '@angular/core';
import { InboxConcessionCountService } from "../inbox-concession-count/inbox-concession-count.service";
import { Observable } from "rxjs";
import { ConcessionsSummary } from "../models/concessions-summary";

@Component({
  selector: 'app-inbox-header',
  templateUrl: './inbox-header.component.html',
  styleUrls: ['./inbox-header.component.css']
})
export class InboxHeaderComponent implements OnInit {
  observableConcessionsSummary: Observable<ConcessionsSummary>;
  concessionsSummary: ConcessionsSummary;
  errorMessage: String;

  constructor(@Inject(InboxConcessionCountService) private inboxConcessionCountService) { }

  ngOnInit() {
    this.observableConcessionsSummary = this.inboxConcessionCountService.getData();
    this.observableConcessionsSummary.subscribe(concessionsSummary => this.concessionsSummary = concessionsSummary,
      error => this.errorMessage = <any>error);
  }

}
