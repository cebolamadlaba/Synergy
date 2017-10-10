import { ConcessionComment } from "./concession-comment";
import { ConcessionRelationship } from "./concession-relationship";
import { ConcessionRelationshipDetail } from "./concession-relationship-detail";
import { User } from "./user";

export class Concession {
    id: number;
    referenceNumber: string;
    childReferenceNumber: string;
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
    requestorId: number;
    bcmUserId: number;
    pcmUserId: number;
    hoUserId: number;
    canExtend: boolean;
    canRenew: boolean;
    concessionComments: ConcessionComment[];
    concessionRelationshipDetails: ConcessionRelationshipDetail[];
    requestor: User;
    statusDescription: string;
    canResubmit: boolean;
    canUpdate: boolean;
	canArchive: boolean;
}
