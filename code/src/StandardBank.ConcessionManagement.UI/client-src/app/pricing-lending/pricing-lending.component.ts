import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-pricing-lending',
    templateUrl: './pricing-lending.component.html',
    styleUrls: ['./pricing-lending.component.css']
})
export class PricingLendingComponent implements OnInit, OnDestroy {
    riskGroupNumber: number;
    private sub: any;

    constructor(private route: ActivatedRoute) { }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.riskGroupNumber = +params['riskGroupNumber'];
        });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}
