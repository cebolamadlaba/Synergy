import { Injectable } from '@angular/core';

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
}
