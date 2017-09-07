import { Concession } from "./concession";

export class UserConcessions {
    pendingConcessionsCount: number;
    dueForExpiryConcessionsCount: number;
    expiredConcessionsCount: number;
    mismatchedConcessionsCount: number;
    declinedConcessionsCount: number;
    actionedConcessionsCount: number;

    pendingConcessions: Concession[];
    dueForExpiryConcessions: Concession[];
    expiredConcessions: Concession[];
    mismatchedConcessions: Concession[];
    declinedConcessions: Concession[];
    actionedConcessions: Concession[];

    showPendingConcessions: boolean;
    showDueForExpiryConcessions: boolean;
    showExpiredConcessions: boolean;
    showMismatchedConcessions: boolean;
    showDeclinedConcessions: boolean;
    showActionedConcessions: boolean;
}
