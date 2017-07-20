using System.Linq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    /// <summary>
    /// Look up table manager
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.ILookupTableManager" />
    public class LookupTableManager : ILookupTableManager
    {
        /// <summary>
        /// The status repository
        /// </summary>
        private readonly IStatusRepository _statusRepository;

        /// <summary>
        /// The sub status repository
        /// </summary>
        private readonly ISubStatusRepository _subStatusRepository;

        /// <summary>
        /// The reference type repository
        /// </summary>
        private readonly IReferenceTypeRepository _referenceTypeRepository;

        /// <summary>
        /// The market segment repository
        /// </summary>
        private readonly IMarketSegmentRepository _marketSegmentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LookupTableManager"/> class.
        /// </summary>
        /// <param name="statusRepository">The status repository.</param>
        /// <param name="subStatusRepository">The sub status repository.</param>
        /// <param name="referenceTypeRepository"></param>
        /// <param name="marketSegmentRepository"></param>
        public LookupTableManager(IStatusRepository statusRepository, ISubStatusRepository subStatusRepository,
            IReferenceTypeRepository referenceTypeRepository, IMarketSegmentRepository marketSegmentRepository)
        {
            _statusRepository = statusRepository;
            _subStatusRepository = subStatusRepository;
            _referenceTypeRepository = referenceTypeRepository;
            _marketSegmentRepository = marketSegmentRepository;
        }

        /// <summary>
        /// Gets the status identifier.
        /// </summary>
        /// <param name="statusName">Name of the status.</param>
        /// <returns></returns>
        public int GetStatusId(string statusName)
        {
            var statuses = _statusRepository.ReadAll();

            return statuses.First(_ => _.Description == statusName).Id;
        }

        /// <summary>
        /// Gets the sub status identifier.
        /// </summary>
        /// <param name="subStatusName">Name of the sub status.</param>
        /// <returns></returns>
        public int GetSubStatusId(string subStatusName)
        {
            var subStatuses = _subStatusRepository.ReadAll();

            return subStatuses.First(_ => _.Description == subStatusName).Id;
        }

        /// <summary>
        /// Gets the reference type name for the id specified
        /// </summary>
        /// <param name="referenceTypeId"></param>
        /// <returns></returns>
        public string GetReferenceTypeName(int referenceTypeId)
        {
            var referenceTypes = _referenceTypeRepository.ReadAll();

            return referenceTypes.First(_ => _.Id == referenceTypeId).Description;
        }

        /// <summary>
        /// Gets the market segment name for the id specified
        /// </summary>
        /// <param name="marketSegmentId"></param>
        /// <returns></returns>
        public string GetMarketSegmentName(int marketSegmentId)
        {
            var marketSegments = _marketSegmentRepository.ReadAll();

            return marketSegments.First(_ => _.Id == marketSegmentId).Description;
        }
    }
}
