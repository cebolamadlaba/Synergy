import { SourceSystemConcession } from "./source-system-concession";
import { SourceSystemProduct } from "./source-system-product";
import { RiskGroup } from "./risk-group";

export class LendingView {
    riskGroup: RiskGroup;
    totalExposure: number;
    weightedAverageMap: number;
    weightedCrsMrs: number;
    sourceSystemProducts: SourceSystemProduct[];
    sourceSystemConcessions: SourceSystemConcession[];
}
