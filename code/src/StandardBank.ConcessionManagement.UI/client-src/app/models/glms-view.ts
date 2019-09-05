
import { RiskGroup } from "./risk-group";
import { LegalEntity } from "../models/legal-entity";
import { GlmsConcession } from "./glms-concession";
import { GlmsFinancial } from "./glms-financial";
import { GlmsProductGroup } from "./glms-product-group";

export class GlmsView {
    riskGroup: RiskGroup;
    legalEntity: LegalEntity;
    GlmsFinancial: GlmsFinancial;
    glmsConcessions: GlmsConcession[];
    glmsProductGroups: GlmsProductGroup[];
}

