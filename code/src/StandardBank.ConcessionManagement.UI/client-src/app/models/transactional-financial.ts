export class TransactionalFinancial {
    id: number;

    totalNumberOfAccounts: number;
    averageAccountManagementFee: number;
    averageMinimumMonthlyFee: number;

    totalChequeIssuingVolumes: number;
    totalChequeDepositVolumes: number;
    totalChequeEncashmentVolumes: number;
    totalChequeEncashmentValues: number;

    totalCashWithdrawalVolumes: number;
    totalCashWithdrawalValues: number;
    averageCashWithdrawalPrice: number;

    averageChequeIssuingValue: number;
    averageChequeIssuingPrice: number;
    averageChequeDepositValue: number;
    averageChequeDepositPrice: number;
    averageChequeEncashmentPrice: number;
}
