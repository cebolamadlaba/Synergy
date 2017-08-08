import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { Period } from "../models/period";

@Injectable()
export class PeriodService {

    constructor(private http: Http) {
    }

    getData(): Observable<Period[]> {
        const url = "/api/Condition/Periods";
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
export class MockPeriodService extends PeriodService {
    model = [new Period()];

    getData(): Observable<Period[]> {
        this.model[0].id = 1;
        this.model[0].description = "Test Period";
        return Observable.of(this.model);
    }
}
