import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from "rxjs";
import { UserService } from "../user/user.service";
import { User } from "../models/user";

@Component({
  selector: 'app-pricing',
  templateUrl: './pricing.component.html',
  styleUrls: ['./pricing.component.css']
})
export class PricingComponent implements OnInit {
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
