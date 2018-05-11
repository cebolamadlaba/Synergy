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
            error => {
                this.errorMessage = <any>error;
                console.log(this.errorMessage);
            });


        this.getLoggedInUserMyAccess();
    }


    getLoggedInUserMyAccess() {

        this.observableLoggedInUser = this.userService.getLoggedInUserMyAccess();
        this.observableLoggedInUser.subscribe(result => {

            if (result == null || result.validated == false) {              

                console.log(result.errorMessage);

                window.location.href = "http://10952iisprdsdc2.za.sbicdirectory.com/ManageUserAccessAudit.htm";
                return;
            }           
            
        }, error => {           
            this.errorMessage = <any>error;
        });
    }
}
