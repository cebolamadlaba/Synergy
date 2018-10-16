import { RiskGroup } from "./risk-group";
import { CashConcession } from "./cash-concession";
import { CashFinancial } from "./cash-financial";
import { CashProductGroup } from "./cash-product-group";

export class CashView {
    riskGroup: RiskGroup;
    cashFinancial: CashFinancial;
    cashConcessions: CashConcession[];
    cashProductGroups: CashProductGroup[];
}
