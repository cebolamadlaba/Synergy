import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { User } from '../models/user';
import { Centre } from '../models/centre';
import { AccountExecutive } from '../models/account-executive';

@Injectable()
export class AeManagementService {

    constructor(private http: Http) { }

    getAEUsers(): Observable<User[]> {
        const url = "/api/AEManagement/AEUsers";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getAAUsers(): Observable<User[]> {
        const url = "/api/AEManagement/AAUsers";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    saveAccountExecutive(accountExecutive: AccountExecutive): Observable<boolean> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/AEManagement/SaveAccountExecutive";
        return this.http.post(url, accountExecutive, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    validateUser(aeUser: User): Observable<string[]> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/AEManagement/ValidateUser";
        return this.http.post(url, aeUser, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    getCentres(): Observable<Centre[]> {
        const url = "/api/AEManagement/Centres";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getAEAAUsers(aeUserId: number): Observable<User[]> {
        const url = "/api/AEManagement/AEAAUsers/" + aeUserId;
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
export class MockAeManagementService extends AeManagementService {

    getAEUsers(): Observable<User[]> {
        var model = [new User()];
        return Observable.of(model);
    }

    getAAUsers(): Observable<User[]> {
        var model = [new User()];
        return Observable.of(model);
    }

    saveAccountExecutive(accountExecutive: AccountExecutive): Observable<boolean> {
        return Observable.of(true);
    }

    validateUser(aeUser: User): Observable<string[]> {
        var model = [''];
        return Observable.of(model);
    }

    getCentres(): Observable<Centre[]> {
        var model = [new Centre()];
        return Observable.of(model);
    }

}
