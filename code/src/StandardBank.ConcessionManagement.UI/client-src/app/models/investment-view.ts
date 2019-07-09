
import { RiskGroup } from "./risk-group";
import { LegalEntity } from "../models/legal-entity";
import { InvestmentConcession } from "./investment-concession";
import { InvestmentProductGroup } from "./investment-product-group";
import { InvestmentFinancial } from "./investment-financial";

export class InvestmentView {
    riskGroup: RiskGroup;
    legalEntity: LegalEntity;
    investmentFinancial: InvestmentFinancial;
    investmentConcessions: InvestmentConcession[];
    investmentProductGroups: InvestmentProductGroup[];
}

