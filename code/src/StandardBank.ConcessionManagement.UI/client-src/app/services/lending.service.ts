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
import { SourceSystemProduct } from "../models/source-system-product";

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

    postExtendConcession(concessionReferenceId): Observable<LendingConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Lending/ExtendConcession/" + concessionReferenceId;
        return this.http.post(url, concessionReferenceId, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    postRenewLendingData(lendingConcession: LendingConcession): Observable<LendingConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Lending/RenewLending";
        return this.http.post(url, lendingConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    postRecallLendingData(lendingConcession: LendingConcession): Observable<LendingConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Lending/UpdateRecalledLending";
        return this.http.post(url, lendingConcession, options).map(this.extractData).catch(this.handleErrorObservable);
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

    getLendingConcessionData(): Observable<LendingConcession> {
        this.model.concession = new Concession();
        this.model.concessionConditions = [new ConcessionCondition()];
        this.model.lendingConcessionDetails = [new LendingConcessionDetail()];
        return Observable.of(this.model);
    }

    getLendingViewData(riskGroupNumber): Observable<LendingView> {
        this.lendingViewModel.riskGroup = new RiskGroup();
        this.lendingViewModel.totalExposure = 1;
        this.lendingViewModel.weightedAverageMap = 1;
        this.lendingViewModel.weightedCrsMrs = 1;
        this.lendingViewModel.sourceSystemProducts = [new SourceSystemProduct()];
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

    postRenewLendingData(lendingConcession: LendingConcession): Observable<LendingConcession> {
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
}
