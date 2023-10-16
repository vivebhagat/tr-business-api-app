using AutoMapper;
using FluentValidation;
using MediatR;
using PropertySolutionHub.Application.Users.BusinessUserComponent.Command;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Users;
using System.ComponentModel.DataAnnotations;

namespace PropertySolutionHub.Application.Users.BusinessUserComponent.Handler
{
    public class CreateBusinessUserToRoleMapCommandHandler : IRequestHandler<CreateBusinessUserToRoleMapCommand, int>
    {
        private readonly IBusinessUserToRoleMapRepository _businessUserToRoleMap;
        private readonly IMapper _mapper;

        public CreateBusinessUserToRoleMapCommandHandler(IBusinessUserToRoleMapRepository businessUserToRoleMap, IMapper mapper)
        {
            _businessUserToRoleMap = businessUserToRoleMap;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateBusinessUserToRoleMapCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating businessUser: " + ex.Message);
            }
        }
    }
}
