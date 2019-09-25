using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Glms;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.GlmsConcession
{
    /// <summary>
    /// Delete Glms concession detail command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{DeleteGlmsConcessionDetailCommand, GlmsConcessionDetail}" />
    public class DeleteGlmsConcessionDetailHandler : IAsyncRequestHandler<DeleteGlmsConcessionDetail, GlmsConcessionDetail>
    {
        /// <summary>
        /// The Glms manager
        /// </summary>
        private readonly IGlmsManager _glmsManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteGlmsConcessionDetailHandler"/> class.
        /// </summary>
        /// <param name="GlmsManager">The Glms manager.</param>
        public DeleteGlmsConcessionDetailHandler(IGlmsManager glmsManager)
        {
            _glmsManager = glmsManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<GlmsConcessionDetail> Handle(DeleteGlmsConcessionDetail message)
        {
            var result = _glmsManager.DeleteConcessionGlms(message.GlmsConcessionDetail);

            message.AuditRecord = new AuditRecord(result, message.User, AuditType.Delete);

            return message.GlmsConcessionDetail;
        }
    }
}
