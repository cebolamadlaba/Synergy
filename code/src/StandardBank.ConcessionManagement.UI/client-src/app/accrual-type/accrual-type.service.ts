import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { AccrualType } from "../models/accrual-type";

@Injectable()
export class AccrualTypeService {

    constructor(private http: Http) {
    }

    getData(): Observable<AccrualType[]> {
        const url = "/api/Concession/AccrualTypes";
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
export class MockAccrualTypeService extends AccrualTypeService {
    model = [new AccrualType()];

    getData(): Observable<AccrualType[]> {
        this.model[0].id = 1;
        this.model[0].description = "Test Accrual Type";
        return Observable.of(this.model);
    }
}
