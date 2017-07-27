import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { LendingView } from "../models/lending-view";
import { RiskGroup } from "../models/risk-group";
import { SourceSystemConcession } from "../models/source-system-concession";
import { SourceSystemProduct } from "../models/source-system-product";

@Injectable()
export class LendingViewService {

    constructor(private http: Http) { }

    getData(riskGroupNumber): Observable<LendingView> {
        const url = "/api/Lending/LendingView/" + riskGroupNumber;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    private extractData(response: Response) {
        let body = response.json();
        return body;
    }

    private handleErrorObservable(error: Response | any) {
        console.error(error.message || error);
        return Observable.throw(error.message || error);
    }

}

@Injectable()
export class MockLendingViewService {
    model = new LendingView();

    constructor() { }
    getData(riskGroupNumber): Observable<LendingView> {
        this.model.riskGroup = new RiskGroup();
        this.model.totalExposure = 1;
        this.model.weightedAverageMap = 1;
        this.model.weightedCrsMrs = 1;
        this.model.sourceSystemProducts = [new SourceSystemProduct()];
        this.model.sourceSystemConcessions = [new SourceSystemConcession()];
        return Observable.of(this.model);
    }
}
