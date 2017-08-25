import { Concession } from "./concession";
import { ConcessionCondition } from "./concession-condition";
import { User } from "./user";
import { TransactionalConcessionDetail } from "./transactional-concession-detail";

export class TransactionalConcession {
    concession: Concession;
    transactionalConcessionDetails: TransactionalConcessionDetail[];
    concessionConditions: ConcessionCondition[];
    currentUser: User;
}
