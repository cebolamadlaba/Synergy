﻿using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Inbox;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;
using ConcessionCondition = StandardBank.ConcessionManagement.Model.UserInterface.ConcessionCondition;
using ConcessionRelationship = StandardBank.ConcessionManagement.Model.UserInterface.ConcessionRelationship;
using User = StandardBank.ConcessionManagement.Model.UserInterface.User;
using System;

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
        IEnumerable<Concession> GetPendingConcessionsForUser(User user);

        /// <summary>
        /// Get the due for expiry concessions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<Concession> GetDueForExpiryConcessionsForUser(User user);

        /// <summary>
        /// Gets the expired concessions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<Concession> GetExpiredConcessionsForUser(User user);

        /// <summary>
        /// Gets the mismatched concessions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<Concession> GetMismatchedConcessionsForUser(User user);

        /// <summary>
        /// Gets the declined concessions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<Concession> GetDeclinedConcessionsForUser(User user);

        /// <summary>
        /// Gets the user concessions
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        UserConcessions GetUserConcessions(User user);

        /// <summary>
        /// Gets the concession conditions
        /// </summary>
        /// <param name="concessionId"></param>
        /// <returns></returns>
        IEnumerable<ConcessionCondition> GetConcessionConditions(int concessionId);

        /// <summary>
        /// Creates a concession and returns the repository entity
        /// </summary>
        /// <param name="concession"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Model.Repository.Concession CreateConcession(Concession concession, User user);

        /// <summary>
        /// Gets the client accounts for the risk group number specified
        /// </summary>
        /// <param name="riskGroupNumber"></param>
        /// <returns></returns>
        IEnumerable<ClientAccount> GetClientAccounts(int riskGroupNumber);
        IEnumerable<Concession> GetActionedConcessionsForUser(User user);

        /// <summary>
        /// Gets the concessions for the risk group id and the concession type
        /// </summary>
        /// <param name="riskGroupId"></param>
        /// <param name="concessionType"></param>
        /// <returns></returns>
        IEnumerable<Concession> GetConcessionsForRiskGroup(int riskGroupId, string concessionType);

        /// <summary>
        /// Creates a concession condition
        /// </summary>
        /// <param name="concessionCondition"></param>
        /// <param name="concession"></param>
        /// <returns></returns>
        Model.Repository.ConcessionCondition CreateConcessionCondition(ConcessionCondition concessionCondition, Concession concession);

        /// <summary>
        /// Gets the concession for the concession reference id specified
        /// </summary>
        /// <param name="concessionReferenceId"></param>
        /// <returns></returns>
        Concession GetConcessionForConcessionReferenceId(string concessionReferenceId);

        /// <summary>
        /// Deactivates the concession
        /// </summary>
        /// <param name="concessionReferenceId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Model.Repository.Concession DeactivateConcession(string concessionReferenceId, User user);

        /// <summary>
        /// Updates the concession
        /// </summary>
        /// <param name="concession"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Model.Repository.Concession UpdateConcession(Concession concession, User user);

        /// <summary>
        /// Deletes the concession condition.
        /// </summary>
        /// <param name="concessionCondition">The concession condition.</param>
        /// <returns></returns>
        Model.Repository.ConcessionCondition DeleteConcessionCondition(ConcessionCondition concessionCondition);

        IEnumerable<Model.UserInterface.Condition> GetConditions(string periodType, string period);

        /// <summary>
        /// Creates the concession comment
        /// </summary>
        /// <param name="concessionComment"></param>
        /// <returns></returns>
        Model.Repository.ConcessionComment
            CreateConcessionComment(Model.Repository.ConcessionComment concessionComment);

        /// <summary>
        /// Gets the approved concessions for the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<ApprovedConcession> GetApprovedConcessionsForUser(int userId);

        /// <summary>
        /// Gets the approved concession details
        /// </summary>
        /// <param name="concession"></param>
        /// <returns></returns>
        IEnumerable<ApprovedConcessionDetail> GetApprovedConcessionDetails(Concession concession);

        /// <summary>
        /// Updates the concession condition.
        /// </summary>
        /// <param name="concessionCondition">The concession condition.</param>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        Model.Repository.ConcessionCondition UpdateConcessionCondition(ConcessionCondition concessionCondition, Concession concession);

        /// <summary>
        /// Creates the concession relationship.
        /// </summary>
        /// <param name="concessionRelationship">The concession relationship.</param>
        /// <returns></returns>
        Model.Repository.ConcessionRelationship CreateConcessionRelationship(ConcessionRelationship concessionRelationship);

        /// <summary>
        /// Activates the concession.
        /// </summary>
        /// <param name="concessionReferenceNumber">The concession reference number.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Model.Repository.Concession ActivateConcession(string concessionReferenceNumber, User user);

        /// <summary>
        /// Gets the concession comments.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <returns></returns>
        IEnumerable<Model.UserInterface.ConcessionComment> GetConcessionComments(int concessionId);

        string GetRagStatus(string period,DateTime dateApproved);
    }
}
