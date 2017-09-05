import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { AdminService } from '../../services/admin.service';
import { Region } from '../../models/region';
import { Centre } from '../../models/centre';
import { Role } from '../../models/role';
import {Usermodel } from '../../models/usermodel';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
    Regions: Region[];
    Centres:Centre[];
    Roles: Role[];
    user: Usermodel;
    users: Usermodel[];

    constructor(private location: Location,private adminService: AdminService) { }

    ngOnInit() {
        this.adminService.GetUserLookupData().subscribe(result => {
            this.Regions = result.regions as Region[];
            this.Centres = result.centres as Centre[];
            this.Roles = result.roles as Role[];
        });
        this.adminService.GetUsers().subscribe(r => {
            this.users = r as Usermodel[];
        });
        this.user = {} as Usermodel;
  }

  save() {
     this.adminService.CreateUser(this.user)
  }

}
