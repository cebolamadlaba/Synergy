using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Glms;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.GlmsConcession
{
    /// <summary>
    /// Add Glms concession detail command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{AddGlmsConcessionDetailCommand, GlmsConcessionDetail}" />
    public class AddOrUpdateGlmsConcessionDetailHandler : IAsyncRequestHandler<AddOrUpdateGlmsConcessionDetail, GlmsConcessionDetail>
    {
        /// <summary>
        /// The Glms manager
        /// </summary>
        private readonly IGlmsManager _glmsManager;
     
        public AddOrUpdateGlmsConcessionDetailHandler(IGlmsManager glmsManager)
        {
            _glmsManager = glmsManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<GlmsConcessionDetail> Handle(AddOrUpdateGlmsConcessionDetail message)
        {
            if (message.GlmsConcessionDetail.GlmsConcessionDetailId == 0)
            {
                var result = _glmsManager.CreateConcessionGlms(message.GlmsConcessionDetail, message.Concession);

                message.AuditRecord = new AuditRecord(result, message.User, AuditType.Insert);
                message.GlmsConcessionDetail.GlmsConcessionDetailId = result.Id;
            }
            else
            {
              // var result = _glmsManager.UpdateConcessionGlms(message.GlmsConcessionDetail, message.Concession);
               // message.AuditRecord = new AuditRecord(result, message.User, AuditType.Update);
            }

            return message.GlmsConcessionDetail;
        }

      
    }
}
