import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../../models/user';
import { Observable } from "rxjs";
import { Location } from '@angular/common';
import { Centre } from '../../models/centre';
import { AaManagementService } from '../../services/aa-management.service';

@Component({
    selector: 'app-aa-management',
    templateUrl: './aa-management.component.html',
    styleUrls: ['./aa-management.component.css']
})
export class AaManagementComponent implements OnInit {

    errorMessage: string;
    validationError: string[];
    saveMessage: string;
    isLoading = true;
    isSaving = false;

    actionType: string;

    @ViewChild('addAAModal') addAAModal;

    centres: Centre[];
    aaUsers: User[];
    addAaUserModel: User;

    observableSave: Observable<boolean>;
    observableErrors: Observable<string[]>;

    accountExecutives: User[];

    constructor(private location: Location, private aaManagementService: AaManagementService) {
        this.addAaUserModel = new User();
    }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
        this.isLoading = true;

        Observable.forkJoin([
            this.aaManagementService.getAAUsers(),
            this.aaManagementService.getCentres(),
            this.aaManagementService.getAEUsers()
        ]).subscribe(results => {
            this.aaUsers = <any>results[0];
            this.centres = <any>results[1];
            this.accountExecutives = <any>results[2];

            this.isLoading = false;
        },
            error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
    }

    addAA() {
        this.addAaUserModel = new User();
        this.actionType = "Add";
        this.addAAModal.show();
    }

    editAa(aaUser: User) {
        this.actionType = "Edit";
        this.addAaUserModel = aaUser;
        this.addAAModal.show();
    }

    saveAA() {
        this.isSaving = true;
        this.errorMessage = null;
        this.validationError = null;
        this.saveMessage = null;

        console.log(this.addAaUserModel);

        this.observableErrors = this.aaManagementService.validateUser(this.addAaUserModel);
        this.observableErrors.subscribe(errors => {
            if (errors != null && errors.length > 0) {
                this.validationError = errors;
                this.isSaving = false;
            } else {
                console.log(this.addAaUserModel);
                this.observableSave = this.aaManagementService.saveAaUser(this.addAaUserModel);
                this.observableSave.subscribe(errors => {

                    if (this.addAaUserModel.id != null && this.addAaUserModel.id > 0) {
                        this.saveMessage = "AA updated successfully!";
                    } else {
                        this.saveMessage = "AA created successfully!";
                    }

                    this.addAaUserModel = new User();

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
