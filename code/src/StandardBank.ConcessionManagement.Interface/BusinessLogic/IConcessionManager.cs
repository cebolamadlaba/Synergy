using System;
using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Inbox;
using Concession = StandardBank.ConcessionManagement.Model.Repository.Concession;
using ConcessionComment = StandardBank.ConcessionManagement.Model.Repository.ConcessionComment;
using ConcessionCondition = StandardBank.ConcessionManagement.Model.UserInterface.ConcessionCondition;
using ConcessionRelationship = StandardBank.ConcessionManagement.Model.UserInterface.ConcessionRelationship;
using User = StandardBank.ConcessionManagement.Model.UserInterface.User;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Concession manager interface
    /// </summary>
    public interface IConcessionManager
    {
        /// <summary>
        /// Gets the pending concessions for user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        IEnumerable<InboxConcession> GetPendingConcessionsForUser(User user);

        /// <summary>
        /// Get the due for expiry concessions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<InboxConcession> GetDueForExpiryConcessionsForUser(User user);

        /// <summary>
        /// Gets the expired concessions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<InboxConcession> GetExpiredConcessionsForUser(User user);

        /// <summary>
        /// Gets the mismatched concessions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<InboxConcession> GetMismatchedConcessionsForUser(User user);

        /// <summary>
        /// Gets the declined concessions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<InboxConcession> GetDeclinedConcessionsForUser(User user);

        /// <summary>
        /// Gets the actioned concessions for user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        IEnumerable<InboxConcession> GetActionedConcessionsForUser(User user);

        /// <summary>
        /// Gets the user concessions
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        UserConcessions GetUserConcessions(User user);

        /// <summary>
        /// Gets the concession conditions.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <returns></returns>
        IEnumerable<ConcessionCondition> GetConcessionConditions(int concessionId);

        /// <summary>
        /// Gets the client accounts.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        IEnumerable<ClientAccount> GetClientAccounts(int riskGroupNumber, User user, string concessiontype, int? sapbpid = null);

        /// <summary>
        /// Searches the client accounts.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <param name="accountNumber">The account number.</param>
        /// <returns></returns>
        //IEnumerable<ClientAccount> SearchClientAccounts(int riskGroupNumber, string accountNumber);

        IEnumerable<string> PrimeRate(DateTime datefilter);


        /// <summary>
        /// Gets the approved concessions for user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IEnumerable<ApprovedConcession> GetApprovedConcessionsForUser(int userAEId,User currentUser);

        /// <summary>
        /// Search for concessions using filters
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<SearchConcessionDetail> SearchConsessions(int userId);

        IEnumerable<SearchConcessionDetail> SearchConsessions(int region, int businesscentre, string status, DateTime datefilter, int userid);


        /// <summary>
        /// Gets the conditions.
        /// </summary>
        /// <param name="periodType">Type of the period.</param>
        /// <param name="period">The period.</param>
        /// <param name="requestorId">The requestor identifier.</param>
        /// <returns></returns>
        IEnumerable<ConcessionCondition> GetConditions(string periodType, string period, int requestorId);

        /// <summary>
        /// Gets the condition counts.
        /// </summary>
        /// <param name="requestorId">The requestor identifier.</param>
        /// <returns></returns>
        ConditionCounts GetConditionCounts(int requestorId);

        /// <summary>
        /// Updates the concession.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Concession UpdateConcession(Model.UserInterface.Concession concession, User user);

        /// <summary>
        /// Deletes the concession condition.
        /// </summary>
        /// <param name="concessionCondition">The concession condition.</param>
        /// <returns></returns>
        Model.Repository.ConcessionCondition DeleteConcessionCondition(ConcessionCondition concessionCondition);

        /// <summary>
        /// Activates the concession.
        /// </summary>
        /// <param name="concessionReferenceNumber">The concession reference number.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Concession ActivateConcession(string concessionReferenceNumber, User user);

        /// <summary>
        /// Creates the concession comment.
        /// </summary>
        /// <param name="concessionComment">The concession comment.</param>
        /// <returns></returns>
        ConcessionComment CreateConcessionComment(ConcessionComment concessionComment);

        /// <summary>
        /// Creates the concession.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Concession CreateConcession(Model.UserInterface.Concession concession, User user);

        /// <summary>
        /// Creates the concession relationship.
        /// </summary>
        /// <param name="concessionRelationship">The concession relationship.</param>
        /// <returns></returns>
        Model.Repository.ConcessionRelationship CreateConcessionRelationship(ConcessionRelationship concessionRelationship);

        /// <summary>
        /// Deactivates the concession.
        /// </summary>
        /// <param name="concessionReferenceNumber">The concession reference number.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Concession DeactivateConcession(string concessionReferenceNumber, bool isRecall, User user);

        /// <summary>
        /// Creates the concession condition.
        /// </summary>
        /// <param name="concessionCondition">The concession condition.</param>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        Model.Repository.ConcessionCondition CreateConcessionCondition(ConcessionCondition concessionCondition,
            Model.UserInterface.Concession concession);

        /// <summary>
        /// Updates the concession condition.
        /// </summary>
        /// <param name="concessionCondition">The concession condition.</param>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        Model.Repository.ConcessionCondition UpdateConcessionCondition(ConcessionCondition concessionCondition,
            Model.UserInterface.Concession concession);

        /// <summary>
        /// Gets the concessions for risk group.
        /// </summary>
        /// <param name="riskGroupId">The risk group identifier.</param>
        /// <param name="concessionType">Type of the concession.</param>
        /// <returns></returns>
        IEnumerable<Model.UserInterface.Concession> GetConcessionsForRiskGroup(int riskGroupId, string concessionType, User currentUser);

        /// <summary>
        /// Gets the approved concessions for risk group.
        /// </summary>
        /// <param name="riskGroupId">The risk group identifier.</param>
        /// <param name="concessionType">Type of the concession.</param>
        /// <returns></returns>
        IEnumerable<Model.UserInterface.Concession> GetApprovedConcessionsForRiskGroup(int riskGroupId,
            string concessionType, User currentUser);

        IEnumerable<Model.UserInterface.Concession> GetApprovedConcessionsForLegalEntityId(int legalEntityId, string concessionType, User currentUser);

        /// <summary>
        /// Gets the concession for concession reference identifier.
        /// </summary>
        /// <param name="concessionReferenceId">The concession reference identifier.</param>
        /// <returns></returns>
        Model.UserInterface.Concession GetConcessionForConcessionReferenceId(string concessionReferenceId, User currentUser);

        /// <summary>
        /// Gets the rag status.
        /// </summary>
        /// <param name="period">The period.</param>
        /// <param name="dateApproved">The date approved.</param>
        /// <returns></returns>
        string GetRagStatus(string period, DateTime dateApproved);

        ConcessionLetter CreateConcessionLetter(ConcessionLetter model);

        ConcessionDetail DeactivateConcessionDetailed(int ConcessionDetailId, User user);
    }
}
