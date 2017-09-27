import { BaseConcessionDetail } from "./base-concession-detail";

export class LendingConcessionDetail extends BaseConcessionDetail {
    lendingConcessionDetailId: number;
    productType: string;
    productTypeId: number;
    limit: number;
    averageBalance: number;
    term: number;
    loadedMap: number;
    approvedMap: number;
    marginAgainstPrime: number;
    initiationFee: number;
    reviewFeeType: string;
    reviewFeeTypeId: number;
    reviewFee: number;
    uffFee: number;
}
