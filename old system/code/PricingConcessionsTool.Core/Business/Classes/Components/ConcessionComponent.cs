using PricingConcessionsTool.Core.Data;
using PricingConcessionsTool.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.Core.Business.Classes.Components
{
    public abstract class ConcessionComponent
    {
        public Concession GetConcession(tblConcession tblConcession)
        {
            return ConcessionFactory.Create(tblConcession,null);
        }

       
        protected tblLegalEntity ConcessionCustomer(Concession concession, PricingEntities db)
        {
            tblLegalEntity tblLegalEntity = null;

            if (concession.Customer.Entity.CustomerId > 0)
            {
                tblLegalEntity = db.tblLegalEntities.Find(concession.Customer.Entity.CustomerId);
            }
            else
            {
                tblLegalEntity = new tblLegalEntity
                {
                    CustomerName = concession.Customer.Entity.CustomerName,
                    fkRiskGroupId = 0,
                    RiskGroupName = concession.Customer.RiskGroupName,
                    fkMarketSegmentId = concession.Customer.Entity.MarketSegment.MarketSegmentId,
                    CustomerNumber = concession.Customer.Entity.CustomerNumber,
                    IsActive = true
                };
            }

            if(!string.IsNullOrWhiteSpace(concession.Customer.AccountNumber))
            {
                concession.AccountList.Add(concession.Customer.AccountNumber);
            }
                        
            foreach (var item in concession.AccountList)
            {
                if (!tblLegalEntity.tblLegalEntityAccounts.Any(acc => acc.AccountNumber == item))
                {
                    tblLegalEntity.tblLegalEntityAccounts.Add(new tblLegalEntityAccount { AccountNumber = item, IsActive = true });
                }                
            }

            return tblLegalEntity;
        }

        protected tblConcession BaseConcession(Concession concession, PricingEntities db)
        {
            tblConcession tblConcession = null;

            if (concession.ConcessionId > 0)
            {
                tblConcession = db.tblConcessions.Find(concession.ConcessionId);
            }
            else
            {
                tblConcession = new tblConcession
                {
                    fkStatusId = (int)concession.Status,

                    fkSubStatusId = (int)concession.SubStatus,

                    Motivation = concession.Motivation,
                    fkConcessionTypeId = (int)concession.ConcessionType,

                    SMTDealNumber = concession.DealNumber,

                    fkRequestorId = concession.RequestorId==0? concession.User.UserId: concession.RequestorId,

                    DatesentForApproval = concession.DatesentForApproval,

                    ConcessionDate = concession.ConcessionDate.Value,

                    DateApproved = concession.DateApproved,

                    ConcessionRef = concession.ReferenceNumber,

                    fkPCMUserId = concession.PricingManagerId,

                    fkTypeId = (int)concession.Type,

                    IsActive = true,                   
                };
            }

            PopulateAccountList(concession, tblConcession);

            PopulateConditionList(concession, tblConcession);

            PopulateCentre(db,concession, tblConcession);

            PopulateCommentList(db,concession, tblConcession);

            return tblConcession;
        }

        private void PopulateCommentList(PricingEntities db, Concession concession, tblConcession tblConcession)
        {
            if (concession.CommentList == null)
                return;

            foreach (var comment in concession.CommentList.Where(c => c.ConcessionCommentId == 0))
            {
                tblConcession.tblConcessionComments.Add(new tblConcessionComment
                {
                    Comment = comment.Comment,                   
                    fkUserId = comment.User.UserId,
                    SystemDate = comment.SystemDate,
                    IsActive = true,
                });
            }
        }

        private void PopulateCentre(PricingEntities db, Concession concession, tblConcession tblConcession)
        {
            if (concession.CenterIId == 0)
            {
                var centre = db.tblCentreUsers.FirstOrDefault(u => u.tblUser.pkUserId == concession.User.UserId && u.IsActive).tblCentre;
                tblConcession.CentreId = centre.pkCentreId;
            }
            else
            {

                tblConcession.CentreId = concession.CenterIId;
            }
        }

        private static void PopulateAccountList(Concession concession, tblConcession tblConcession)
        {
            foreach (var item in concession.AccountList)
            {
                if (!tblConcession.tblConcessionAccounts.Any(acc => acc.AccountNumber == item))
                {
                    tblConcession.tblConcessionAccounts.Add(new tblConcessionAccount { AccountNumber = item, IsActive = true });
                }
            }
        }

        private static void PopulateConditionList(Concession concession, tblConcession tblConcession)
        {
            foreach (var condition in concession.ConditionList)
            {
                tblConcessionCondition tblConcessionCondition = null;

                if (condition.ConcessionConditionId == 0)
                {
                    tblConcessionCondition = new tblConcessionCondition
                    {
                        IsActive = true,
                        fkConditionProductId = condition.ConditionProduct.ConditionProductId,
                        fkConditionTypeId = condition.ConditionType.ConditionTypeId,
                        InterestRate = condition.InterestRate,
                        Value = condition.Value,
                        Volume = condition.Volume
                    };

                    tblConcession.tblConcessionConditions.Add(tblConcessionCondition);
                }
                else
                {
                    tblConcessionCondition = tblConcession.tblConcessionConditions.First(c => c.pkConcessionConditionId == condition.ConcessionConditionId);

                    tblConcessionCondition.fkConditionProductId = condition.ConditionProduct.ConditionProductId;
                    tblConcessionCondition.fkConditionTypeId = condition.ConditionType.ConditionTypeId;
                    tblConcessionCondition.InterestRate = condition.InterestRate;
                    tblConcessionCondition.Value = condition.Value;
                    tblConcessionCondition.Volume = condition.Volume;
                }
            }
        }

        public void UpdateConcessionRef(int concessionId)
        {
            using (var db = new PricingEntities())
            {
                var dbConcession = db.tblConcessions.Find(concessionId);

                dbConcession.ConcessionRef = ConcessionFactory.GenerateRef(dbConcession);

                db.SaveChanges();
            }

        }



        protected int? GetBCM(int fkRequestorId, PricingEntities db)
        {
            var centre = db.tblCentreUsers.FirstOrDefault(u => u.fkUserId == fkRequestorId);

            if (centre != null)
            {
                var manager = db.tblCentreBusinessManagers.FirstOrDefault(c => c.fkCentreId == centre.fkCentreId);

                return manager == null ? null : (int?)manager.fkUserId;
            }
            return null;
        }
    }
}
