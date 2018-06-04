import { BaseConcessionDetail } from "./base-concession-detail";

export class TradeConcessionDetail extends BaseConcessionDetail {
    tradeConcessionDetailId: number;

    legalEntity: string;
    loadedRate: string;
    approvedRate: string;
    TradeProduct: string;
    TradeProductType: string;
    ApprovedRate: number;
    LoadedRate :number;
    Communications: string;
    EstablishmentFee: number;
    FlatFee: number;
    GBBNumber: string;
    Max: number;
    Min: number;
    Term: number;
    fkTradeProductId: number;
    TradeProductTypeID: number;
    fkLegalEntityAccountId: number;
    adValorem: number;
    currency: number;

 
}
