using System.Collections.Generic;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using System.Threading.Tasks;
using AutoMapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Linq;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    /// <summary>
    /// Create user request handler
    /// </summary>
    /// <seealso cref="int" />
    public class CreateUserHandler : IAsyncRequestHandler<CreateUser, int>
    {
        /// <summary>
        /// The user manager
        /// </summary>
        private readonly IUserManager _userManager;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The centre user repository
        /// </summary>
        private readonly ICentreUserRepository _centreUserRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserHandler"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="centreUserRepository">The centre user repository.</param>
        public CreateUserHandler(IUserManager userManager, IMapper mapper, ICentreUserRepository centreUserRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _centreUserRepository = centreUserRepository;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public Task<int> Handle(CreateUser message)
        {
            var auditRecords = new List<AuditRecord>();
            var id = _userManager.CreateUser(message.User);

            var mappedUser = _mapper.Map<User>(message.User);
            mappedUser.Id = id;

            AddCentreIds(message, mappedUser);

            auditRecords.Add(new AuditRecord(mappedUser, message.CurrentUser, AuditType.Insert));

            SetUserCentres(message, mappedUser, auditRecords);

            message.AuditRecords = auditRecords;

            return Task.FromResult(id);
        }

        /// <summary>
        /// Adds the centre ids.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="model">The model.</param>
        private void AddCentreIds(CreateUser message, User model)
        {
            if (message.User.UserCentres != null)
            {
                var centreIds = new List<int>();

                foreach (var userCentre in message.User.UserCentres)
                    centreIds.Add(userCentre.Id);

                if (model.CentreIds != null)
                    centreIds.AddRange(model.CentreIds);

                model.CentreIds = centreIds;
            }

            if (message.User.CentreId > 0)
            {
                var centreIds = new List<int>();

                if (model.CentreIds != null)
                    centreIds.AddRange(model.CentreIds);

                centreIds.Add(message.User.CentreId);
                model.CentreIds = centreIds;
            }
        }

        /// <summary>
        /// Sets the user centres.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="model">The model.</param>
        /// <param name="auditRecords">The audit records.</param>
        private void SetUserCentres(CreateUser message, User model, List<AuditRecord> auditRecords)
        {
            //insert the centres
            if (model.CentreIds != null && model.CentreIds.Any())
            {
                foreach (var centreId in model.CentreIds)
                {
                    var centreUser = new CentreUser
                    {
                        CentreId = centreId,
                        IsActive = true,
                        UserId = model.Id
                    };

                    centreUser = _centreUserRepository.Create(centreUser);
                    auditRecords.Add(new AuditRecord(centreUser, message.CurrentUser, AuditType.Insert));
                }
            }
        }
    }
}
