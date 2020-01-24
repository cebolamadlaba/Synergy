
import { TradeProduct } from "../models/trade-product";

export class TradeProductType {

    public static readonly LocalGuarantee = "Local guarantee";
    public static readonly InwardTT = "Inward TT";
    public static readonly OutwardTT = "Outward TT";

    tradeProductTypeID: number;
    tradeProductType: string;
    products: TradeProduct[];
}
