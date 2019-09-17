import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import { BaseRateCode } from '../models/base-rate-code';
import { InterestType } from '../models/interest-type';
import { GlmsGroup } from '../models/glms-group';
import { InterestPricingCategory } from '../models/interest-pricing-category';
import { RateType } from '../models/rate-type';
import { SlabType } from '../models/slab-type';

@Injectable()
export class GlmsBaseService {

    constructor(public http: Http) {
    }

    //add glms concession look up section
    getInterestType(): Observable<InterestType[]> {
        const url = "/api/Concession/InterestType";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getBaseRateCode(): Observable<BaseRateCode[]> {
        const url = "/api/Concession/BaseRateCode";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getGlmsGroup(): Observable<GlmsGroup[]> {
        const url = "/api/Concession/GlmsGroup";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getInterestPricingCategory(): Observable<InterestPricingCategory[]> {
        const url = "/api/Concession/InterestPricingCategory";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getRateType(): Observable<RateType[]> {
        const url = "/api/Concession/RateType";
        return this.http.get(url).map(this.extractData).catch(this.handleErrorObservable);
    }

    getSlabType(): Observable<SlabType[]> {
        const url = "/api/Concession/SlabType";
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
