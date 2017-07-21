import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from "rxjs";
import { UserConcessionsService } from "../user-concessions/user-concessions.service";
import { UserConcessions } from "../models/user-concessions";

@Component({
    selector: 'app-due-expiry-inbox',
    templateUrl: './due-expiry-inbox.component.html',
    styleUrls: ['./due-expiry-inbox.component.css']
})
export class DueExpiryInboxComponent implements OnInit {
    observableUserConcessions: Observable<UserConcessions>;
    userConcessions: UserConcessions;
    errorMessage: String;

    constructor( @Inject(UserConcessionsService) private userConcessionsService) { }

    ngOnInit() {
        this.observableUserConcessions = this.userConcessionsService.getData();
        this.observableUserConcessions.subscribe(userConcessions => this.userConcessions = userConcessions,
            error => this.errorMessage = <any>error);
    }
}
