﻿import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { User } from "../models/user";

@Injectable()
export class UserService {

    constructor(private http: Http) {
    }

    getData(): Observable<User> {
        const url = "/api/Application/LoggedInUser";
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
export class MockUserService extends UserService {
    model = new User();

    getData(): Observable<User> {
        this.model.id = 1;
        this.model.firstName = "Mocked";
        this.model.surname = "User";
        return Observable.of(this.model);
    }
}