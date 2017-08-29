import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-bol-add-concession',
  templateUrl: './bol-add-concession.component.html',
  styleUrls: ['./bol-add-concession.component.css']
})
export class BolAddConcessionComponent implements OnInit {

    constructor(private location: Location) { }

  ngOnInit() {
  }

  goBack() {
      this.location.back();
  }

}
