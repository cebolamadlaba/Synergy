import { Injectable } from '@angular/core';
import { HttpResponse } from '@angular/common/http';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import { Local } from 'protractor/built/driverProviders';
import { window } from 'rxjs/operator/window';

@Injectable()
export class FileService {

    constructor(private http: Http, private loc: Local) { }

    downloadFile(name: string): Observable<HttpResponse<Blob>> {
        return this.http.get('http://localhost:20201/assets/documents/'+name+'.xlsx').map(this.extractData).catch(this.handleErrorObservable);
    }

    private extractData(response: Response) {
        let body = response;
        return body;
    }

    private handleErrorObservable(error: Response | any) {
        console.error(error.message || error);
        return Observable.throw(error.message || error);
    }

}
