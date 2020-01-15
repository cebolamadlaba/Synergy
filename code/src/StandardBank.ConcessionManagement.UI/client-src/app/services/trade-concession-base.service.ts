import { Injectable } from '@angular/core';

import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';

import { TradeProductType } from "../models/trade-product-type";
import { TradeConcessionDetail } from "../models/trade-concession-detail";

@Injectable()
export class TradeConcessionBaseService {

    constructor() { }

    disableFieldBase(
        isSelectedTradeConcessionDetail: boolean,
        tradeConcessionDetail: TradeConcessionDetail,
        tradeConcessionForm: FormGroup,
        rowIndex: number,
        fieldname: string,
        canEdit: boolean,
        isSaved: boolean) {

        let selectedProductType = this.getSelectedProductTypeBase(rowIndex, tradeConcessionForm);

        switch (fieldname) {
            case "producttype":
            case "product":
                return canEdit ? null : '';
            case "accountNumber":
                return !isSelectedTradeConcessionDetail && canEdit ? null : '';
            case "gbbnumberText":
                return isSelectedTradeConcessionDetail && canEdit ? null : '';
            case "term":
                return tradeConcessionDetail.show_term && canEdit ? null : '';
            case "advalorem":
            case "min":
            case "max":
                let disabled = false;

                if (selectedProductType.tradeProductType == TradeProductType.InwardTT ||
                    selectedProductType.tradeProductType == TradeProductType.OutwardTT) {
                    disabled = true;
                }

                let selectedTradeConcessionNotNull = isSelectedTradeConcessionDetail != null;

                if (isSaved)
                    return '';
                else if (selectedTradeConcessionNotNull && disabled)
                    return '';
                else
                    return null;
            case "communication":
                this.disableCommunicationFeeBase(tradeConcessionForm, rowIndex, canEdit);
                break;
            case "flatfee":
                return tradeConcessionDetail.show_flatfee && canEdit ? null : '';
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

    getSelectedProductTypeBase(rowIndex, tradeConcessionForm: FormGroup): TradeProductType {
        const control = <FormArray>tradeConcessionForm.controls['concessionItemRows'];

        let currentConcession = control.controls[rowIndex];

        let selectedProductType: TradeProductType = currentConcession.get('producttype').value;

        return selectedProductType;
    }
}
