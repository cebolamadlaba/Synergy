import { BaseConcessionDetail } from "./base-concession-detail";

export class BolConcessionDetail extends BaseConcessionDetail {
    bolConcessionDetailId: number;

    legalEntity: string;
    bOLUserId: string;
    chargeCode: string;
    chargeCodeDesc: string;
    chargeCodeLength: number;
    loadedRate: string;
    approvedRate: string; 
}
