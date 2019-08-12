import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../../models/user';
import { Observable } from "rxjs";
import { Location } from '@angular/common';
import { Centre } from '../../models/centre';
import { BolTradeAeManagementService } from '../../services/bol-trade-ae-management.service';
import { AccountExecutive } from '../../models/account-executive';
import { UserService } from '../../services/user.service';
import { RoleSubRole } from "../../models/RoleSubRole";
import { SubRoleEnum } from "../../models/subrole-enum";

@Component({
    selector: 'app-bol-trade-ae-management',
    templateUrl: './bol-trade-ae-management.component.html',
    styleUrls: ['./bol-trade-ae-management.component.css']
})
export class BolTradeAeManagementComponent implements OnInit {

    errorMessage: string;
    validationError: string[];
    saveMessage: string;
    isLoading = true;
    isSaving = false;
    isAeAAsLoading = false;
    canAdd = true;

    actionType: string;

    @ViewChild('addBolTradeAEModal') addBolTradeAEModal;

    centres: Centre[];
    bolTradeAeUsers: User[];
    addBolTradeAeUserModel: User;
    roleSubRole: RoleSubRole[];

    observableSave: Observable<boolean>;
    observableErrors: Observable<string[]>;

    selectedAccountAssistant: User;
    selectedAccountAssistants: User[];
    selectedRoleSubRole: RoleSubRole;
    observableSelectedAccountAssistants: Observable<User[]>;
    accountAssistants: User[];
    accountAssistantsFiltered: User[];
    currentUser: User;

    constructor(private location: Location, private bolTradeAeManagementService: BolTradeAeManagementService, private userService: UserService) {
        this.addBolTradeAeUserModel = new User();
    }

    ngOnInit() {
        this.loadData(null);
    }

    loadData(subRole: RoleSubRole) {
        this.isLoading = true;

        Observable.forkJoin([
            this.bolTradeAeManagementService.getBolTradAEUsers(),
            this.bolTradeAeManagementService.getCentres(),
            this.bolTradeAeManagementService.getBolTradAAUsers(),
            this.userService.getData(),
            this.bolTradeAeManagementService.getRoleSubRoles()
        ]).subscribe(results => {
            this.bolTradeAeUsers = <any>results[0];
            this.centres = <any>results[1];
            this.accountAssistants = <any>results[2];
            this.currentUser = <any>results[3];
            this.roleSubRole = <any>results[4];

            if (this.currentUser.isRequestor)
                this.canAdd = false;

            this.isLoading = false;

            if (this.roleSubRole.length > 0) {

                this.roleSubRole = this.roleSubRole.filter(a => {
                    return a.subRoleId != SubRoleEnum.NoSubrole;
                });

                this.selectedRoleSubRole = this.roleSubRole[0];

                this.setSelectedSubRole(this.selectedRoleSubRole);
            }

        }, error => {
            this.errorMessage = <any>error;
            this.isLoading = false;
        });
    }

    addBolTradeAE() {
        this.addBolTradeAeUserModel = new User();
        this.selectedAccountAssistant = null;
        this.selectedAccountAssistants = null;
        this.actionType = "Add";
        this.addBolTradeAEModal.show();
    }

    editBolTradeAe(aeUser: User) {
        this.isAeAAsLoading = true;
        this.actionType = "Edit";
        this.addBolTradeAEModal.show();
        this.addBolTradeAeUserModel = aeUser;
        this.selectedAccountAssistant = null;
        this.selectedAccountAssistants = null;

        this.observableSelectedAccountAssistants = this.bolTradeAeManagementService.getAEAAUsers(aeUser.id);
        this.observableSelectedAccountAssistants.subscribe(result => {
            if (result != null && result.length > 0)
                this.selectedAccountAssistants = result;

            this.isAeAAsLoading = false;
        }, error => {
            this.addBolTradeAEModal.hide();
            this.isAeAAsLoading = false;
            this.errorMessage = <any>error;
        });
    }

    setSelectedSubRole(selection: RoleSubRole) {
        this.selectedRoleSubRole = selection;

        this.accountAssistantsFiltered = this.accountAssistants.filter(a => {
            return a.subRoleId != null && a.subRoleId == this.selectedRoleSubRole.subRoleId;
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


        this.observableErrors = this.bolTradeAeManagementService.validateUser(this.addBolTradeAeUserModel);
        this.observableErrors.subscribe(errors => {
            if (errors != null && errors.length > 0) {
                this.validationError = errors;
                this.isSaving = false;
            } else {
                var accountExecutive = new AccountExecutive();
                accountExecutive.user = this.addBolTradeAeUserModel;
                accountExecutive.accountAssistants = this.selectedAccountAssistants;

                this.observableSave = this.bolTradeAeManagementService.saveAccountExecutive(accountExecutive);
                this.observableSave.subscribe(errors => {

                    if (this.addBolTradeAeUserModel.id != null && this.addBolTradeAeUserModel.id > 0) {
                        this.saveMessage = "AE updated successfully!";
                    } else {
                        this.saveMessage = "AE created successfully!";
                    }

                    this.addBolTradeAeUserModel = new User();
                    this.selectedAccountAssistant = null;
                    this.selectedAccountAssistants = null;

                    this.isSaving = false;

                    //after saving reload the data
                    this.loadData(null);
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
