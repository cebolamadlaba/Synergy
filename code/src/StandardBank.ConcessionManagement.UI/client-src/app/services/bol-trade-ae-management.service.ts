import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { User } from '../models/user';
import { Centre } from '../models/centre';
import { AccountExecutive } from '../models/account-executive';
import { RoleSubRole } from "../models/RoleSubRole";

@Injectable()
export class BolTradeAeManagementService {


    constructor(private http: Http) { }

    getBolTradAEUsers(): Observable<User[]> {
        const url = "/api/BolTradeAEManagement/AEUsers";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getBolTradAAUsers(): Observable<User[]> {
        const url = "/api/BolTradeAEManagement/BolOrTradeAAUsers";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    saveAccountExecutive(accountExecutive: AccountExecutive): Observable<boolean> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/BolTradeAEManagement/SaveAccountExecutive";
        return this.http.post(url, accountExecutive, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    validateUser(aeUser: User): Observable<string[]> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/BolTradeAEManagement/ValidateUser";
        return this.http.post(url, aeUser, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    getCentres(): Observable<Centre[]> {
        const url = "/api/BolTradeAEManagement/Centres";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getAEAAUsers(aeUserId: number): Observable<User[]> {
        const url = "/api/BolTradeAEManagement/BolTradeAEAAUsers/" + aeUserId;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getRoleSubRoles(): Observable<RoleSubRole[]> {
        const url = "/api/BolTradeAAManagement/RoleSubRoles";
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
export class MockBolTradeAeManagementService extends BolTradeAeManagementService {

    getBolTradAEUsers(): Observable<User[]> {
        var model = [new User()];
        return Observable.of(model);
    }

    getBolTradAAUsers(): Observable<User[]> {
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

    getRoleSubRoles(): Observable<RoleSubRole[]> {
        var model = [new RoleSubRole()];
        return Observable.of(model);
    }

    getCentres(): Observable<Centre[]> {
        var model = [new Centre()];
        return Observable.of(model);
    }

}
