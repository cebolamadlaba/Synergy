import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { ConcessionsSummary } from "../models/concessions-summary";

@Injectable()
export class InboxConcessionCountService {

  constructor(private _http: Http) {
  }

  getData(): Observable<ConcessionsSummary> {
    var url = "/api/inbox/ConcessionsSummary";

    return this._http.get(url).map(this.extractData).catch(this.handleErrorObservable);
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
