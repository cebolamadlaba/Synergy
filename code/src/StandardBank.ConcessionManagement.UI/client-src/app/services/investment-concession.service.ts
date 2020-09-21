import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { InvestmentView } from "../models/investment-view";
import { extendConcessionModel } from "../models/extendConcessionModel";

import { RiskGroup } from "../models/risk-group";
import { InvestmentConcession } from "../models/investment-concession";
import { Concession } from "../models/concession";
import { ConcessionCondition } from "../models/concession-condition";
import { InvestmentConcessionDetail } from "../models/investment-concession-detail";
import { InvestmentFinancial } from "../models/investment-financial";


import { SearchConcessionDetail } from '../models/search-concession-detail';
import { ApprovedConcessionDetail } from "../models/approved-concession-detail";

@Injectable()
export class InvestmentConcessionService {

    constructor(private http: Http) { }

    getInvestmentViewData(riskGroupNumber, sapbpid): Observable<InvestmentView> {
        const url = "/api/Investment/InvestmentView/" + riskGroupNumber + "/" + sapbpid;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getInvestmentConcessionData(concessionReferenceId): Observable<InvestmentConcession> {
        const url = "/api/Investment/InvestmentConcessionData/" + concessionReferenceId;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    postNewInvestmentData(investmentConcession: InvestmentConcession): Observable<InvestmentConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Investment/NewInvestment";
        return this.http.post(url, investmentConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    postUpdateInvestmentData(investmentConcession: InvestmentConcession): Observable<InvestmentConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Investment/UpdateInvestment";
        return this.http.post(url, investmentConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    postForwardInvestmentPCM(investmentConcession: SearchConcessionDetail): Observable<ApprovedConcessionDetail> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Investment/ForwardInvestmentPCM";
        return this.http.post(url, investmentConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    postExtendConcession(extendConcessionModel: extendConcessionModel): Observable<InvestmentConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Investment/ExtendConcession/";
        return this.http.post(url, extendConcessionModel, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    postChildConcession(investmentConcession: InvestmentConcession, relationshipType: string): Observable<InvestmentConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Investment/" + relationshipType + "Investment";
        return this.http.post(url, investmentConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    postRecallInvestmentData(investmentConcession: InvestmentConcession): Observable<InvestmentConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Investment/UpdateRecalledInvestment";
        return this.http.post(url, investmentConcession, options).map(this.extractData).catch(this.handleErrorObservable);
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

