using PricingConcessionsTool.DTO;
using PricingConcessionsTool.DTO.Enums;
namespace PricingConcessionsTool.Core.Business.Classes
{
    public static class FinancialInfoFactory
    {
        public static FinancialInfo Create(int customerId, ConcessionTypes concessionType)
        {
            FinancialInfo result = null;

            switch (concessionType)
            {
                case ConcessionTypes.NotSet:
                    break;
                case ConcessionTypes.Lending:
                    result = new FinancialInfoLending
                    {
                        TotalExposure = 1,
                        WeightedAverageMAP = 2,
                        WeightedCRSOrMRS = 3
                    };
                    break;
                case ConcessionTypes.Investment:
                    result = new FinancialInvestment
                    {
                        TotalLiabilityBalance = 1,
                        WeightedAverageMTP = 2,
                        WeightedAverageNetMargin = 3

                    };
                    break;
                case ConcessionTypes.Mas:
                    result = new FinancialMas
                    {
                        TotalCreditTurnover = 1,
                        TotalDebitTurnover = 2,
                        WeightedAveragePrice = 3
                    };
                    break;
                case ConcessionTypes.Trade:
                    result = new FinancialInfoTrade
                    {
                        AverageLCPrice = 1,
                        AverageLCValue = 2,
                        AverageTTPrice = 3,
                        AverageTTValue = 4,
                        TotalFBCValue = 5,
                        TotalFBCVolume = 6,
                        TotalLCValue = 7,
                        TotalLCVolume = 8,
                        TotalTTValue = 9,
                        TotalTTVolume = 10
                    };
                    break;
                case ConcessionTypes.Transactional:
                    result = new FinancialInfoTransactional
                    {
                        AverageChequeDepositPrice = 1,
                        AverageChequeDepositValue = 2,
                        AverageChequeEncashmentPrice = 3,
                        AverageChequeIssuingPrice = 4,
                        AverageChequeWithdrawalPrice = 5,
                        AverageChequeIssuingValue = 6,
                        TotalCashWithdrawalValue = 7,
                        TotalCashWithdrawalVolume = 8,
                        TotalChequeDepositVolume = 9,
                        TotalChequeEncashmentValue = 10,
                        TotalChequeIssuingVolume = 11

                    };
                    break;
                case ConcessionTypes.Bol:
                    result = new FinancialInfoBol
                    {
                        TotalCollections = 1,
                        TotalPayments = 2,
                        TotalValueAddedTransactions = 3,
                        WeightedAveragePrice = 4
                    };
                    break;
                case ConcessionTypes.Cash:
                    result = new FinancialCash
                    {
                        TotalAutosafeCashTurnover = 1,
                        TotalAutosafeCashVolume = 2,
                        TotalCashCentreTurnover = 3,
                        TotalCashCentreVolume = 4,
                        WeightedAverageAutoSafePrice = 5,
                        WeightedAverageBranchPrice = 6,
                        WeightedAverageCashCentrePrice = 7

                    };
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}