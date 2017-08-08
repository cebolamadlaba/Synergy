import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { PeriodType } from "../models/period-type";

@Injectable()
export class PeriodTypeService {

    constructor(private http: Http) {
    }

    getData(): Observable<PeriodType[]> {
        const url = "/api/Condition/PeriodTypes";
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
export class MockPeriodTypeService extends PeriodTypeService {
    model = [new PeriodType()];

    getData(): Observable<PeriodType[]> {
        this.model[0].id = 1;
        this.model[0].description = "Test Period Type";
        return Observable.of(this.model);
    }
}