using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Rule manager interface
    /// </summary>
    public interface IRuleManager
    {
        /// <summary>
        /// Updates the base fields on approval.
        /// </summary>
        /// <param name="concessionDetail">The concession detail.</param>
        void UpdateBaseFieldsOnApproval(ConcessionDetail concessionDetail);
    }
}
