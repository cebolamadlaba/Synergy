using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;


namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddOrUpdateProvinceDetail
{
    /// <summary>
    /// Add cash concession detail command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{AddCashConcessionDetailCommand, CashConcessionDetail}" />
    public class AddOrUpdateProvinceDetailHandler : IAsyncRequestHandler<AddOrUpdateProvinceDetail>
    {
        /// <summary>
        /// The cash manager
        /// </summary>
        private readonly IProvinceManager _provinceManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddOrUpdateProvinceDetailHandler"/> class.
        /// </summary>
        /// <param name="cashManager">The cash manager.</param>
        public AddOrUpdateProvinceDetailHandler(IProvinceManager provinceManager)
        {
            _provinceManager = provinceManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<Model.UserInterface.Province> Handle(AddOrUpdateProvinceDetail message)
        {
            var result = _provinceManager.MaintainProvince(message.Province);
            return message.Province = result;
        }
    }
}
