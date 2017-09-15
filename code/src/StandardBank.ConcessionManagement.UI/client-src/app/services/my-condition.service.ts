import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { Condition } from "../models/condition";
import { ConditionCounts } from "../models/condition-counts";

@Injectable()
export class MyConditionService {

    constructor(private http: Http) {
    }

    getMyConditions(period:string,periodType:string): Observable<Condition[]> {
        const url = "/api/Condition/MyConditions/"+period+"/"+periodType;
        return this.http.get(url).map(x => x.json()).catch(this.handleErrorObservable);
    }

    getConditionCounts(): Observable<ConditionCounts> {
        const url = "/api/Condition/ConditionCounts";
        return this.http.get(url).map(x => x.json()).catch(this.handleErrorObservable);
    }

    private handleErrorObservable(error: Response | any) {
        console.error(error.message || error);
        return Observable.throw(error.message || error);
    }
}

@Injectable()
export class MockMyConditionService {
    model = [new Condition()];
    modelConditionCounts = new ConditionCounts();

    getMyConditions(period, periodType): Observable<Condition[]> {
        this.model[0].concessionId = 1;
        return Observable.of(this.model);
    }

    getConditionCounts(): Observable<ConditionCounts> {
        this.modelConditionCounts.ongoingCount = 100;
        this.modelConditionCounts.standardCount = 200;
        return Observable.of(this.modelConditionCounts);
    }
}
