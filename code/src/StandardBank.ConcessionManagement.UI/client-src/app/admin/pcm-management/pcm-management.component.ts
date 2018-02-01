import { Component, OnInit, ViewChild } from '@angular/core';
import { Location } from '@angular/common';
import { PcmManagementService } from '../../services/pcm-management.service';
import { Observable } from "rxjs";
import { User } from '../../models/user';

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

    constructor(private location: Location, private pcmManagementService: PcmManagementService) { }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
        this.isLoading = true;

        Observable.forkJoin([
            this.pcmManagementService.getPCMUsers()
        ]).subscribe(results => {
            this.pcmUsers = <any>results[0];

            this.isLoading = false;
        },
            error => {
                this.errorMessage = <any>error;
                this.isLoading = false;
            });
    }

    addPCM() {
        this.actionType = "Add";
        this.addPCMModal.show();
    }

    editPcm(pcmUser: User) {
        this.actionType = "Edit";
        this.addPCMModal.show();
    }

    createPCM() {

    }

    goBack() {
        this.location.back();
    }

}
