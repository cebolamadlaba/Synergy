import { LendingConcessionDetail } from "./lending-concession-detail";
import { BaseConcession } from "./base-concession";

export class LendingConcession extends BaseConcession {
    lendingConcessionDetails: LendingConcessionDetail[];
}
