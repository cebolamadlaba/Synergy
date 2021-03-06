import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../../models/user';
import { Observable } from "rxjs";
import { Location } from '@angular/common';
import { Centre } from '../../models/centre';
import { BolTradeManagementService } from '../../services/bol-trade-management.service';
import { LookupDataService } from '../../services/lookup-data.service';
import { UserService } from '../../services/user.service';
import { RoleSubRole } from "../../models/RoleSubRole";
import { RouteConfigLoadEnd } from '@angular/router';
import { SubRoleEnum } from "../../models/subrole-enum";
import { RoleEnum } from '../../models/role-enum';

@Component({
    selector: 'app-bol-trade-management',
    templateUrl: './bol-trade-management.component.html',
    styleUrls: ['./bol-trade-management.component.css']
})
export class BolTradeManagementComponent implements OnInit {

    currentUser: User;
    errorMessage: string;
    validationError: string[];
    saveMessage: string;
    isLoading = true;
    isSaving = false;
    isAdd: boolean;

    actionType: string;

    @ViewChild('addBolTradeModal') addBolTradeModal;

    centres: Centre[];
    bolTradeUsers: User[];
    addBolTradeUserModel: User;
    roleSubRole: RoleSubRole[];
    bolTradeUsersFiltered: User[];

    observableSave: Observable<boolean>;
    observableErrors: Observable<string[]>;

    accountExecutives: User[];
    selectedRoleSubRole: RoleSubRole;

    constructor(
        private location: Location,
        private bolTradeManagementService: BolTradeManagementService,
        private userService: UserService,
        private lookupDataService: LookupDataService, ) {
        this.addBolTradeUserModel = new User();
    }

    ngOnInit() {
        this.loadData(null);
    }

    loadData(subRole: RoleSubRole) {
        this.isLoading = true;

        Observable.forkJoin([
            this.bolTradeManagementService.getBolOrTradeUsers(),
            this.bolTradeManagementService.getCentres(),
            this.bolTradeManagementService.getAEUsers(),
            this.lookupDataService.getRoleSubRolesByRoleId(RoleEnum.AA),
            this.userService.getData()
        ]).subscribe(results => {
            this.bolTradeUsers = <any>results[0];
            this.centres = <any>results[1];
            this.accountExecutives = <any>results[2];
            this.roleSubRole = <any>results[3];
            this.currentUser = <any>results[4];
            this.bolTradeUsersFiltered = this.bolTradeUsers;
            this.isLoading = false;

            if (this.roleSubRole.length > 0) {

                if (subRole == null) {
                    this.selectedRoleSubRole = this.roleSubRole[0];
                }
                else {
                    this.selectedRoleSubRole = this.roleSubRole.filter(c => c.subRoleId == subRole.subRoleId)[0];
                }
                this.bolTradeUsersFiltered = this.bolTradeUsers.filter(re => re.subRoleId == this.selectedRoleSubRole.subRoleId);
            }
        },
            error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
    }


    filterBolTradeUsers(selection: RoleSubRole) {

        this.selectedRoleSubRole = selection;

        if (selection.subRoleId == SubRoleEnum.NoSubrole) {
            this.bolTradeUsersFiltered = this.bolTradeUsers.filter(re => re.subRoleId == null);
        }
        else {
            this.bolTradeUsersFiltered = this.bolTradeUsers.filter(re => re.subRoleId == selection.subRoleId);
        }
    }

    addBolTrade() {
        this.addBolTradeUserModel = new User();
        this.actionType = "Add";
        this.isAdd = true;
        if (this.currentUser.isRequestor) {
            this.addBolTradeUserModel.accountExecutiveUserId = this.currentUser.id;
        }
        this.addBolTradeModal.show();
    }

    editBolTrade(bolTradeUser: User) {
        this.actionType = "Edit";
        this.isAdd = false;
        this.addBolTradeUserModel = bolTradeUser;
        if (this.currentUser.isRequestor) {
            this.addBolTradeUserModel.accountExecutiveUserId = this.currentUser.id;
        }
        this.addBolTradeModal.show();
    }

    saveBolOrTradeUser() {
        this.isSaving = true;
        this.errorMessage = null;
        this.validationError = null;
        this.saveMessage = null;
        if (this.addBolTradeUserModel.subRoleId == SubRoleEnum.NoSubrole) {
            this.addBolTradeUserModel.subRoleId = null;
        }

        console.log(this.addBolTradeUserModel);

        this.observableErrors = this.bolTradeManagementService.validateUser(this.addBolTradeUserModel);
        this.observableErrors.subscribe(errors => {
            if (errors != null && errors.length > 0) {
                this.validationError = errors;
                this.isSaving = false;
            } else {
                console.log(this.addBolTradeUserModel);
                if (this.isAdd)
                    this.observableSave = this.bolTradeManagementService.saveBolOrTradeUser(this.addBolTradeUserModel);
                else
                    this.observableSave = this.bolTradeManagementService.updateAccountAssistantSubRole(this.addBolTradeUserModel);

                this.observableSave.subscribe(errors => {

                    if (this.addBolTradeUserModel.id != null && this.addBolTradeUserModel.id > 0) {
                        this.saveMessage = "AA updated successfully!";
                    } else {
                        this.saveMessage = "AA created successfully!";
                    }

                    this.addBolTradeUserModel = new User();

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
