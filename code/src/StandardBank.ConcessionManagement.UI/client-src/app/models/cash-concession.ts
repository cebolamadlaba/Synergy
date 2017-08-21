import { Concession } from "./concession";
import { CashConcessionDetail } from "./cash-concession-detail";
import { ConcessionCondition } from "./concession-condition";
import { User } from "./user";

export class CashConcession {
    concession: Concession;
    cashConcessionDetails: CashConcessionDetail[];
    concessionConditions: ConcessionCondition[];
    currentUser: User;
}
