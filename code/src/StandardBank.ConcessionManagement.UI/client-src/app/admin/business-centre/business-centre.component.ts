import { Component, OnInit } from '@angular/core';
import { BusinessCentreManagementModel } from '../../models/business-centre-management-model';
import { BusinessCentreService } from '../../services/business-centre.service';
import { Location } from '@angular/common';
import { Observable } from "rxjs";
import { User } from '../../models/user';
import { BusinessCentreManagementLookupModel } from '../../models/business-centre-management-lookup-model';

@Component({
    selector: 'app-business-centre',
    templateUrl: './business-centre.component.html',
    styleUrls: ['./business-centre.component.css']
})
export class BusinessCentreComponent implements OnInit {

    errorMessage: string;
    validationError: string[];
    saveMessage: string;
    isLoading = true;

    observableBusinessCentreManagementModels: Observable<BusinessCentreManagementModel[]>;
    businessCentreManagementModels: BusinessCentreManagementModel[];
    addBusinessCentreManagementModel: BusinessCentreManagementModel;

    observableBusinessCentreManagers: Observable<User[]>;
    businessCentreManagers: User[];

    observableErrors: Observable<string[]>;
    observableSave: Observable<boolean>;

    observableBusinessCentreManagementLookupModel: Observable<BusinessCentreManagementLookupModel>;
    businessCentreManagementLookupModel: BusinessCentreManagementLookupModel;

    selectedAccountExecutive: User;
    selectedAccountExecutives: User[];

    constructor(private location: Location, private businessCentreService: BusinessCentreService) {
        this.addBusinessCentreManagementModel = new BusinessCentreManagementModel();
    }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
        this.isLoading = true;

        Observable.forkJoin([
            this.businessCentreService.getBusinessCentreManagementModels(),
            this.businessCentreService.getBusinessCentreManagementLookupModel()
        ]).subscribe(results => {
            this.businessCentreManagementModels = <any>results[0];
            this.businessCentreManagementLookupModel = <any>results[1];

            this.isLoading = false;
        },
            error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
    }

    createBusinessCentre() {
        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;
        this.saveMessage = null;

        this.addBusinessCentreManagementModel.isActive = true;

        this.observableErrors = this.businessCentreService.validateBusinessCentreManagementModel(this.addBusinessCentreManagementModel);
        this.observableErrors.subscribe(errors => {
            if (errors != null && errors.length > 0) {
                this.validationError = errors;
                this.isLoading = false;
            } else {
                this.observableSave = this.businessCentreService.createBusinessCentreManagementModel(this.addBusinessCentreManagementModel);
                this.observableSave.subscribe(errors => {
                    this.saveMessage = "Business Centre created successfully!";
                    this.addBusinessCentreManagementModel = new BusinessCentreManagementModel();

                    //after saving reload the data
                    this.loadData();
                }, error => {
                    this.isLoading = false;
                    this.errorMessage = <any>error;
                });
            }
        }, error => {
            this.isLoading = false;
            this.errorMessage = <any>error;
        });
    }

    addAccountExecutive() {
        if (this.selectedAccountExecutive != null) {
            if (this.selectedAccountExecutives == null) {
                this.selectedAccountExecutives = [];
            }

            if (!this.selectedAccountExecutives.find(result => result.id == this.selectedAccountExecutive.id)) {
                this.selectedAccountExecutives.push(this.selectedAccountExecutive);
            }
        }
    }

    goBack() {
        this.location.back();
    }

}
