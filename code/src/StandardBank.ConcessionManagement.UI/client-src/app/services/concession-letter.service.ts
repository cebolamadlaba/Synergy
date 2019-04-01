import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

import { LegalEntityConcessionLetterModel } from '../models/legal-entity-concession-letter';

@Injectable()
export class ConcessionLetterService {

    constructor(private http: Http) {
    }

    generateConcessionLetterForConcessionsByLegalEntityId(legalEntityConcessionLetter: LegalEntityConcessionLetterModel): Observable<any> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Concession/GenerateConcessionLetterForLegalEntity/" + legalEntityConcessionLetter.legalEntityId;
        return this.http.post(url, legalEntityConcessionLetter, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    generateConcessionLetterForConcessionsByConcessionIds(concessionIds: string, legalEntityConcessionLetter: LegalEntityConcessionLetterModel): Observable<any> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/Concession/GenerateConcessionLetterForConcessions/" + concessionIds;
        return this.http.post(url, legalEntityConcessionLetter, options).map(this.extractData).catch(this.handleErrorObservable);
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
