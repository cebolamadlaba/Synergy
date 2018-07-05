import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from "rxjs";
import { UserService } from "../services/user.service";
import { User } from "../models/user";
import { AccountExecutiveAssistant } from "../models/account-executive-assistant";

import { StaticClass } from '../models/static-class';

@Component({
  selector: 'app-page-header',
  templateUrl: './page-header.component.html',
  styleUrls: ['./page-header.component.css']
})
export class PageHeaderComponent implements OnInit {
    observableLoggedInUser: Observable<User>;
    observableMyAccessnUser: Observable<User>;
    user: User;
    usererrorMessage: String 
    currentExecutiveUser = "";

    constructor( @Inject(UserService) private userService) { }  

    ngOnInit() {      

        //this.currentExecutiveUser = StaticClass.GetUser();

        this.observableLoggedInUser = this.userService.getData();
        this.observableLoggedInUser.subscribe(user => {

            //set AE to first value
            this.user = user;
            var ae = this.user.accountExecutives[0];
            var aename = ae.accountExecutiveDisplayName;
            StaticClass.SetUser(aename, ae.isActive);
            this.currentExecutiveUser = StaticClass.GetUser();

        },
            error => {
                this.usererrorMessage = <any>error;
                console.log(this.usererrorMessage);

            });

        this.enforceMyAcess();
    }

    aESelected(ae: AccountExecutiveAssistant) {

        console.log(ae.accountExecutiveDisplayName);

        var aename = ae.accountExecutiveDisplayName;
        StaticClass.SetUser(aename, ae.accountExecutiveUserId);

        this.currentExecutiveUser = StaticClass.GetUser();
        this.user.accountExecutiveUserId = StaticClass.GetUserID();
    }      

    enforceMyAcess() {

        this.observableMyAccessnUser = this.userService.getLoggedInUserMyAccess();
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
