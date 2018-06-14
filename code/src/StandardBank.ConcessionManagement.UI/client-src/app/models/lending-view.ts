import { RiskGroup } from "./risk-group";
import { LendingConcession } from "./lending-concession";
import { LendingProductGroup } from "./lending-product-group";
import { LendingFinancial } from "./lending-financial";

export class LendingView {
    riskGroup: RiskGroup;
    lendingFinancial: LendingFinancial;
    lendingConcessions: LendingConcession[];
    lendingProductGroups: LendingProductGroup[];
}
