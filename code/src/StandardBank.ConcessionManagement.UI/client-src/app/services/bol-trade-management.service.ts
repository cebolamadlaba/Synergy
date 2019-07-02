import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { User } from '../models/user';
import { Centre } from '../models/centre';
import { Injectable } from '@angular/core';
import { RoleSubRole } from "../models/RoleSubRole";

@Injectable()
export class BolTradeManagementService {

    constructor(private http: Http) { }

    getBolOrTradeUsers(): Observable<User[]> {
        const url = "/api/BolTradeAAManagement/BolOrTradeAAUsers";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    saveBolOrTradeUser(aaUser: User): Observable<boolean> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/BolTradeAAManagement/SaveBolOrTradeAccountExecutive";
        return this.http.post(url, aaUser, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    validateUser(aaUser: User): Observable<string[]> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/BolTradeAAManagement/ValidateUser";
        return this.http.post(url, aaUser, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    getCentres(): Observable<Centre[]> {
        const url = "/api/BolTradeAAManagement/Centres";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getRoleSubRoles(): Observable<RoleSubRole[]> {
        const url = "/api/BolTradeAAManagement/RoleSubRoles";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getAEUsers(): Observable<User[]> {
        const url = "/api/BolTradeAAManagement/AEUsers";
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
export class MockAaManagementService extends BolTradeManagementService {

    getBolOrTradeAAUsers(): Observable<User[]> {
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

    getRoleSubRoles(): Observable<RoleSubRole[]> {
        var model = [new RoleSubRole()];
        return Observable.of(model);
    }

    getAEUsers(): Observable<User[]> {
        var model = [new User()];
        return Observable.of(model);
    }

}
