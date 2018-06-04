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
    observableMyAccessnUser: Observable<User>;
    user: User;
    usererrorMessage: String;

    constructor( @Inject(UserService) private userService) { }

    ngOnInit() {
        this.observableLoggedInUser = this.userService.getData();
        this.observableLoggedInUser.subscribe(user => this.user = user,
            error => {
                this.usererrorMessage = <any>error;
                console.log(this.usererrorMessage);
            });

        this.enforceMyAcess();
    }
      

    enforceMyAcess() {

        this.observableMyAccessnUser = this.userService.getLoggedInUserMyAccess();

        //this.usererrorMessage = "Error during My Access validation process";

        this.observableMyAccessnUser.subscribe(maresult => {

            try {
         
                if (maresult == null || maresult.validated == false) {

                    console.log(maresult.errorMessage);

                    //if user does not have acces, redirect to login page
                    if (maresult.errorMessage.startsWith("No access granted")) {

                        window.location.href = "http://10952iisprdsdc2.za.sbicdirectory.com/ManageUserAccessAudit.htm";
                    }
                    //Another reason that user does not have access, ie. WS 
                    else {

                        this.usererrorMessage = maresult.errorMessage;
                    }
                    return;
                }
                else {
                    console.log("User OK: " + maresult.aNumber);

                }
            } catch (e) {
                this.usererrorMessage = e;
            }      
            
        }, error => {           
            this.usererrorMessage = <any>error;
        });
    }
}
