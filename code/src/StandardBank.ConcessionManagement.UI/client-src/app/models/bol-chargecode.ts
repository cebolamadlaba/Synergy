import { RiskGroup } from "./risk-group";

export class BolChargeCode {
    chargeCodeId: number;
    chargeCodeTypeId: number;
    chargeCode: string;
    description: string;
    length: number;
    pkChargeCodeId: number;
    fkChargeCodeTypeId: number;
    isActive: boolean;
    IsNonUniversal: boolean;
    riskGroups: Array<RiskGroup>;
}
