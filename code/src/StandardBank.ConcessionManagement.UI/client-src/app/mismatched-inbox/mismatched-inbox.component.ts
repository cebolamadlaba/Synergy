import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from "rxjs";
import { UserConcessionsService } from "../user-concessions/user-concessions.service";
import { UserConcessions } from "../models/user-concessions";

@Component({
    selector: 'app-mismatched-inbox',
    templateUrl: './mismatched-inbox.component.html',
    styleUrls: ['./mismatched-inbox.component.css']
})
export class MismatchedInboxComponent implements OnInit {
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
