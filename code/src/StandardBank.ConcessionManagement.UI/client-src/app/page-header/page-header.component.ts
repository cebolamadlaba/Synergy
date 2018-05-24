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

                    if (maresult.errorMessage == "Not_Valid") {

                        window.location.href = "http://10952iisprdsdc2.za.sbicdirectory.com/ManageUserAccessAudit.htm";
                    }
                    else {

                        this.usererrorMessage = "Error during My Access validation process: " + maresult.errorMessage;
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
