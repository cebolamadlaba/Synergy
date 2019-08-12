import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { RiskGroup } from "../models/risk-group";
import { Concession } from "../models/concession";
import { TransactionalView } from "../models/transactional-view";
import { TransactionalConcessionService } from "../services/transactional-concession.service";
import { TransactionalConcession } from "../models/transactional-concession";
import { UserService } from "../services/user.service";

@Component({
    selector: 'app-pricing-transactional',
    templateUrl: './pricing-transactional.component.html',
    styleUrls: ['./pricing-transactional.component.css']
})
export class PricingTransactionalComponent implements OnInit, OnDestroy {
    showHide = false;
    riskGroupNumber: number;
    sapbpid: number;
    entityName: string;
    entityNumber: string;
    private sub: any;
    observableTransactionalView: Observable<TransactionalView>;
    transactionalView: TransactionalView = new TransactionalView();
    errorMessage: String;
    pageLoaded = false;
    canRequest = false;
    isLoading = true;

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private location: Location,
        @Inject(TransactionalConcessionService) private transactionalConcessionService,
        private userService: UserService) {
        this.transactionalView.riskGroup = new RiskGroup();
        this.transactionalView.transactionalConcessions = [new TransactionalConcession()];
        this.transactionalView.transactionalConcessions[0].concession = new Concession();
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
            this.sapbpid = +params['sapbpid'];

            if (this.riskGroupNumber || this.sapbpid) {
                this.observableTransactionalView = this.transactionalConcessionService.getTransactionalViewData(this.riskGroupNumber, this.sapbpid);
                this.observableTransactionalView.subscribe(transactionalView => {
                    this.transactionalView = transactionalView;

                    if (this.riskGroupNumber || this.riskGroupNumber > 0) {
                        this.entityName = this.transactionalView.riskGroup.name;
                        this.entityNumber = this.transactionalView.riskGroup.number.toString();
                    }
                    else {
                        this.entityName = this.transactionalView.legalEntity.customerName;
                        this.entityNumber = this.transactionalView.legalEntity.customerNumber;
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
        //this.location.back();
        this.router.navigate(['/pricing', this.riskGroupNumber]);
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

}
