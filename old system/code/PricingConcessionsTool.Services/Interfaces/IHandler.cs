using PricingConcessionsTool.DTO;
using PricingConcessionsTool.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.Services.Interfaces
{
    public interface IHandler
    {
        bool CanHandle(Concession concession);
        Result Save(Concession concession);
        Result Edit(Concession concession);
        Result RemoveConcession(Concession concession);
        //Result Forward(Concession Concession);
        //Result Decline(Concession Concession);
        //Result Approve(Concession Concession);
    }
}
