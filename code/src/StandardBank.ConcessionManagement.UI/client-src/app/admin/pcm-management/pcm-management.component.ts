import { Component, OnInit, ViewChild } from '@angular/core';
import { Location } from '@angular/common';
import { PcmManagementService } from '../../services/pcm-management.service';
import { Observable } from "rxjs";
import { User } from '../../models/user';
import { RoleSubRole } from '../../models/RoleSubRole';
import { RegionCentresModel } from '../../models/region-centres-model';
import { Centre } from '../../models/centre';
import { UserService } from '../../services/user.service';
import { SubRoleEnum } from "../../models/subrole-enum";

@Component({
    selector: 'app-pcm-management',
    templateUrl: './pcm-management.component.html',
    styleUrls: ['./pcm-management.component.css']
})
export class PcmManagementComponent implements OnInit {

    errorMessage: string;
    validationError: string[];
    saveMessage: string;
    isLoading = true;
    isSaving = false;

    actionType: string;

    @ViewChild('addPCMModal') addPCMModal;

    pcmUsers: User[];
    regionCentresModels: RegionCentresModel[];
    addPcmUserModel: User;
    selectedRegionCentresModel: RegionCentresModel;
    selectedCentre: Centre;
    isPcmBcmsLoading = false;
    roleSubRoles: RoleSubRole[];
    selectedRoleSubRole: RoleSubRole;

    observableSave: Observable<boolean>;
    observableErrors: Observable<string[]>;

    currentUser: User;

    constructor(private location: Location, private pcmManagementService: PcmManagementService, private userService: UserService) {
        this.addPcmUserModel = new User();
        this.selectedRegionCentresModel = new RegionCentresModel();
        this.selectedCentre = new Centre();
    }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
        this.isLoading = true;

        Observable.forkJoin([
            this.pcmManagementService.getPCMUsers(),
            this.pcmManagementService.getRegionCentres(),
            this.pcmManagementService.getRoleSubRoles(),
            this.userService.getData()
        ]).subscribe(results => {
            this.pcmUsers = <any>results[0];
            this.regionCentresModels = <any>results[1];
            this.roleSubRoles = <any>results[2];
            this.currentUser = <any>results[3];

            if (this.roleSubRoles.length > 0) {
                this.roleSubRoles = this.roleSubRoles.filter(a => {
                    return a.subRoleId == SubRoleEnum.NoSubrole || a.subRoleId == SubRoleEnum.PCMSnI;
                });
            }

            this.isLoading = false;
        },
            error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
    }

    addPCM() {
        this.addPcmUserModel = new User();
        this.selectedRegionCentresModel = new RegionCentresModel();
        this.selectedCentre = new Centre();
        this.selectedRoleSubRole = new RoleSubRole();
        this.actionType = "Add";
        this.addPCMModal.show();
    }

    editPcm(pcmUser: User) {
        this.actionType = "Edit";
        this.addPcmUserModel = pcmUser;
        this.selectedRegionCentresModel = new RegionCentresModel();
        this.selectedCentre = new Centre();
        this.selectedRoleSubRole = this.setSelectedSubRole();
        this.addPCMModal.show();
    }

    savePCM() {
        this.isSaving = true;
        this.errorMessage = null;
        this.validationError = null;
        this.saveMessage = null;

        this.addUSubRole();

        this.observableErrors = this.pcmManagementService.validateUser(this.addPcmUserModel);
        this.observableErrors.subscribe(errors => {
            if (errors != null && errors.length > 0) {
                this.validationError = errors;
                this.isSaving = false;
            } else {

                this.observableSave = this.pcmManagementService.savePcmUser(this.addPcmUserModel);
                this.observableSave.subscribe(errors => {

                    if (this.addPcmUserModel.id != null && this.addPcmUserModel.id > 0) {
                        this.saveMessage = "PCM updated successfully!";
                    } else {
                        this.saveMessage = "PCM created successfully!";
                    }

                    this.addPcmUserModel = new User();
                    this.selectedRegionCentresModel = new RegionCentresModel();
                    this.selectedCentre = new Centre();
                    this.selectedRoleSubRole = new RoleSubRole();

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

    removeUserCentre(index: number) {
        this.addPcmUserModel.userCentres.splice(index, 1);
    }

    addUserCentre() {
        if (this.selectedCentre != null) {
            if (this.addPcmUserModel.userCentres == null) {
                this.addPcmUserModel.userCentres = [];
            }

            if (!this.addPcmUserModel.userCentres.find(result => result.id == this.selectedCentre.id)) {
                this.addPcmUserModel.userCentres.push(this.selectedCentre);
            }
        }
    }

    addUSubRole() {
        if (this.selectedRoleSubRole != null) {
            if (this.addPcmUserModel.roleSubRole == null) {
                this.addPcmUserModel.roleSubRole = new RoleSubRole();
            }

            this.addPcmUserModel.roleSubRole = this.selectedRoleSubRole;
            this.addPcmUserModel.subRoleId = this.selectedRoleSubRole.subRoleId;
        }
    }

    setSelectedSubRole() {
        let roleSubRole = this.roleSubRoles.filter(item => {
            return item.subRoleId == this.addPcmUserModel.roleSubRole.subRoleId;
        })[0];

        return roleSubRole;
    }

    goBack() {
        this.location.back();
    }

}
