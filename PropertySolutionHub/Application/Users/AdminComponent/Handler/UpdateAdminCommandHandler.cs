using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.AdminComponent.Command;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.AdminComponent.Handler
{
    public class UpdateAdminCommandHandler : IRequestHandler<UpdateAdminCommand, Admin>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public UpdateAdminCommandHandler(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }
        public async Task<Admin> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _adminRepository.UpdateAdmin(request.Admin);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating admin: " + ex.Message);
            }
        }
    }
}
