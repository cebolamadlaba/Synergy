
import { RiskGroup } from "./risk-group";
import { TradeConcession } from "./trade-concession";
import { TradeProduct } from "./trade-product";
import { TradeFinancial } from "./trade-financial";

export class TradeView {
    riskGroup: RiskGroup;
    tradeFinancial: TradeFinancial;
    tradeConcessions: TradeConcession[];
    tradeProducts: TradeProduct[];
}

