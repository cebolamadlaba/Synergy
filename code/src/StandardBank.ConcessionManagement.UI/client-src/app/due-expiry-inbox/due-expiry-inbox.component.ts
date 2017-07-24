﻿import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from "rxjs";
import { UserConcessionsService } from "../user-concessions/user-concessions.service";
import { UserConcessions } from "../models/user-concessions";
import { Subject } from 'rxjs/Rx'
import 'rxjs/add/operator/map';

@Component({
    selector: 'app-due-expiry-inbox',
    templateUrl: './due-expiry-inbox.component.html',
    styleUrls: ['./due-expiry-inbox.component.css']
})
export class DueExpiryInboxComponent implements OnInit {
    dtOptions: DataTables.Settings = {};
    dtTrigger: Subject<UserConcessions> = new Subject();
    observableUserConcessions: Observable<UserConcessions>;
    userConcessions: UserConcessions;
    errorMessage: String;

    constructor( @Inject(UserConcessionsService) private userConcessionsService) { }

    ngOnInit() {
        this.dtOptions = {
            pagingType: 'full_numbers',
            language: {
                emptyTable: "No records found!",
                search: "",
                searchPlaceholder: "Search"
            }
        };

        this.observableUserConcessions = this.userConcessionsService.getData();
        this.observableUserConcessions.subscribe(
            userConcessions => {
                this.userConcessions = userConcessions;
                this.dtTrigger.next();
            },
            error => this.errorMessage = <any>error);
    }
}
