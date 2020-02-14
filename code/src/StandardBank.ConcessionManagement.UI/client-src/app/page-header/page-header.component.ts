import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from "rxjs";
import { UserService } from "../services/user.service";
import { User } from "../models/user";
import { AccountExecutiveAssistant } from "../models/account-executive-assistant";

import { StaticClass } from '../models/static-class';
import { UserConcessionsService } from "../services/user-concessions.service";


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
    showUatWarning = false;
    currentExecutiveUser = "";

    constructor(@Inject(UserService) private userService, @Inject(UserConcessionsService) private userConcessionsService, ) {       
    }  

    ngOnInit() {        

        this.observableLoggedInUser = this.userService.getData();
        this.observableLoggedInUser.subscribe(user => {

            //set AE to first value
            this.user = user;            

            if (user.isAdminAssistant) {

                if (user.accountExecutiveUserId > 0) {

                    var ae = this.user.accountExecutives.find(r => r.accountExecutiveUserId == user.accountExecutiveUserId);
                    if (ae) {
                        var aename = ae.accountExecutiveDisplayName;
                        StaticClass.SetUser(aename, ae.isActive);
                        this.currentExecutiveUser = StaticClass.GetUser();
                    }
                }
                else {

                    if (this.user.accountExecutives.length > 0) {

                        var ae = this.user.accountExecutives[0];
                        var aename = ae.accountExecutiveDisplayName;
                        StaticClass.SetUser(aename, ae.isActive);
                        this.currentExecutiveUser = StaticClass.GetUser();
                    }
                    else {

                        this.currentExecutiveUser = "Manager to link";
                    }
                }
            }


        },
            error => {
                this.usererrorMessage = <any>error;
                console.log(this.usererrorMessage);

            });

        this.enforceMyAcess();
        this.uatWarning();        
    }

    aESelected(ae: AccountExecutiveAssistant) {

        console.log(ae.accountExecutiveDisplayName);

        var aename = ae.accountExecutiveDisplayName;

        StaticClass.SetUser(aename, ae.accountExecutiveUserId);

        this.currentExecutiveUser = StaticClass.GetUser();
        this.user.accountExecutiveUserId = StaticClass.GetUserID();

        this.userConcessionsService.getCacheAEUser(ae.accountExecutiveUserId).subscribe(entity => {
            console.log("CacheAEUser");
            var results = entity;

            location.reload();

        }, error => {
            console.log("error:" + <any>error);

        });
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

    uatWarning() {
        this.userService.getUATWarning().subscribe(result => {
            this.showUatWarning = result;
        }, error => {
            this.usererrorMessage = <any>error;
        });
    }
}
