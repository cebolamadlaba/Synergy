import { Component, OnInit, Inject, OnDestroy, Injector } from '@angular/core';
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
import { BaseComponentService } from '../services/base-component.service';
import { Http } from '@angular/http';

@Component({
    selector: 'app-pricing-bol',
    templateUrl: './pricing-bol.component.html',
    styleUrls: ['./pricing-bol.component.css']
})
export class PricingBolComponent extends BaseComponentService implements OnInit, OnDestroy {
    riskGroupNumber: number;
    sapbpid: number;
    private sub: any;

    observableBolView: Observable<BolView>;
    bolView: BolView = new BolView();
    errorMessage: String;
    showHide = false;
    pageLoaded = false;
    isLoading = true;
    canRequest = false;

    entityName: string;
    entityNumber: string;

    constructor(
        injector: Injector,
        private route: ActivatedRoute,
        private location: Location,
        public router: Router,
        @Inject(BolConcessionService) private bolConcessionService, public userService: UserService) {
        super(router, userService);
        this.bolView.riskGroup = new RiskGroup();
        this.bolView.bolConcessions = [new BolConcession()];
        this.bolView.bolConcessions[0].concession = new Concession();
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
            this.sapbpid = +params['sapbpid'];

            if (this.riskGroupNumber || this.sapbpid) {
                this.observableBolView = this.bolConcessionService.getBolViewData(this.riskGroupNumber, this.sapbpid);
                this.observableBolView.subscribe(bolView => {


                    this.bolView = bolView;

                    if (this.riskGroupNumber || this.riskGroupNumber > 0) {
                        this.entityName = this.bolView.riskGroup.name;
                        this.entityNumber = this.bolView.riskGroup.number.toString();
                    }
                    else {
                        this.entityName = this.bolView.legalEntity.customerName;
                        this.entityNumber = this.bolView.legalEntity.customerNumber;
                    }

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
        this.router.navigate(['/pricing', { riskGroupNumber: this.riskGroupNumber, sapbpid: this.sapbpid }]);
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

}
