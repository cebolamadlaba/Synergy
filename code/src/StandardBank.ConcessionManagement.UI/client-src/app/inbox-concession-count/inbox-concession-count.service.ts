import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { ConcessionsSummary } from "../models/concessions-summary";

@Injectable()
export class InboxConcessionCountService {

    constructor(private http: Http) {
    }

    getData(): Observable<ConcessionsSummary> {
        const url = "/api/inbox/ConcessionsSummary";
        console.log("here");
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
export class MockInboxConcessionCountService extends InboxConcessionCountService {
    model = new ConcessionsSummary();

    getData(): Observable<ConcessionsSummary> {
        this.model.pendingConcessions = 1;
        this.model.declinedConcessions = 2;
        this.model.dueForExpiryConcessions = 3;
        this.model.expiredConcessions = 4;
        this.model.mismatchedConcessions = 5;
        return Observable.of(this.model);
    }
}