using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.AdminComponent.Command;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.AdminComponent.Handler
{
    public class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommand, bool>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public DeleteAdminCommandHandler(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _adminRepository.DeleteAdmin(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating admin: " + ex.Message);
            }
        }
    }
}
