import { RiskGroup } from "./risk-group";
import { TransactionalConcession } from "./transactional-concession";
import { TransactionalFinancial } from "./transactional-financial";
import { TransactionalProduct } from "./transactional-product";
import { LegalEntity } from "../models/legal-entity";

export class TransactionalView {
    riskGroup: RiskGroup;
    legalEntity: LegalEntity;
    transactionalConcessions: TransactionalConcession[];
    transactionalFinancial: TransactionalFinancial;
    transactionalProducts: TransactionalProduct[];
}
