import { BaseConcessionDetail } from "./base-concession-detail";

export class TradeConcessionDetail extends BaseConcessionDetail {
    tradeConcessionDetailId: number;

    legalEntity: string;   
  
    tradeProduct: string;
    tradeProductType: string;
    approvedRate: number;
    loadedRate :number;
    communication: string;
    establishmentFee: number;
    flatFee: number;
    gbbNumber: string;
    max: number;
    min: number;
    term: number;
    fkTradeProductId: number;
    tradeProductTypeID: number;

    fkLegalEntityAccountId: number;  
    fkLegalEntityGBBNumber: number;

    adValorem: number;
    currency: number;

    show_term: boolean;
    show_advalorem: boolean;
    show_min: boolean;
    show_max: boolean;
    show_communication: boolean;
    show_flatfee: boolean;

    disablecontrolset: boolean;
}
