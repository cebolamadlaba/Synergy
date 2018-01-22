import { Component, OnInit } from '@angular/core';
import { BusinessCentreManagementModel } from '../../models/business-centre-management-model';
import { BusinessCentreService } from '../../services/business-centre.service';
import { Location } from '@angular/common';
import { Observable } from "rxjs";

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

    observableErrors: Observable<string[]>;
    observableSave: Observable<boolean>;

    constructor(private location: Location, private businessCentreService: BusinessCentreService) {
        this.addBusinessCentreManagementModel = new BusinessCentreManagementModel();
    }

    ngOnInit() {
        this.loadBusinessCentreManagementModels();
    }

    loadBusinessCentreManagementModels() {
        this.isLoading = true;

        this.observableBusinessCentreManagementModels = this.businessCentreService.getBusinessCentreManagementModels();
        this.observableBusinessCentreManagementModels.subscribe(businessCentreManagementModels => {
            this.businessCentreManagementModels = businessCentreManagementModels;
            this.isLoading = false;
        }, error => {
            this.isLoading = false;
            this.errorMessage = <any>error;
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

                    //after saving reload the business centres
                    this.loadBusinessCentreManagementModels();
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

    goBack() {
        this.location.back();
    }

}
