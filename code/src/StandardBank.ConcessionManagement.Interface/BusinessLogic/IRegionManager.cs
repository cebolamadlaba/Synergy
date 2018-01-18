using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.UserInterface;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Region manager interface
    /// </summary>
    public interface IRegionManager
    {
        /// <summary>
        /// Gets the regions.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Region> GetRegions();

        /// <summary>
        /// Gets the region description.
        /// </summary>
        /// <param name="regionId">The region identifier.</param>
        /// <returns></returns>
        string GetRegionDescription(int regionId);

        /// <summary>
        /// Validates the region.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <returns></returns>
        IEnumerable<string> ValidateRegion(Region region);

        /// <summary>
        /// Creates the region.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <returns></returns>
        Model.Repository.Region CreateRegion(Region region);
    }
}
