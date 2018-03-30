import { Component, OnInit, Inject } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Subject } from 'rxjs/Rx';
import { MyConditionService } from '../services/my-condition.service';
import { Observable } from "rxjs";
import { Period } from '../models/period';
import { Centre } from '../models/Centre';
import { BcmManagementService } from '../services/bcm-management.service';
import { LookupDataService } from "../services/lookup-data.service";
import { RegionService } from "../services/region.service";
import { ConditionCounts } from "../models/condition-counts";
import { ConcessionCondition } from "../models/concession-condition";
import { Router, RouterModule } from '@angular/router';
import { ConcessionTypes } from '../constants/concession-types';
import { SearchConcessionDetail } from '../models/search-concession-detail';
import { UserConcessionsService } from "../services/user-concessions.service";
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Region } from '../models/region';
import { ConcessionSubStatus } from '../constants/concession-sub-status';

import { TransactionalConcessionService } from "../services/transactional-concession.service";
import { CashConcessionService } from "../services/cash-concession.service";
import { LendingService } from "../services/lending.service";
import { SearchConcessionFilterPipe } from "../filters/search-concession-filter.pipe";


@Component({
    selector: 'app-conditions',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

    periods: Period[];
    regions: Region[];
    businesscentres: Centre[]
    datepipe: DatePipe;

    errorMessage: String;
    validationError: String[];
    saveMessage: String;
    warningMessage: String;
    isLoading = true;
    status: string = "BCM";
    displayDate = new Date();

    today: String;

    planModel: any = { start_time: new Date() };


    region: Region;
    businesscentre: Centre;

    standardClass: string = "activeWidget";
    ongoingClass: string = "";

    constructor(
        @Inject(MyConditionService) private conditionService,
        @Inject(LookupDataService) private lookupDataService,
        @Inject(TransactionalConcessionService) private transactionalConcessionService,
        @Inject(CashConcessionService) private cashConcessionService,
        @Inject(LendingService) private lendingConcessionService,

        @Inject(UserConcessionsService) private userConcessionsService,
        @Inject(RegionService) private regionService,
        @Inject(BcmManagementService) private businesscentreService,

        private router: Router) { }

    observableApprovedConcessions: Observable<SearchConcessionDetail[]>;
    approvedConcessions: SearchConcessionDetail[];

    ngOnInit() {


        this.observableApprovedConcessions = this.lookupDataService.searchConsessions();
      
        this.today = new Date().toISOString().split('T')[0];

        this.observableApprovedConcessions.subscribe(approvedConcession => {
            this.approvedConcessions = approvedConcession;
            this.isLoading = false;
        }, error => {
            this.errorMessage = <any>error;
            this.isLoading = false;
        });

        this.regionService.getAll().subscribe(data => {
            this.regions = data;

        }, err => this.errorMessage = err);

        this.businesscentreService.getCentres().subscribe(data => {
            this.businesscentres = data;

        }, err => this.errorMessage = err);
    }
    periodFilter(value: string) {
        this.getFilteredView();
    }

 
    getFilteredView() {

        let businessCentreid = this.businesscentre == null ? null : this.businesscentre.id;
        let regionid = this.region == null ? null : this.region.id;

        this.lookupDataService.searchConsessionsFiltered(regionid, businessCentreid, this.status, this.today).subscribe(filteredconcessions => {
            this.approvedConcessions = filteredconcessions;

            this.isLoading = false;
        }, error => this.errorMessage = <any>error);


    }

    forwardPCM(concessiondetailed: SearchConcessionDetail) {
        
        if (confirm("Are you sure you want to forward this concession to PCM ?")) {             

            this.isLoading = true;
            this.errorMessage = null;
            this.validationError = null;  

            switch (concessiondetailed.concessionType) {
                case ConcessionTypes.Lending:
                    this.forwardLendingtoPCM(concessiondetailed);
                    break;
                case ConcessionTypes.Cash:
                    this.forwardCashtoPCM(concessiondetailed);
                    break;
                case ConcessionTypes.Transactional:
                    this.forwardTransactionaltoPCM(concessiondetailed);
                    break;
            }
        }       
    }

    openConcessionView(event, concessiondetailed: SearchConcessionDetail) {

        if (event.srcElement.tagName.toLowerCase() != "button") {

            switch (concessiondetailed.concessionType) {
                case ConcessionTypes.Lending:
                    this.router.navigate(['/lending-view-concession', concessiondetailed.riskGroupNumber, concessiondetailed.referenceNumber]);
                    break;
                case ConcessionTypes.Cash:
                    this.router.navigate(['/cash-view-concession', concessiondetailed.riskGroupNumber, concessiondetailed.referenceNumber]);
                    break;
                case ConcessionTypes.Transactional:
                    this.router.navigate(['/transactional-view-concession', concessiondetailed.riskGroupNumber, concessiondetailed.referenceNumber]);
                    break;
                case ConcessionTypes.BOL:
                    this.router.navigate(['/bol-view-concession', concessiondetailed.riskGroupNumber, concessiondetailed.referenceNumber]);
                    break;
            }
        }
    }

    forwardTransactionaltoPCM(concessiondetailed: SearchConcessionDetail) {     
       
        concessiondetailed.subStatus = ConcessionSubStatus.PCMPending;       
        concessiondetailed.comments = "Forwarded by PCM";     

        if (!this.validationError) {
            this.transactionalConcessionService.postForwardTransactionalPCM(concessiondetailed).subscribe(entity => {
                console.log("data saved");

                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;

                this.getFilteredView();

            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        } else {
            this.isLoading = false;
        }
    }

    forwardCashtoPCM(concessiondetailed: SearchConcessionDetail) {

        concessiondetailed.subStatus = ConcessionSubStatus.PCMPending;
        concessiondetailed.comments = "Forwarded by PCM";

        if (!this.validationError) {
            this.cashConcessionService.postForwardCashPCM(concessiondetailed).subscribe(entity => {
                console.log("data saved");

                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;

                this.getFilteredView();

            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        } else {
            this.isLoading = false;
        }
    }

    forwardLendingtoPCM(concessiondetailed: SearchConcessionDetail) {

        concessiondetailed.subStatus = ConcessionSubStatus.PCMPending;
        concessiondetailed.comments = "Forwarded by PCM";

        if (!this.validationError) {
            this.lendingConcessionService.postForwardLendingPCM(concessiondetailed).subscribe(entity => {
                console.log("data saved");

                this.saveMessage = entity.concession.referenceNumber;
                this.isLoading = false;

                this.getFilteredView();

            }, error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
        } else {
            this.isLoading = false;
        }
    }  
}
