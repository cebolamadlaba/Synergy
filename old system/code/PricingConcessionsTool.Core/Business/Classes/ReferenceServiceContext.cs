using System;
using System.Collections.Generic;
using System.Linq;

using PricingConcessionsTool.Core.Business.Interfaces;
using PricingConcessionsTool.DTO.ReferenceData;
using PricingConcessionsTool.Core.Data;

namespace PricingConcessionsTool.Core.Business.Classes
{
    public class ReferenceServiceContext : DataContextBase, IReferenceServiceContext
    {
        public List<ConditionType> GetConditionTypes()
        {
            var list = new List<ConditionType>();

            using (var db = new PricingEntities())
            {
                var conditionTypes = db.rtblConditionTypes
                                       .Where(p => p.IsActive)
                                       .OrderBy(d => d.Description)
                                       .ToList();

                foreach (var item in conditionTypes)
                {
                    list.Add(new ConditionType { ConditionTypeId = item.pkConditionTypeId, Description = item.Description });
                }
            }

            return list;
        }

        public List<ConditionProduct> GetConditionProducts(int conditionTypeId)
        {
            var list = new List<ConditionProduct>();

            using (var db = new PricingEntities())
            {
                var conditionTypes = db.tblConditionTypeProducts
                                       .Where(p => p.IsActive && p.fkConditionTypeId == conditionTypeId && p.rtblConditionProduct.IsActive)
                                       .OrderBy(d => d.rtblConditionProduct.Description)
                                       .ToList();

                foreach (var item in conditionTypes)
                {
                    list.Add(new ConditionProduct { ConditionProductId = item.rtblConditionProduct.pkConditionProductId, Description = item.rtblConditionProduct.Description });
                }
            }
            return list;
        }

        public List<MarketSegment> GetMarketSegments()
        {
            var list = new List<MarketSegment>();

            using (var db = new PricingEntities())
            {
                var marketSegments = db.rtblMarketSegments
                                       .Where(p => p.IsActive)
                                       .OrderBy(d => d.Description)
                                       .ToList();

                foreach (var item in marketSegments)
                {
                    list.Add(new MarketSegment { MarketSegmentId = item.pkMarketSegmentId, Description = item.Description });
                }
            }

            return list;
        }

        public List<ProductType> GetProductTypes(int concessionTypeId)
        {
            var list = new List<ProductType>();

            using (var db = new PricingEntities())
            {
                var productTypes = db.rtblProducts
                                       .Where(p => p.IsActive && p.fkConcessionTypeId == concessionTypeId)
                                       .OrderBy(d => d.Description)
                                       .ToList();

                foreach (var item in productTypes)
                {
                    list.Add(new ProductType
                    {
                        ProductTypeId = item.pkProductId,
                        Description = item.Description
                    });
                }
            }

            return list;
        }

        public List<ReviewFeeType> ReviewFeeTypes()
        {
            var list = new List<ReviewFeeType>();

            using (var db = new PricingEntities())
            {
                var reviewFeeTypes = db.rtblReviewFeeTypes
                                       .Where(p => p.IsActive)
                                       .OrderBy(d => d.Description)
                                       .ToList();

                foreach (var item in reviewFeeTypes)
                {
                    list.Add(new ReviewFeeType { ReviewFeeTypeId = item.pkReviewFeeTypeId, Description = item.Description });
                }
            }

            return list;
        }

        public List<TransactionType> GetTransactionTypes(int concessionTypeId)
        {
            var list = new List<TransactionType>();

            using (var db = new PricingEntities())
            {
                var transactionTypes = db.rtblTransactionTypes
                                       .Where(p => p.IsActive && p.fkConcessionTypeId == concessionTypeId)
                                       .OrderBy(d => d.Description)
                                       .ToList();

                foreach (var item in transactionTypes)
                {
                    list.Add(new TransactionType { TransactionTypeId = item.pkTransactionTypeId, Description = item.Description });
                }
            }

            return list;
        }

        public List<ChannelType> GetChannelTypes()
        {
            var list = new List<ChannelType>();

            using (var db = new PricingEntities())
            {
                var channelTypes = db.rtblChannelTypes
                                       .Where(p => p.IsActive)
                                       .OrderBy(d => d.Description)
                                       .ToList();

                foreach (var item in channelTypes)
                {
                    list.Add(new ChannelType { ChannelTypeId = item.pkChannelTypeId, Description = item.Description });
                }
            }

            return list;
        }

        public List<BaseRate> GetBaseRates(int channelTypeId)
        {
            var list = new List<BaseRate>();

            using (var db = new PricingEntities())
            {
                var rates = db.tblChannelTypeBaseRates
                                       .Where(p => p.fkChannelTypeId == channelTypeId)
                                       .Select(br => br.rtblBaseRate)
                                       .OrderBy(d => d.BaseRate)
                                       .ToList();

                foreach (var item in rates)
                {
                    list.Add(new BaseRate { BaseRateId = item.pkBaseRateId, Rate = item.BaseRate });
                }
            }

            return list;
        }

        public List<BusinesOnlineTransactionType> GetBusinesOnlineTransactionTypes(int transactionGroupId)
        {
            var list = new List<BusinesOnlineTransactionType>();

            using (var db = new PricingEntities())
            {
                var transactionGroups = db.tblBusinesOnlineTransactionTypes
                                       .Where(p => p.IsActive && p.fkTransactionGroupId == transactionGroupId)
                                       .OrderBy(d => d.Description)
                                       .ToList();

                foreach (var item in transactionGroups)
                {
                    list.Add(new BusinesOnlineTransactionType { BusinesOnlineTransactionTypeId = item.pkBusinesOnlineTransactionTypeId, Description = item.Description });
                }
            }

            return list;
        }

        public List<TransactionGroup> GetTransactionGroups()
        {
            var list = new List<TransactionGroup>();

            using (var db = new PricingEntities())
            {
                var transactionGroups = db.rtblTransactionGroups
                                       .Where(p => p.IsActive)
                                       .OrderBy(d => d.Description)
                                       .ToList();

                foreach (var item in transactionGroups)
                {
                    list.Add(new TransactionGroup { TransactionGroupId = item.pkTransactionGroupId, Description = item.Description });
                }
            }

            return list;
        }

        public List<BusinesOnlineUser> GetBusinesOnlineUsers()
        {
            var list = new List<BusinesOnlineUser>();

            using (var db = new PricingEntities())
            {
                var users = db.tblBolUsers
                                       .Where(p => p.IsActive)
                                       .OrderBy(d => d.UserName)
                                       .ToList();

                foreach (var item in users)
                {
                    list.Add(new BusinesOnlineUser { BusinesOnlineUserId = item.pkBolUserId, UserName = item.UserName });
                }
            }

            return list;
        }
    }
}
