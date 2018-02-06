import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../../models/user';
import { Observable } from "rxjs";
import { Location } from '@angular/common';
import { Centre } from '../../models/centre';
import { AeManagementService } from '../../services/ae-management.service';

@Component({
    selector: 'app-ae-management',
    templateUrl: './ae-management.component.html',
    styleUrls: ['./ae-management.component.css']
})
export class AeManagementComponent implements OnInit {

    errorMessage: string;
    validationError: string[];
    saveMessage: string;
    isLoading = true;
    isSaving = false;

    actionType: string;

    @ViewChild('addAEModal') addAEModal;

    centres: Centre[];
    aeUsers: User[];
    addAeUserModel: User;

    observableSave: Observable<boolean>;
    observableErrors: Observable<string[]>;

    constructor(private location: Location, private aeManagementService: AeManagementService) {
        this.addAeUserModel = new User();
    }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
        this.isLoading = true;

        Observable.forkJoin([
            this.aeManagementService.getAEUsers(),
            this.aeManagementService.getCentres()
        ]).subscribe(results => {
            this.aeUsers = <any>results[0];
            this.centres = <any>results[1];

            this.isLoading = false;
        },
            error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
    }

    addAE() {
        if (this.actionType == "Edit") {
            this.addAeUserModel = new User();
        }

        this.actionType = "Add";
        this.addAEModal.show();
    }

    editAe(aeUser: User) {
        this.actionType = "Edit";
        this.addAeUserModel = aeUser;
        this.addAEModal.show();
    }

    saveAE() {
        this.isSaving = true;
        this.errorMessage = null;
        this.validationError = null;
        this.saveMessage = null;

        this.observableErrors = this.aeManagementService.validateUser(this.addAeUserModel);
        this.observableErrors.subscribe(errors => {
            if (errors != null && errors.length > 0) {
                this.validationError = errors;
                this.isSaving = false;
            } else {
                this.observableSave = this.aeManagementService.saveAeUser(this.addAeUserModel);
                this.observableSave.subscribe(errors => {

                    if (this.addAeUserModel.id != null && this.addAeUserModel.id > 0) {
                        this.saveMessage = "AE updated successfully!";
                    } else {
                        this.saveMessage = "AE created successfully!";
                    }

                    this.addAeUserModel = new User();

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
