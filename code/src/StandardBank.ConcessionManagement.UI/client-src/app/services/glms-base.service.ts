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
import { ConcessionRelationshipDetail } from '../models/concession-relationship-detail';
import { FormGroup } from '@angular/forms';
import { GlmsConcession } from '../models/glms-concession';
import { ConditionType } from '../models/condition-type';

@Injectable()
export class GlmsBaseService extends BaseComponentService {

    tierValidationError: String[];

    errorMessage: String;
    validationError: String[];
    saveMessage: String;
    warningMessage: String;
    showHide = false;
    isLoading = true;
    isRecalling = false;
    canEdit = false;
    motivationEnabled = false;
    canBcmApprove = false;
    canPcmApprove = false;
    canApproveChanges: boolean;
    isEditing = false;
    public glmsConcessionForm: FormGroup;
    glmsConcession: GlmsConcession;
    selectedConditionTypes: ConditionType[];
    concessionReferenceId: string;
    canRecall = false;

    constructor(public http: Http, public router: Router, public userService: UserService) {
        super(router,userService);
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

    addValidationError(validationDetail) {
        if (!this.validationError)
            this.validationError = [];

        if (!this.validationError.includes(validationDetail)) {
            this.validationError.push(validationDetail);
        }
    }

    compressClick() {
        this.showHide = !this.showHide;
    }

    disableFieldBase(fieldname: string, index: number = null, concessionRelationship: ConcessionRelationshipDetail = null) {
        switch (fieldname) {
            case 'errorMessage':
                return (this.errorMessage) && !this.isLoading;
            case 'validationError':
                return (this.validationError) && !this.isLoading;
            case 'saveMessage':
                return this.saveMessage && !this.isLoading;
            case 'warningMessage':
                return this.warningMessage && !this.isLoading;
            case 'sntDealNumber':
                return (this.isRecalling || this.canEdit) ? null : '';
            case 'motivationEnabled':
                return this.motivationEnabled ? null : '';
            case 'saveDisable':
                return this.saveMessage ? '' : null;
            case 'Comments':
                return this.canBcmApprove || this.canPcmApprove || this.canApproveChanges;
            case 'newConcession':
                return this.canPcmApprove || this.isEditing || this.isRecalling;
            case 'viewConcessionTableCanEdit':
                return this.canEdit ? null : '';
            case 'concessionItemRowsDelete':
                return this.glmsConcessionForm.controls.concessionItemRows.value.length > 1 && !this.saveMessage;
            case 'hasNoConditions':
                return this.glmsConcessionForm.controls.conditionItemsRows.value.length == 0;
            case 'hasConditions':
                return this.glmsConcessionForm.controls.conditionItemsRows.value.length > 0;
            case 'archiveDelete':
                return this.glmsConcessionForm.controls.concessionItemRows.value.length > 1;
            case 'noCommentsAdded':
                return !this.glmsConcession.concession.concessionComments || this.glmsConcession.concession.concessionComments.length == 0;
            case 'noRelatedConcessions':
                return !this.glmsConcession.concession.concessionRelationshipDetails || this.glmsConcession.concession.concessionRelationshipDetails.length == 0;
            case 'ProductType':
                return this.selectedConditionTypes[index] != null;
            case 'interestRateDisable':
                return this.selectedConditionTypes[index] != null && this.selectedConditionTypes[index].enableInterestRate ? null : '';
            case 'volumeDisable':
                return this.selectedConditionTypes[index] != null && this.selectedConditionTypes[index].enableConditionVolume ? null : '';
            case 'valueDisable':
                return this.selectedConditionTypes[index] != null && this.selectedConditionTypes[index].enableConditionValue ? null : '';
            case 'addCondition':
                return this.canBcmApprove || this.canPcmApprove || this.isEditing || this.isRecalling;
            case 'parentReferenceCheck':
                return concessionRelationship.parentConcessionReference == this.concessionReferenceId;
            case 'childReferenceCheck':
                return concessionRelationship.childConcessionReference == this.concessionReferenceId;
            case 'deleteConcessionRow':
                return (this.canBcmApprove || this.canPcmApprove || this.isEditing || this.isRecalling) && this.glmsConcessionForm.controls.concessionItemRows.value.length > 1;
            case 'canRecall':
                return this.canRecall && !this.isRecalling;
            default:
                break;
        }
    }
}
