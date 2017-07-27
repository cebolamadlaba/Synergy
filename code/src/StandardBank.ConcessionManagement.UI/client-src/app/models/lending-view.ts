import { RiskGroup } from "./risk-group";
import { SourceSystemProduct } from "./source-system-product";
import { LendingConcession } from "./lending-concession";

export class LendingView {
    riskGroup: RiskGroup;
    totalExposure: number;
    weightedAverageMap: number;
    weightedCrsMrs: number;
    sourceSystemProducts: SourceSystemProduct[];
    lendingConcessions: LendingConcession[];
}
