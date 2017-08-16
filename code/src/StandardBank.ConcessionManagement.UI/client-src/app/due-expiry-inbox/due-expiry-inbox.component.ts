import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { UserConcessionsService } from "../user-concessions/user-concessions.service";
import { UserConcessions } from "../models/user-concessions";
import { Subject } from 'rxjs/Rx'
import 'rxjs/add/operator/map';
import { Router, RouterModule } from '@angular/router';
import { Concession } from "../models/concession";

@Component({
    selector: 'app-due-expiry-inbox',
    templateUrl: './due-expiry-inbox.component.html',
    styleUrls: ['./due-expiry-inbox.component.css']
})
export class DueExpiryInboxComponent implements OnInit, OnDestroy {
    dtOptions: DataTables.Settings = {};
    dtTrigger: Subject<UserConcessions> = new Subject();
    observableUserConcessions: Observable<UserConcessions>;
    userConcessions: UserConcessions;
    errorMessage: String;

    constructor( @Inject(UserConcessionsService) private userConcessionsService,
        private router: Router) { }

    ngOnInit() {
        this.dtOptions = {
            pagingType: 'full_numbers',
            language: {
                emptyTable: "No records found!",
                search: "",
                searchPlaceholder: "Search"
            }
        };

        this.loadUserConcessions();
    }

    loadUserConcessions() {
        this.observableUserConcessions = this.userConcessionsService.getData();
        this.observableUserConcessions.subscribe(
            userConcessions => {
                this.userConcessions = userConcessions;
                this.dtTrigger.next();
            },
            error => this.errorMessage = <any>error);
    }

    openConcessionView(concession: Concession) {
        switch (concession.concessionType) {
            case "Lending":
                this.router.navigate(['/lending-inbox-view-concession', concession.riskGroupNumber, concession.referenceNumber]);
                break;
        }
    }

    renewConcession(concession: Concession) {
        switch (concession.concessionType) {
            case "Lending":
                alert("renew concession");
                break;
        }

        this.loadUserConcessions();
    }

    extendConcession(concession: Concession) {
        switch (concession.concessionType) {
            case "Lending":
                alert("extend concession");
                break;
        }

        this.loadUserConcessions();
    }

    ngOnDestroy() {
        this.dtTrigger.unsubscribe();
    }
}
