import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from "rxjs";
import { UserConcessionsService } from "../services/user-concessions.service";
import { UserConcessions } from "../models/user-concessions";
import { Subject } from 'rxjs/Rx'
import 'rxjs/add/operator/map';
import { InboxConcession } from "../models/inbox-concession";
import { Router, RouterModule } from '@angular/router';
import { ConcessionTypes } from '../constants/concession-types';

import { User } from "../models/user";
import { UserService } from "../services/user.service";

import { AccountExecutiveAssistant } from "../models/account-executive-assistant";

@Component({
    selector: 'app-pending-inbox',
    templateUrl: './pending-inbox.component.html',
    styleUrls: ['./pending-inbox.component.css']
})
export class PendingInboxComponent implements OnInit {
    dtOptions: DataTables.Settings = {};
    dtTrigger: Subject<UserConcessions> = new Subject();
    observableUserConcessions: Observable<UserConcessions>;
    userConcessions: UserConcessions;
    errorMessage: String;
    observableLoggedInUser: Observable<User>;
    user: User;

    isElevatedUser = false;

    constructor(
        @Inject(UserConcessionsService) private userConcessionsService,
        @Inject(UserService) private userService,
        private router: Router) {
    }

    ngOnInit() {

        this.dtOptions = {
            pagingType: 'full_numbers',
            language: {
                emptyTable: "No records found!",
                search: "",
                searchPlaceholder: "Search"
            },
            order: [[6, 'desc']]
        };

        this.observableUserConcessions = this.userConcessionsService.getData();
        this.observableUserConcessions.subscribe(
            userConcessions => {
                this.userConcessions = userConcessions;

                if (userConcessions && userConcessions.isElevatedUser) {

                    this.dtOptions = {
                        pagingType: 'full_numbers',
                        language: {
                            emptyTable: "No records found!",
                            search: "",
                            searchPlaceholder: "Search here"
                        },
                        order: [[8, 'desc']]
                    };

                }
                else {


                    this.dtOptions = {
                        pagingType: 'full_numbers',
                        language: {
                            emptyTable: "No records found!",
                            search: "",
                            searchPlaceholder: "Search"
                        },
                        order: [[6, 'desc']]
                    };
                }

                this.dtTrigger.next();
            },
            error => this.errorMessage = <any>error);
    }


    openConcessionView(concession: InboxConcession) {
        switch (concession.concessionType) {
            case ConcessionTypes.Lending:
                this.router.navigate(['/lending-view-concession', concession.riskGroupNumber, concession.customerNumber, concession.referenceNumber]);
                break;
            case ConcessionTypes.Cash:
                this.router.navigate(['/cash-view-concession', concession.riskGroupNumber, concession.referenceNumber]);
                break;
            case ConcessionTypes.Transactional:
                this.router.navigate(['/transactional-view-concession', concession.riskGroupNumber, concession.referenceNumber]);
                break;
            case ConcessionTypes.BOLDesc:
                this.router.navigate(['/bol-view-concession', concession.riskGroupNumber, concession.referenceNumber]);
                break;
            case ConcessionTypes.Trade:
                this.router.navigate(['/trade-view-concession', concession.riskGroupNumber, concession.referenceNumber]);
                break;
            case ConcessionTypes.Investment:
                this.router.navigate(['/investments-view-concession', concession.riskGroupNumber, concession.referenceNumber]);
                break;

        }
    }


}
