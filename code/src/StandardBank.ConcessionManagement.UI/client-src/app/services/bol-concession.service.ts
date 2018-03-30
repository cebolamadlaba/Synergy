import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { CashView } from "../models/cash-view";
import { RiskGroup } from "../models/risk-group";
import { BolConcession } from "../models/bol-concession";
import { Concession } from "../models/concession";
import { ConcessionCondition } from "../models/concession-condition";
import { BolConcessionDetail } from "../models/bol-concession-detail";
import { BolFinancial } from "../models/bol-financial";

import { SearchConcessionDetail } from '../models/search-concession-detail';
import { ApprovedConcessionDetail } from "../models/approved-concession-detail";

@Injectable()
export class BolConcessionService {

    constructor(private http: Http) { }

    getBolViewData(riskGroupNumber): Observable<CashView> {
        const url = "/api/Bol/BolView/" + riskGroupNumber;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getBolConcessionData(concessionReferenceId): Observable<BolConcession> {
        const url = "/api/Bol/BolConcessionData/" + concessionReferenceId;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    postNewBolData(bolConcession: BolConcession): Observable<BolConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Bol/NewBol";
        return this.http.post(url, bolConcession, options).map(this.extractData).catch(this.handleErrorObservable);
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
    //    const url = "/api/Bol/ForwardCashPCM";
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
    //    const url = "/api/Bol/" + relationshipType + "Cash";
    //    return this.http.post(url, bolConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    //}

    //postRecallBolData(bolConcession: BolConcession): Observable<BolConcession> {
    //    let headers = new Headers({ 'Content-Type': 'application/json' });
    //    let options = new RequestOptions({ headers: headers });
    //    const url = "/api/Bol/UpdateRecalledBol";
    //    return this.http.post(url, bolConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    //}

    //getlatestCrsOrMrs(riskGroupNumber): Observable<number> {
    //    const url = "/api/Bol/LatestCrsOrMrs/" + riskGroupNumber;
    //    return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    //}

    //getBolFinancial(riskGroupNumber): Observable<BolFinancial> {
    //    const url = "/api/Bol/BolFinancial/" + riskGroupNumber;
    //    return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    //}

    private extractData(response: Response) {
        let body = response.json();
        return body;
    }

    private handleErrorObservable(error: Response | any) {

        alert(Response);

        console.error(error.message || error);
        return Observable.throw(error.message || error);
    }
}

