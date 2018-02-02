import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { User } from '../models/user';
import { RegionCentresModel } from '../models/region-centres-model';

@Injectable()
export class PcmManagementService {

    constructor(private http: Http) { }

    getPCMUsers(): Observable<User[]> {
        const url = "/api/PCMManagement/PCMUsers";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getRegionCentres(): Observable<RegionCentresModel[]> {
        const url = "/api/PCMManagement/RegionCentres";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    savePcmUser(pcmUser: User): Observable<boolean> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/PCMManagement/SavePcmUser";
        return this.http.post(url, pcmUser, options).map(this.extractData).catch(this.handleErrorObservable);
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

    getRegionCentres(): Observable<RegionCentresModel[]> {
        var model = [new RegionCentresModel()];
        return Observable.of(model);
    }

    savePcmUser(pcmUser: User): Observable<boolean> {
        return Observable.of(true);
    }

}
