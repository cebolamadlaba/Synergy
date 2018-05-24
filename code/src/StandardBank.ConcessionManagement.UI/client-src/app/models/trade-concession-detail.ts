import { BaseConcessionDetail } from "./base-concession-detail";

export class TradeConcessionDetail extends BaseConcessionDetail {
    tradeConcessionDetailId: number;

    legalEntity: string;

    chargeCode: string;
    chargeCodeDesc: string;
    chargeCodeLength: number;
    loadedRate: string;
    approvedRate: string;
    fkChargeCodeId: number;
    fkLegalEntityBOLUserId: number;
}
