import { Component, OnInit, Inject } from '@angular/core';
import { ApprovedConcession } from "../models/approved-concession";
import { Observable } from "rxjs";
import { UserConcessionsService } from "../user-concessions/user-concessions.service";
import { Router, RouterModule } from '@angular/router';

@Component({
    selector: 'app-approved-concessions',
    templateUrl: './approved-concessions.component.html',
    styleUrls: ['./approved-concessions.component.css']
})
export class ApprovedConcessionsComponent implements OnInit {
    errorMessage: String;
    observableApprovedConcessions: Observable<ApprovedConcession[]>;
    approvedConcessions: ApprovedConcession[];

    constructor( @Inject(UserConcessionsService) private userConcessionsService, private router: Router) { }

    ngOnInit() {
        this.observableApprovedConcessions = this.userConcessionsService.getApprovedConcessions();
        this.observableApprovedConcessions.subscribe(approvedConcession => this.approvedConcessions = approvedConcession, error => this.errorMessage = <any>error);
    }

    openConcessionView(approvedConcession: ApprovedConcession) {
        switch (approvedConcession.concessionType) {
            case "Lending":
                this.router.navigate(['/lending-view-concession', approvedConcession.riskGroupNumber, approvedConcession.concessionReferenceNumber]);
                break;
        }
    }

}