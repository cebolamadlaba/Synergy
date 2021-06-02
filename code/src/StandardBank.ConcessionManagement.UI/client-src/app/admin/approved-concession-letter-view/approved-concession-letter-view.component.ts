import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http';

import { Observable } from "rxjs";
import { Router, RouterModule } from '@angular/router';

import { ApprovedConcession } from "../../models/approved-concession";
import { ApprovedConcessionDetail } from "../../models/approved-concession-detail";
import { LegalEntityConcessionLetterModel } from '../../models/legal-entity-concession-letter';
import { ConcessionTypes } from '../../constants/concession-types';

import { UserConcessionsService } from "../../services/user-concessions.service";
import { ConcessionLetterService } from "../../services/concession-letter.service";
import { LegalEntityAddressService } from "../../services/legal-entity-address.service";

import saveAs from 'file-saver';

@Component({
  selector: 'app-approved-concession-letter-view',
  templateUrl: './approved-concession-letter-view.component.html',
  styleUrls: ['./approved-concession-letter-view.component.css']
})
export class ApprovedConcessionLetterViewComponent implements OnInit {

    errorMessage: String;
    isLoading = true;

    legalEntityId: number;

    legalEntityConcessionLetterModel: LegalEntityConcessionLetterModel;

    observableApprovedConcessions: Observable<ApprovedConcession[]>;
    approvedConcessions: ApprovedConcession[];

    constructor(@Inject(UserConcessionsService) private userConcessionsService,
        private router: Router,
        private http: HttpClient,
        private concessionLetterService: ConcessionLetterService,
        private legalEntityAddressService: LegalEntityAddressService) { }

    ngOnInit() {
        this.initLegalEntityConcessionLetter();
        this.observableApprovedConcessions = this.userConcessionsService.getApprovedConcessionsView();
        this.observableApprovedConcessions.subscribe(approvedConcession => {
            this.approvedConcessions = approvedConcession;
            this.isLoading = false;
        }, error => {
            this.errorMessage = <any>error;
            this.isLoading = false;
        });
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

}
