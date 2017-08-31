<<<<<<< Updated upstream
import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { RiskGroup } from "../models/risk-group";
=======
﻿import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
>>>>>>> Stashed changes

@Component({
    selector: 'app-pricing-cashman',
    templateUrl: './pricing-cashman.component.html',
    styleUrls: ['./pricing-cashman.component.css']
})
export class PricingCashmanComponent implements OnInit, OnDestroy {
    riskGroupNumber: number;
    private sub: any;

<<<<<<< Updated upstream
    constructor(
        private router: Router,
        private route: ActivatedRoute) { }
=======
    constructor(private location: Location) { }
>>>>>>> Stashed changes

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
        });
    }

<<<<<<< Updated upstream
    goBack() {
        //this.location.back();
        this.router.navigate(['/pricing', this.riskGroupNumber]);
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
=======
  goBack() {
      this.location.back();
  }

>>>>>>> Stashed changes
}
