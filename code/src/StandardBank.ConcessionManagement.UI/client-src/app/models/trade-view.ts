
import { RiskGroup } from "./risk-group";
import { TradeConcession } from "./trade-concession";
import { TradeProductGroup } from "./trade-product-group";
import { TradeFinancial } from "./trade-financial";

export class TradeView {
    riskGroup: RiskGroup;
    tradeFinancial: TradeFinancial;
    tradeConcessions: TradeConcession[];
    tradeProductGroups: TradeProductGroup[];
}

