import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { LendingConcession } from "../models/lending-concession";
import { Concession } from "../models/concession";
import { ConcessionCondition } from "../models/concession-condition";
import { LendingConcessionDetail } from "../models/lending-concession-detail";

@Injectable()
export class LendingService {

    constructor(private http: Http) {
    }

    getData(concessionReferenceId): Observable<LendingConcession> {
        const url = "/api/Lending/LendingConcessionData/" + concessionReferenceId;
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

    getData(): Observable<LendingConcession> {
        this.model.concession = new Concession();
        this.model.concessionConditions = [new ConcessionCondition()];
        this.model.lendingConcessionDetails = [new LendingConcessionDetail()];
        return Observable.of(this.model);
    }
}