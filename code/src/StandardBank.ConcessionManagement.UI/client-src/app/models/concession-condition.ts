export class ConcessionCondition {
    concessionConditionId: number;
    concessionId: number;
    conditionType: string;
    conditionTypeId: number;
    productType: string;
    conditionProductId: number;
    interestRate: number;
    conditionVolume: number;
    conditionValue: number;
    periodType: string;
    periodTypeId: number;
    period: string;
    periodId: number;
    expectedTurnoverValue: number;
    expiryDate: Date;
    riskGroupName: string;
    riskGroupNumber: number;
    concessionReferenceNumber: string;
    ragStatus: string;
    approvedDate: Date;
	concessionType: string;
    conditionMet: boolean;

    actualVolume: string;
    actualValue: string;
    actualTurnover: string;
}
