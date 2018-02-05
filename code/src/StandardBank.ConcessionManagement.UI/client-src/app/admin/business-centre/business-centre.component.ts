import { Component, OnInit, ViewChild } from '@angular/core';
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
    isSaving = false;

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
    observableSelectedAccountExecutives: Observable<User[]>;

    @ViewChild('addBusinessCentreModal') addBusinessCentreModal;

    actionType: string;
    isBcmAEsLoading = false;
    canAdd = true;

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

            if (this.businessCentreManagementLookupModel && this.businessCentreManagementLookupModel.currentUser &&
                this.businessCentreManagementLookupModel.currentUser.isBCM) {
                this.canAdd = false;
            }

            this.isLoading = false;
        },
            error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
    }

    createBusinessCentre() {
        this.isSaving = true;
        this.errorMessage = null;
        this.validationError = null;
        this.saveMessage = null;

        this.addBusinessCentreManagementModel.isActive = true;
        this.addBusinessCentreManagementModel.accountExecutives = this.selectedAccountExecutives;

        this.observableErrors = this.businessCentreService.validateBusinessCentreManagementModel(this.addBusinessCentreManagementModel);
        this.observableErrors.subscribe(errors => {
            if (errors != null && errors.length > 0) {
                this.validationError = errors;
                this.isSaving = false;
            } else {
                this.observableSave = this.businessCentreService.createBusinessCentreManagementModel(this.addBusinessCentreManagementModel);
                this.observableSave.subscribe(errors => {

                    if (this.addBusinessCentreManagementModel.centreId != null && this.addBusinessCentreManagementModel.centreId > 0) {
                        this.saveMessage = "Business Centre updated successfully!";
                    } else {
                        this.saveMessage = "Business Centre created successfully!";
                    }
                    
                    this.addBusinessCentreManagementModel = new BusinessCentreManagementModel();
                    this.selectedAccountExecutive = null;
                    this.selectedAccountExecutives = null;

                    this.isSaving = false;

                    //after saving reload the data
                    this.loadData();
                }, error => {
                    this.isSaving = false;
                    this.errorMessage = <any>error;
                });
            }
        }, error => {
            this.isSaving = false;
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

    removeAccountExecutive(index: number) {
        this.selectedAccountExecutives.splice(index, 1);
    }

    editBusinessCentre(businessCentreManagementModel: BusinessCentreManagementModel) {
        this.isBcmAEsLoading = true;
        this.addBusinessCentreModal.show();
        this.actionType = "Edit";
        this.addBusinessCentreManagementModel = businessCentreManagementModel;
        this.selectedAccountExecutive = null;
        this.selectedAccountExecutives = null;

        this.observableSelectedAccountExecutives = this.businessCentreService.getBusinessCentreAccountExecutives(businessCentreManagementModel.centreId);
        this.observableSelectedAccountExecutives.subscribe(result => {
            if (result != null && result.length > 0)
                this.selectedAccountExecutives = result;

            this.isBcmAEsLoading = false;
        }, error => {
            this.addBusinessCentreModal.hide();
            this.isBcmAEsLoading = false;
            this.errorMessage = <any>error;
        });
    }

    addBusinessCentre() {
        this.actionType = "Add";
        this.addBusinessCentreManagementModel = new BusinessCentreManagementModel();
        this.selectedAccountExecutive = null;
        this.selectedAccountExecutives = null;
        this.addBusinessCentreModal.show();
    }

    goBack() {
        this.location.back();
    }

}
