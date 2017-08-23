import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { ReviewFeeType } from "../models/review-fee-type";

@Injectable()
export class ReviewFeeTypeService {

    constructor(private http: Http) {
    }

    getData(): Observable<ReviewFeeType[]> {
        const url = "/api/Concession/ReviewFeeTypes";
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
export class MockReviewFeeTypeService extends ReviewFeeTypeService {
    model = [new ReviewFeeType()];

    getData(): Observable<ReviewFeeType[]> {
        this.model[0].id = 1;
        this.model[0].description = "Review Fee Test";
        return Observable.of(this.model);
    }
}

