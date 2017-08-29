import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-mas-add-concession',
  templateUrl: './mas-add-concession.component.html',
  styleUrls: ['./mas-add-concession.component.css']
})
export class MasAddConcessionComponent implements OnInit {

    constructor(private location: Location) { }

  ngOnInit() {
  }

  goBack() {
      this.location.back();
  }

}
