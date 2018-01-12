import { ApprovedConcessionDetail } from "./approved-concession-detail";

export class ApprovedConcession {
    riskGroupName: string;
    riskGroupNumber: number;
    legalEntityId: number;
    customerName: string;
    segment: string;
    approvedConcessionDetails: ApprovedConcessionDetail[];
}
