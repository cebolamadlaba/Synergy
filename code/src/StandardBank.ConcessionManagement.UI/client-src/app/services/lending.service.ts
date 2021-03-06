import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { LendingConcession } from "../models/lending-concession";
import { Concession } from "../models/concession";
import { ConcessionCondition } from "../models/concession-condition";
import { LendingConcessionDetail } from "../models/lending-concession-detail";
import { LendingView } from "../models/lending-view";
import { RiskGroup } from "../models/risk-group";
import { LendingFinancial } from "../models/lending-financial";

import { SearchConcessionDetail } from '../models/search-concession-detail';
import { ApprovedConcessionDetail } from "../models/approved-concession-detail";
import { extendConcessionModel } from "../models/extendConcessionModel";

@Injectable()
export class LendingService {

    constructor(private http: Http) {
    }

    getLendingConcessionData(concessionReferenceId): Observable<LendingConcession> {
        const url = "/api/Lending/LendingConcessionData/" + concessionReferenceId;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getLendingViewData(riskGroupNumber): Observable<LendingView> {
        const url = "/api/Lending/LendingView/" + riskGroupNumber;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getLendingViewDataBySAPBPID(sapbpid): Observable<LendingView> {
        const url = "/api/Lending/LendingViewBySAPBPID/" + sapbpid;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    postNewLendingData(lendingConcession: LendingConcession): Observable<LendingConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Lending/NewLending";
        return this.http.post(url, lendingConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    postUpdateLendingData(lendingConcession: LendingConcession): Observable<LendingConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Lending/UpdateLending";
        return this.http.post(url, lendingConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    postForwardLendingPCM(lendingConcession: SearchConcessionDetail): Observable<ApprovedConcessionDetail> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Lending/ForwardLendingPCM";
        return this.http.post(url, lendingConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    postExtendConcession(extendConcessionModel: extendConcessionModel, extensionFee): Observable<LendingConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Lending/ExtendConcession/" + extensionFee;
        return this.http.post(url, extendConcessionModel, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    postChildConcession(lendingConcession: LendingConcession, relationshipType: string): Observable<LendingConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Lending/" + relationshipType + "Lending";
        return this.http.post(url, lendingConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    postRecallLendingData(lendingConcession: LendingConcession): Observable<LendingConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Lending/UpdateRecalledLending";
        return this.http.post(url, lendingConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    getlatestCrsOrMrs(riskGroupNumber): Observable<number> {
        const url = "/api/Lending/LatestCrsOrMrs/" + riskGroupNumber;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getLendingFinancial(riskGroupNumber): Observable<LendingFinancial> {
        const url = "/api/Lending/LendingFinancial/" + riskGroupNumber;
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
export class MockLendingService extends LendingService {
    model = new LendingConcession();
    lendingViewModel = new LendingView();
    lendingFinancialModel = new LendingFinancial();

    getLendingConcessionData(): Observable<LendingConcession> {
        this.model.concession = new Concession();
        this.model.concessionConditions = [new ConcessionCondition()];
        this.model.lendingConcessionDetails = [new LendingConcessionDetail()];
        return Observable.of(this.model);
    }

    getLendingViewData(riskGroupNumber): Observable<LendingView> {
        this.lendingViewModel.riskGroup = new RiskGroup();
        this.lendingViewModel.lendingFinancial = new LendingFinancial();
        this.lendingViewModel.lendingFinancial.totalExposure = 1;
        this.lendingViewModel.lendingFinancial.weightedAverageMap = 1;
        this.lendingViewModel.lendingFinancial.weightedCrsOrMrs = 1;
        this.lendingViewModel.lendingConcessions = [new LendingConcession()];
        return Observable.of(this.lendingViewModel);
    }

    postNewLendingData(lendingConcession: LendingConcession): Observable<LendingConcession> {
        this.model.concession = new Concession();
        this.model.concessionConditions = [new ConcessionCondition()];
        this.model.lendingConcessionDetails = [new LendingConcessionDetail()];
        return Observable.of(this.model);
    }

    postUpdateLendingData(lendingConcession: LendingConcession): Observable<LendingConcession> {
        this.model.concession = new Concession();
        this.model.concessionConditions = [new ConcessionCondition()];
        this.model.lendingConcessionDetails = [new LendingConcessionDetail()];
        return Observable.of(this.model);
    }


    postExtendConcession(concessionReferenceId): Observable<LendingConcession> {
        this.model.concession = new Concession();
        this.model.concessionConditions = [new ConcessionCondition()];
        this.model.lendingConcessionDetails = [new LendingConcessionDetail()];
        return Observable.of(this.model);
    }

    postChildConcession(lendingConcession: LendingConcession, relationshipType: string): Observable<LendingConcession> {
        this.model.concession = new Concession();
        this.model.concessionConditions = [new ConcessionCondition()];
        this.model.lendingConcessionDetails = [new LendingConcessionDetail()];
        return Observable.of(this.model);
    }

    postRecallLendingData(lendingConcession: LendingConcession): Observable<LendingConcession> {
        this.model.concession = new Concession();
        this.model.concessionConditions = [new ConcessionCondition()];
        this.model.lendingConcessionDetails = [new LendingConcessionDetail()];
        return Observable.of(this.model);
    }

    getlatestCrsOrMrs(riskGroupNumber): Observable<number> {
        return Observable.of(1);
    }

    getLendingFinancial(riskGroupNumber): Observable<LendingFinancial> {
        this.lendingFinancialModel.totalExposure = 1;
        this.lendingFinancialModel.weightedAverageMap = 1;
        this.lendingFinancialModel.weightedCrsOrMrs = 1;
        return Observable.of(this.lendingFinancialModel);
    }
}
