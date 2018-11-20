import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http';

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

    @ViewChild('lgModal') modal;

    errorMessage: String;
    saveMessage: String;
    isLoading = true;

    legalEntityId: number;
    clientContactPerson: string;
    clientName: string;
    clientPostalAddress: string;
    clientCity: string;
    clientPostalCode: string;

    public uploadProgress: number;

    observableApprovedConcessions: Observable<ApprovedConcession[]>;
    approvedConcessions: ApprovedConcession[];

    constructor( @Inject(UserConcessionsService) private userConcessionsService, private router: Router, private http: HttpClient) { }

    ngOnInit() {
        this.observableApprovedConcessions = this.userConcessionsService.getApprovedConcessions();
        this.observableApprovedConcessions.subscribe(approvedConcession => {
            this.approvedConcessions = approvedConcession;
            this.isLoading = false;
        }, error => {
            this.errorMessage = <any>error;
            this.isLoading = false;
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
            case ConcessionTypes.BOL:
                this.router.navigate(['/bol-view-concession', approvedConcession.riskGroupNumber, approvedConcessionDetail.referenceNumber]);
                break;
            case ConcessionTypes.Trade:
                this.router.navigate(['/trade-view-concession', approvedConcession.riskGroupNumber, approvedConcessionDetail.referenceNumber]);
                break;
            case ConcessionTypes.Investment:
                this.router.navigate(['/investments-view-concession', approvedConcession.riskGroupNumber, approvedConcessionDetail.referenceNumber]);
                break;
        }
    }

    openCustomerDetailModal(legalEntityId: number) {
        this.legalEntityId = legalEntityId;

        this.clientContactPerson = "";
        this.clientName = "";
        this.clientPostalAddress = "";
        this.clientCity = "";
        this.clientPostalCode = "";

        this.modal.show();
    }

    closeCustomerDetailModal() {
        this.modal.hide();
    }

    //printConcession(legalEntityId: number) {
    printConcession() {

        let data = {
            "ClientContactPerson": this.clientContactPerson,
            "ClientName": this.clientName,
            "ClientPostalAddress": this.clientPostalAddress,
            "ClientCity": this.clientCity,
            "ClientPostalCode": this.clientPostalCode
        };

        var selectedConcessions = this.approvedConcessions.filter(items => items.legalEntityId == this.legalEntityId);
        var concessionIds = "";

        //if there are selected concessions we need to get the concession detail id's and use those to
        //generate the concession letter, otherwise it means the user is choosing to generate the
        //concession letter for all the concessions for the legal entity so we use that instead
        if (selectedConcessions != null && selectedConcessions.length > 0) {

            for (var i = 0; i < selectedConcessions.length; i++) {
                var selectedConcessionDetails = selectedConcessions[i].approvedConcessionDetails.filter(item => item.isSelected);

                if (selectedConcessionDetails != null && selectedConcessionDetails.length > 0) {
                    for (var j = 0; j < selectedConcessionDetails.length; j++) {
                        if (concessionIds.length == 0) {
                            concessionIds = String(selectedConcessionDetails[j].concessionId);
                        } else {
                            concessionIds = concessionIds + "," + String(selectedConcessionDetails[j].concessionId);
                        }
                    }
                }
            }
        }

        if (concessionIds != null && concessionIds.length > 0) {
            window.open("/api/Concession/GenerateConcessionLetterForConcessions/" + concessionIds);
        } else {
            window.open("/api/Concession/GenerateConcessionLetterForLegalEntity/" + this.legalEntityId);
        }

        this.modal.hide();
    }

    upload(event, concessionDetailId) {
        let reader = new FileReader();
        if (event.target.files && event.target.files.length > 0) {
            let file = event.target.files[0];

            const formData = new FormData();
            formData.append("ConcessionDetailedId", concessionDetailId);
            formData.append("file", file);


            const req = new HttpRequest('POST', 'api/Concession/UploadLetter', formData, {
                reportProgress: true,
            });

            this.http.request(req).subscribe(event => {
                if (event.type === HttpEventType.UploadProgress)
                    this.uploadProgress = Math.round(100 * event.loaded / event.total);
                else if (event instanceof HttpResponse)
                    console.log('Files uploaded!');

                this.observableApprovedConcessions = this.userConcessionsService.getApprovedConcessions();
                this.observableApprovedConcessions.subscribe(approvedConcession => {
                    this.approvedConcessions = approvedConcession;
                    this.isLoading = false;
                }, error => {
                    this.errorMessage = <any>error;
                    this.isLoading = false;
                    this.isLoading = false;
                });
            });
        }
    }
}
