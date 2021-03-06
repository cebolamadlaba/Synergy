import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { RiskGroup } from "../models/risk-group";

import { GlmsConcession } from "../models/glms-concession";
import { GlmsView } from "../models/glms-view";
import { GlmsTierDataView } from "../models/glms-tier-data-view";

import { GlmsConcessionService } from "../services/glms-concession.service"

import { Concession } from "../models/concession";
import { UserService } from "../services/user.service";
import { BaseComponentService } from '../services/base-component.service';
import { Http } from '@angular/http';

@Component({
  selector: 'app-pricing-glms',
  templateUrl: './pricing-glms.component.html',
  styleUrls: ['./pricing-glms.component.css']
})
export class PricingGlmsComponent extends BaseComponentService implements OnInit {

    riskGroupNumber: number;
    sapbpid: number;
    private sub: any;

    entityName: string;
    entityNumber: string;

    observableGlmsView: Observable<GlmsView>;
    glmsView: GlmsView = new GlmsView();
    errorMessage: String;
    showHide = false;
    pageLoaded = false;
    isLoading = true;
    canRequest = false;

    glmsTierDataViewList: GlmsTierDataView[];

    constructor(
        public router: Router,
        private route: ActivatedRoute,
        private location: Location,
        @Inject(GlmsConcessionService) private glmsConcessionService, public userService: UserService
    ) {
        super(router, userService);
        this.glmsView.riskGroup = new RiskGroup();
        this.glmsView.glmsConcessions = [new GlmsConcession()];
        this.glmsView.glmsConcessions[0].concession = new Concession();
    }


    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
            this.sapbpid = +params['sapbpid'];

            if (this.riskGroupNumber || this.sapbpid) {

                this.observableGlmsView = this.glmsConcessionService.getGlmsViewData(this.riskGroupNumber, this.sapbpid);
                this.observableGlmsView.subscribe(glmsView => {
                    this.glmsView = glmsView;

                    if (this.riskGroupNumber || this.riskGroupNumber > 0) {
                        this.entityName = this.glmsView.riskGroup.name;
                        this.entityNumber = this.glmsView.riskGroup.number.toString();
                    }
                    else {
                        this.entityName = this.glmsView.legalEntity.customerName;
                        this.entityNumber = this.glmsView.legalEntity.customerNumber;
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

    openManageTier(x, tier) {
        this.glmsTierDataViewList = tier.glmsTierDataView;
    }

    goBack() {
        this.router.navigate(['/pricing', { riskGroupNumber: this.riskGroupNumber, sapbpid: this.sapbpid }]);
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}
