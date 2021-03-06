import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from "rxjs";
import { UserConcessionsService } from "../services/user-concessions.service";
import { UserConcessions } from "../models/user-concessions";
import { Subject } from 'rxjs/Rx'
import 'rxjs/add/operator/map';
import { Router, RouterModule } from '@angular/router';
import { InboxConcession } from "../models/inbox-concession";
import { ConcessionTypes } from '../constants/concession-types';

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
            },
            order: [[6, 'desc']]
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
            case ConcessionTypes.Lending:
                this.router.navigate(['/lending-view-concession', concession.riskGroupNumber, concession.customerNumber, concession.referenceNumber]);
                break;
            case ConcessionTypes.Cash:
                this.router.navigate(['/cash-view-concession', concession.riskGroupNumber, concession.customerNumber, concession.referenceNumber]);
                break;
            case ConcessionTypes.Transactional:
                this.router.navigate(['/transactional-view-concession', concession.riskGroupNumber, concession.customerNumber, concession.referenceNumber]);
                break;
            case ConcessionTypes.BOLDesc:
                this.router.navigate(['/bol-view-concession', concession.riskGroupNumber, concession.customerNumber, concession.referenceNumber]);
                break;
            case ConcessionTypes.Trade:
                this.router.navigate(['/trade-view-concession', concession.riskGroupNumber, concession.customerNumber, concession.referenceNumber]);
                break;
            case ConcessionTypes.Investment:
                this.router.navigate(['/investments-view-concession', concession.riskGroupNumber, concession.customerNumber, concession.referenceNumber]);
                break;
            case ConcessionTypes.Glms:
                this.router.navigate(['/glms-view-concession', concession.riskGroupNumber, concession.customerNumber, concession.referenceNumber]);
                break;
        }
    }

    getEntity(fieldName: string, concession: InboxConcession) {
        switch (fieldName) {
            case "entityName":
                {
                    if (concession.riskGroupName != null && concession.riskGroupName.trim() != "") {
                        return concession.riskGroupName;
                    }
                    else {
                        return concession.customerName;
                    }
                }
            case "entityNumber":
                {
                    if (concession.riskGroupNumber != null && concession.riskGroupNumber > 0) {
                        return concession.riskGroupNumber;
                    }
                    else {
                        return concession.customerNumber;
                    }
                }
            default:
                return "n/a";
        }
    }
}
