using System.Collections.Generic;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Inbox;
using Concession = StandardBank.ConcessionManagement.Model.Repository.Concession;
using ConcessionComment = StandardBank.ConcessionManagement.Model.Repository.ConcessionComment;
using ConcessionRelationship = StandardBank.ConcessionManagement.Model.Repository.ConcessionRelationship;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    /// <summary>
    /// Concession manager
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.IConcessionManager" />
    public class ConcessionManager : IConcessionManager
    {
        public IEnumerable<InboxConcession> GetPendingConcessionsForUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<InboxConcession> GetDueForExpiryConcessionsForUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<InboxConcession> GetExpiredConcessionsForUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<InboxConcession> GetMismatchedConcessionsForUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<InboxConcession> GetDeclinedConcessionsForUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<InboxConcession> GetActionedConcessionsForUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public UserConcessions GetUserConcessions(User user)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ConcessionCondition> GetConcessionConditions(int concessionId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ClientAccount> GetClientAccounts(int riskGroupNumber)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ApprovedConcession> GetApprovedConcessionsForUser(int userId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Condition> GetConditions(string periodType, string period)
        {
            throw new System.NotImplementedException();
        }

        public ConditionCounts GetConditionCounts()
        {
            throw new System.NotImplementedException();
        }

        public Concession UpdateConcession(Model.UserInterface.Concession concession, User user)
        {
            throw new System.NotImplementedException();
        }

        public Model.Repository.ConcessionCondition DeleteConcessionCondition(ConcessionCondition concessionCondition)
        {
            throw new System.NotImplementedException();
        }

        public Concession ActivateConcession(string concessionReferenceNumber, User user)
        {
            throw new System.NotImplementedException();
        }

        public ConcessionComment CreateConcessionComment(ConcessionComment concessionComment)
        {
            throw new System.NotImplementedException();
        }

        public Concession CreateConcession(Model.UserInterface.Concession concession, User user)
        {
            throw new System.NotImplementedException();
        }

        public ConcessionRelationship CreateConcessionRelationship(Model.UserInterface.ConcessionRelationship concessionRelationship)
        {
            throw new System.NotImplementedException();
        }

        public Concession DeactivateConcession(string concessionReferenceNumber, User user)
        {
            throw new System.NotImplementedException();
        }

        public Model.Repository.ConcessionCondition CreateConcessionCondition(ConcessionCondition concessionCondition, Model.UserInterface.Concession concession)
        {
            throw new System.NotImplementedException();
        }

        public Model.Repository.ConcessionCondition UpdateConcessionCondition(ConcessionCondition concessionCondition, Model.UserInterface.Concession concession)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Model.UserInterface.Concession> GetConcessionsForRiskGroup(int riskGroupId, string concessionType)
        {
            throw new System.NotImplementedException();
        }

        public Model.UserInterface.Concession GetConcessionForConcessionReferenceId(string concessionReferenceId)
        {
            throw new System.NotImplementedException();
        }
    }
}
