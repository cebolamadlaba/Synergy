import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions } from '@angular/http';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs';

import { LegalEntityAddress } from '../models/legal-entity-address';

@Injectable()
export class LegalEntityAddressService {

    constructor(private http: Http) { }

    getLegalEntityAddress(legalEntityId): Observable<LegalEntityAddress> {
        const url = "/api/Concession/GetLegalEntityAddress/" + legalEntityId;
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
