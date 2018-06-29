export class ApprovedConcessionDetail {
    concessionId: number;
    concessionDetailId: number;
    referenceNumber: string;
    concessionType: string;
    status: string;
    dateOpened: Date;
    dateSentForApproval: Date;
    expiryDate: Date;
    dateApproved: Date;
    isSelected: boolean;
    concessionLetterURL: string;
}
