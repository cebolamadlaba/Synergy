import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { ClientAccount } from "../models/client-account";

@Injectable()
export class ClientAccountService {

    constructor(private http: Http) {
    }

    getData(riskGroupNumber): Observable<ClientAccount[]> {
        const url = "/api/Concession/ClientAccounts/" + riskGroupNumber;
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
export class MockClientAccountService {
    model = [new ClientAccount()];

    constructor() { }
    getData(riskGroupNumber): Observable<ClientAccount[]> {
        this.model[0].accountNumber = "Test Account Number";
        this.model[0].legalEntityAccountId = 1;
        this.model[0].legalEntityId = 1;
        this.model[0].riskGroupId = 1;
        this.model[0].customerName = "Test Customer Name";
        return Observable.of(this.model);
    }
}
