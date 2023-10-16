using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.BusinessUserComponent.Command;
using PropertySolutionHub.Application.Users.BusinessUserRoleComponent.Command;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserRoleComponent.Handler
{
    public class UpdateBusinessUserRoleCommandHandler : IRequestHandler<UpdateBusinessUserRoleCommand, BusinessUserRole>
    {
        private readonly IBusinessUserRoleRepository _businessUserRole;
        private readonly IMapper _mapper;

        public UpdateBusinessUserRoleCommandHandler(IBusinessUserRoleRepository businessUserRole, IMapper mapper)
        {
            _businessUserRole = businessUserRole;
            _mapper = mapper;
        }

        public async Task<BusinessUserRole> Handle(UpdateBusinessUserRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var businessUserRoleEntity = _mapper.Map<BusinessUserRole>(request.BusinessUserRole);
                return await _businessUserRole.UpdateBusinessUserRole(businessUserRoleEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating role: " + ex.Message);
            }
        }
    }
}
