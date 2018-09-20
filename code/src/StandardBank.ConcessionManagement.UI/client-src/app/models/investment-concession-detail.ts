import { BaseConcessionDetail } from "./base-concession-detail";

export class InvestmentConcessionDetail extends BaseConcessionDetail {
    investmentConcessionDetailId: number;

    legalEntity: string;   
  
    investmentProduct: string;
    investmentProductType: string;
    approvedRate: number;
    loadedRate :number;
    communication: string;
    establishmentFee: number;
    flatFee: number;
    gbbNumber: string;
    max: number;
    min: number;
    term: number;
    fkInvestmentProductId: number;
    investmentProductTypeID: number;

    fkLegalEntityAccountId: number;  
    fkLegalEntityGBBNumber: number;

    adValorem: number;
    currency: number;

    disablecontrolset: boolean;
}
