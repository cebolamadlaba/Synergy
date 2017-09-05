import { RiskGroup } from "./risk-group";
import { LendingConcession } from "./lending-concession";
import { LendingProduct } from "./lending-product";
import { LendingFinancial } from "./lending-financial";

export class LendingView {
    riskGroup: RiskGroup;
    lendingFinancial: LendingFinancial;
    lendingConcessions: LendingConcession[];
    lendingProducts: LendingProduct[];
}
