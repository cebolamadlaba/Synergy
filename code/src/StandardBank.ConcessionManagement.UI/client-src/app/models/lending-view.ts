import { RiskGroup } from "./risk-group";
import { LendingConcession } from "./lending-concession";
import { LendingProduct } from "./lending-product";

export class LendingView {
    riskGroup: RiskGroup;
    totalExposure: number;
    weightedAverageMap: number;
    weightedCrsMrs: number;
    lendingConcessions: LendingConcession[];
    lendingProducts: LendingProduct[];
}
