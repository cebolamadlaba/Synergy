import { RiskGroup } from "./risk-group";
import { BolConcession } from "./bol-concession";
import { BolProduct } from "./bol-product";
import { BolFinancial } from "./bol-financial";
import { BolProductGroup } from "./bol-product-group";
import { LegalEntity } from "./legal-entity";


export class BolView {
    riskGroup: RiskGroup;
    legalEntity: LegalEntity;
    bolFinancial: BolFinancial;
    bolConcessions: BolConcession[];
    bolProductGroups: BolProductGroup[];
}
