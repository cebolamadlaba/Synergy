import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AdminService } from '../../../services/admin.service';
import { Region } from '../../../models/region';
import { Centre } from '../../../models/centre';
import { Role } from '../../../models/role';
import { User } from "../../../models/user";
import { Location } from '@angular/common';
import { RoleSubRole } from "../../../models/RoleSubRole";

import { SubRoleEnum } from "../../../models/subrole-enum";

@Component({
    selector: 'app-edit-user',
    templateUrl: './edit-user.component.html',
    styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {
    Regions: Region[];
    Centres: Centre[];
    Roles: Role[];
    RoleSubRole: RoleSubRole[];
    user = {} as User;
    id: number;
    success: boolean;
    error: boolean;

    constructor( @Inject(AdminService)
    private adminService,
        private route: ActivatedRoute,
        private location: Location,
    ) { }

    ngOnInit() {
        this.adminService.GetUserLookupData().subscribe(result => {
            this.Centres = result.centres as Centre[];
            this.Roles = result.roles as Role[];
            this.RoleSubRole = result.roleSubRole as RoleSubRole[];
        });
        this.route.params.subscribe(params => {
            this.id = +params['id'];
        });
        this.adminService.GetUser(this.id).subscribe(r => {
            this.user = r as User;
        });
    }

    save() {
        if (this.user.subRoleId == SubRoleEnum.NoSubrole) {
            this.user.subRoleId = null;
        }
        this.adminService.UpdateUser(this.user, this.id).subscribe(r => {
            this.adminService.GetUser(this.id).subscribe(res => {
                this.user = res as User;

                //location.reload();
            });

            this.success = true;
        }, err => {
            this.error = true;
            console.log(err);
        });
    }

    goBack() {
        this.location.back();
    }

}
