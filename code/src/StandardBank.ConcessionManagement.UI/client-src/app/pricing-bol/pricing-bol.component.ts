import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { RiskGroup } from "../models/risk-group";

import { BolConcession } from "../models/bol-concession";
import { BolConcessionService } from "../services/bol-concession.service";
import { BolView } from "../models/bol-view";
import { Concession } from "../models/concession";

import { UserService } from "../services/user.service";

@Component({
    selector: 'app-pricing-bol',
    templateUrl: './pricing-bol.component.html',
    styleUrls: ['./pricing-bol.component.css']
})
export class PricingBolComponent implements OnInit, OnDestroy {
    riskGroupNumber: number;
    private sub: any;
  
    observableBolView: Observable<BolView>;
    bolView: BolView = new BolView();
    errorMessage: String;
    showHide = false;
    pageLoaded = false;
    isLoading = true;
    canRequest = false;
 
    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private location: Location,
        @Inject(BolConcessionService) private bolConcessionService, private userService: UserService) {
        this.bolView.riskGroup = new RiskGroup();
        this.bolView.bolConcessions = [new BolConcession()];
        this.bolView.bolConcessions[0].concession = new Concession();
    }
  
    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];

            if (this.riskGroupNumber) {
                this.observableBolView = this.bolConcessionService.getBolViewData(this.riskGroupNumber);
                this.observableBolView.subscribe(bolView => {
                  

                    this.bolView = bolView;
                    this.pageLoaded = true;
                    this.isLoading = false;
                }, error => {
                    this.errorMessage = <any>error;
                    this.isLoading = false;
                });
            }
        });

        this.userService.getData().subscribe(user => {
            this.canRequest = user.canRequest;
        });

    }


    goBack() {

        this.router.navigate(['/pricing', this.riskGroupNumber]);
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

}
