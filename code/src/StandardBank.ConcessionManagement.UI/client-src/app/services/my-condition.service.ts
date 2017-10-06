import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { ConditionCounts } from "../models/condition-counts";
import { ConcessionCondition } from "../models/concession-condition";

@Injectable()
export class MyConditionService {

    constructor(private http: Http) {
    }

    getMyConditions(period: string, periodType: string): Observable<ConcessionCondition[]> {
        const url = "/api/Condition/MyConditions/"+period+"/"+periodType;
        return this.http.get(url).map(x => x.json()).catch(this.handleErrorObservable);
    }

    getConditionCounts(): Observable<ConditionCounts> {
        const url = "/api/Condition/ConditionCounts";
        return this.http.get(url).map(x => x.json()).catch(this.handleErrorObservable);
	}

	updateCondition(condition: ConcessionCondition): Observable<ConcessionCondition> {
		let headers = new Headers({ 'Content-Type': 'application/json' });
		let options = new RequestOptions({ headers: headers });
		const url = "/api/Condition/UpdateCondition";
		return this.http.post(url, condition, options).map(x => x.json()).catch(this.handleErrorObservable);
	}

    private handleErrorObservable(error: Response | any) {
        console.error(error.message || error);
        return Observable.throw(error.message || error);
    }
}

@Injectable()
export class MockMyConditionService {
    model = [new ConcessionCondition()];
    modelConditionCounts = new ConditionCounts();

    getMyConditions(period, periodType): Observable<ConcessionCondition[]> {
        this.model[0].concessionId = 1;
        return Observable.of(this.model);
    }

    getConditionCounts(): Observable<ConditionCounts> {
        this.modelConditionCounts.ongoingCount = 100;
        this.modelConditionCounts.standardCount = 200;
        return Observable.of(this.modelConditionCounts);
	}

	updateCondition(condition: ConcessionCondition): Observable<ConcessionCondition> {
		return Observable.of(this.model[0]);
	}
}
