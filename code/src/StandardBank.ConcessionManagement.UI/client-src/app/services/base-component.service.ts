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

}
