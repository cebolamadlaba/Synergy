export class ConcessionRelationshipDetail {
    relationshipType: string;
    parentConcessionId: number;
    parentConcessionReference: string;
    parentConcession: string;
    parentIsActive: boolean;
    relationship: string;
    childConcessionId: number;
    childConcessionReference: string;
    childConcession: string;
    childIsActive: boolean;
    date: Date;
    user: string;
}
