import { BaseConcessionDetail } from "./base-concession-detail";

export class InvestmentConcessionDetail extends BaseConcessionDetail {
    investmentConcessionDetailId: number;

    legalEntity: string;   
  
    investmentProduct: string;
    investmentProductType: string;
    approvedRate: number;
    loadedRate :number;  
  
    balance: number;
    noticeperiod: number;
    rate: number;

    fkInvestmentProductId: number;
    investmentProductTypeID: number;

    fkLegalEntityAccountId: number;  
    fkLegalEntityGBBNumber: number;   

    disablecontrolset: boolean;
}
