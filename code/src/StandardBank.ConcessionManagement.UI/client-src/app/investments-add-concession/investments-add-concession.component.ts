import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-investments-add-concession',
  templateUrl: './investments-add-concession.component.html',
  styleUrls: ['./investments-add-concession.component.css']
})
export class InvestmentsAddConcessionComponent implements OnInit {

    constructor(private location: Location) { }

    ngOnInit() {
    }

    goBack() {
        this.location.back();
    }

}
