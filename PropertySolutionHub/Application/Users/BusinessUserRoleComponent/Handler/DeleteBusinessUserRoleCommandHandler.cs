using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.BusinessUserComponent.Command;
using PropertySolutionHub.Application.Users.BusinessUserRoleComponent.Command;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserRoleComponent.Handler
{
    public class DeleteBusinessUserRoleCommandHandler : IRequestHandler<DeleteBusinessUserRoleCommand, bool>
    {
        private readonly IBusinessUserRoleRepository _businessUserRole;
        private readonly IMapper _mapper;

        public DeleteBusinessUserRoleCommandHandler(IBusinessUserRoleRepository businessUserRole, IMapper mapper)
        {
            _businessUserRole = businessUserRole;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteBusinessUserRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _businessUserRole.DeleteBusinessUserRole(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating role: " + ex.Message);
            }
        }
    }
}
