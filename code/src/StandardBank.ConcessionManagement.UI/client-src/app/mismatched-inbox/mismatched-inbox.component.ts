import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from "rxjs";
import { UserConcessionsService } from "../user-concessions/user-concessions.service";
import { UserConcessions } from "../models/user-concessions";
import { Subject } from 'rxjs/Rx'
import 'rxjs/add/operator/map';
import { Concession } from "../models/concession";
import { Router, RouterModule } from '@angular/router';

@Component({
    selector: 'app-mismatched-inbox',
    templateUrl: './mismatched-inbox.component.html',
    styleUrls: ['./mismatched-inbox.component.css']
})
export class MismatchedInboxComponent implements OnInit {
    dtOptions: DataTables.Settings = {};
    dtTrigger: Subject<UserConcessions> = new Subject();
    observableUserConcessions: Observable<UserConcessions>;
    userConcessions: UserConcessions;
    errorMessage: String;

    constructor( @Inject(UserConcessionsService) private userConcessionsService, private router: Router) { }

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

    openConcessionView(concession: Concession) {
        switch (concession.concessionType) {
            case "Lending":
                this.router.navigate(['/lending-view-concession', concession.riskGroupNumber, concession.referenceNumber]);
                break;
        }
    }
}
