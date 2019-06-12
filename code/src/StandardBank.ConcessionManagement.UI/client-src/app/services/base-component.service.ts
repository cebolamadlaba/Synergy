import { Injectable } from '@angular/core';

import { DecimalPipe } from '@angular/common';

declare var accounting: any;

@Injectable()
export class BaseComponentService {

    constructor() { }

    public HasDuplicateConcessionAccountProduct(concessionDetails: any[], productTypeId: number, legalEntityId: number, legalEntityAccountId: number): boolean {
        let duplicates = concessionDetails.filter((item) => {
            return item.productTypeId == productTypeId
                && item.legalEntityId == legalEntityId
                && item.legalEntityAccountId == legalEntityAccountId;
        });

        return duplicates.length > 1;
    }

    public HasDuplicateConcessionAccountChannel(concessionDetails: any[], channelTypeId: number, legalEntityId: number, legalEntityAccountId: number): boolean {
        let duplicates = concessionDetails.filter((item) => {
            return item.channelTypeId == channelTypeId
                && item.legalEntityId == legalEntityId
                && item.legalEntityAccountId == legalEntityAccountId;
        });

        return duplicates.length > 1;
    }

    public HasDuplicateConcessionAccountTransaction(concessionDetails: any[], transactionTypeId: number, legalEntityId: number, legalEntityAccountId: number): boolean {
        let duplicates = concessionDetails.filter((item) => {
            return item.transactionTypeId == transactionTypeId
                && item.legalEntityId == legalEntityId
                && item.legalEntityAccountId == legalEntityAccountId;
        });

        return duplicates.length > 1;
    }

    public HasDuplicateConcessionAccountChargeCode(concessionDetails: any[], fkChargeCodeId: number, legalEntityId: number, legalEntityAccountId: number): boolean {
        let duplicates = concessionDetails.filter((item) => {
            return item.fkChargeCodeId == fkChargeCodeId
                && item.legalEntityId == legalEntityId
                && item.legalEntityAccountId == legalEntityAccountId;
        });

        return duplicates.length > 1;
    }

    public HasDuplicateConcessionAccountTradeProduct(concessionDetails: any[], tradeProductTypeID: number, legalEntityId: number, legalEntityAccountId: number): boolean {
        let duplicates = concessionDetails.filter((item) => {
            return item.fkTradeProductId == tradeProductTypeID
                && item.legalEntityId == legalEntityId
                && item.legalEntityAccountId == legalEntityAccountId;
        });

        return duplicates.length > 1;
    }

    public unformat(itemValue: number) {
        return accounting.unformat(itemValue);
    }

    public formatDecimal(itemValue: number) {

        if (itemValue != null) {
            itemValue = this.unformat(itemValue);
            return new DecimalPipe('en-US').transform(itemValue, '1.2-2');
        }

        return 0.00;
    }

    public formatDecimalThree(itemValue: number) {

        if (itemValue != null) {
            itemValue = this.unformat(itemValue);
            return new DecimalPipe('en-US').transform(itemValue, '1.3-3');
        }

        return null;
    }
}
