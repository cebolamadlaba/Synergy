import { CashConcessionDetail } from "./cash-concession-detail";
import { BaseConcession } from "./base-concession";

export class CashConcession extends BaseConcession {
    cashConcessionDetails: CashConcessionDetail[];
}
