using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using Region = StandardBank.ConcessionManagement.Model.UserInterface.Region;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    /// <summary>
    /// The region manager
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.IRegionManager" />
    public class RegionManager : IRegionManager
    {
        /// <summary>
        /// The region repository
        /// </summary>
        private readonly IRegionRepository _regionRepository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionManager"/> class.
        /// </summary>
        /// <param name="regionRepository">The region repository.</param>
        /// <param name="mapper">The mapper.</param>
        public RegionManager(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the regions.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Region> GetRegions() => _mapper.Map<IEnumerable<Region>>(_regionRepository.ReadAll());

        /// <summary>
        /// Gets the region description.
        /// </summary>
        /// <param name="regionId">The region identifier.</param>
        /// <returns></returns>
        public string GetRegionDescription(int regionId)
        {
            var regions = _regionRepository.ReadAll();

            return regions.First(_ => _.Id == regionId && _.IsActive).Description;
        }

        /// <summary>
        /// Validates the region.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <returns></returns>
        public IEnumerable<string> ValidateRegion(Region region)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(region.Description))
                errors.Add("Please supply a valid description");

            var regions = _regionRepository.ReadAll();

            if (!string.IsNullOrWhiteSpace(region.Description) && regions != null && regions.Any())
            {
                if (region.Id > 0)
                {
                    //this means it's an update
                    foreach (var matchingDescriptionRegion in regions.Where(_ =>
                        _.Description.ToLowerInvariant() == region.Description.ToLowerInvariant()))
                    {
                        if (matchingDescriptionRegion.Id != region.Id)
                            errors.Add("There is already a region with the same description, please use another description");
                    }
                }
                else
                {
                    //this means it's a create

                    if (regions.Any(_ => _.Description.ToLowerInvariant() == region.Description.ToLowerInvariant()))
                        errors.Add(
                            "There is already a region with the same description, please use another description");
                }
            }

            return errors;
        }
    }
}
