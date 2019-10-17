import { BaseConcessionDetail } from "./base-concession-detail";
import { GlmsTierData } from "./glms-tier-data";
import { GlmsTierDataView } from "./glms-tier-data-view";

export class GlmsConcessionDetail extends BaseConcessionDetail {

    glmsConcessionDetailId: number;
    legalEntity: string;   
    glmsProduct: string;
    glmsProductType: string;
    interestPricingCategoryId: number;
    glmsGroupId: number;
    groupNumber: number;
    interestTypeId: number;
    rateTypeId: number;
    slabTypeId: number;  
    tieredFrom: number;
    tieredTo: number;
    baseRate: number;
    value: number;
    glmsTierData: GlmsTierData[];
    glmsTierDataView: GlmsTierDataView[];
}
