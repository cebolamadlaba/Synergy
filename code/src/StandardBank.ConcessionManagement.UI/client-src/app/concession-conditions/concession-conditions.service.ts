import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

@Injectable()
export class ConcessionConditionsService {

    constructor(private http: Http) {
    }

    //getData(): Observable<User> {
    //    const url = "/api/Application/LoggedInUser";
    //    return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    //}

    //private extractData(response: Response) {
    //    let body = response.json();
    //    return body;
    //}

    //private handleErrorObservable(error: Response | any) {
    //    console.error(error.message || error);
    //    return Observable.throw(error.message || error);
    //}

}
