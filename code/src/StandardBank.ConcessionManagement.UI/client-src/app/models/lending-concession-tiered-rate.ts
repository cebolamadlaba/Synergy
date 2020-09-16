
export class LendingConcessionTieredRate {

    id: number;
    concessionLendingId: number;
    limit: number;
    marginToPrime: number;
    approvedMap: number;

    limitString: string;
    marginToPrimeString: string;
    approvedMapString: string;
}
