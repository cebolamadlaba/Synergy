import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { BusinessCentreManagementModel } from '../models/business-centre-management-model';


@Injectable()
export class BusinessCentreService {

    constructor(private http: Http) {
    }

    getBusinessCentreManagementModels(): Observable<BusinessCentreManagementModel[]> {
        const url = "/api/BusinessCentre/BusinessCentreManagementModels";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    validateBusinessCentreManagementModel(businessCentreManagementModel: BusinessCentreManagementModel): Observable<string[]> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/BusinessCentre/ValidateBusinessCentreManagementModel";
        return this.http.post(url, businessCentreManagementModel, options).map(this.extractData).catch(this.handleErrorObservable);
    }

    createBusinessCentreManagementModel(businessCentreManagementModel: BusinessCentreManagementModel): Observable<boolean> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        const url = "/api/BusinessCentre/CreateBusinessCentreManagementModel";
        return this.http.post(url, businessCentreManagementModel, options).map(this.extractData).catch(this.handleErrorObservable);
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
export class MockBusinessCentreService extends BusinessCentreService {

    getBusinessCentreManagementModels(): Observable<BusinessCentreManagementModel[]> {
        var model = [new BusinessCentreManagementModel()];
        return Observable.of(model);
    }

    validateBusinessCentreManagementModel(businessCentreManagementModel: BusinessCentreManagementModel): Observable<string[]> {
        var model = [""];
        return Observable.of(model);
    }

    createBusinessCentreManagementModel(businessCentreManagementModel: BusinessCentreManagementModel): Observable<boolean> {
        return Observable.of(true);
    }

}
