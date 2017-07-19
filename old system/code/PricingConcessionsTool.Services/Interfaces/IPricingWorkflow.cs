using PricingConcessionsTool.DTO;
using System.Collections.Generic;

namespace PricingConcessionsTool.Services.Interfaces
{
    public interface IPricingWorkflow
    {
        Result Save(Concession concession);
        Result Forward(Concession Concession);
        Result Decline(Concession Concession);
        Result Approve(Concession Concession);
        Result ApproveWithChanges(Concession concession);
        string GenerateLetters(List<int> concessionIds);
        Result DeclineChanges(Concession concession);
        Result AcceptChanges(Concession concession);
        Result Edit(Concession concession);
        Result RemoveConcession(Concession concession);
    }
}