import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { UserConcessions } from "../models/user-concessions";
import { ApprovedConcession } from "../models/approved-concession";

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

    private extractBytes(response: Response) {
        let bytes = response.text();
        return bytes;
    }

    private handleErrorObservable(error: Response | any) {
        console.error(error.message || error);
        return Observable.throw(error.message || error);
    }

    getApprovedConcessions(): Observable<ApprovedConcession[]> {
        const url = "/api/Concession/UserApprovedConcessions";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    printConcessionLetters(concessionIds: number[]): Observable<any> {
        console.log(concessionIds);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Concession/PrintConcessionLetters";

        //TODO: This is not working, because this is afterall JavaScript which is nothing but a string that a browser reads
        return this.http.post(url, concessionIds, options).map(this.extractBytes).catch(this.handleErrorObservable);
    }
}

@Injectable()
export class MockUserConcessionsService extends UserConcessionsService {
    model = new UserConcessions();
    approvedConcessionModel = [new ApprovedConcession()];
    printData: any;

    getData(): Observable<UserConcessions> {
        this.model.pendingConcessionsCount = 1;
        this.model.declinedConcessionsCount = 2;
        this.model.dueForExpiryConcessionsCount = 3;
        this.model.expiredConcessionsCount = 4;
        this.model.mismatchedConcessionsCount = 5;
        return Observable.of(this.model);
    }

    getApprovedConcessions(): Observable<ApprovedConcession[]> {
        this.approvedConcessionModel[0].concessionId = 1;
        return Observable.of(this.approvedConcessionModel);
    }

    printConcessionLetters(concessionIds: number[]): Observable<any> {
        return Observable.of(this.printData);
    }
}
