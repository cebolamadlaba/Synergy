import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { User } from '../models/user';

@Injectable()
export class PcmManagementService {

    constructor(private http: Http) { }

    getPCMUsers(): Observable<User[]> {
        const url = "/api/PCMManagement/PCMUsers";
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
export class MockPcmManagementService extends PcmManagementService {

    getPCMUsers(): Observable<User[]> {
        var model = [new User()];
        return Observable.of(model);
    }

}
