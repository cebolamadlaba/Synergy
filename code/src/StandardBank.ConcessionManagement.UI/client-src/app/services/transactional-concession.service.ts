import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { RiskGroup } from "../models/risk-group";
import { Concession } from "../models/concession";
import { ConcessionCondition } from "../models/concession-condition";
import { TransactionalConcession } from "../models/transactional-concession";
import { TransactionalView } from "../models/transactional-view";
import { TransactionalConcessionDetail } from "../models/transactional-concession-detail";

@Injectable()
export class TransactionalConcessionService {

    constructor(private http: Http) { }

    getTransactionalViewData(riskGroupNumber): Observable<TransactionalView> {
        const url = "/api/Transactional/TransactionalView/" + riskGroupNumber;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getTransactionalConcessionData(concessionReferenceId): Observable<TransactionalConcession> {
        const url = "/api/Transactional/TransactionalConcessionData/" + concessionReferenceId;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    postNewTransactionalData(transactionalConcession: TransactionalConcession): Observable<TransactionalConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Transactional/NewTransactional";
        return this.http.post(url, transactionalConcession, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    postUpdateTransactionalData(transactionalConcession: TransactionalConcession): Observable<TransactionalConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Transactional/UpdateTransactional";
        return this.http.post(url, transactionalConcession, options).map(this.extractData).catch(this.handleErrorObservable);
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
export class MockTransactionalConcessionService extends TransactionalConcessionService {
    transactionalView = new TransactionalView();
    transactionalConcession = new TransactionalConcession();

    getTransactionalViewData(riskGroupNumber): Observable<TransactionalView> {
        this.transactionalView.riskGroup = new RiskGroup();
        this.transactionalView.transactionalConcessions = [new TransactionalConcession()];
        return Observable.of(this.transactionalView);
    }

    getTransactionalConcessionData(concessionReferenceId): Observable<TransactionalConcession> {
        this.transactionalConcession.concession = new Concession();
        this.transactionalConcession.concessionConditions = [new ConcessionCondition()];
        this.transactionalConcession.transactionalConcessionDetails = [new TransactionalConcessionDetail()];
        return Observable.of(this.transactionalConcession);
    }

    postNewTransactionalData(transactionalConcession: TransactionalConcession): Observable<TransactionalConcession> {
        this.transactionalConcession.concession = new Concession();
        this.transactionalConcession.concessionConditions = [new ConcessionCondition()];
        this.transactionalConcession.transactionalConcessionDetails = [new TransactionalConcessionDetail()];
        return Observable.of(this.transactionalConcession);
    }

    postUpdateTransactionalData(transactionalConcession: TransactionalConcession): Observable<TransactionalConcession> {
        this.transactionalConcession.concession = new Concession();
        this.transactionalConcession.concessionConditions = [new ConcessionCondition()];
        this.transactionalConcession.transactionalConcessionDetails = [new TransactionalConcessionDetail()];
        return Observable.of(this.transactionalConcession);
    }
}
