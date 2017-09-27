import { InboxConcession } from "./inbox-concession";

export class UserConcessions {
    pendingConcessionsCount: number;
    dueForExpiryConcessionsCount: number;
    expiredConcessionsCount: number;
    mismatchedConcessionsCount: number;
    declinedConcessionsCount: number;
    actionedConcessionsCount: number;

    pendingConcessions: InboxConcession[];
    dueForExpiryConcessions: InboxConcession[];
    expiredConcessions: InboxConcession[];
    mismatchedConcessions: InboxConcession[];
    declinedConcessions: InboxConcession[];
    actionedConcessions: InboxConcession[];

    showPendingConcessions: boolean;
    showDueForExpiryConcessions: boolean;
    showExpiredConcessions: boolean;
    showMismatchedConcessions: boolean;
    showDeclinedConcessions: boolean;
    showActionedConcessions: boolean;
}
