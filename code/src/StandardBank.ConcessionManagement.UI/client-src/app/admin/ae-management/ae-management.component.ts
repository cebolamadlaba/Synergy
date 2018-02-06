import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../../models/user';
import { Observable } from "rxjs";
import { Location } from '@angular/common';
import { Centre } from '../../models/centre';
import { AeManagementService } from '../../services/ae-management.service';
import { AccountExecutive } from '../../models/account-executive';

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
    isAeAAsLoading = false;

    actionType: string;

    @ViewChild('addAEModal') addAEModal;

    centres: Centre[];
    aeUsers: User[];
    addAeUserModel: User;

    observableSave: Observable<boolean>;
    observableErrors: Observable<string[]>;

    selectedAccountAssistant: User;
    selectedAccountAssistants: User[];
    observableSelectedAccountAssistants: Observable<User[]>;
    accountAssistants: User[];

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
            this.aeManagementService.getCentres(),
            this.aeManagementService.getAAUsers()
        ]).subscribe(results => {
            this.aeUsers = <any>results[0];
            this.centres = <any>results[1];
            this.accountAssistants = <any>results[2];

            this.isLoading = false;
        },
            error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
    }

    addAE() {
        this.addAeUserModel = new User();
        this.selectedAccountAssistant = null;
        this.selectedAccountAssistants = null;
        this.actionType = "Add";
        this.addAEModal.show();
    }

    editAe(aeUser: User) {
        this.isAeAAsLoading = true;
        this.actionType = "Edit";
        this.addAEModal.show();
        this.addAeUserModel = aeUser;
        this.selectedAccountAssistant = null;
        this.selectedAccountAssistants = null;

        this.observableSelectedAccountAssistants = this.aeManagementService.getAEAAUsers(aeUser.id);
        this.observableSelectedAccountAssistants.subscribe(result => {
            if (result != null && result.length > 0)
                this.selectedAccountAssistants = result;

            this.isAeAAsLoading = false;
        }, error => {
            this.addAEModal.hide();
            this.isAeAAsLoading = false;
            this.errorMessage = <any>error;
        });
    }

    addAccountAssistant() {
        if (this.selectedAccountAssistant != null) {
            if (this.selectedAccountAssistants == null) {
                this.selectedAccountAssistants = [];
            }

            if (!this.selectedAccountAssistants.find(result => result.id == this.selectedAccountAssistant.id)) {
                this.selectedAccountAssistants.push(this.selectedAccountAssistant);
            }
        }
    }

    removeAccountAssistant(index: number) {
        this.selectedAccountAssistants.splice(index, 1);
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
                var accountExecutive = new AccountExecutive();
                accountExecutive.user = this.addAeUserModel;
                accountExecutive.accountAssistants = this.selectedAccountAssistants;

                this.observableSave = this.aeManagementService.saveAccountExecutive(accountExecutive);
                this.observableSave.subscribe(errors => {

                    if (this.addAeUserModel.id != null && this.addAeUserModel.id > 0) {
                        this.saveMessage = "AE updated successfully!";
                    } else {
                        this.saveMessage = "AE created successfully!";
                    }

                    this.addAeUserModel = new User();
                    this.selectedAccountAssistant = null;
                    this.selectedAccountAssistants = null;

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
