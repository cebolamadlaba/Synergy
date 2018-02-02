import { Component, OnInit, ViewChild } from '@angular/core';
import { Location } from '@angular/common';
import { PcmManagementService } from '../../services/pcm-management.service';
import { Observable } from "rxjs";
import { User } from '../../models/user';
import { RegionCentresModel } from '../../models/region-centres-model';
import { Centre } from '../../models/centre';

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

    actionType: string;

    @ViewChild('addPCMModal') addPCMModal;

    pcmUsers: User[];
    regionCentresModels: RegionCentresModel[];
    addPcmUserModel: User;
    selectedRegionCentresModel: RegionCentresModel;
    selectedCentre: Centre;
    isPcmBcmsLoading = false;

    observableSave: Observable<boolean>;

    constructor(private location: Location, private pcmManagementService: PcmManagementService) {
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
            this.pcmManagementService.getRegionCentres()
        ]).subscribe(results => {
            this.pcmUsers = <any>results[0];
            this.regionCentresModels = <any>results[1];

            this.isLoading = false;
        },
            error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
    }

    addPCM() {
        this.actionType = "Add";
        this.addPcmUserModel = new User();
        this.selectedRegionCentresModel = new RegionCentresModel();
        this.selectedCentre = new Centre();
        this.addPCMModal.show();
    }

    editPcm(pcmUser: User) {
        this.actionType = "Edit";
        this.addPcmUserModel = pcmUser;
        this.selectedRegionCentresModel = new RegionCentresModel();
        this.selectedCentre = new Centre();
        this.addPCMModal.show();
    }

    savePCM() {
        this.isLoading = true;
        this.errorMessage = null;
        this.validationError = null;
        this.saveMessage = null;

        this.addPcmUserModel.isActive = true;

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

            //after saving reload the data
            this.loadData();
        }, error => {
            this.isLoading = false;
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

    goBack() {
        this.location.back();
    }

}
