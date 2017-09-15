import { RiskGroup } from "./risk-group";
import { TransactionalConcession } from "./transactional-concession";
import { TransactionalFinancial } from "./transactional-financial";
import { TransactionalProduct } from "./transactional-product";

export class TransactionalView {
    riskGroup: RiskGroup;
    transactionalConcessions: TransactionalConcession[];
    transactionalFinancial: TransactionalFinancial;
    transactionalProducts: TransactionalProduct[];
}
