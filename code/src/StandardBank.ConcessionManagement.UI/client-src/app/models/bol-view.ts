import { RiskGroup } from "./risk-group";
import { BolConcession } from "./bol-concession";
import { BolProduct } from "./bol-product";
import { BolFinancial } from "./bol-financial";

export class BolView {
    riskGroup: RiskGroup;
    bolFinancial: BolFinancial;
    bolConcessions: BolConcession[];
    BolProducts: BolProduct[];
}