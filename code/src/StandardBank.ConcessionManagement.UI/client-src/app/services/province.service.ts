import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Http, Response } from '@angular/http';
import { Province } from "../models/province";

@Injectable()
export class ProvinceService {

    constructor(private http: Http) {
    }
    getProvinces(): Observable<Province[]> {
        const url = "/api/Province/Provinces";
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
