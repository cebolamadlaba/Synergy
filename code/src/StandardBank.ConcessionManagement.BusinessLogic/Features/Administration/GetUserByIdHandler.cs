using AutoMapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    public class GetUserByIdHandler : MediatR.IRequestHandler<GetUserById, User>
    {
        private readonly IAdminRepository adminRepository;
        private readonly IMapper mapper;

        public GetUserByIdHandler(IAdminRepository adminRepository,IMapper mapper)
        {
            this.adminRepository = adminRepository;
            this.mapper = mapper;
        }
        public User Handle(GetUserById message)
        {
            var result = adminRepository.GetUser(message.Id);
            return mapper.Map<User>(result);
        }
    }
}
