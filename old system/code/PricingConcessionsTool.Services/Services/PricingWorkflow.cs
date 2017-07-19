using System;
using PricingConcessionsTool.DTO;
using System.Collections.Generic;
using System.Linq;
using PricingConcessionsTool.Services.Interfaces;
using PricingConcessionsTool.Core.Business.Interfaces;
using PricingConcessionsTool.Core.Business.Classes;
using PricingConcessionsTool.DTO.Enums;
using PricingConcessionsTool.Services.Properties;

namespace PricingConcessionsTool.Services.Services
{
    public class PricingWorkflow : IPricingWorkflow
    {
        private readonly INotificationService _notificationService;

        private readonly List<IHandler> _handlers = new List<IHandler>();

        private readonly IConcessionServiceContext _concessionServiceContext;

        private readonly IUserServiceContext _userServiceContext;

        private readonly IDocumentService _documentService;

        private LogContext _log;

        public PricingWorkflow()
        {
            _handlers.Add(new RequestorHandler());

            _notificationService = new NotificationService();
            _userServiceContext = new UserServiceContext();
            _concessionServiceContext = new ConcessionServiceContext();
            _documentService = new DocumentService();
            _notificationService = new NotificationService();

            _log = new LogContext();

        }

        public Result Approve(Concession concession)
        {
            try
            { 
            concession.DateApproved = DateTime.Now;

            concession.ExpiryDate = DateTime.Now.AddMonths(Settings.Default.ConcessionLifetimeMonths);

            var result = _concessionServiceContext.Approve(concession);

            if (result.IsSuccessful)
            {
                concession.Requestor = _userServiceContext.GetUserProfileById(concession.RequestorId);

                if (concession.BusinessCentreManagerId.HasValue)
                    concession.BusinessCentreManager = _userServiceContext.GetUserProfileById(concession.BusinessCentreManagerId.Value);

                _notificationService.SendEmail(EmailFactory.GetEmail(NotificationTypes.Approved, concession));
            }

            return result;
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }

        public Result ApproveWithChanges(Concession concession)
        {
            try
            { 
            var result = _concessionServiceContext.ApproveWithChanges(concession);

            if (result.IsSuccessful)
            {
                concession.Requestor = _userServiceContext.GetUserProfileById(concession.RequestorId);

                if (concession.BusinessCentreManagerId.HasValue)
                    concession.BusinessCentreManager = _userServiceContext.GetUserProfileById(concession.BusinessCentreManagerId.Value);

                _notificationService.SendEmail(EmailFactory.GetEmail(NotificationTypes.ApprovedWithChanges, concession));
            }

            return result;
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }

        public Result Decline(Concession concession)
        {
            try
            { 
            var result = _concessionServiceContext.Decline(concession);

            if (result.IsSuccessful)
            {
                concession.Requestor = _userServiceContext.GetUserProfileById(concession.RequestorId);

                if (concession.BusinessCentreManagerId.HasValue)
                    concession.BusinessCentreManager = _userServiceContext.GetUserProfileById(concession.BusinessCentreManagerId.Value);

                _notificationService.SendEmail(EmailFactory.GetEmail(NotificationTypes.Declined, concession));
            }

            return result;
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }


        public Result Forward(Concession concession)
        {
            try
            { 
            return _concessionServiceContext.Forward(concession);
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }

        public Result Save(Concession concession)
        {
            try
            {
                concession.Type = Types.New;

                var handler = _handlers.FirstOrDefault(h => h.CanHandle(concession));

            if (handler == null)
            {
                throw new NotImplementedException(string.Format("Handler for {0} not implemented.", concession.Status));
            }
            return handler.Save(concession);
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }

        public string GenerateLetters(List<int> concessionIds)
        {
            try
            {
                var pdfData = _documentService.GenerateDocument(_concessionServiceContext.GetConcessions(concessionIds));

                return _documentService.SaveDocument(pdfData);
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }

        public Result DeclineChanges(Concession concession)
        {
            try
            {
                return _concessionServiceContext.DeclineChanges(concession);
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }

        public Result AcceptChanges(Concession concession)
        {
            try
            {
                concession.DateApproved = DateTime.Now;

                concession.ExpiryDate = DateTime.Now.AddMonths(Settings.Default.ConcessionLifetimeMonths);

                return _concessionServiceContext.AcceptChanges(concession);
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }

        public Result Edit(Concession concession)
        {
            try
            {
                concession.Type = Types.Existing;

                var handler = _handlers.FirstOrDefault(h => h.CanHandle(concession));

                if (handler == null)
                {
                    throw new NotImplementedException(string.Format("Handler for {0} not implemented.", concession.Status));
                }
                return handler.Edit(concession);
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }

        public Result RemoveConcession(Concession concession)
        {
            try
            {
                concession.Type = Types.Removal;

                var handler = _handlers.FirstOrDefault(h => h.CanHandle(concession));

                if (handler == null)
                {
                    throw new NotImplementedException(string.Format("Handler for {0} not implemented.", concession.Status));
                }
                return handler.RemoveConcession(concession);
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            };
        }
    }    
}
