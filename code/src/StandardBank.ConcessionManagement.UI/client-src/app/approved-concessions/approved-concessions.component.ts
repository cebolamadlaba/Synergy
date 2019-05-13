import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http';

import { Observable } from "rxjs";
import { Router, RouterModule } from '@angular/router';

import { ApprovedConcession } from "../models/approved-concession";
import { ApprovedConcessionDetail } from "../models/approved-concession-detail";
import { LegalEntityConcessionLetterModel } from '../models/legal-entity-concession-letter';
import { ConcessionTypes } from '../constants/concession-types';

import { UserConcessionsService } from "../services/user-concessions.service";
import { ConcessionLetterService } from "../services/concession-letter.service";
import { LegalEntityAddressService } from "../services/legal-entity-address.service";

import saveAs from 'file-saver';

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
    //clientContactPerson: string;
    //clientName: string;
    //clientPostalAddress: string;
    //clientCity: string;
    //clientPostalCode: string;
    legalEntityConcessionLetterModel: LegalEntityConcessionLetterModel;

    public uploadProgress: number;

    observableApprovedConcessions: Observable<ApprovedConcession[]>;
    approvedConcessions: ApprovedConcession[];

    constructor( @Inject(UserConcessionsService) private userConcessionsService,
        private router: Router,
        private http: HttpClient,
        private concessionLetterService: ConcessionLetterService,
        private legalEntityAddressService: LegalEntityAddressService) { }

    ngOnInit() {
        this.initLegalEntityConcessionLetter();
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
            case ConcessionTypes.BOLDesc:
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

    initLegalEntityConcessionLetter() {
        this.legalEntityConcessionLetterModel = new LegalEntityConcessionLetterModel();
        this.legalEntityConcessionLetterModel.clientContactPerson = "";
        this.legalEntityConcessionLetterModel.clientName = "";
        this.legalEntityConcessionLetterModel.clientPostalAddress = "";
        this.legalEntityConcessionLetterModel.clientCity = "";
        this.legalEntityConcessionLetterModel.clientPostalCode = "";

        if (this.legalEntityId != null && this.legalEntityId > 0) {
            // Get LegalEntityAddress.
            this.legalEntityAddressService.getLegalEntityAddress(this.legalEntityId).subscribe(result => {
                this.legalEntityConcessionLetterModel.clientContactPerson = result.contactPerson;
                this.legalEntityConcessionLetterModel.clientName = result.customerName;
                this.legalEntityConcessionLetterModel.clientPostalAddress = result.postalAddress;
                this.legalEntityConcessionLetterModel.clientCity = result.city;
                this.legalEntityConcessionLetterModel.clientPostalCode = result.postalCode;
            }, error => { alert('Failed to retrieve Legal Entity Address'); });
        }

    }

    openCustomerDetailModal(legalEntityId: number) {
        this.legalEntityId = legalEntityId;

        this.initLegalEntityConcessionLetter();

        this.modal.show();
    }

    closeCustomerDetailModal() {
        this.modal.hide();
    }

    //printConcession(legalEntityId: number) {
    printConcession() {
        this.isLoading = true;
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

                        if (selectedConcessionDetails[j].concessionType == ConcessionTypes.BOLDesc) {
                            selectedConcessionDetails[j].concessionType = ConcessionTypes.BOL;
                        }

                        if (concessionIds.length == 0) {
                            concessionIds = String(selectedConcessionDetails[j].concessionId);
                        } else {
                            concessionIds = concessionIds + "," + String(selectedConcessionDetails[j].concessionId);
                        }
                    }
                }
            }
        }

        let observable;
        if (concessionIds != null && concessionIds.length > 0) {
            this.legalEntityConcessionLetterModel.legalEntityId = this.legalEntityId;
            observable = this.concessionLetterService.generateConcessionLetterForConcessionsByConcessionIds(concessionIds, this.legalEntityConcessionLetterModel);
        } else {
            this.legalEntityConcessionLetterModel.legalEntityId = this.legalEntityId;
            observable = this.concessionLetterService.generateConcessionLetterForConcessionsByLegalEntityId(this.legalEntityConcessionLetterModel);
        }

        observable.subscribe(result => {
            var byteCharacters = atob(result.bytes);
            var byteNumbers = new Array(byteCharacters.length);
            for (var i = 0; i < byteCharacters.length; i++) {
                byteNumbers[i] = byteCharacters.charCodeAt(i);
            }
            var byteArray = new Uint8Array(byteNumbers);

            var blob = new Blob([byteArray], { type: result.contentType });
            saveAs(blob, result.filename);
            this.isLoading = false;
        }, error => {
            this.errorMessage = "Could not generate Concession Letter for the selected item";
            this.isLoading = false;
        });

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
