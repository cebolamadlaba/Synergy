import { BolConcessionDetail } from "./bol-concession-detail";
import { BaseConcession } from "./base-concession";

export class BolConcession extends BaseConcession {
    bolConcessionDetails: BolConcessionDetail[];
}
