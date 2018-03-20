import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { Region } from '../models/region';

@Injectable()
export class RegionService {

    constructor(private http: Http) {
    }   

    getAll(): Observable<Region[]> {
        const url = "/api/Region/All";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    validate(region: Region): Observable<string[]> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Region/Validate";
        return this.http.post(url, region, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    create(region: Region): Observable<boolean> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Region/Create";
        return this.http.post(url, region, options).map(this.extractData).catch(this.handleErrorObservable);
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
export class MockRegionService extends RegionService {
    
    getAll(): Observable<Region[]> {
        var model = [new Region()];
        return Observable.of(model);
    }

    validate(region: Region): Observable<string[]> {
        var model = [""];
        return Observable.of(model);
    }

    create(region: Region): Observable<boolean> {
        return Observable.of(true);
    }
}
