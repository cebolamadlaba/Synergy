import { Component, OnInit } from '@angular/core';
import { Observable } from "rxjs";
import { Location, DatePipe } from '@angular/common';

import { BaseComponentService } from '../services/base-component.service';

@Component({
  selector: 'app-glms-add-concession',
  templateUrl: './glms-add-concession.component.html',
  styleUrls: ['./glms-add-concession.component.css']
})
export class GlmsAddConcessionComponent implements OnInit {


   riskGroupNumber: number;


    constructor(private location: Location) { }

  ngOnInit() {
  }


    goBack() {
        this.location.back();
    }

}
