import { TradeConcessionDetail } from "./trade-concession-detail";
import { BaseConcession } from "./base-concession";

export class TradeConcession extends BaseConcession {
    tradeConcessionDetails: TradeConcessionDetail[];
}
