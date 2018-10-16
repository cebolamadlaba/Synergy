import { InvestmentConcessionDetail } from "./investment-concession-detail";
import { BaseConcession } from "./base-concession";

export class InvestmentConcession extends BaseConcession {
    investmentConcessionDetails: InvestmentConcessionDetail[];
}
