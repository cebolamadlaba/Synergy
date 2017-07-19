using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.DTO
{
    public abstract class FinancialInfo
    {

    }

    public class FinancialInfoLending : FinancialInfo
    {
        public decimal? TotalExposure { get; set; }

        public decimal? WeightedAverageMAP { get; set; }

        public decimal? WeightedCRSOrMRS { get; set; }
    }

    public class FinancialInvestment : FinancialInfo
    {
        public decimal? TotalLiabilityBalance { get; set; }

        public decimal? WeightedAverageMTP { get; set; }

        public decimal? WeightedAverageNetMargin { get; set; }

    }

    public class FinancialMas : FinancialInfo
    {
        public decimal? TotalCreditTurnover { get; set; }

        public decimal? TotalDebitTurnover { get; set; }

        public decimal? WeightedAveragePrice { get; set; }

    }

    public class FinancialCash : FinancialInfo
    {
        public decimal? WeightedAverageBranchPrice { get; set; }

        public decimal? TotalCashCentreTurnover { get; set; }

        public decimal? TotalCashCentreVolume { get; set; }

        public decimal? TotalAutosafeCashTurnover { get; set; }

        public decimal? TotalAutosafeCashVolume { get; set; }

        public decimal? WeightedAverageCashCentrePrice { get; set; }

        public decimal? WeightedAverageAutoSafePrice { get; set; }

    }

    public class FinancialInfoBol : FinancialInfo
    {
        public decimal? TotalPayments { get; set; }

        public decimal? TotalCollections { get; set; }

        public decimal? TotalValueAddedTransactions { get; set; }

        public decimal? WeightedAveragePrice { get; set; }
    }

    public class FinancialInfoTransactional : FinancialInfo
    {
        public decimal? TotalChequeIssuingVolume { get; set; }

        public decimal? TotalChequeDepositVolume { get; set; }

        public decimal? TotalChequeEncashmentVolume { get; set; }

        public decimal? TotalChequeEncashmentValue { get; set; }

        public decimal? TotalCashWithdrawalVolume { get; set; }

        public decimal? TotalCashWithdrawalValue { get; set; }

        public decimal? AverageChequeIssuingValue { get; set; }

        public decimal? AverageChequeIssuingPrice { get; set; }

        public decimal? AverageChequeDepositValue { get; set; }

        public decimal? AverageChequeDepositPrice { get; set; }

        public decimal? AverageChequeEncashmentPrice { get; set; }

        public decimal? AverageChequeWithdrawalPrice { get; set; }


    }

    public class FinancialInfoTrade : FinancialInfo
    {
        public decimal ? TotalTTVolume { get; set; }
        public decimal? TotalTTValue { get; set; }
        public decimal? TotalLCVolume { get; set; }
        public decimal? TotalLCValue { get; set; }
        public decimal? TotalFBCVolume { get; set; }
        public decimal? TotalFBCValue { get; set; }
        public decimal? AverageTTValue { get; set; }
        public decimal? AverageTTPrice { get; set; }        
        public decimal? AverageLCValue { get; set; }
        public decimal? AverageLCPrice { get; set; }

    }

}
