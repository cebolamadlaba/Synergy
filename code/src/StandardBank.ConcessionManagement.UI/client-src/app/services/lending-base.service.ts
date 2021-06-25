import { Injectable } from '@angular/core';
import { ConditionType } from "../models/condition-type";
import { LendingConcessionDetail } from "../models/lending-concession-detail";
import { ProductTypeFieldLogic } from '../models/product-type-field-logic';

@Injectable()
export class LendingBaseService {
    validationError: String[];

    constructor() { }

    setProductTypeFieldLogic(productType: string, selectedProductTypeFieldLogic: ProductTypeFieldLogic): ProductTypeFieldLogic {
        if (productType === "Overdraft") {
            selectedProductTypeFieldLogic.termIsEnabled = true;
            selectedProductTypeFieldLogic.reviewFeeTypeIsEnabled = true;
            selectedProductTypeFieldLogic.reviewFeeIsEnabled = true;
            selectedProductTypeFieldLogic.uffFeeIsEnabled = true;
            selectedProductTypeFieldLogic.frequencyIsEnabled = false;
            selectedProductTypeFieldLogic.serviceFeeIsEnabled = false;
        }
        else if (productType === "Temporary Overdraft") {
            selectedProductTypeFieldLogic.termIsEnabled = true;
            selectedProductTypeFieldLogic.reviewFeeTypeIsEnabled = true;
            selectedProductTypeFieldLogic.reviewFeeIsEnabled = true;
            selectedProductTypeFieldLogic.uffFeeIsEnabled = true;
            selectedProductTypeFieldLogic.frequencyIsEnabled = false;
            selectedProductTypeFieldLogic.serviceFeeIsEnabled = false;
        }
        else if (productType.indexOf("VAF") == 0) {
            selectedProductTypeFieldLogic.termIsEnabled = true;
            selectedProductTypeFieldLogic.reviewFeeTypeIsEnabled = false;
            selectedProductTypeFieldLogic.reviewFeeIsEnabled = false;
            selectedProductTypeFieldLogic.uffFeeIsEnabled = false;
            selectedProductTypeFieldLogic.frequencyIsEnabled = true;
            selectedProductTypeFieldLogic.serviceFeeIsEnabled = true;
        }
        else {
            selectedProductTypeFieldLogic.termIsEnabled = true;
            selectedProductTypeFieldLogic.reviewFeeTypeIsEnabled = false;
            selectedProductTypeFieldLogic.reviewFeeIsEnabled = false;
            selectedProductTypeFieldLogic.uffFeeIsEnabled = false;
            selectedProductTypeFieldLogic.frequencyIsEnabled = false;
            selectedProductTypeFieldLogic.serviceFeeIsEnabled = false;
        }
        return selectedProductTypeFieldLogic;
    }

    disableFieldBase(
        selectedConditionType: ConditionType,
        lendingConcessionDetail: LendingConcessionDetail,
        selectedProductTypeFieldLogic: ProductTypeFieldLogic,
        fieldname: string,
        canEdit: boolean,
        canSaveMessage: boolean
    ) {
        switch (fieldname) {
            case 'productType':
            case 'accountNumber':
            case 'limit':
            case 'term':
            case 'marginAgainstPrime':
            case 'initiationFee':
            case 'mrsEri':
                return (!canSaveMessage || canEdit) ? null : '';
            case 'reviewFeeType':
                return (selectedProductTypeFieldLogic.reviewFeeTypeIsEnabled && canEdit) ? null : '';
            case 'reviewFee':
                return (selectedProductTypeFieldLogic.reviewFeeIsEnabled && canEdit) ? null : '';
            case 'uffFee':
                return (selectedProductTypeFieldLogic.uffFeeIsEnabled && canEdit) ? null : '';
            case 'frequency':
                return (selectedProductTypeFieldLogic.frequencyIsEnabled && canEdit) ? null : '';
            case 'serviceFee':
                return (selectedProductTypeFieldLogic.serviceFeeIsEnabled && canEdit) ? null : '';
            case 'interestRate':
                return selectedConditionType != null && selectedConditionType.enableInterestRate ? null : '';
            case 'volume':
                return selectedConditionType != null && selectedConditionType.enableConditionVolume ? null : '';
            case 'value':
                return selectedConditionType != null && selectedConditionType.enableConditionValue ? null : '';
            case 'extensionFee':
                return '';
        }
    }


    disableTempOverDraftField(
        selectedProductTypeFieldLogic: ProductTypeFieldLogic,
        fieldname: string,
        canEdit: boolean,
        canSaveMessage: boolean

    ) {
        switch (fieldname) {
            case 'productType':
            case 'accountNumber':
                return (!canSaveMessage || canEdit) ? null : '';
            case 'limit':             
            case 'term':
                return selectedProductTypeFieldLogic.termIsEnabled = false;
            case 'marginAgainstPrime':
            case 'initiationFee':
            case 'mrsEri':
            case 'reviewFeeType':              
            case 'reviewFee':              
            case 'uffFee':               
            case 'frequency':               
            case 'serviceFee':
            case 'extensionFee':
                return selectedProductTypeFieldLogic.fieldIsEnabled == false;
        }
    }


    disableOverDraftField(
        selectedProductTypeFieldLogic: ProductTypeFieldLogic,
        fieldname: string

    ) {
        switch (fieldname) {
            case 'productType':
            case 'accountNumber':
            case 'limit':
            case 'term':
                return selectedProductTypeFieldLogic.termIsEnabled = false;
            case 'marginAgainstPrime':
            case 'initiationFee':
            case 'mrsEri':
            case 'reviewFeeType':
            case 'reviewFee':
            case 'uffFee':
            case 'frequency':
            case 'serviceFee':
            case 'extensionFee':
                return selectedProductTypeFieldLogic.fieldIsEnabled == false;
        }
    }


    addValidationError(validationDetail) {
        if (!this.validationError)
            this.validationError = [];

        this.validationError.push(validationDetail);
    }
}
