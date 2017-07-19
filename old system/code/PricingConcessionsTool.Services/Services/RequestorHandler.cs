using PricingConcessionsTool.Core.Business.Classes;
using PricingConcessionsTool.Core.Business.Interfaces;
using PricingConcessionsTool.DTO;
using PricingConcessionsTool.DTO.Enums;
using PricingConcessionsTool.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.Services.Services
{
    public class RequestorHandler : IHandler
    {
        private readonly ConcessionService _concessionService;

        private readonly IlogContext _log;

        public RequestorHandler()
        {
            _concessionService = new ConcessionService();
            _log = new LogContext();
        }
        public bool CanHandle(Concession concession)
        {
            return concession.User.IsRequestor;
        }

        public Result Edit(Concession concession)
        {
            return _concessionService.EditConcession(concession);
        }

        public Result RemoveConcession(Concession concession)
        {
            try
            {
                return _concessionService.RemoveConcession(concession);
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }

        public Result Save(Concession concession)
        {
            // perform requstor specific actions here

            try
            {

                return _concessionService.SaveConcession(concession);
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }
    }
}
