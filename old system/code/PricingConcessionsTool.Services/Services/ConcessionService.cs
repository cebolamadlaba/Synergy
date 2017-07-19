using PricingConcessionsTool.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PricingConcessionsTool.DTO;
using PricingConcessionsTool.DTO.Enums;
using PricingConcessionsTool.Core.Business.Interfaces;
using PricingConcessionsTool.Core.Business.Classes;

namespace PricingConcessionsTool.Services.Services
{
    public class ConcessionService : IConcessionService
    {
        IConcessionServiceContext _concessionServiceContext = null;

        INotificationService _notificationService = new NotificationService();

        private readonly IUserServiceContext _userServiceContext;

        IlogContext _log = null;

        public ConcessionService()
        {
            _concessionServiceContext = new ConcessionServiceContext();
            _userServiceContext = new UserServiceContext();
            _log = new LogContext();
        }

        public Result SaveConcession(Concession concession)
        {
            try
            {
                var result = _concessionServiceContext.SaveConcession(concession);

                //if (result.IsSuccessful)
                //{
                //    concession.Requestor = _userServiceContext.GetUserProfileById(concession.RequestorId);

                //    if (concession.BusinessCentreManagerId.HasValue)
                //        concession.BusinessCentreManager = _userServiceContext.GetUserProfileById(concession.BusinessCentreManagerId.Value);

                //    _notificationService.SendEmail(EmailFactory.GetEmail(NotificationTypes.Approved, concession));
                //}

                return result;

            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }

        public Result RemoveConcession(Concession concession)
        {
            return _concessionServiceContext.RemoveConcession(concession);
        }

        public Result EditConcession(Concession concession)
        {
            return _concessionServiceContext.EditConcession(concession);
        }

        public RiskGroup GetRiskGroup(int riskGroupNumber)
        {
            try
            {
                return _concessionServiceContext.GetRiskGroup(riskGroupNumber);
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }

        public List<Concession> GetCustomerConcessions(int customerId, string concessionType, bool pending, string username)
        {
            try
            {
                var cType = (ConcessionTypes)Enum.Parse(typeof(ConcessionTypes), concessionType);

                return _concessionServiceContext.GetCustomerConcessions(customerId, cType, pending, username);

            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }


        public Concession GetConcession(int concessionId)
        {
            try
            {
                return _concessionServiceContext.GetConcession(concessionId);
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }

        public FinancialInfo GetFinancialInfo(int customerId, ConcessionTypes lending)
        {
            try
            {
                return _concessionServiceContext.GetFinancialInfo(customerId, lending);
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }

        public List<Concession> GetBCMConcessions(string username, bool pending)
        {
            try
            {
                return _concessionServiceContext.GetBCMConcessions(username, pending);
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }

        public List<Concession> GetRequestorConcessions(string username, bool pending)
        {
            try
            {
                return _concessionServiceContext.GetRequestorConcessions(username, pending);
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }

        public List<Concession> GetConcessions(string username, int roleId, bool pending)
        {
            try
            {
                var list = new List<Concession>();

                switch ((Roles)roleId)
                {
                    case Roles.Requestor:
                        list = _concessionServiceContext.GetRequestorConcessions(username, pending);
                        break;
                    case Roles.SuiteHead:
                    case Roles.BCM:
                        list = _concessionServiceContext.GetBCMConcessions(username, pending);
                        break;
                    case Roles.PCM:
                        list = _concessionServiceContext.GetPCMConcessions(username, pending);
                        break;
                    case Roles.HeadOffice:
                        break;
                }

                return list;
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }

        public List<Concession> GetCustomerProducts(int customerId, string concessionType)
        {
            try
            {
                return _concessionServiceContext.GetCustomerProducts(customerId, concessionType);
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }

        public dynamic GetCustomerAccounts(int riskGroupid, int productTypeId)
        {
            try
            {
                return _concessionServiceContext.GetCustomerAccounts(riskGroupid, productTypeId);
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }
    }

}
