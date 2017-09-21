import { Component, OnInit, Inject } from '@angular/core';
import { ApprovedConcession } from "../models/approved-concession";
import { Observable } from "rxjs";
import { UserConcessionsService } from "../services/user-concessions.service";
import { Router, RouterModule } from '@angular/router';

@Component({
    selector: 'app-approved-concessions',
    templateUrl: './approved-concessions.component.html',
    styleUrls: ['./approved-concessions.component.css']
})
export class ApprovedConcessionsComponent implements OnInit {
    errorMessage: String;
    validationError: String[];
    saveMessage: String;
    isLoading = true;

    observableApprovedConcessions: Observable<ApprovedConcession[]>;
    approvedConcessions: ApprovedConcession[];

    constructor( @Inject(UserConcessionsService) private userConcessionsService, private router: Router) { }

    ngOnInit() {
        this.observableApprovedConcessions = this.userConcessionsService.getApprovedConcessions();
        this.observableApprovedConcessions.subscribe(approvedConcession => {
            this.approvedConcessions = approvedConcession;
            this.isLoading = false;
        }, error => this.errorMessage = <any>error);
    }

    openConcessionView(approvedConcession: ApprovedConcession) {
        switch (approvedConcession.concessionType) {
            case "Lending":
                this.router.navigate(['/lending-view-concession', approvedConcession.riskGroupNumber, approvedConcession.concessionReferenceNumber]);
                break;
            case "Cash":
                this.router.navigate(['/cash-view-concession', approvedConcession.riskGroupNumber, approvedConcession.concessionReferenceNumber]);
                break;
            case "Transactional":
                this.router.navigate(['/transactional-view-concession', approvedConcession.riskGroupNumber, approvedConcession.concessionReferenceNumber]);
                break;
        }
    }

    printConcession(concessionReferenceNumber: string) {
        window.open("/api/Concession/GenerateConcessionLetter/" + concessionReferenceNumber);
    }
}
