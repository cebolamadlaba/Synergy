import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { CashView } from "../models/cash-view";
import { RiskGroup } from "../models/risk-group";
import { SourceSystemProduct } from "../models/source-system-product";
import { CashConcession } from "../models/cash-concession";
import { Concession } from "../models/concession";
import { ConcessionCondition } from "../models/concession-condition";
import { CashConcessionDetail } from "../models/cash-concession-detail";

@Injectable()
export class CashConcessionService {

    constructor(private http: Http) { }

    getCashViewData(riskGroupNumber): Observable<CashView> {
        const url = "/api/Cash/CashView/" + riskGroupNumber;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    postNewCashData(cashConcession: CashConcession): Observable<CashConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Cash/NewCash";
        return this.http.post(url, cashConcession, options).map(this.extractData).catch(this.handleErrorObservable);
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
    cashViewModel = new CashView();
    cashConcessionModel = new CashConcession();

    getCashViewData(riskGroupNumber): Observable<CashView> {
        this.cashViewModel.riskGroup = new RiskGroup();
        this.cashViewModel.sourceSystemProducts = [new SourceSystemProduct()];
        this.cashViewModel.cashConcessions = [new CashConcession()];
        return Observable.of(this.cashViewModel);
    }

    postNewCashData(cashConcession: CashConcession): Observable<CashConcession> {
        this.cashConcessionModel.concession = new Concession();
        this.cashConcessionModel.concessionConditions = [new ConcessionCondition()];
        this.cashConcessionModel.cashConcessionDetails = [new CashConcessionDetail()];
        return Observable.of(this.cashConcessionModel);
    }
}
