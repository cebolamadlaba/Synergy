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


import { SearchConcessionDetail } from '../models/search-concession-detail';
import { ApprovedConcessionDetail } from "../models/approved-concession-detail";

@Injectable()
export class TradeConcessionService {

    constructor(private http: Http) { }

    getTradeViewData(riskGroupNumber): Observable<TradeView> {
        const url = "/api/Trade/TradeView/" + riskGroupNumber;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    //getBolConcessionData(concessionReferenceId): Observable<BolConcession> {
    //    const url = "/api/Bol/BolConcessionData/" + concessionReferenceId;
    //    return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    //}

    postNewTradeData(tradeConcession: TradeConcession): Observable<TradeConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Trade/NewTrade";
        return this.http.post(url, tradeConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    //postUpdateBolData(bolConcession: BolConcession): Observable<BolConcession> {
    //    let headers = new Headers({ 'Content-Type': 'application/json' });
    //    let options = new RequestOptions({ headers: headers });
    //    const url = "/api/Bol/UpdateBol";
    //    return this.http.post(url, bolConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    //}

    //postForwardBolPCM(bolConcession: SearchConcessionDetail): Observable<ApprovedConcessionDetail> {
    //    let headers = new Headers({ 'Content-Type': 'application/json' });
    //    let options = new RequestOptions({ headers: headers });
    //    const url = "/api/Bol/ForwardBolPCM";
    //    return this.http.post(url, bolConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    //}

    //postExtendConcession(concessionReferenceId): Observable<BolConcession> {
    //    let headers = new Headers({ 'Content-Type': 'application/json' });
    //    let options = new RequestOptions({ headers: headers });
    //    const url = "/api/Bol/ExtendConcession/" + concessionReferenceId;
    //    return this.http.post(url, concessionReferenceId, options).map(this.extractData).catch(this.handleErrorObservable);
    //}

    //postChildConcession(bolConcession: BolConcession, relationshipType: string): Observable<BolConcession> {
    //    let headers = new Headers({ 'Content-Type': 'application/json' });
    //    let options = new RequestOptions({ headers: headers });
    //    const url = "/api/Bol/" + relationshipType + "Bol";
    //    return this.http.post(url, bolConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    //}

    //postRecallBolData(bolConcession: BolConcession): Observable<BolConcession> {
    //    let headers = new Headers({ 'Content-Type': 'application/json' });
    //    let options = new RequestOptions({ headers: headers });
    //    const url = "/api/Bol/UpdateRecalledBol";
    //    return this.http.post(url, bolConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    //}

    //createupdateBOLChargeCode(bolChargecode: BolChargeCode): Observable<BolChargeCode> {
    //    let headers = new Headers({ 'Content-Type': 'application/json' });
    //    let options = new RequestOptions({ headers: headers });
    //    const url = "/api/Bol/createupdateBOLChargeCode";
    //    return this.http.post(url, bolChargecode, options).map(this.extractData).catch(this.handleErrorObservable);
    //}
    //postNewBolChargeCodeType(bolChargecodeType: BolChargeCodeType): Observable<BolChargeCodeType> {
    //    let headers = new Headers({ 'Content-Type': 'application/json' });
    //    let options = new RequestOptions({ headers: headers });
    //    const url = "/api/Bol/NewBolChargeCodeType";
    //    return this.http.post(url, bolChargecodeType, options).map(this.extractData).catch(this.handleErrorObservable);
    //}

    private extractData(response: Response) {
        let body = response.json();
        return body;
    }

    private handleErrorObservable(error: Response | any) {
             
        
        console.error(error.message || error);
        return Observable.throw(error.message || error);
    }
}

