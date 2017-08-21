import { RiskGroup } from "./risk-group";
import { SourceSystemProduct } from "./source-system-product";
import { CashConcession } from "./cash-concession";

export class CashView {
    riskGroup: RiskGroup;
    sourceSystemProducts: SourceSystemProduct[];
    cashCentreTurnover: number;
    cashCentreVolume: number;
    cashCentrePrice: number;
    branchTurnover: number;
    branchVolume: number;
    branchPrice: number;
    autoSafeTurnover: number;
    autoSafeVolume: number;
    autoSafePrice: number;
    cashConcessions: CashConcession[];
}
