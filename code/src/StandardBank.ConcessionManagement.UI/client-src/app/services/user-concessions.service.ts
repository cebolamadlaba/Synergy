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


    recallConcession(concessionReferenceId: string): Observable<boolean> {
        const url = "/api/Concession/RecallConcession/" + concessionReferenceId;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    deactivateConcession(concessionReferenceId: string): Observable<boolean> {
        const url = "/api/Concession/DeactivateConcession/" + concessionReferenceId;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    deactivateConcessionDetailed(concessionReferenceDetailedId: number): Observable<boolean> {
        const url = "/api/Concession/DeactivateConcessionDetailed/" + concessionReferenceDetailedId;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getCacheAEUser(accountExecutiveUserId: number): any {
   
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/inbox/CacheAEUser";
        return this.http.post(url, accountExecutiveUserId, options).map(this.extractData).catch(this.handleErrorObservable);
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
        this.approvedConcessionModel[0].legalEntityId = 1;
        return Observable.of(this.approvedConcessionModel);
    }

    deactivateConcession(concessionReferenceId): Observable<boolean> {
        return Observable.of(true);
    }
}
