using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// ConcessionRelationship repository interface
    /// </summary>
    public interface IConcessionRelationshipRepository
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        ConcessionRelationship Create(ConcessionRelationship model);

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        ConcessionRelationship ReadById(int id);

        /// <summary>
        /// Reads the by child concession identifier.
        /// </summary>
        /// <param name="childConcessionId">The child concession identifier.</param>
        /// <returns></returns>
        IEnumerable<ConcessionRelationship> ReadByChildConcessionId(int childConcessionId);

        /// <summary>
        /// Reads the by parent concession identifier.
        /// </summary>
        /// <param name="parentConcessionId">The parent concession identifier.</param>
        /// <returns></returns>
        IEnumerable<ConcessionRelationship> ReadByParentConcessionId(int parentConcessionId);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ConcessionRelationship> ReadAll();

        /// <summary>
        /// Reads the by child concession identifier relationship identifier relationships.
        /// </summary>
        /// <param name="childConcessionId">The child concession identifier.</param>
        /// <param name="relationshipId">The relationship identifier.</param>
        /// <returns></returns>
        IEnumerable<ConcessionRelationship> ReadByChildConcessionIdRelationshipIdRelationships(int childConcessionId,
            int relationshipId);

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(ConcessionRelationship model);

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Delete(ConcessionRelationship model);
    }
}