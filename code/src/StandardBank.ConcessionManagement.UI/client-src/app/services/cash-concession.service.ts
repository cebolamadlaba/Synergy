import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { CashView } from "../models/cash-view";
import { RiskGroup } from "../models/risk-group";
import { CashConcession } from "../models/cash-concession";
import { Concession } from "../models/concession";
import { ConcessionCondition } from "../models/concession-condition";
import { CashConcessionDetail } from "../models/cash-concession-detail";
import { CashFinancial } from "../models/cash-financial";

import { SearchConcessionDetail } from '../models/search-concession-detail';
import { ApprovedConcessionDetail } from "../models/approved-concession-detail";

import { ChannelType } from "../models/channel-type";
import { TableNumber } from "../models/table-number";

@Injectable()
export class CashConcessionService {

    constructor(private http: Http) { }

    getCashViewData(riskGroupNumber, sapbpid): Observable<CashView> {
        const url = "/api/Cash/CashView/" + riskGroupNumber + "/" + sapbpid;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getCashConcessionData(concessionReferenceId): Observable<CashConcession> {
        const url = "/api/Cash/CashConcessionData/" + concessionReferenceId;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    postNewCashData(cashConcession: CashConcession): Observable<CashConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Cash/NewCash";
        return this.http.post(url, cashConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    postUpdateCashData(cashConcession: CashConcession): Observable<CashConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Cash/UpdateCash";
        return this.http.post(url, cashConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    postForwardCashPCM(cashConcession: SearchConcessionDetail): Observable<ApprovedConcessionDetail> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Cash/ForwardCashPCM";
        return this.http.post(url, cashConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    postExtendConcession(concessionReferenceId): Observable<CashConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Cash/ExtendConcession/" + concessionReferenceId;
        return this.http.post(url, concessionReferenceId, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    postChildConcession(cashConcession: CashConcession, relationshipType: string): Observable<CashConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Cash/" + relationshipType + "Cash";
        return this.http.post(url, cashConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    postRecallCashData(cashConcession: CashConcession): Observable<CashConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Cash/UpdateRecalledCash";
        return this.http.post(url, cashConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    getlatestCrsOrMrs(riskGroupNumber): Observable<number> {
        const url = "/api/Cash/LatestCrsOrMrs/" + riskGroupNumber;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getCashFinancial(riskGroupNumber): Observable<CashFinancial> {
        const url = "/api/Cash/CashFinancial/" + riskGroupNumber;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }


    createupdateTableNumber(tablenumber: TableNumber): Observable<TableNumber> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Cash/CreateupdateTableNumber";
        return this.http.post(url, tablenumber, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    createChannelType(channeltype: ChannelType): Observable<ChannelType> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Cash/CreateChannelType";
        return this.http.post(url, channeltype, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    private extractData(response: Response) {
        let body = response.json();
        return body;
    }

    private handleErrorObservable(error: Response | any) {

        if (error._body) {

            console.log(error._body);
            console.error(error._body);
            return Observable.throw(error._body || error);
        }
        else {

            console.error(error.message || error);
            return Observable.throw(error.message || error);
        }
    }
}

@Injectable()
export class MockCashConcessionService extends CashConcessionService {
    cashViewModel = new CashView();
    cashConcessionModel = new CashConcession();
    cashFinancialModel = new CashFinancial();

    getCashViewData(riskGroupNumber, sapbpid): Observable<CashView> {
        this.cashViewModel.riskGroup = new RiskGroup();
        this.cashViewModel.cashConcessions = [new CashConcession()];
        return Observable.of(this.cashViewModel);
    }

    getCashConcessionData(concessionReferenceId): Observable<CashConcession> {
        this.cashConcessionModel.concession = new Concession();
        this.cashConcessionModel.concessionConditions = [new ConcessionCondition()];
        this.cashConcessionModel.cashConcessionDetails = [new CashConcessionDetail()];
        return Observable.of(this.cashConcessionModel);
    }

    postNewCashData(cashConcession: CashConcession): Observable<CashConcession> {
        this.cashConcessionModel.concession = new Concession();
        this.cashConcessionModel.concessionConditions = [new ConcessionCondition()];
        this.cashConcessionModel.cashConcessionDetails = [new CashConcessionDetail()];
        return Observable.of(this.cashConcessionModel);
    }

    postUpdateCashData(cashConcession: CashConcession): Observable<CashConcession> {
        this.cashConcessionModel.concession = new Concession();
        this.cashConcessionModel.concessionConditions = [new ConcessionCondition()];
        this.cashConcessionModel.cashConcessionDetails = [new CashConcessionDetail()];
        return Observable.of(this.cashConcessionModel);
    }

    postExtendConcession(concessionReferenceId): Observable<CashConcession> {
        this.cashConcessionModel.concession = new Concession();
        this.cashConcessionModel.concessionConditions = [new ConcessionCondition()];
        this.cashConcessionModel.cashConcessionDetails = [new CashConcessionDetail()];
        return Observable.of(this.cashConcessionModel);
    }

    postChildConcession(cashConcession: CashConcession, relationshipType: string): Observable<CashConcession> {
        this.cashConcessionModel.concession = new Concession();
        this.cashConcessionModel.concessionConditions = [new ConcessionCondition()];
        this.cashConcessionModel.cashConcessionDetails = [new CashConcessionDetail()];
        return Observable.of(this.cashConcessionModel);
    }

    postRecallCashData(cashConcession: CashConcession): Observable<CashConcession> {
        this.cashConcessionModel.concession = new Concession();
        this.cashConcessionModel.concessionConditions = [new ConcessionCondition()];
        this.cashConcessionModel.cashConcessionDetails = [new CashConcessionDetail()];
        return Observable.of(this.cashConcessionModel);
    }

    getlatestCrsOrMrs(riskGroupNumber): Observable<number> {
        return Observable.of(100);
    }

    getCashFinancial(riskGroupNumber): Observable<CashFinancial> {
        return Observable.of(this.cashFinancialModel);
    }
}
