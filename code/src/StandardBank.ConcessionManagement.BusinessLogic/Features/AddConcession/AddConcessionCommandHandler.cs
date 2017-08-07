using System;
using System.Linq;
using AutoMapper;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcession
{
    /// <summary>
    /// Add concession command handler
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{AddConcessionCommand, Concession}" />
    public class AddConcessionCommandHandler : IRequestHandler<AddConcessionCommand, Concession>
    {
        /// <summary>
        /// The concession repository
        /// </summary>
        private readonly IConcessionRepository _concessionRepository;

        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The legal entity repository
        /// </summary>
        private readonly ILegalEntityRepository _legalEntityRepository;

        /// <summary>
        /// The pricing manager
        /// </summary>
        private readonly IPricingManager _pricingManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConcessionCommandHandler"/> class.
        /// </summary>
        /// <param name="concessionRepository">The concession repository.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="legalEntityRepository">The legal entity repository.</param>
        /// <param name="pricingManager">The pricing manager.</param>
        public AddConcessionCommandHandler(IConcessionRepository concessionRepository,
            ILookupTableManager lookupTableManager, IMapper mapper, ILegalEntityRepository legalEntityRepository,
            IPricingManager pricingManager)
        {
            _concessionRepository = concessionRepository;
            _lookupTableManager = lookupTableManager;
            _mapper = mapper;
            _legalEntityRepository = legalEntityRepository;
            _pricingManager = pricingManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public Concession Handle(AddConcessionCommand message)
        {
            var mappedConcession = _mapper.Map<Model.Repository.Concession>(message.Concession);
            mappedConcession.TypeId = _lookupTableManager.GetReferenceTypeId(message.Concession.Type);

            //TODO: Get legal entity id
            //message.Concession.CustomerName
            //message.Concession.RiskGroupNumber
            //message.Concession.RiskGroupName
            //mappedConcession.LegalEntityId = _legalEntityRepository.
            if (message.Concession.RiskGroupNumber.HasValue)
            {
                var riskGroup = _pricingManager.GetRiskGroupForRiskGroupNumber(message.Concession.RiskGroupNumber.Value);
                var legalEntity = _legalEntityRepository.ReadByRiskGroupIdIsActive(riskGroup.Id, true).First();

                mappedConcession.LegalEntityId = legalEntity.Id;
            }

            mappedConcession.ConcessionTypeId =
                _lookupTableManager.GetConcessionTypeId(message.Concession.ConcessionType);

            mappedConcession.StatusId = _lookupTableManager.GetStatusId("Pending");
            mappedConcession.SubStatusId = _lookupTableManager.GetSubStatusId("BCM Pending");
            mappedConcession.ConcessionDate = DateTime.Now;
            mappedConcession.RequestorId = message.User.Id;

            mappedConcession.CentreId = message.User.SelectedCentre.Id;
            mappedConcession.IsCurrent = true;
            mappedConcession.IsActive = true;

            var result = _concessionRepository.Create(mappedConcession);

            //need to generate the concession reference based on the id returned
            var concessionReference =
                $"{message.Concession.Type.Substring(0, 1)}{Convert.ToString(result.Id).PadLeft(12, '0')}";

            result.ConcessionRef = concessionReference;

            _concessionRepository.Update(result);

            return _mapper.Map<Concession>(result);
        }
    }
}
