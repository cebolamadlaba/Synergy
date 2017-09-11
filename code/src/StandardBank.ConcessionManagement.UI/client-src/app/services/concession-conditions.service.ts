import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { ConcessionCondition } from "../models/concession-condition";

@Injectable()
export class ConcessionConditionsService {

    constructor(private http: Http) {
    }

    getData(concessionId): Observable<ConcessionCondition[]> {
        const url = "/api/Concession/ConcessionConditions/" + concessionId;
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
export class MockConcessionConditionsService extends ConcessionConditionsService {
    model = [new ConcessionCondition()];

    getData(concessionId): Observable<ConcessionCondition[]> {
        this.model[0].concessionConditionId = 1;
        this.model[0].concessionId = 1;
        this.model[0].conditionType = "Test Condition Type";
        this.model[0].productType = "Test Product Type";
        this.model[0].interestRate = 1.5;
        this.model[0].conditionVolume = 100;
        this.model[0].conditionValue = 2.5;
        this.model[0].periodType = "Test Period Type";
        this.model[0].period = "Test Period";
        return Observable.of(this.model);
    }

}
