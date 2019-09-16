import { Component, OnInit } from '@angular/core';
import { Observable } from "rxjs";

import { BaseComponentService } from '../services/base-component.service';

@Component({
  selector: 'app-glms-add-concession',
  templateUrl: './glms-add-concession.component.html',
  styleUrls: ['./glms-add-concession.component.css']
})
export class GlmsAddConcessionComponent implements OnInit {


   riskGroupNumber: number;


  constructor() { }

  ngOnInit() {
  }


    goBack() {
        this.router.navigate(['/pricing', this.riskGroupNumber]);
    }

}
