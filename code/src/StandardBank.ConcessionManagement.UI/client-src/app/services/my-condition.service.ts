import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { Condition } from "../models/condition";

@Injectable()
export class MyConditionService {

    constructor(private http: Http) {
    }
    getMyConditions(period:string,periodType:string): Observable<Condition[]> {
        const url = "/api/Condition/MyConditions/"+period+"/"+periodType;
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

    getMyConditions(period, periodType): Observable<Condition[]> {
        this.model[0].concessionId = 1;
        return Observable.of(this.model);
    }

}