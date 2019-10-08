import { RiskGroup } from "./risk-group";
import { LegalEntity } from './legal-entity';
import { LendingConcession } from "./lending-concession";
import { LendingProductGroup } from "./lending-product-group";
import { LendingFinancial } from "./lending-financial";

export class LendingView {
    riskGroup: RiskGroup;
    legalEntity: LegalEntity;
    lendingFinancial: LendingFinancial;
    lendingConcessions: LendingConcession[];
    lendingProductGroups: LendingProductGroup[];
}
