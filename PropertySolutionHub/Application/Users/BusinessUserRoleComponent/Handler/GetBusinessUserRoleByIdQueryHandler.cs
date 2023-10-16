using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.BusinessUserToRoleMapComponent.Query;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserRoleComponent.Handler
{
    public class GetBusinessUserRoleByIdQueryHandler : IRequestHandler<GetBusinessUserRolesByIdQuery, BusinessUserRole>
    {
        private readonly IBusinessUserRoleRepository _businessUserRole;
        private readonly IMapper _mapper;

        public GetBusinessUserRoleByIdQueryHandler(IBusinessUserRoleRepository businessUserRole, IMapper mapper)
        {
            _businessUserRole = businessUserRole;
            _mapper = mapper;
        }

        public async Task<BusinessUserRole> Handle(GetBusinessUserRolesByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _businessUserRole.GetBusinessuserRoleById(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting roles: " + ex.Message);
            }
        }
    }
}