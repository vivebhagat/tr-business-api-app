using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.AdminComponent.Command;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.AdminComponent.Handler
{
    public class AdminRoleCommandHandler : IRequestHandler<AdminRoleCommand, RoleAuthResponse>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public AdminRoleCommandHandler(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        public async Task<RoleAuthResponse> Handle(AdminRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _adminRepository.RoleSelect(request.RoleSelectionRequestDto.Role, request.RoleSelectionRequestDto.RefreshToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
