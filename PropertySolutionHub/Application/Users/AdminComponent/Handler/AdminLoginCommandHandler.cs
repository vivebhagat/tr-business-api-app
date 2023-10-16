using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.AdminComponent.Command;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.AdminComponent.Handler
{
    public class AdminLoginCommandHandler : IRequestHandler<AdminLoginCommand, AuthResponse>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public AdminLoginCommandHandler(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        public async Task<AuthResponse> Handle(AdminLoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _adminRepository.Login(request.LoginRequestDto.UserName, request.LoginRequestDto.PassWord);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
