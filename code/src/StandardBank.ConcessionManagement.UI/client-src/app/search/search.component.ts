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


    region: Region;
    businesscentre: Centre;

    standardClass: string = "activeWidget";
    ongoingClass: string = "";

    constructor(
        @Inject(MyConditionService) private conditionService,
        @Inject(LookupDataService) private lookupDataService,
        @Inject(UserConcessionsService) private userConcessionsService,
        @Inject(RegionService) private regionService,
        @Inject(BcmManagementService) private businesscentreService,

        private router: Router) { }

    observableApprovedConcessions: Observable<SearchConcessionDetail[]>;
    approvedConcessions: SearchConcessionDetail[];


    ngOnInit() {


        this.observableApprovedConcessions = this.lookupDataService.searchConsessions();

        this.observableApprovedConcessions.subscribe(approvedConcession => {
            this.approvedConcessions = approvedConcession;
            this.isLoading = false;
        }, error => {
            this.errorMessage = <any>error;
            this.isLoading = false;
        });

        this.regionService.getAll().subscribe(data => {
            this.regions = data;

            //this.region = this.regions[0];


        }, err => this.errorMessage = err);

        this.businesscentreService.getCentres().subscribe(data => {
            this.businesscentres = data;


            //this.businesscentre = this.businesscentres[0];


        }, err => this.errorMessage = err);
    }
    periodFilter(value: string) {
        this.getFilteredView();
    }
    getFilteredView() {

        let businessCentreid = this.businesscentre == null ? null : this.businesscentre.id;
        let regionid = this.region == null ? null : this.region.id;

        this.lookupDataService.searchConsessionsFiltered(regionid, businessCentreid, this.status, this.displayDate.toDateString()).subscribe(filteredconcessions => {
            this.approvedConcessions = filteredconcessions;

            this.isLoading = false;
        }, error => this.errorMessage = <any>error);


    }

    forwardPCM(concessiondetailed: SearchConcessionDetail) {
        if (confirm("Are you sure you want to forward this consession to PCM ?")) {
            this.isLoading = true;

            //update the condition in the database
            //concessionCondition.conditionMet = true;

            //this.observableCondition = this.conditionService.updateCondition(concessionCondition);
            //this.observableCondition.subscribe(
            //    condition => {
            //        this.condition = condition;
            //        this.loadAll();
            //    },
            //    error => this.errorMessage = <any>error);
        }
    }

    openConcessionView(concessiondetailed: SearchConcessionDetail) {

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
        }
    }
}
