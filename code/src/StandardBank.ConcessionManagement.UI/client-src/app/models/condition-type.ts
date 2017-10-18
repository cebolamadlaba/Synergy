import { ConditionProduct } from "./condition-product";

export class ConditionType {
    id: number;
    description: string;
    conditionProducts: ConditionProduct[];
    enableInterestRate: boolean;
    enableConditionVolume: boolean;
    enableConditionValue: boolean;
    enableExpectedTurnoverValue: boolean;
}
