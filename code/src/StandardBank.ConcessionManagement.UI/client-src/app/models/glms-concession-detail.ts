import { BaseConcessionDetail } from "./base-concession-detail";
import { GlmsTierData } from "./glms-tier-data";

export class GlmsConcessionDetail extends BaseConcessionDetail {

    glmsConcessionDetailId: number;
    legalEntity: string;   
    glmsProduct: string;
    glmsProductType: string;
    interestPricingCategoryId: number;
    glmsGroupId: number;
    interestTypeId: number;
    rateTypeId: number;
    slabTypeId: number;  
    tieredFrom: number;
    tieredTo: number;
    baseRate: number;
    value: number;
    glmsTierData: GlmsTierData[];

}
