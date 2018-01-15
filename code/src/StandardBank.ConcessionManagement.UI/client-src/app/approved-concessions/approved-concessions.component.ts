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
        var concessionDetailIds = "";

        //if there are selected concessions we need to get the concession detail id's and use those to
        //generate the concession letter, otherwise it means the user is choosing to generate the
        //concession letter for all the concessions for the legal entity so we use that instead
        if (selectedConcessions != null && selectedConcessions.length > 0) {

            for (var i = 0; i < selectedConcessions.length; i++) {
                var selectedConcessionDetails = selectedConcessions[i].approvedConcessionDetails.filter(item => item.isSelected);

                if (selectedConcessionDetails != null && selectedConcessionDetails.length > 0) {
                    for (var j = 0; j < selectedConcessionDetails.length; j++) {
                        if (concessionDetailIds.length == 0) {
                            concessionDetailIds = String(selectedConcessionDetails[j].concessionDetailId);
                        } else {
                            concessionDetailIds = concessionDetailIds + "," + String(selectedConcessionDetails[j].concessionDetailId);
                        }
                    }
                }
            }
        }

        if (concessionDetailIds != null && concessionDetailIds.length > 0) {
            window.open("/api/Concession/GenerateConcessionLetterForConcessionDetails/" + concessionDetailIds);
        } else {
            window.open("/api/Concession/GenerateConcessionLetterForLegalEntity/" + legalEntityId);
        }
    }
}
