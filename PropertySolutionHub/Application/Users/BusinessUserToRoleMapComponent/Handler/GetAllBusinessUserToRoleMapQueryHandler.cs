using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.BusinessUserToRoleMapComponent.Query;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserToRoleMapComponent.Handler
{
    public class GetAllBusinessUserToRoleMapQueryHandler : IRequestHandler<GetAllBusinessuserRolesQuery, List<BusinessUserRole>>
    {
        private readonly IBusinessUserToRoleMapRepository _businessUserToRoleMapRepository;
        private readonly IMapper _mapper;

        public GetAllBusinessUserToRoleMapQueryHandler(IBusinessUserToRoleMapRepository businessUserToRoleMapRepository, IMapper mapper)
        {
            _businessUserToRoleMapRepository = businessUserToRoleMapRepository;
            _mapper = mapper;
        }

        public async Task<List<BusinessUserRole>> Handle(GetAllBusinessuserRolesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _businessUserToRoleMapRepository.GetAllBusinessUserRoles();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting roles: " + ex.Message);
            }
        }
    }
}