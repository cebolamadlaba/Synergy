﻿import { Concession } from "./concession";
import { LendingConcessionDetail } from "./lending-concession-detail";
import { ConcessionCondition } from "./concession-condition";
import { User } from "./user";

export class LendingConcession {
    concession: Concession;
    lendingConcessionDetails: LendingConcessionDetail[];
    concessionConditions: ConcessionCondition[];
    currentUser: User;
}
