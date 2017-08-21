import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
    selector: 'app-transactional-add-concession',
    templateUrl: './transactional-add-concession.component.html',
    styleUrls: ['./transactional-add-concession.component.css']
})
export class TransactionalAddConcessionComponent implements OnInit {

    constructor(private location: Location) { }

    ngOnInit() {
    }

    goBack() {
        this.location.back();
    }

}
