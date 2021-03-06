import { ApprovedConcessionDetail } from "./approved-concession-detail";

export class ApprovedConcession {
    riskGroupName: string;
    riskGroupNumber: number;
    legalEntityId: number;
    customerName: string;
    customerNumber: number;
    segment: string;
    approvedConcessionDetails: ApprovedConcessionDetail[];
}
