using System;
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
        /// The province repository
        /// </summary>
        private readonly IProvinceRepository _provinceRepository;

        /// <summary>
        /// The concession type repository
        /// </summary>
        private readonly IConcessionTypeRepository _concessionTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LookupTableManager"/> class.
        /// </summary>
        /// <param name="statusRepository">The status repository.</param>
        /// <param name="subStatusRepository">The sub status repository.</param>
        /// <param name="referenceTypeRepository"></param>
        /// <param name="marketSegmentRepository"></param>
        /// <param name="provinceRepository"></param>
        /// <param name="concessionTypeRepository"></param>
        public LookupTableManager(IStatusRepository statusRepository, ISubStatusRepository subStatusRepository,
            IReferenceTypeRepository referenceTypeRepository, IMarketSegmentRepository marketSegmentRepository,
            IProvinceRepository provinceRepository, IConcessionTypeRepository concessionTypeRepository)
        {
            _statusRepository = statusRepository;
            _subStatusRepository = subStatusRepository;
            _referenceTypeRepository = referenceTypeRepository;
            _marketSegmentRepository = marketSegmentRepository;
            _provinceRepository = provinceRepository;
            _concessionTypeRepository = concessionTypeRepository;
        }

        /// <summary>
        /// Gets the status identifier.
        /// </summary>
        /// <param name="statusName">Name of the status.</param>
        /// <returns></returns>
        public int GetStatusId(string statusName)
        {
            var statuses = _statusRepository.ReadAll();

            return statuses.First(_ => _.Description == statusName && _.IsActive).Id;
        }

        /// <summary>
        /// Gets the sub status identifier.
        /// </summary>
        /// <param name="subStatusName">Name of the sub status.</param>
        /// <returns></returns>
        public int GetSubStatusId(string subStatusName)
        {
            var subStatuses = _subStatusRepository.ReadAll();

            return subStatuses.First(_ => _.Description == subStatusName && _.IsActive).Id;
        }

        /// <summary>
        /// Gets the reference type name for the id specified
        /// </summary>
        /// <param name="referenceTypeId"></param>
        /// <returns></returns>
        public string GetReferenceTypeName(int referenceTypeId)
        {
            var referenceTypes = _referenceTypeRepository.ReadAll();

            return referenceTypes.First(_ => _.Id == referenceTypeId && _.IsActive).Description;
        }

        /// <summary>
        /// Gets the market segment name for the id specified
        /// </summary>
        /// <param name="marketSegmentId"></param>
        /// <returns></returns>
        public string GetMarketSegmentName(int marketSegmentId)
        {
            var marketSegments = _marketSegmentRepository.ReadAll();

            return marketSegments.First(_ => _.Id == marketSegmentId && _.IsActive).Description;
        }

        /// <summary>
        /// Gets the province name for the id specified
        /// </summary>
        /// <param name="provinceId"></param>
        /// <returns></returns>
        public string GetProvinceName(int provinceId)
        {
            var provinces = _provinceRepository.ReadAll();

            return provinces.First(_ => _.Id == provinceId && _.IsActive).Description;
        }

        /// <summary>
        /// Gets the concession type id for the code passed in
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int GetConcessionTypeId(string code)
        {
            var concessionTypes = _concessionTypeRepository.ReadAll();

            return concessionTypes.First(_ => _.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase)).Id;
        }
    }
}
