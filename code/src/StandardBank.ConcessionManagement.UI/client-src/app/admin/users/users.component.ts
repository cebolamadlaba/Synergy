import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { AdminService } from '../../services/admin.service';
import { Region } from '../../models/region';
import { Centre } from '../../models/centre';
import { Role } from '../../models/role';
import { User } from "../../models/user";
import { RoleSubRole } from "../../models/RoleSubRole";
import { SubRoleEnum } from "../../models/subrole-enum";

@Component({
    selector: 'app-users',
    templateUrl: './users.component.html',
    styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
    Centres: Centre[];
    Roles: Role[];
    user: User;
    RoleSubRole: RoleSubRole[];
    users: User[];

    constructor(private location: Location, private adminService: AdminService) { }

    ngOnInit() {
        this.adminService.GetUserLookupData().subscribe(result => {
            this.Centres = result.centres as Centre[];
            this.Roles = result.roles as Role[];
            this.RoleSubRole = result.roleSubRole as RoleSubRole[];
        });
        this.adminService.GetUsers().subscribe(r => {
            this.users = r as User[];
        });
        this.user = {} as User;
    }

    save() {

        if (this.user.subRoleId == SubRoleEnum.NoSubrole) {
            this.user.subRoleId = null;
        }

        this.adminService.CreateUser(this.user).subscribe(res => location.reload());
    }
    deleteUser(anumber) {
        this.adminService.DeleteUser(anumber).subscribe(r => location.reload());
    }
    edit(i) {
        this.user = this.users[i];
        console.log(this.user);
    }

    goBack() {
        this.location.back();
    }

}
