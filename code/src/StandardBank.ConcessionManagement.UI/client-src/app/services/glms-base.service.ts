import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import { BaseRateCode } from '../models/base-rate-code';
import { InterestType } from '../models/interest-type';
import { GlmsGroup } from '../models/glms-group';
import { InterestPricingCategory } from '../models/interest-pricing-category';
import { RateType } from '../models/rate-type';
import { SlabType } from '../models/slab-type';
import { BaseComponentService } from './base-component.service';
import { Router } from '@angular/router';
import { UserService } from './user.service';
import { ConditionType } from "../models/condition-type";

@Injectable()
export class GlmsBaseService extends BaseComponentService {

    tierValidationError: String[];
    validationError: String[];

    constructor(public http: Http, public router: Router, public userService: UserService) {
        super(router, userService);
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

    public addTierValidationError(validationDetail) {
        if (!this.tierValidationError)
            this.tierValidationError = [];
        this.tierValidationError.push(validationDetail);
    }

    disableFieldBase(fieldname: string, canEdit: boolean, index: number = null, selectedConditionTypes: ConditionType[], isRecalling: boolean = null, motivationEnabled: boolean = null) {
        switch (fieldname) {
            case 'smtDealNumber':
                if (isRecalling == null) {
                    return canEdit ? null : '';
                } else {
                    return (isRecalling || canEdit) ? null : '';
                }
            case 'motivation':
                if (motivationEnabled == null) {
                    return canEdit ? null : '';
                } else {
                    return motivationEnabled ? null : '';
                }
            case 'concessions':
            case 'glmsGroup':
            case 'interestPricingCategory':
            case 'interestType':
            case 'slabType':
            case 'expiryDate':
            case 'addTier':
            case 'manageConditions':
                return canEdit ? null : '';
            case 'interestRate':
                return selectedConditionTypes[index] != null && selectedConditionTypes[index].enableInterestRate ? null : '';
            case 'volume':
                return selectedConditionTypes[index] != null && selectedConditionTypes[index].enableConditionVolume ? null : '';
            case 'value':
                return selectedConditionTypes[index] != null && selectedConditionTypes[index].enableConditionValue ? null : '';
            default:
                break;
        }
    }

    addValidationError(validationDetail) {
        if (!this.validationError)
            this.validationError = [];

        this.validationError.push(validationDetail);
    }
}
