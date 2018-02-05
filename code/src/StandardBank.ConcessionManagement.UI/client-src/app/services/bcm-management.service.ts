import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { User } from '../models/user';
import { Centre } from '../models/centre';

@Injectable()
export class BcmManagementService {

    constructor(private http: Http) { }

    getBCMUsers(): Observable<User[]> {
        const url = "/api/BCMManagement/BCMUsers";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    saveBcmUser(bcmUser: User): Observable<boolean> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/BCMManagement/SaveBcmUser";
        return this.http.post(url, bcmUser, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    validateUser(bcmUser: User): Observable<string[]> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/BCMManagement/ValidateUser";
        return this.http.post(url, bcmUser, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    getCentres(): Observable<Centre[]> {
        const url = "/api/BCMManagement/Centres";
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
export class MockBcmManagementService extends BcmManagementService {

    getBCMUsers(): Observable<User[]> {
        var model = [new User()];
        return Observable.of(model);
    }

    saveBcmUser(bcmUser: User): Observable<boolean> {
        return Observable.of(true);
    }

    validateUser(bcmUser: User): Observable<string[]> {
        var model = [''];
        return Observable.of(model);
    }

    getCentres(): Observable<Centre[]> {
        var model = [new Centre()];
        return Observable.of(model);
    }

}
