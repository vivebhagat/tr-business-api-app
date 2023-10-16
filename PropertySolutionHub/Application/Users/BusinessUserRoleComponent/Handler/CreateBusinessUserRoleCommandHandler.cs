using AutoMapper;
using FluentValidation;
using MediatR;
using PropertySolutionHub.Application.Users.BusinessUserComponent.Command;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Users;
using System.ComponentModel.DataAnnotations;

namespace PropertySolutionHub.Application.Users.BusinessUserComponent.Handler
{
    public class CreateBusinessUserRoleCommandHandler : IRequestHandler<CreateBusinessUserRoleCommand, int>
    {
        private readonly IBusinessUserRoleRepository _businessUserRole;
        private readonly IMapper _mapper;

        public CreateBusinessUserRoleCommandHandler(IBusinessUserRoleRepository businessUserRole, IMapper mapper)
        {
            _businessUserRole = businessUserRole;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateBusinessUserRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var businessUserRoleEntity = _mapper.Map<BusinessUserRole>(request.BusinessUserRole);
                return await _businessUserRole.CreateBusinessUserRole(businessUserRoleEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating role: " + ex.Message);
            }
        }
    }
}
