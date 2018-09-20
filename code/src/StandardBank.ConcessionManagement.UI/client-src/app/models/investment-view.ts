
import { RiskGroup } from "./risk-group";
import { InvestmentConcession } from "./investment-concession";
import { InvestmentProductGroup } from "./investment-product-group";
import { InvestmentFinancial } from "./investment-financial";

export class InvestmentView {
    riskGroup: RiskGroup;
    investmentFinancial: InvestmentFinancial;
    investmentConcessions: InvestmentConcession[];
    investmentProductGroups: InvestmentProductGroup[];
}

