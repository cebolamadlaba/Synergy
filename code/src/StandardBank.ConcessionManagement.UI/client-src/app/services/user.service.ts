import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { User } from "../models/user";
import { Centre } from "../models/centre";
import { Role } from "../models/role";
import { Region } from "../models/region";

@Injectable()
export class UserService {

    constructor(private http: Http) {
    }

    getData(): Observable<User> {
        const url = "/api/Application/LoggedInUser";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getUserRiskGroupDetailsData(sapbpidOrRiskGroupNumber): Observable<User> {
        const url = "/api/Pricing/PricingRiskGroupUser/" + sapbpidOrRiskGroupNumber;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getLoggedInUserMyAccess(): any {
      
        const url = "/api/Application/ValidateUserMyAccess";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getUATWarning(): Observable<User> {
        const url = "/api/Application/UatWarning";
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

    checkPendingConcessionInRiskGroupOrSapbPid(sapbpidOrRiskGroupNumber, concessionTypeId): Observable<number> {
        const url = "/api/Pricing/CheckPendingConcessionInRiskGroupOrSapbPid/" + sapbpidOrRiskGroupNumber + "/" + concessionTypeId;
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

}

@Injectable()
export class MockUserService extends UserService {
    model = new User();

    getData(): Observable<User> {
        this.model.id = 1;
        this.model.firstName = "Mocked";
        this.model.surname = "User";
        this.model.userCentres = [new Centre()];
        this.model.selectedCentre = new Centre();
        this.model.userRoles = [new Role()];
        return Observable.of(this.model);
    }
}
