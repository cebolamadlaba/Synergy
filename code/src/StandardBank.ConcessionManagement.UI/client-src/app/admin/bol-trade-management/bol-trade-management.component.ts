import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../../models/user';
import { Observable } from "rxjs";
import { Location } from '@angular/common';
import { Centre } from '../../models/centre';
import { BolTradeManagementService } from '../../services/bol-trade-management.service';
import { RoleSubRole } from "../../models/RoleSubRole";
import { RouteConfigLoadEnd } from '@angular/router';

@Component({
  selector: 'app-bol-trade-management',
  templateUrl: './bol-trade-management.component.html',
  styleUrls: ['./bol-trade-management.component.css']
})
export class BolTradeManagementComponent implements OnInit {

    errorMessage: string;
    validationError: string[];
    saveMessage: string;
    isLoading = true;
    isSaving = false;

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

    constructor(private location: Location, private bolTradeManagementService: BolTradeManagementService) {
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
            this.bolTradeManagementService.getRoleSubRoles()           
        ]).subscribe(results => {
            this.bolTradeUsers = <any>results[0];
            this.centres = <any>results[1];
            this.accountExecutives = <any>results[2];
            this.roleSubRole = <any>results[3];
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
        this.bolTradeUsersFiltered = this.bolTradeUsers.filter(re => re.subRoleId == selection.subRoleId);
    }

    addBolTrade() {
        this.addBolTradeUserModel = new User();
        this.actionType = "Add";
        this.addBolTradeModal.show();
    }

    editBolTrade(bolTradeUser: User) {
        this.actionType = "Edit";
        this.addBolTradeUserModel = bolTradeUser;
        this.addBolTradeModal.show();
    }

    saveBolOrTradeUser() {
        this.isSaving = true;
        this.errorMessage = null;
        this.validationError = null;
        this.saveMessage = null;

        console.log(this.addBolTradeUserModel);

        this.observableErrors = this.bolTradeManagementService.validateUser(this.addBolTradeUserModel);
        this.observableErrors.subscribe(errors => {
            if (errors != null && errors.length > 0) {
                this.validationError = errors;
                this.isSaving = false;
            } else {
                console.log(this.addBolTradeUserModel);
                this.observableSave = this.bolTradeManagementService.saveBolOrTradeUser(this.addBolTradeUserModel);
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
