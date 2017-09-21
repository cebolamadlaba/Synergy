import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from "rxjs";
import { UserConcessionsService } from "../services/user-concessions.service";
import { UserConcessions } from "../models/user-concessions";

@Component({
    selector: 'app-inbox-header',
    templateUrl: './inbox-header.component.html',
    styleUrls: ['./inbox-header.component.css']
})
export class InboxHeaderComponent implements OnInit {
    observableUserConcessions: Observable<UserConcessions>;
    userConcessions: UserConcessions;
    errorMessage: String;
    isLoading = true;

    constructor( @Inject(UserConcessionsService) private userConcessionsService) { }

    ngOnInit() {
        this.observableUserConcessions = this.userConcessionsService.getData();
        this.observableUserConcessions.subscribe(userConcessions => {
            this.userConcessions = userConcessions;
            this.isLoading = false;
        },
            error => this.errorMessage = <any>error);
    }
}
