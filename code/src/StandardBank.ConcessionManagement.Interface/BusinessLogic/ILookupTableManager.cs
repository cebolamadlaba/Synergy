﻿namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Lookup table manager
    /// </summary>
    public interface ILookupTableManager
    {
        /// <summary>
        /// Gets the status identifier.
        /// </summary>
        /// <param name="statusName">Name of the status.</param>
        /// <returns></returns>
        int GetStatusId(string statusName);

        /// <summary>
        /// Gets the sub status identifier.
        /// </summary>
        /// <param name="subStatusName">Name of the sub status.</param>
        /// <returns></returns>
        int GetSubStatusId(string subStatusName);

        /// <summary>
        /// Gets the reference type name for the reference type id specified
        /// </summary>
        /// <param name="referenceTypeId"></param>
        /// <returns></returns>
        string GetReferenceTypeName(int referenceTypeId);

        /// <summary>
        /// Gets the market segment name for the id specified
        /// </summary>
        /// <param name="marketSegmentId"></param>
        /// <returns></returns>
        string GetMarketSegmentName(int marketSegmentId);

        /// <summary>
        /// Gets the province name for the id specified
        /// </summary>
        /// <param name="provinceId"></param>
        /// <returns></returns>
        string GetProvinceName(int provinceId);

        /// <summary>
        /// Gets the concession type id for the code passed in
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        int GetConcessionTypeId(string code);
    }
}
