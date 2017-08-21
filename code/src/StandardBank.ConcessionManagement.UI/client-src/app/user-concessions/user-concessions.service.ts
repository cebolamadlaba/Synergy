﻿import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { UserConcessions } from "../models/user-concessions";

@Injectable()
export class UserConcessionsService {

    constructor(private http: Http) {
    }

    getData(): Observable<UserConcessions> {
        const url = "/api/inbox/UserConcessions";
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
export class MockUserConcessionsService extends UserConcessionsService {
    model = new UserConcessions();

    getData(): Observable<UserConcessions> {
        this.model.pendingConcessionsCount = 1;
        this.model.declinedConcessionsCount = 2;
        this.model.dueForExpiryConcessionsCount = 3;
        this.model.expiredConcessionsCount = 4;
        this.model.mismatchedConcessionsCount = 5;
        return Observable.of(this.model);
    }
}