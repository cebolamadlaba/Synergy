import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

@Injectable()
export class RiskGroupNameService {

    constructor(private http: Http) { }

    getData(riskGroupNumber): Observable<String> {
        const url = "/api/Pricing/RiskGroupName/" + riskGroupNumber;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    private extractData(response: Response) {
        let body = response.text();
        return body;
    }

    private handleErrorObservable(error: Response | any) {
        console.error(error.message || error);
        return Observable.throw(error.message || error);
    }

}

@Injectable()
export class MockRiskGroupNameService extends RiskGroupNameService {

    getData(riskGroupNumber): Observable<String> {
        return Observable.of("Test Risk Group");
    }

}