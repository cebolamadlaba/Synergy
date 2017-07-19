using PricingConcessionsTool.Core.Data;
using PricingConcessionsTool.DTO;
using PricingConcessionsTool.DTO.Enums;
using PricingConcessionsTool.DTO.ReferenceData;
using System.Collections.Generic;
using System.Linq;
using System;

namespace PricingConcessionsTool.Core.Business.Classes
{
    public class ConcessionFactory
    {
        public static Concession Create(tblConcession dbConcession, tblConcession previousVersion)
        {
            Concession concession = null;

            switch ((ConcessionTypes)dbConcession.fkConcessionTypeId)
            {
                case ConcessionTypes.Lending:

                    var tblConcessionLending = dbConcession.tblConcessionLendings.First();

                    var concessionLending = new ConcessionLending();

                    concessionLending.ProductType = new ProductType
                    {
                        ProductTypeId = tblConcessionLending.rtblProduct.pkProductId,
                        Description = tblConcessionLending.rtblProduct.Description
                    };
                    concessionLending.Limit = tblConcessionLending.Limit.Value;
                    concessionLending.Term = tblConcessionLending.Term;
                    concessionLending.MarginAbovePrime = tblConcessionLending.MarginToPrime;
                    concessionLending.ReviewFee = tblConcessionLending.ReviewFee;
                    concessionLending.UnutilizedFacilityFee = tblConcessionLending.UFFFee;
                    concessionLending.InitiationFee = tblConcessionLending.InitiationFee;
                    concessionLending.ApprovedMarginAbovePrime = tblConcessionLending.MarginToPrime;

                    if (previousVersion != null)
                    {
                        //Loaded
                        concessionLending.MarginAbovePrime = previousVersion.tblConcessionLendings.First().MarginToPrime;
                    }

                    if (tblConcessionLending.rtblReviewFeeType != null)
                    {
                        concessionLending.ReviewFeeType = new ReviewFeeType
                        {
                            ReviewFeeTypeId = tblConcessionLending.rtblReviewFeeType.pkReviewFeeTypeId,
                            Description = tblConcessionLending.rtblReviewFeeType.Description
                        };
                    }

                    concession = concessionLending;

                    break;
                case ConcessionTypes.Investment:

                    var tblConcessionInvestment = dbConcession.tblConcessionInvestments.FirstOrDefault();

                    var concessionInvestment = new ConcessionInvestment();

                    concessionInvestment.ProductType = new ProductType
                    {
                        ProductTypeId = tblConcessionInvestment.fkProductTypeId,
                        Description = tblConcessionInvestment.rtblProduct.Description
                    };
                    concessionInvestment.InterestToCustomer = tblConcessionInvestment.InterestToCustomer;
                    concessionInvestment.Term = tblConcessionInvestment.Term;
                    concessionInvestment.Balance = tblConcessionInvestment.Balance;
                    concessionInvestment.ApprovedInterestToCustomer = tblConcessionInvestment.InterestToCustomer;


                    if (previousVersion != null)
                    {
                        //Loaded
                        concessionInvestment.InterestToCustomer = previousVersion.tblConcessionInvestments.FirstOrDefault().InterestToCustomer;
                    }

                    concession = concessionInvestment;

                    break;

                case ConcessionTypes.Mas:

                    var tblConcessionMas = dbConcession.tblConcessionMas.First();

                    var concessionMas = new ConcessionMas();

                    concessionMas.TransactionType = new TransactionType
                    {
                        TransactionTypeId = tblConcessionMas.fkTransactionTypeId,
                        Description = tblConcessionMas.rtblTransactionType.Description
                    };
                    concessionMas.Turnover = tblConcessionMas.Turnover;
                    concessionMas.MerchantNumber = tblConcessionMas.MerchantNumber;
                    concessionMas.CommissionRate = tblConcessionMas.CommissionRate;
                    concessionMas.ApprovedCommissionRate = tblConcessionMas.CommissionRate;

                    if (previousVersion != null)
                    {
                        //Loaded
                        concessionMas.CommissionRate = previousVersion.tblConcessionMas.FirstOrDefault().CommissionRate;
                    }

                    concession = concessionMas;

                    break;

                case ConcessionTypes.Trade:

                    var tblConcessionTrade = dbConcession.tblConcessionTrades.First();

                    var concessionTrade = new ConcessionTrade();

                    concessionTrade.TransactionType = new TransactionType
                    {
                        TransactionTypeId = tblConcessionTrade.fkTransactionTypeId.Value,
                        Description = tblConcessionTrade.rtblTransactionType.Description
                    };

                    concessionTrade.BaseRate = new BaseRate
                    {
                        BaseRateId = tblConcessionTrade.fkBaseRateId.Value,
                        Rate = tblConcessionTrade.rtblBaseRate.BaseRate
                    };

                    concessionTrade.ChannelType = new ChannelType
                    {
                        ChannelTypeId = tblConcessionTrade.rtblChannelType.pkChannelTypeId,
                        Description = tblConcessionTrade.rtblChannelType.Description,
                    };

                    concessionTrade.Volume = tblConcessionTrade.TransactionVolume;
                    concessionTrade.Value = tblConcessionTrade.TransactionValue.Value;
                    concessionTrade.AdValorem = tblConcessionTrade.AdValorem;
                    concessionTrade.TableNumber = tblConcessionTrade.TableNumber;

                    concession = concessionTrade;

                    break;

                case ConcessionTypes.Bol:

                    var tblConcessionBol = dbConcession.tblConcessionBols.First();

                    var concessionBol = new ConcessionBol();

                    concessionBol.TransactionGroup = new  TransactionGroup
                    {
                        TransactionGroupId = tblConcessionBol.fkTransactionGroupId.Value,
                        Description = tblConcessionBol.rtblTransactionGroup.Description
                    };

                    concessionBol.BusinesOnlineTransactionType = new BusinesOnlineTransactionType
                    {
                        BusinesOnlineTransactionTypeId = tblConcessionBol.fkBusinesOnlineTransactionTypeId.Value,
                        Description = tblConcessionBol.tblBusinesOnlineTransactionType.Description,
                    };

                    concessionBol.BusinesOnlineUser = new BusinesOnlineUser
                    {
                        BusinesOnlineUserId = tblConcessionBol.BolUseId.Value,
                        UserName = tblConcessionBol.tblBolUser.UserName

                    };
                    concessionBol.BaseFee = tblConcessionBol.Fee;
                    concessionBol.Value = tblConcessionBol.TransactionValue;
                    concessionBol.Volume = tblConcessionBol.TransactionVolume;


                    //if (previousVersion != null)
                    //{
                    //    //Loaded
                    //    tblConcessionInvestment.InterestToCustomer = previousVersion.tblConcessionInvestments.FirstOrDefault().InterestToCustomer;
                    //}

                    concession = concessionBol;

                    break;


                case ConcessionTypes.Cash:

                    var tblConcessionCash = dbConcession.tblConcessionCashes.First();

                    var concessionCash = new ConcessionCash();

                    concessionCash.ChannelType = new ChannelType
                    {
                        ChannelTypeId = tblConcessionCash.fkChannelTypeId,
                        Description = tblConcessionCash.rtblChannelType.Description
                    };

                    concessionCash.BaseRate = new BaseRate
                    {
                        BaseRateId = tblConcessionCash.fkBaseRateId.Value,
                        Rate = tblConcessionCash.rtblBaseRate.BaseRate
                    };

                    concessionCash.Volume = tblConcessionCash.CashVolume;
                    concessionCash.Value = tblConcessionCash.CashValue;
                    concessionCash.AdValorem = tblConcessionCash.AdValorem;
                    concessionCash.TableNumber = tblConcessionCash.TableNumber;

                    concessionCash.ApprovedAdValorem = tblConcessionCash.AdValorem;
                    concessionCash.ApprovedBaseRate   = new BaseRate
                    {
                        BaseRateId = tblConcessionCash.fkBaseRateId.Value,
                        Rate = tblConcessionCash.rtblBaseRate.BaseRate
                    };

                    if (previousVersion != null)
                    {

                        concessionCash.ApprovedAdValorem = previousVersion.tblConcessionCashes.FirstOrDefault().AdValorem;
                        concessionCash.BaseRate = new BaseRate
                        {
                            BaseRateId = previousVersion.tblConcessionCashes.FirstOrDefault().fkBaseRateId.Value,
                            Rate = previousVersion.tblConcessionCashes.FirstOrDefault().rtblBaseRate.BaseRate
                        };
                    }

                    concession = concessionCash;

                    break;

                case ConcessionTypes.Transactional:

                    var tblConcessionTransactional = dbConcession.tblConcessionTransactionals.First();

                    var concessionTransactional = new ConcessionTransactional();

                    concessionTransactional.TransactionType = new TransactionType
                    {
                        TransactionTypeId = tblConcessionTransactional.fkTransactionTypeId.Value,
                        Description = tblConcessionTransactional.rtblTransactionType.Description
                    };

                    concessionTransactional.BaseRate = new BaseRate
                    {
                        BaseRateId = tblConcessionTransactional.fkBaseRateId.Value,
                        Rate = tblConcessionTransactional.rtblBaseRate.BaseRate
                    };

                    concessionTransactional.ChannelType = new ChannelType
                    {
                        ChannelTypeId = tblConcessionTransactional.rtblChannelType.pkChannelTypeId,
                        Description = tblConcessionTransactional.rtblChannelType.Description,
                    };

                    concessionTransactional.Volume = tblConcessionTransactional.TransactionVolume;
                    concessionTransactional.Value = tblConcessionTransactional.TransactionValue.Value;
                    concessionTransactional.AdValorem = tblConcessionTransactional.AdValorem;
                    concessionTransactional.TableNumber = tblConcessionTransactional.TableNumber;

                    concession = concessionTransactional;

                    break;

            }

            SetBaseConcession(concession, dbConcession);

            concession.ConditionList = CreatConditionList(dbConcession);

            concession.AccountList = CreateAccountList(dbConcession);

            concession.CommentList = CreateCommentList(dbConcession);

            return concession;

        }

        private static void SetBaseConcession(Concession concession, tblConcession dbConcession)
        {
            concession.ConcessionId = dbConcession.pkConcessionId;
            concession.ConcessionType = (ConcessionTypes)dbConcession.fkConcessionTypeId;
            concession.ConcessionDate = dbConcession.ConcessionDate;
            concession.Status = (ConcessionStatuses)dbConcession.fkStatusId;
            concession.SubStatus = (ConcessionSubStatuses)dbConcession.fkSubStatusId;
            concession.DealNumber = dbConcession.SMTDealNumber;
            concession.ReferenceNumber = dbConcession.ConcessionRef;
            concession.Motivation = dbConcession.Motivation;
            concession.RequestorId = dbConcession.fkRequestorId;
            concession.PricingManagerId = dbConcession.fkPCMUserId;
            concession.BusinessCentreManagerId = dbConcession.fkBCMUserId;
            concession.DatesentForApproval = dbConcession.DatesentForApproval;
            concession.DateApproved = dbConcession.ConcessionDate;
            concession.ExpiryDate = dbConcession.ExpiryDate;
            concession.Type =(Types)dbConcession.fkTypeId;
            concession.CenterIId = dbConcession.CentreId.Value;

            concession.Customer = new Customer
            {
                RiskGroupNumber = dbConcession.tblLegalEntity.tblRiskGroup.RiskGroupNumber,
                CustomerId = dbConcession.tblLegalEntity.pkLegalEntityId,
                IsNewCustomer = false,
                RiskGroupName = dbConcession.tblLegalEntity.RiskGroupName,
                CustomerName = dbConcession.tblLegalEntity.CustomerName,
                CustomerNumber = dbConcession.tblLegalEntity.CustomerNumber,

                Entity = new LegalEntity
                {
                    CustomerId = dbConcession.tblLegalEntity.pkLegalEntityId,
                    CustomerName = dbConcession.tblLegalEntity.CustomerName,
                    CustomerNumber = dbConcession.tblLegalEntity.CustomerNumber,
                    MarketSegment = new MarketSegment
                    {
                        MarketSegmentId = dbConcession.tblLegalEntity.fkMarketSegmentId,
                        Description = dbConcession.tblLegalEntity.rtblMarketSegment.Description
                    }
                }
            };

            concession.ConcessionTypeDescription = dbConcession.rtblConcessionType.Description;

            concession.ConcessionTypeCode = dbConcession.rtblConcessionType.Code;

            concession.StatusDescription = dbConcession.rtblStatu.Description;
            concession.SubStatusDescription = dbConcession.rtblSubStatu.Description;

            var days = DateTime.Now.Date.Subtract(concession.ConcessionDate.Value).Days;

            concession.TotalDaysOpen = days==0?1:days;

        }

        private static List<string> CreateAccountList(tblConcession dbConcession)
        {
            var list = new List<string>();

            dbConcession.tblConcessionAccounts.Where(c => c.IsActive).ToList().ForEach(acc =>
            {
                list.Add(acc.AccountNumber);
            });

            return list;
        }


        private static List<ConcessionComment> CreateCommentList(tblConcession dbConcession)
        {
            var list = new List<ConcessionComment>();

            if (dbConcession.tblConcessionComments == null)
                return list;

            foreach (var dbComment in dbConcession.tblConcessionComments)
            {
                list.Add(new ConcessionComment
                {
                    ConcessionCommentId = dbComment.pkConcessionCommentId,
                    Comment = dbComment.Comment,
                    User = new UserProfile
                    {
                        UserId = dbComment.fkUserId,
                        ANumber = dbComment.tblUser.ANumber,
                        FullName = dbComment.tblUser.Surname + " " + dbComment.tblUser.FirstName
                    },
                    SystemDate = dbComment.SystemDate
                });
            }

            return list;
        }


        private static List<ConcessionCondition> CreatConditionList(tblConcession dbConcession)
        {
            var list = new List<ConcessionCondition>();

            dbConcession.tblConcessionConditions.Where(c => c.IsActive).ToList().ForEach(condition =>
              {
                  list.Add(new ConcessionCondition
                  {
                      ConcessionConditionId = condition.pkConcessionConditionId,
                      ConditionType = new ConditionType
                      {
                          ConditionTypeId = condition.rtblConditionType.pkConditionTypeId,
                          Description = condition.rtblConditionType.Description
                      },
                      ConditionProduct = new ConditionProduct
                      {
                          ConditionProductId = condition.rtblConditionProduct.pkConditionProductId,
                          Description = condition.rtblConditionProduct.Description

                      },
                      InterestRate = condition.InterestRate,
                      Value = condition.Value,
                      Volume = condition.Volume.Value

                  });
              });
            return list;
        }

        public static string GenerateRef(tblConcession dbConcession)
        {
            char pad = '0';
            int totalWith = 12;

            switch ((ConcessionTypes)dbConcession.fkConcessionTypeId)
            {
                case ConcessionTypes.Lending:
                    return string.Format("L{0}", dbConcession.pkConcessionId.ToString().PadLeft(totalWith, pad));
                case ConcessionTypes.Cash:
                    return string.Format("C{0}", dbConcession.pkConcessionId.ToString().PadLeft(totalWith, pad));
                case ConcessionTypes.Bol:
                    return string.Format("B{0}", dbConcession.pkConcessionId.ToString().PadLeft(totalWith, pad));
                case ConcessionTypes.Mas:
                    return string.Format("M{0}", dbConcession.pkConcessionId.ToString().PadLeft(totalWith, pad));
                case ConcessionTypes.Investment:
                    return string.Format("I{0}", dbConcession.pkConcessionId.ToString().PadLeft(totalWith, pad));
                case ConcessionTypes.Trade:
                    return string.Format("F{0}", dbConcession.pkConcessionId.ToString().PadLeft(totalWith, pad));
                case ConcessionTypes.Transactional:
                    return string.Format("T{0}", dbConcession.pkConcessionId.ToString().PadLeft(totalWith, pad));
            }
            return string.Empty;
        }
    }
}
