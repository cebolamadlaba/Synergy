import { Concession } from "./concession";

export class UserConcessions {
    pendingConcessionsCount: number;
    dueForExpiryConcessionsCount: number;
    expiredConcessionsCount: number;
    mismatchedConcessionsCount: number;
    declinedConcessionsCount: number;

    pendingConcessions: Concession[];
    dueForExpiryConcessions: Concession[];
    expiredConcessions: Concession[];
    mismatchedConcessions: Concession[];
    declinedConcessions: Concession[];
}
