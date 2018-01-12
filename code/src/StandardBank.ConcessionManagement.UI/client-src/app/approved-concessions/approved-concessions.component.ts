import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from "rxjs";
import { UserConcessionsService } from "../services/user-concessions.service";
import { Router, RouterModule } from '@angular/router';
import { ApprovedConcession } from "../models/approved-concession";
import { ApprovedConcessionDetail } from "../models/approved-concession-detail";
import { ConcessionTypes } from '../constants/concession-types';

@Component({
    selector: 'app-approved-concessions',
    templateUrl: './approved-concessions.component.html',
    styleUrls: ['./approved-concessions.component.css']
})
export class ApprovedConcessionsComponent implements OnInit {
    errorMessage: String;
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
        }, error => {
            this.errorMessage = <any>error;
            this.isLoading = false;
        });
    }

    openConcessionView(approvedConcession: ApprovedConcession, approvedConcessionDetail: ApprovedConcessionDetail) {
        switch (approvedConcessionDetail.concessionType) {
            case ConcessionTypes.Lending:
                this.router.navigate(['/lending-view-concession', approvedConcession.riskGroupNumber, approvedConcessionDetail.referenceNumber]);
                break;
            case ConcessionTypes.Cash:
                this.router.navigate(['/cash-view-concession', approvedConcession.riskGroupNumber, approvedConcessionDetail.referenceNumber]);
                break;
            case ConcessionTypes.Transactional:
                this.router.navigate(['/transactional-view-concession', approvedConcession.riskGroupNumber, approvedConcessionDetail.referenceNumber]);
                break;
        }
    }

    printConcession(legalEntityId: number) {
        var selectedConcessions = this.approvedConcessions.filter(items => items.legalEntityId == legalEntityId);
        console.log(selectedConcessions);
        //window.open("/api/Concession/GenerateConcessionLetter/" + legalEntityId);
    }
}
