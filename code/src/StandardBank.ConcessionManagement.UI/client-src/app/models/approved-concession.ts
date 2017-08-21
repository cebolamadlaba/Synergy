import { ApprovedConcessionDetail } from "./approved-concession-detail";

export class ApprovedConcession {
    riskGroupName: string;
    riskGroupNumber: number;
    concessionId: number;
    concessionReferenceNumber: string;
    concessionType: string;
    approvedConcessionDetails: ApprovedConcessionDetail[];
}
