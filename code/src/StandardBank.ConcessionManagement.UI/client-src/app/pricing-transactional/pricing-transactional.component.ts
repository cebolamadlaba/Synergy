import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { RiskGroup } from "../models/risk-group";
import { SourceSystemProduct } from "../models/source-system-product";
import { Concession } from "../models/concession";
import { TransactionalView } from "../models/transactional-view";
import { TransactionalConcessionService } from "../services/transactional-concession.service";
import { TransactionalConcession } from "../models/transactional-concession";

@Component({
    selector: 'app-pricing-transactional',
    templateUrl: './pricing-transactional.component.html',
    styleUrls: ['./pricing-transactional.component.css']
})
export class PricingTransactionalComponent implements OnInit, OnDestroy {
    showHide = true;
    riskGroupNumber: number;
    private sub: any;
    observableTransactionalView: Observable<TransactionalView>;
    transactionalView: TransactionalView = new TransactionalView();
    errorMessage: String;
    pageLoaded = false;
    
    constructor(
        private route: ActivatedRoute,
        private location: Location,
        @Inject(TransactionalConcessionService) private transactionalConcessionService) {
        this.transactionalView.riskGroup = new RiskGroup();
        this.transactionalView.sourceSystemProducts = [new SourceSystemProduct()];
        this.transactionalView.transactionalConcessions = [new TransactionalConcession()];
        this.transactionalView.transactionalConcessions[0].concession = new Concession();
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];

            if (this.riskGroupNumber) {
                this.observableTransactionalView = this.transactionalConcessionService.getTransactionalViewData(this.riskGroupNumber);
                this.observableTransactionalView.subscribe(transactionalView => {
                    this.transactionalView = transactionalView;
                    this.pageLoaded = true;
                }, error => this.errorMessage = <any>error);
            }
        });
    }

    goBack() {
        this.location.back();
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

}
