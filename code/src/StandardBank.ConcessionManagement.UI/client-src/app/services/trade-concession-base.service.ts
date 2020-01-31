import { Injectable } from '@angular/core';

import { FormGroup, FormArray, FormBuilder, Validators, FormControl, AbstractControl } from '@angular/forms';

import { TradeProductType } from "../models/trade-product-type";
import { TradeConcessionDetail } from "../models/trade-concession-detail";

@Injectable()
export class TradeConcessionBaseService {
    validationError: String[];

    constructor() { }

    disableFieldBase(
        isSelectedTradeConcessionDetail: boolean,
        tradeConcessionDetail: TradeConcessionDetail,
        tradeConcessionForm: FormGroup,
        rowIndex: number,
        fieldname: string,
        canEdit: boolean,
        isSaved: boolean) {

        let currentConcession = this.getCurrentConcesion(rowIndex, tradeConcessionForm);
        let selectedProductType = this.getFieldValueBase(currentConcession, 'producttype');
        let isLocalGuarantee = !(selectedProductType.tradeProductType == TradeProductType.InwardTT || selectedProductType.tradeProductType == TradeProductType.OutwardTT);

        switch (fieldname) {
            case "producttype":
            case "product":
                return canEdit ? null : '';
            case "accountNumber":
                return !isSelectedTradeConcessionDetail && canEdit ? null : '';
            case "gbbnumberText":
                return isSelectedTradeConcessionDetail && canEdit ? null : '';
            case "term":
                if (tradeConcessionDetail != null) {
                    return tradeConcessionDetail.show_term && canEdit ? null : '';
                }
                else {
                    return (!isSelectedTradeConcessionDetail) || isSaved ? '' : null;
                }
            case "advalorem":
            case "min":
            case "max":

                //disable:
                // if !canEdit or isSaved;
                // if not local guarantee.
                // if local guarantee and flat fee value not null or empty.

                if (!canEdit || isSaved) {
                    return '';
                }
                else if (!isLocalGuarantee) {
                    return '';
                }
                else if (isLocalGuarantee && this.isNullOrEmptyFlatFee(currentConcession)) {
                    return null;
                }
                else if (isLocalGuarantee && !this.isNullOrEmptyFlatFee(currentConcession)) {
                    return '';
                }
                else {
                    return null;
                }
            case "communication":
                this.disableCommunicationFeeBase(tradeConcessionForm, rowIndex, canEdit);
                break;
            case "flatfee":
                if (tradeConcessionDetail != null) {
                    return tradeConcessionDetail.show_flatfee && canEdit ? null : '';
                }
                else {
                    return canEdit ? null : '';
                }
            case "currency":
                return (!isSelectedTradeConcessionDetail) && canEdit ? null : '';
            case "estfee":
            case "rate":
                return (isSelectedTradeConcessionDetail) && canEdit ? null : '';
            case "approvedRate":
                return canEdit ? null : ''
            case "expiryDate":
                return !isSelectedTradeConcessionDetail && canEdit ? null : '';

        }
    }

    disableCommunicationFeeBase(tradeConcessionForm: FormGroup, rowIndex, canEdit: boolean) {
        const control = <FormArray>tradeConcessionForm.controls['concessionItemRows'];
        let currentrow = control.controls[rowIndex];

        let productype = currentrow.get('producttype').value;

        if (productype != null && productype.tradeProductType != "" && productype.tradeProductType != TradeProductType.OutwardTT) {
            currentrow.get('communication').disable();
            currentrow.get('communication').setValue(null);
            return true;
        }
        else {
            if (canEdit) {
                currentrow.get('communication').enable();
                return false;
            }
            else {
                currentrow.get('communication').disable();
                return true;
            }
        }
    }

    getCurrentConcesion(rowIndex, tradeConcessionForm: FormGroup) {
        const control = <FormArray>tradeConcessionForm.controls['concessionItemRows'];
        return control.controls[rowIndex];
    }

    getFieldValueBase(currentConcession: AbstractControl, fieldName: string) {
        return currentConcession.get(fieldName).value;
    }

    isNullOrEmptyFlatFee(currentConcession: AbstractControl) {
        let flatFeeValue = this.getFieldValueBase(currentConcession, 'flatfee');

        if (flatFeeValue == null)
            return true;

        return false;
    }

    addValidationError(validationDetail) {
        if (!this.validationError)
            this.validationError = [];

        this.validationError.push(validationDetail);
    }
}
