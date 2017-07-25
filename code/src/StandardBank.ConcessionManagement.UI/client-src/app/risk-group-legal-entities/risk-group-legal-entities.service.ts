import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { LegalEntity } from '../models/legal-entity';

@Injectable()
export class RiskGroupLegalEntitiesService {

    constructor(private http: Http) {
    }

    getData(riskGroupNumber): Observable<LegalEntity[]> {
        const url = "/api/pricing/RiskGroupLegalEntities/" + riskGroupNumber;
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
export class MockRiskGroupLegalEntitiesService extends RiskGroupLegalEntitiesService {
    model = [new LegalEntity()];

    getData(riskGroupNumber): Observable<LegalEntity[]> {
        this.model[0].id = 1;
        this.model[0].customerName = "Mocked Customer Name";
        this.model[0].riskGroupNumber = riskGroupNumber;
        return Observable.of(this.model);
    }
}