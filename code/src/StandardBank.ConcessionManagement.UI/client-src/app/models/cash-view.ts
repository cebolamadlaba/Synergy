import { RiskGroup } from "./risk-group";
import { CashConcession } from "./cash-concession";
import { CashFinancial } from "./cash-financial";
import { CashProductGroup } from "./cash-product-group";
import { LegalEntity } from "./legal-entity";

export class CashView {
    riskGroup: RiskGroup;
    legalEntity: LegalEntity;
    cashFinancial: CashFinancial;
    cashConcessions: CashConcession[];
    cashProductGroups: CashProductGroup[];
}
