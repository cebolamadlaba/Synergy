import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Province } from "../models/province";
import { Http, Response, Headers, RequestOptions } from '@angular/http';

@Injectable()
export class ProvinceService {

    constructor(private http: Http) {
    }
    getProvinces(): Observable<Province[]> {
        const url = "/api/Province/Provinces";
      return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    postProvince(province: Province): Observable<Province> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Province/UpdateProvince";
        return this.http.post(url, province, options).map(this.extractData).catch(this.handleErrorObservable);
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
export class MockProvinceService extends ProvinceService {
    getProvinces(): Observable<Province[]> {
        return Observable.of([new Province()]);
    }

    postProvince(province: Province): Observable<Province> {
        return Observable.of(new Province());
    }
}