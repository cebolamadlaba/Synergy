import { RiskGroup } from "./risk-group";
import { CashConcession } from "./cash-concession";
import { CashFinancial } from "./cash-financial";
import { CashProduct } from "./cash-product";

export class CashView {
    riskGroup: RiskGroup;
    cashFinancial: CashFinancial;
    cashConcessions: CashConcession[];
    cashProducts: CashProduct[];
}
