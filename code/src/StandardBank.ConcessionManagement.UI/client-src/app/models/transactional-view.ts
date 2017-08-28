import { RiskGroup } from "./risk-group";
import { SourceSystemProduct } from "./source-system-product";
import { TransactionalConcession } from "./transactional-concession";

export class TransactionalView {
    riskGroup: RiskGroup;
    sourceSystemProducts: SourceSystemProduct[];
    transactionalConcessions: TransactionalConcession[];
    totalNumberOfAccounts: number;
    averageAccountManagementFee: number;
    averageMinimumMonthlyFee: number;
    totalChequeIssuingVolumes: number;
    totalChequeDepositVolumes: number;
    totalChequeEncashmentVolumes: number;
    totalCashWithdrawalVolumes: number;
    totalCashWithdrawalValues: number;
    averageChequeIssuingValue: number;
    averageChequeIssuingPrice: number;
    averageChequeDepositValue: number;
    averageChequeDepositPrice: number;
    averageChequeEncashmentPrice: number;
    averageCashWithdrawalPrice: number;
}
