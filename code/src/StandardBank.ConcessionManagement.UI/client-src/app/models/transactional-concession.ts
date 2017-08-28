import { TransactionalConcessionDetail } from "./transactional-concession-detail";
import { BaseConcession } from "./base-concession";

export class TransactionalConcession extends BaseConcession {
    transactionalConcessionDetails: TransactionalConcessionDetail[];
}
