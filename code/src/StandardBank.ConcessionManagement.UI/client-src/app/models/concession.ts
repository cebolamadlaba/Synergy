export class Concession {
    id: number;
    referenceNumber: string;
    riskGroupId: number;
    riskGroupNumber: number;
    riskGroupName: string;
    type: string;
    dateOpened: Date;
    seqment: string;
    dateSentForApproval: Date;
    accountNumber: string;
    concessionType: string;
    smtDealNumber: string;
    motivation: string;
    mrsCrs: number;
    status: string;
    subStatus: string;
    comments: string;
    bcmUserId: number;
    pcmUserId: number;
    hoUserId: number;
    canExtend: boolean;
    canRenew: boolean;
}
