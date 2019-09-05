import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { GlmsView } from "../models/glms-view";
import { Observable } from 'rxjs';

@Injectable()
export class GlmsConcessionService {

    constructor(private http: Http) { }

    getGlmsViewData(riskGroupNumber, sapbpid): Observable<GlmsView> {
        const url = "/api/Glms/GlmsView/" + riskGroupNumber + "/" + sapbpid;
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
