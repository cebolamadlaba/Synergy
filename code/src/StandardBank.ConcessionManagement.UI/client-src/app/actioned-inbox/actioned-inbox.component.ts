import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from "rxjs";
import { UserConcessionsService } from "../services/user-concessions.service";
import { UserConcessions } from "../models/user-concessions";
import { Subject } from 'rxjs/Rx'
import 'rxjs/add/operator/map';
import { Router, RouterModule } from '@angular/router';
import { InboxConcession } from "../models/inbox-concession";

@Component({
  selector: 'app-actioned-inbox',
  templateUrl: './actioned-inbox.component.html',
  styleUrls: ['./actioned-inbox.component.css']
})
export class ActionedInboxComponent implements OnInit {
    dtOptions: DataTables.Settings = {};
    dtTrigger: Subject<UserConcessions> = new Subject();
    observableUserConcessions: Observable<UserConcessions>;
    userConcessions: UserConcessions;
    errorMessage: String;

    constructor(
        @Inject(UserConcessionsService) private userConcessionsService,
        private router: Router) {
    }

    ngOnInit() {
        this.dtOptions = {
            pagingType: 'full_numbers',
            language: {
                emptyTable: "No records found!",
                search: "",
                searchPlaceholder: "Search"
            }
        };

        this.observableUserConcessions = this.userConcessionsService.getData();
        this.observableUserConcessions.subscribe(
            userConcessions => {
                this.userConcessions = userConcessions;
                this.dtTrigger.next();
            },
            error => this.errorMessage = <any>error);
    }

    openConcessionView(concession: InboxConcession) {
        switch (concession.concessionType) {
            case "Lending":
                this.router.navigate(['/lending-view-concession', concession.riskGroupNumber, concession.referenceNumber]);
                break;
            case "Cash":
                this.router.navigate(['/cash-view-concession', concession.riskGroupNumber, concession.referenceNumber]);
                break;
            case "Transactional":
                this.router.navigate(['/transactional-view-concession', concession.riskGroupNumber, concession.referenceNumber]);
                break;
        }
    }
}