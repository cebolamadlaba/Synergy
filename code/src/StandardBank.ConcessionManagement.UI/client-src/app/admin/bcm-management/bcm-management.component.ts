import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../../models/user';
import { Observable } from "rxjs";
import { Location } from '@angular/common';
import { BcmManagementService } from '../../services/bcm-management.service';
import { Centre } from '../../models/centre';

@Component({
    selector: 'app-bcm-management',
    templateUrl: './bcm-management.component.html',
    styleUrls: ['./bcm-management.component.css']
})
export class BcmManagementComponent implements OnInit {

    errorMessage: string;
    validationError: string[];
    saveMessage: string;
    isLoading = true;
    isSaving = false;

    actionType: string;

    @ViewChild('addBCMModal') addBCMModal;

    centres: Centre[];
    bcmUsers: User[];
    addBcmUserModel: User;

    observableSave: Observable<boolean>;
    observableErrors: Observable<string[]>;

    constructor(private location: Location, private bcmManagementService: BcmManagementService) {
        this.addBcmUserModel = new User();
    }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
        this.isLoading = true;

        Observable.forkJoin([
            this.bcmManagementService.getBCMUsers(),
            this.bcmManagementService.getCentres()
        ]).subscribe(results => {
            this.bcmUsers = <any>results[0];
            this.centres = <any>results[1];

            this.isLoading = false;
        },
            error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
    }

    addBCM() {
        this.addBcmUserModel = new User();
        this.actionType = "Add";
        this.addBCMModal.show();
    }

    editBcm(bcmUser: User) {
        this.actionType = "Edit";
        this.addBcmUserModel = bcmUser;
        this.addBCMModal.show();
    }

    saveBCM() {
        this.isSaving = true;
        this.errorMessage = null;
        this.validationError = null;
        this.saveMessage = null;

        this.observableErrors = this.bcmManagementService.validateUser(this.addBcmUserModel);
        this.observableErrors.subscribe(errors => {
            if (errors != null && errors.length > 0) {
                this.validationError = errors;
                this.isSaving = false;
            } else {
                this.observableSave = this.bcmManagementService.saveBcmUser(this.addBcmUserModel);
                this.observableSave.subscribe(errors => {

                    if (this.addBcmUserModel.id != null && this.addBcmUserModel.id > 0) {
                        this.saveMessage = "BCM updated successfully!";
                    } else {
                        this.saveMessage = "BCM created successfully!";
                    }

                    this.addBcmUserModel = new User();

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

    goBack() {
        this.location.back();
    }
}
