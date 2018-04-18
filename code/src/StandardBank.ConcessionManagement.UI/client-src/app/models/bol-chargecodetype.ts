import { BolChargeCode } from "../models/bol-chargecode";

export class BolChargeCodeType
{
    pkChargeCodeTypeId: number;
    description: string;
    bolchargecodes: BolChargeCode[];
}
