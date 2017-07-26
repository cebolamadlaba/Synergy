import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { RiskGroup } from "../models/risk-group";

@Injectable()
export class RiskGroupService {

    constructor(private http: Http) { }

    getData(riskGroupNumber): Observable<RiskGroup> {
        const url = "/api/Pricing/RiskGroup/" + riskGroupNumber;
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
export class MockRiskGroupService extends RiskGroupService {
    model = new RiskGroup();

    getData(riskGroupNumber): Observable<RiskGroup> {
        this.model.id = 1;
        this.model.name = "Risk Group Test";
        this.model.number = 1;
        return Observable.of(this.model);
    }

}