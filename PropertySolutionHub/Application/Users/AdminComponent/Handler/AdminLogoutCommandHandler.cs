using MediatR;
using PropertySolutionHub.Application.Users.AdminComponent.Command;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.AdminComponent.Handler
{
    public class AdminLogoutCommandHandler : IRequestHandler<AdminLogoutCommand, bool>
    {
        private readonly IAdminRepository _adminRepository;

        public AdminLogoutCommandHandler(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<bool> Handle(AdminLogoutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _adminRepository.Logout(request.UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
