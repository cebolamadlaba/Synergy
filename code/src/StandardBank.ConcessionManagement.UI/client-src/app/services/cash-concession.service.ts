import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { CashView } from "../models/cash-view";
import { RiskGroup } from "../models/risk-group";
import { SourceSystemProduct } from "../models/source-system-product";
import { CashConcession } from "../models/cash-concession";

@Injectable()
export class CashConcessionService {

    constructor(private http: Http) { }

    getCashViewData(riskGroupNumber): Observable<CashView> {
        const url = "/api/Cash/CashView/" + riskGroupNumber;
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
export class MockCashConcessionService extends CashConcessionService {
    model = new CashView();

    getCashViewData(riskGroupNumber): Observable<CashView> {
        this.model.riskGroup = new RiskGroup();
        this.model.sourceSystemProducts = [new SourceSystemProduct()];
        this.model.cashConcessions = [new CashConcession()];
        return Observable.of(this.model);
    }
}
