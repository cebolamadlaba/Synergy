using PricingConcessionsTool.Core.Data;
using PricingConcessionsTool.DTO;
using PricingConcessionsTool.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.Core.Business.Interfaces
{

    public interface IConcessionComponent
    {
        bool CanHandle(ConcessionTypes concessionType);

        Result SaveConcession(Concession concession);

        void UpdateConcessionRef(int concessionId);
        Result RemoveConcession(Concession concession);
    }
}
