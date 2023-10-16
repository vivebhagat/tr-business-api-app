using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.AdminComponent.Query;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.AdminComponent.Handler
{
    public class GetAdminByIdQueryHandler : IRequestHandler<GetAdminByIdQuery, Admin>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public GetAdminByIdQueryHandler(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        public async Task<Admin> Handle(GetAdminByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _adminRepository.GetAdminById(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting admin: " + ex.Message);
            }
        }
    }
}

