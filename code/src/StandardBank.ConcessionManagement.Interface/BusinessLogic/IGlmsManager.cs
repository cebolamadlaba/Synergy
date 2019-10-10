using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Glms;
using System.Collections.Generic;
using System.Threading.Tasks;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;
using GlmsTierData = StandardBank.ConcessionManagement.Model.UserInterface.GlmsTierData;
using User = StandardBank.ConcessionManagement.Model.UserInterface.User;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Gmls manager interface
    /// </summary>
    public interface IGlmsManager
    {
        GlmsView GetGlmsViewData(int riskGroupNumber, int sapbpid, User currentUser);

        ConcessionGlms CreateConcessionGlms(GlmsConcessionDetail glmsConcessionDetail, Concession concession);

        ConcessionGlms DeleteConcessionGlms(GlmsConcessionDetail glmsConcessionDetail);

        GlmsConcession GetGlmsConcession(string concessionReferenceId, User user);

        ConcessionGlms UpdateConcessionGlms(GlmsConcessionDetail glmsConcessionDetail, Concession concession);

        void AddGlmsTierData(IEnumerable<GlmsTierData> tierData, int concessionDetailId);

        void DeleteGlmsTierData(int concessionDetailId);

        Task ForwardGlmsConcession(GlmsConcession glmsConcession, User user);

    }
}
