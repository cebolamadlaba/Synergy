import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from "rxjs";
import { UserService } from "../services/user.service";
import { User } from "../models/user";

@Component({
  selector: 'app-page-header',
  templateUrl: './page-header.component.html',
  styleUrls: ['./page-header.component.css']
})
export class PageHeaderComponent implements OnInit {
    observableLoggedInUser: Observable<User>;
    user: User;
    errorMessage: String;

    constructor( @Inject(UserService) private userService) { }

    ngOnInit() {
        this.observableLoggedInUser = this.userService.getData();
        this.observableLoggedInUser.subscribe(user => this.user = user,
            error => this.errorMessage = <any>error);
    }
}
