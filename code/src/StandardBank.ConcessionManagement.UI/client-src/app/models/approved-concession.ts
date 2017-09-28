import { ApprovedConcessionDetail } from "./approved-concession-detail";

export class ApprovedConcession {
    riskGroupName: string;
    riskGroupNumber: number;
    concessionId: number;
    referenceNumber: string;
    approvedConcessionDetails: ApprovedConcessionDetail[];
}
