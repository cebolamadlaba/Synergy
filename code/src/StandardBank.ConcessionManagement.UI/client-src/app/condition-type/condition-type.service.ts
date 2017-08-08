import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { ConditionType } from "../models/condition-type";
import { ConditionProduct } from "../models/condition-product";

@Injectable()
export class ConditionTypeService {

    constructor(private http: Http) {
    }

    getData(): Observable<ConditionType[]> {
        const url = "/api/Condition/ConditionTypes";
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
export class MockConditionTypeService extends ConditionTypeService {
    model = [new ConditionType()];

    getData(): Observable<ConditionType[]> {
        this.model[0].id = 1;
        this.model[0].description = "Test Condition Type";
        this.model[0].conditionProducts = [new ConditionProduct()];
        return Observable.of(this.model);
    }
}