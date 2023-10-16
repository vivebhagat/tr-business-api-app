using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.BusinessUserToRoleMapComponent.Query;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserToRoleMapComponent.Handler
{
    public class GetAllBusinessUserRoleQueryHandler : IRequestHandler<GetAllBusinessuserRolesQuery, List<BusinessUserRole>>
    {
        private readonly IBusinessUserRoleRepository _businessUserRole;
        private readonly IMapper _mapper;

        public GetAllBusinessUserRoleQueryHandler(IBusinessUserRoleRepository businessUserRole, IMapper mapper)
        {
            _businessUserRole = businessUserRole;
            _mapper = mapper;
        }

        public async Task<List<BusinessUserRole>> Handle(GetAllBusinessuserRolesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _businessUserRole.GetAllBusinessUserRoles();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting roles: " + ex.Message);
            }
        }
    }
}