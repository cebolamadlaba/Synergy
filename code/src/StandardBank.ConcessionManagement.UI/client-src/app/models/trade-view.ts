
import { RiskGroup } from "./risk-group";
import { TradeConcession } from "./trade-concession";
import { TradeProductGroup } from "./trade-product-group";
import { TradeFinancial } from "./trade-financial";
import { LegalEntity } from "../models/legal-entity";

export class TradeView {
    riskGroup: RiskGroup;
    legalEntity: LegalEntity;
    tradeFinancial: TradeFinancial;
    tradeConcessions: TradeConcession[];
    tradeProductGroups: TradeProductGroup[];
}

