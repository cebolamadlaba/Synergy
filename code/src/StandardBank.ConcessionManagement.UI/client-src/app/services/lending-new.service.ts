import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { LendingConcession } from "../models/lending-concession";
import { Concession } from "../models/concession";
import { ConcessionCondition } from "../models/concession-condition";
import { LendingConcessionDetail } from "../models/lending-concession-detail";

@Injectable()
export class LendingNewService {

    constructor(private http: Http) {
    }

    postData(lendingConcession: LendingConcession): Observable<LendingConcession> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Lending/NewLending";
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
export class MockLendingNewService {

    model = new LendingConcession();

    postData(lendingConcession: LendingConcession): Observable<LendingConcession> {
        this.model.concession = new Concession();
        this.model.concessionConditions = [new ConcessionCondition()];
        this.model.lendingConcessionDetails = [new LendingConcessionDetail()];
        return Observable.of(this.model);
    }

}