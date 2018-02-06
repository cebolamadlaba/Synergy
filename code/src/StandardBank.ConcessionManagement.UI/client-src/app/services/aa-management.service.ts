import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { User } from '../models/user';
import { Centre } from '../models/centre';

@Injectable()
export class AaManagementService {

    constructor(private http: Http) { }

    getAAUsers(): Observable<User[]> {
        const url = "/api/AAManagement/AAUsers";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    saveAaUser(aaUser: User): Observable<boolean> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/AAManagement/SaveAaUser";
        return this.http.post(url, aaUser, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    validateUser(aaUser: User): Observable<string[]> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/AAManagement/ValidateUser";
        return this.http.post(url, aaUser, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    getCentres(): Observable<Centre[]> {
        const url = "/api/AAManagement/Centres";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getAEUsers(): Observable<User[]> {
        const url = "/api/AAManagement/AEUsers";
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
export class MockAaManagementService extends AaManagementService {

    getAAUsers(): Observable<User[]> {
        var model = [new User()];
        return Observable.of(model);
    }

    saveAaUser(aaUser: User): Observable<boolean> {
        return Observable.of(true);
    }

    validateUser(aaUser: User): Observable<string[]> {
        var model = [''];
        return Observable.of(model);
    }

    getCentres(): Observable<Centre[]> {
        var model = [new Centre()];
        return Observable.of(model);
    }

    getAEUsers(): Observable<User[]> {
        var model = [new User()];
        return Observable.of(model);
    }

}
