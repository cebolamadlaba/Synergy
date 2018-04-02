import { Concession } from "./concession";
import { ConcessionCondition } from "./concession-condition";
import { User } from "./user";

export class BaseConcession {
    concession: Concession;
    concessionConditions: ConcessionCondition[];
    currentUser: User;
    primeRate: string;
}
