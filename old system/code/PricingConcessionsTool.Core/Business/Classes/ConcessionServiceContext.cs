using PricingConcessionsTool.Core.Business.Classes.Components;
using PricingConcessionsTool.Core.Business.Interfaces;
using PricingConcessionsTool.Core.Data;
using PricingConcessionsTool.DTO;
using PricingConcessionsTool.DTO.Enums;
using PricingConcessionsTool.DTO.ReferenceData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PricingConcessionsTool.Core.Business.Classes
{
    public class ConcessionServiceContext : IConcessionServiceContext
    {
        List<IConcessionComponent> _components = null;
        IUserServiceContext _userServiceContext=null;

        public ConcessionServiceContext()
        {
            _components = new List<IConcessionComponent>();
            _components.Add(new ConcessionComponentLending());
            _components.Add(new ConcessionComponentCash());
            _components.Add(new ConcessionComponentInvestment());
            _components.Add(new ConcessionComponentMas());
            _components.Add(new ConcessionComponentBol());
            _components.Add(new ConcessionComponentTrade());
            _components.Add(new ConcessionComponentTransactional());
            _userServiceContext = new UserServiceContext();
        }

        public List<Concession> GetCustomerConcessions(int customerId, ConcessionTypes concessionType, bool pending, string username)
        {
            var list = new List<Concession>();

            using (var db = new PricingEntities())
            {
                var role = db.tblUserRoles.FirstOrDefault(u => u.IsActive && u.tblUser.ANumber == username).fkRoleId;

                var dbtblConcessions =
                    db.tblConcessions
                    .Where(r => r.IsActive && r.fkLegalEntityId == customerId
                                &&
                                (
                                (r.fkStatusId == (int)ConcessionStatuses.Pending && pending)
                                ||
                                 (r.fkStatusId == (int)ConcessionStatuses.Approved && !pending)
                                 &&
                                 (r.fkConcessionTypeId == (int)concessionType || concessionType == ConcessionTypes.NotSet)
                                ))
                    .ToList();

                foreach (var concession in dbtblConcessions)
                {
                    tblConcession prevtblConcession = null;

                    if (concession.fkStatusId == (int)ConcessionStatuses.Approved)
                    {
                        prevtblConcession = db.tblConcessions
                            .FirstOrDefault(con => con.ConcessionRef == concession.ConcessionRef && con.pkConcessionId != concession.pkConcessionId);
                    }
                   
                    var c = ConcessionFactory.Create(concession,prevtblConcession);
                    c.Requestor = _userServiceContext.GetUserProfileById(concession.fkRequestorId);
                    if (concession.fkBCMUserId.HasValue)
                        c.BusinessCentreManager = _userServiceContext.GetUserProfileById(concession.fkBCMUserId.Value);
                    list.Add(c);
                }
            }

            return list;
        }

        public RiskGroup GetRiskGroup(int riskGroupNumber)
        {
            var riskGroup = new RiskGroup
            {

            };

            using (var db = new PricingEntities())
            {
                var dbRiskGroup = db.tblRiskGroups.FirstOrDefault(r => r.RiskGroupNumber == riskGroupNumber);

                if (dbRiskGroup == null)
                {
                    // Problems
                }
                else
                {
                    riskGroup.RiskGroupName = dbRiskGroup.RiskGroupName;
                    riskGroup.RiskGroupNumber = dbRiskGroup.RiskGroupNumber;

                    dbRiskGroup.tblLegalEntities.ToList().ForEach(cus =>
                   {
                       riskGroup.EntityList.Add(
                           new LegalEntity
                           {
                               CustomerId = cus.pkLegalEntityId,
                               CustomerName = cus.CustomerName,
                               CustomerNumber = cus.CustomerNumber,
                               DisplayName =  string.Format("{0} - ({1})", cus.CustomerName, cus.CustomerNumber),

                               MarketSegment = new MarketSegment
                               {
                                   MarketSegmentId = cus.rtblMarketSegment.pkMarketSegmentId,
                                   Description = cus.rtblMarketSegment.Description
                               },
                               AccountList = GetAccounts(cus.tblLegalEntityAccounts)
                           });
                   });

                }
            }

            return riskGroup;
        }

        private List<string> GetAccounts(ICollection<tblLegalEntityAccount> tblLegalEntityAccounts)
        {
            var list = new List<string>();

            if (tblLegalEntityAccounts != null)
            {
                foreach (var acc in tblLegalEntityAccounts)
                {
                    list.Add(acc.AccountNumber);
                }
            }

            return list;
        }

        public Result SaveConcession(Concession concession)
        {
            var handler = _components.FirstOrDefault(h => h.CanHandle(concession.ConcessionType));

            if (handler == null)
            {
                throw new NotImplementedException(string.Format("Handler for {0} not implemented.", concession.ConcessionType));
            }

            //var objectValidationResult = Validate(concession);

            //if (!objectValidationResult.IsSuccessful)
            //{
            //    return objectValidationResult;
            //}

            var result = handler.SaveConcession(concession);

            if (result.ConcessionId > 0)
            {
                handler.UpdateConcessionRef(result.ConcessionId);
            }

            result.IsSuccessful = true;

            return result;
        }

        private Result Validate(Concession concession)
        {
            using (var db = new PricingEntities())
            {
                var record = db.tblScenarioManagerToolDeals.FirstOrDefault(smt => smt.DealNumber == concession.DealNumber);

                if (record == null)
                {
                    return new Result
                    {
                        IsSuccessful = false,
                        Message = string.Format("SMT (Scenario Manager Tool) Deal number: {0} doesn't exist", concession.DealNumber)
                    };
                }
                else
                {
                    return new Result
                    {
                        IsSuccessful = true
                    };
                }
            }
        }

        public Concession GetConcession(int concessionId)
        {
            using (var db = new PricingEntities())
            {
                var concession = db.tblConcessions.Find(concessionId);

                var result = ConcessionFactory.Create(concession,null);

                result.FinancialInfo = GetFinancialInfo(concession.fkLegalEntityId, result.ConcessionType);

                return result;
            }
        }

        public FinancialInfo GetFinancialInfo(int customerId, ConcessionTypes concessionType)
        {
            return FinancialInfoFactory.Create(customerId, concessionType);
        }

        public Result Decline(Concession concession)
        {
            int newSubStatusId = 0;

            int userId = 0;

            switch (concession.SubStatus)
            {
                case ConcessionSubStatuses.BCMPending:
                    newSubStatusId = (int)ConcessionSubStatuses.BCMDeclined;
                    break;
                case ConcessionSubStatuses.PCMPending:
                    newSubStatusId = (int)ConcessionSubStatuses.PCMDeclined;
                    break;
            }

            using (var db = new PricingEntities())
            {
                userId = db.tblUsers.FirstOrDefault(u => u.ANumber == concession.UserName).pkUserId;

                var dbConcession = db.tblConcessions.Find(concession.ConcessionId);

                dbConcession.fkSubStatusId = newSubStatusId;

                dbConcession.tblConcessionApprovals.Add(new tblConcessionApproval
                {
                    fkUserId = userId,
                    IsActive = true,
                    fkOldSubStatusId = (int)concession.SubStatus,
                    fkNewSubStatusId = newSubStatusId,
                    SystemDate = DateTime.Now
                });

                AddComment(concession, userId, dbConcession);

                db.SaveChanges();
            }

            return new Result
            {
                IsSuccessful = true,
                ConcessionId = concession.ConcessionId
            };

        }

        public Result Forward(Concession concession)
        {
            int newSubStatusId = 0;

            int userId = 0;

            switch (concession.SubStatus)
            {
                case ConcessionSubStatuses.BCMPending:
                    newSubStatusId = (int)ConcessionSubStatuses.PCMPending;
                    break;
            }

            using (var db = new PricingEntities())
            {
                userId = db.tblUsers.FirstOrDefault(u => u.ANumber == concession.UserName).pkUserId;

                var dbConcession = db.tblConcessions.Find(concession.ConcessionId);

                dbConcession.fkSubStatusId = newSubStatusId;

                dbConcession.fkPCMUserId = GetPCM(userId, db);

                dbConcession.tblConcessionApprovals.Add(new tblConcessionApproval
                {
                    fkUserId = userId,
                    IsActive = true,
                    fkOldSubStatusId = (int)concession.SubStatus,
                    fkNewSubStatusId = newSubStatusId,
                    SystemDate = DateTime.Now
                });
                AddComment(concession, userId, dbConcession);
                db.SaveChanges();
            }

            return new Result
            {
                IsSuccessful = true,
                ConcessionId = concession.ConcessionId
            };
        }

        private static void AddComment(Concession concession, int userId, tblConcession dbConcession)
        {
            if (!string.IsNullOrWhiteSpace(concession.NewComment))
            {
                dbConcession.tblConcessionComments.Add(new tblConcessionComment
                {
                    Comment = concession.NewComment,
                    fkConcessionSubStatusId = (int)concession.SubStatus,
                    fkUserId = userId,
                    IsActive = true,
                    SystemDate = DateTime.Now
                });

            }
        }

        private int? GetPCM(int userId, PricingEntities db)
        {
            return 3;
        }

        public Result Approve(Concession concession)
        {
            using (var db = new PricingEntities())
            {
                int newSubStatusId = 0;

                int userId = 0;

                if (concession.Type == Types.Removal)
                {
                    var dbConcession = db.tblConcessions.Find(concession.ConcessionId);

                    dbConcession.fkStatusId = (int)ConcessionStatuses.Removed;

                    if (concession.User.IsBCM)
                    {
                        newSubStatusId = (int)ConcessionSubStatuses.BCMApproved;
                    }
                    if (concession.User.IsPCM)
                    {
                        newSubStatusId = (int)ConcessionSubStatuses.PCMApproved;
                    }
                    if (concession.User.IsHOUser)
                    {
                        newSubStatusId = (int)ConcessionSubStatuses.HOApproved;

                    }

                    dbConcession.IsActive = false;

                    dbConcession.fkSubStatusId = newSubStatusId;

                    dbConcession.DateApproved = concession.DateApproved;

                    //Delete current record - This is the c concession

                    var dbActiveConcession = db.tblConcessions.FirstOrDefault(c => c.IsActive && c.ConcessionRef == concession.ReferenceNumber);

                    dbActiveConcession.IsActive = false;

                    db.SaveChanges();

                }
                else
                {
                    switch (concession.SubStatus)
                    {
                        case ConcessionSubStatuses.PCMPending:
                            newSubStatusId = (int)ConcessionSubStatuses.PCMApproved;
                            break;
                    }


                    userId = db.tblUsers.FirstOrDefault(u => u.ANumber == concession.UserName).pkUserId;

                    var dbConcession = db.tblConcessions.Find(concession.ConcessionId);

                    dbConcession.fkStatusId = (int)ConcessionStatuses.Approved;

                    dbConcession.fkSubStatusId = newSubStatusId;

                    dbConcession.DateApproved = concession.DateApproved;

                    dbConcession.ExpiryDate = concession.ExpiryDate;

                    dbConcession.fkPCMUserId = GetPCM(userId, db);


                    dbConcession.tblConcessionApprovals.Add(new tblConcessionApproval
                    {
                        fkUserId = userId,
                        IsActive = true,
                        fkOldSubStatusId = (int)concession.SubStatus,
                        fkNewSubStatusId = newSubStatusId,
                        SystemDate = DateTime.Now
                    });

                    AddComment(concession, userId, dbConcession);

                    if (concession.Type == Types.Existing)
                    {
                        var concessionToDelete = db.tblConcessions.Where(c => c.ConcessionRef == concession.ReferenceNumber && c.pkConcessionId != concession.ConcessionId);

                        foreach (var c in concessionToDelete)
                        {
                            c.IsActive = false;
                        }
                    }

                    db.SaveChanges();

                }
            }
            return new Result
            {
                IsSuccessful = true,
                ConcessionId = concession.ConcessionId
            };
        }

        public List<Concession> GetBCMConcessions(string username, bool pending)
        {
            var list = new List<Concession>();

            using (var db = new PricingEntities())
            {
                var user = db.tblUsers
                             .Where(u => u.ANumber == username && u.IsActive)
                             .FirstOrDefault();

                var dbtblConcessions = db.tblConcessions
                                        .Where
                                        (r => r.IsActive
                                        && r.fkBCMUserId == user.pkUserId
                                        &&
                                        (
                                        (r.fkSubStatusId == (int)ConcessionSubStatuses.BCMPending && pending)
                                        ||
                                         (r.fkSubStatusId != (int)ConcessionSubStatuses.BCMPending && !pending)

                                        )
                                        )
                                        .ToList();

                foreach (var concession in dbtblConcessions)
                {
                    var c = ConcessionFactory.Create(concession,null);
                    c.Requestor = _userServiceContext.GetUserProfileById(concession.fkRequestorId);
                    if (concession.fkBCMUserId.HasValue)
                        c.BusinessCentreManager = _userServiceContext.GetUserProfileById(concession.fkBCMUserId.Value);
                    list.Add(c);
                }

            }

            return list;
        }

        public List<Concession> GetRequestorConcessions(string username, bool pending)
        {
            var list = new List<Concession>();

            using (var db = new PricingEntities())
            {
                var user = db.tblUsers
                             .Where(u => u.ANumber == username && u.IsActive)
                             .FirstOrDefault();

                var dbtblConcessions = db.tblConcessions
                                        .Where
                                        (r => r.IsActive
                                        && r.fkRequestorId == user.pkUserId
                                        &&
                                        (
                                        (r.fkSubStatusId == (int)ConcessionSubStatuses.BCMPending && pending)
                                        ||
                                         (r.fkStatusId == (int)ConcessionStatuses.Approved && !pending)
                                           ||
                                          (r.fkStatusId == (int)ConcessionStatuses.ApprovedWithChanges && pending)
                                        ))
                                        .ToList();

                foreach (var concession in dbtblConcessions)
                {
                    var c = ConcessionFactory.Create(concession,null);
                    c.Requestor = _userServiceContext.GetUserProfileById(concession.fkRequestorId);
                    if (concession.fkBCMUserId.HasValue)
                        c.BusinessCentreManager = _userServiceContext.GetUserProfileById(concession.fkBCMUserId.Value);
                    list.Add(c);
                }

            }

            return list;
        }


        public List<Concession> GetPCMConcessions(string username, bool pending)
        {
            var list = new List<Concession>();

            using (var db = new PricingEntities())
            {
                var user = db.tblUsers
                             .Where(u => u.ANumber == username && u.IsActive)
                             .FirstOrDefault();

                var dbtblConcessions = db.tblConcessions
                                        .Where
                                        (r => r.IsActive
                                        && r.fkPCMUserId == user.pkUserId
                                        &&
                                        (
                                        (r.fkSubStatusId == (int)ConcessionSubStatuses.PCMPending && pending)
                                        ||
                                         (r.fkSubStatusId != (int)ConcessionSubStatuses.PCMPending && !pending)
                                        )
                                        )
                                        .ToList();

                foreach (var concession in dbtblConcessions)
                {
                    list.Add(ConcessionFactory.Create(concession,null));
                }

            }

            return list;
        }

        public Result ApproveWithChanges(Concession concession)
        {
            var userId = 0;
            var requestorId = 0;

            using (var db = new PricingEntities())
            {
                userId = db.tblUsers.FirstOrDefault(u => u.ANumber == concession.UserName).pkUserId;

                var dbConcession = db.tblConcessions.Find(concession.ConcessionId);

                dbConcession.IsActive = false;

                concession.RequestorANumber = dbConcession.tblUser.ANumber;

                requestorId = dbConcession.fkRequestorId;

                AddComment(concession, userId, dbConcession);

                db.SaveChanges();
            }

            var handler = _components.FirstOrDefault(h => h.CanHandle(concession.ConcessionType));

            if (handler == null)
            {
                throw new NotImplementedException(string.Format("Handler for {0} not implemented.", concession.ConcessionType));
            }

            concession.ConcessionId = 0;

            foreach (var condition in concession.ConditionList)
            {
                condition.ConcessionConditionId = 0;
            }

            concession.RequestorId = requestorId;

            concession.DateApproved = DateTime.Now;

            concession.Status = ConcessionStatuses.ApprovedWithChanges;

            concession.SubStatus = ConcessionSubStatuses.PCMApproved;

            if (!string.IsNullOrWhiteSpace(concession.NewComment))
            {
                concession.CommentList.Add(new ConcessionComment
                {
                    Comment = concession.NewComment,
                    SystemDate = DateTime.Now,
                    User = new UserProfile { UserId = userId },
                    SubStatus = concession.SubStatus
                });

            }

            foreach (var comment in concession.CommentList)
            {
                comment.ConcessionCommentId = 0;
            }

            var result = handler.SaveConcession(concession);

            result.IsSuccessful = true;
            result.ConcessionId = result.ConcessionId;

            return result;
        }

        public List<Concession> GetConcessions(List<int> concessionIds)
        {
            var list = new List<Concession>();

            using (var db = new PricingEntities())
            {


                var dbtblConcessions = db.tblConcessions
                                        .Where
                                        (r => concessionIds.Contains(r.pkConcessionId))
                                        .ToList();

                foreach (var concession in dbtblConcessions)
                {
                    var c = ConcessionFactory.Create(concession, null);
                    c.Requestor = _userServiceContext.GetUserProfileById(concession.fkRequestorId);
                    if (concession.fkBCMUserId.HasValue)
                        c.BusinessCentreManager = _userServiceContext.GetUserProfileById(concession.fkBCMUserId.Value);
                    list.Add(c);
                }

            }

            return list;
        }

        public Result DeclineChanges(Concession concession)
        {
            using (var db = new PricingEntities())
            {
                var userId = db.tblUsers.FirstOrDefault(u => u.ANumber == concession.UserName).pkUserId;

                var dbConcession = db.tblConcessions.Find(concession.ConcessionId);

                dbConcession.fkSubStatusId = (int)ConcessionSubStatuses.RequestorDeclinedChanges;

                dbConcession.fkStatusId = (int)ConcessionStatuses.Declined;

                dbConcession.tblConcessionApprovals.Add(new tblConcessionApproval
                {
                    fkUserId = userId,
                    IsActive = true,
                    fkOldSubStatusId = (int)concession.SubStatus,
                    fkNewSubStatusId = (int)ConcessionSubStatuses.RequestorAccepetedChanges,
                    SystemDate = DateTime.Now
                });

                AddComment(concession, userId, dbConcession);

                db.SaveChanges();
            }

            return new Result
            {
                IsSuccessful = true,
                ConcessionId = concession.ConcessionId
            };
        }

        public Result AcceptChanges(Concession concession)
        {
            using (var db = new PricingEntities())
            {
                var userId = db.tblUsers.FirstOrDefault(u => u.ANumber == concession.UserName).pkUserId;

                var dbConcession = db.tblConcessions.Find(concession.ConcessionId);

                dbConcession.fkSubStatusId = (int)ConcessionSubStatuses.RequestorAccepetedChanges;

                dbConcession.fkStatusId = (int)ConcessionStatuses.Approved;

                dbConcession.DateApproved = concession.DateApproved;

                dbConcession.ExpiryDate = concession.ExpiryDate;

                dbConcession.tblConcessionApprovals.Add(new tblConcessionApproval
                {
                    fkUserId = userId,
                    IsActive = true,
                    fkOldSubStatusId = (int)concession.SubStatus,
                    fkNewSubStatusId = (int)ConcessionSubStatuses.RequestorAccepetedChanges,
                    SystemDate = DateTime.Now
                });

                if (concession.Type == Types.Existing)
                {
                    var concessionToDelete = db.tblConcessions.Where(c => c.ConcessionRef == concession.ReferenceNumber && c.pkConcessionId != concession.ConcessionId);

                    foreach (var c in concessionToDelete)
                    {
                        c.IsActive = false;
                    }
                }

                AddComment(concession, userId, dbConcession);

                db.SaveChanges();
            }

            return new Result
            {
                IsSuccessful = true,
                ConcessionId = concession.ConcessionId
            };
        }

        public Result EditConcession(Concession concession)
        {
            var handler = _components.FirstOrDefault(h => h.CanHandle(concession.ConcessionType));

            if (handler == null)
            {
                throw new NotImplementedException(string.Format("Handler for {0} not implemented.", concession.ConcessionType));
            }

            concession.ConcessionId = 0;

            concession.ConditionList.ForEach
                (c =>
                {
                    c.ConcessionConditionId = 0;
                });

            concession.Status = ConcessionStatuses.Pending;

            concession.ConcessionDate = DateTime.Now;

            concession.SubStatus = ConcessionSubStatuses.BCMPending;

            concession.DatesentForApproval = DateTime.Now;

            concession.DateApproved = null;

            foreach (var comment in concession.CommentList)
            {
                comment.ConcessionCommentId = 0;
            }

            var result = handler.SaveConcession(concession);

            result.IsSuccessful = true;

            return result;
        }

        public Result RemoveConcession(Concession concession)
        {
            var handler = _components.FirstOrDefault(h => h.CanHandle(concession.ConcessionType));

            if (handler == null)
            {
                throw new NotImplementedException(string.Format("Handler for {0} not implemented.", concession.ConcessionType));
            }

            concession.ConcessionId = 0;

            concession.ConditionList.ForEach
                (c =>
                {
                    c.ConcessionConditionId = 0;
                });

            concession.Status = ConcessionStatuses.Pending;

            concession.ConcessionDate = DateTime.Now;

            concession.SubStatus = ConcessionSubStatuses.BCMPending;

            concession.DatesentForApproval = DateTime.Now;

            concession.DateApproved = null;

            concession.Type = Types.Removal;

            foreach (var comment in concession.CommentList)
            {
                comment.ConcessionCommentId = 0;
            }

            var result = handler.SaveConcession(concession);

            result.IsSuccessful = true;

            return result;

        }

        public List<Concession> GetCustomerProducts(int customerId, string concessionType)
        {
            throw new NotImplementedException();
        }

        public dynamic GetCustomerAccounts(int riskGroupid, int productTypeId)
        {
            List<string> accounts = new List<string>();
            // Please do a get on the accounts by the paramaters above

            accounts.Add("000000001");
            accounts.Add("000000002");
            accounts.Add("000000003");
            accounts.Add("000000004");
            accounts.Add("000000005");

            return accounts;
        }
    }
}
