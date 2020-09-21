import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { TradeView } from "../models/trade-view";

import { RiskGroup } from "../models/risk-group";
import { TradeConcession } from "../models/trade-concession";
import { Concession } from "../models/concession";
import { ConcessionCondition } from "../models/concession-condition";
import { TradeConcessionDetail } from "../models/trade-concession-detail";
import { TradeFinancial } from "../models/trade-financial";
import { extendConcessionModel } from "../models/extendConcessionModel";


import { SearchConcessionDetail } from '../models/search-concession-detail';
import { ApprovedConcessionDetail } from "../models/approved-concession-detail";

@Injectable()
export class TradeConcessionService {

    constructor(private http: Http) { }

    getTradeViewData(riskGroupNumber, sapbpid): Observable<TradeView> {
        const url = "/api/Trade/TradeView/" + riskGroupNumber + "/" + sapbpid;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getTradeConcessionData(concessionReferenceId): Observable<TradeConcession> {
        const url = "/api/Trade/TradeConcessionData/" + concessionReferenceId;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    postNewTradeData(tradeConcession: TradeConcession): Observable<TradeConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Trade/NewTrade";
        return this.http.post(url, tradeConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    }


    postUpdateTradeData(tradeConcession: TradeConcession): Observable<TradeConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Trade/UpdateTrade";
        return this.http.post(url, tradeConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    postForwardTradePCM(tradeConcession: SearchConcessionDetail): Observable<ApprovedConcessionDetail> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Trade/ForwardTradePCM";
        return this.http.post(url, tradeConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    postExtendConcession(extendConcessionModel: extendConcessionModel): Observable<TradeConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Trade/ExtendConcession";
        return this.http.post(url,extendConcessionModel, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    postChildConcession(bolConcession: TradeConcession, relationshipType: string): Observable<TradeConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Trade/" + relationshipType + "Trade";
        return this.http.post(url, bolConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    postRecallTradeData(tradeConcession: TradeConcession): Observable<TradeConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Trade/UpdateRecalledTrade";
        return this.http.post(url, tradeConcession, options).map(this.extractData).catch(this.handleErrorObservable);
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

