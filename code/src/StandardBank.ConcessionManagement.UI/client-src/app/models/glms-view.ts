
import { RiskGroup } from "./risk-group";
import { LegalEntity } from "../models/legal-entity";
import { GlmsConcession } from "./glms-concession";
import { GlmsProductGroup } from "./glms-product-group";

export class GlmsView {
    riskGroup: RiskGroup;
    legalEntity: LegalEntity;
    glmsConcessions: GlmsConcession[];
    glmsProductGroups: GlmsProductGroup[];
}

