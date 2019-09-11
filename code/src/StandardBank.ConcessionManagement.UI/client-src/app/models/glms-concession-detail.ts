import { BaseConcessionDetail } from "./base-concession-detail";

export class GlmsConcessionDetail extends BaseConcessionDetail {
    glmsConcessionDetailId: number;
    legalEntity: string;   
    glmsProduct: string;
    glmsProductType: string;
    interestPricingCategory: string;
    interestTypeId: number;
    rateType: number;
    slabType: number;  
    tieredFrom: number;
    tieredTo: number;
    baseRate: number;
    Value: number;

}
