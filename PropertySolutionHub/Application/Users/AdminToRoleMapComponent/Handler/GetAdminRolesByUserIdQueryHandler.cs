using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.AdminToRoleMapComponent.Query;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.AdminToRoleMapComponent.Handler
{
    public class GetAdminRolesByUserIdQueryHandler : IRequestHandler<GetAdminRolesByUserIdQuery, List<AdminToRoleMap>>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public GetAdminRolesByUserIdQueryHandler(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        public async Task<List<AdminToRoleMap>> Handle(GetAdminRolesByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _adminRepository.GetAdminRoleByUserId(request.UserId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting admin roles: " + ex.Message);
            }
        }
    }
}