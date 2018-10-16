using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Common;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    /// <summary>
    /// Update user request handler
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{StandardBank.ConcessionManagement.BusinessLogic.Features.Administration.UpdateUser}" />
    public class UpdateUserHandler : MediatR.IRequestHandler<UpdateUser>
    {
        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IUserRepository _userRepository;

        private readonly IRoleRepository _roleRepository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// The centre user repository
        /// </summary>
        private readonly ICentreUserRepository _centreUserRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="centreUserRepository">The centre user repository.</param>
        public UpdateUserHandler(IUserRepository userRepository, IMapper mapper, ICacheManager cacheManager,
            ICentreUserRepository centreUserRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _cacheManager = cacheManager;
            _centreUserRepository = centreUserRepository;
            _roleRepository = roleRepository; 
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(UpdateUser message)
        {
            var auditRecords = new List<AuditRecord>();
            var aNumber = message.Model.ANumber;

            _cacheManager.Remove(CacheKey.UserInterface.SiteHelper.LoggedInUser,
                new CacheKeyParameter(nameof(aNumber), aNumber));

            var model = _mapper.Map<User>(message.Model);

            AddCentreIds(message, model);

            auditRecords.Add(new AuditRecord(model, message.CurrentUser, AuditType.Update));

            _userRepository.UpdateUser(model);

            SetUserCentres(message, model, auditRecords);

            message.AuditRecords = auditRecords;
        }

        /// <summary>
        /// Adds the centre ids.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="model">The model.</param>
        private void AddCentreIds(UpdateUser message, User model)
        {
            if (message.Model.UserCentres != null)
            {
                var centreIds = new List<int>();
                var currentrole = _roleRepository.ReadById(model.RoleId);
                
                //if user is in AE(Account-Execitive/ Requestor) or BCM roles, we remove all other centres, as they can only belong to one centre..
                if (currentrole.RoleName == Constants.Roles.Requestor || currentrole.RoleName == Constants.Roles.BCM || currentrole.RoleName == Constants.Roles.AA)
                {
                    //... 
                }
                else
                {
                    //if there were previous, centres add them  again..
                    foreach (var userCentre in message.Model.UserCentres)
                        centreIds.Add(userCentre.Id);

                    if (model.CentreIds != null)
                        centreIds.AddRange(model.CentreIds);
                }

                model.CentreIds = centreIds;
            }

            if (message.Model.CentreId > 0)
            {
                var centreIds = new List<int>();

                if (model.CentreIds != null)
                    centreIds.AddRange(model.CentreIds);

                if (!centreIds.Contains(message.Model.CentreId))
                    centreIds.Add(message.Model.CentreId);

                model.CentreIds = centreIds;
            }
        }

        /// <summary>
        /// Sets the user centres.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="model">The model.</param>
        /// <param name="auditRecords">The audit records.</param>
        private void SetUserCentres(UpdateUser message, User model, List<AuditRecord> auditRecords)
        {
            var userCentres = _centreUserRepository.ReadByUserId(model.Id);

            //delete any centres that shouldn't exist
            foreach (var userCentre in userCentres)
            {
                if (model.CentreIds == null || !model.CentreIds.Any(_ => _ == userCentre.CentreId))
                {
                    _centreUserRepository.Delete(userCentre);
                    auditRecords.Add(new AuditRecord(userCentre, message.CurrentUser, AuditType.Delete));
                }

            }

            //insert the centres that don't exist
            if (model.CentreIds != null && model.CentreIds.Any())
            {
                foreach (var centreId in model.CentreIds)
                {
                    if (!userCentres.Any(_ => _.CentreId == centreId))
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
}
