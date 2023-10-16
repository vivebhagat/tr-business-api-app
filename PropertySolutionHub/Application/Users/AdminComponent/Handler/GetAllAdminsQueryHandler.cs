using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.AdminComponent.Query;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.AdminComponent.Handler
{
    public class GetAllAdminsQueryHandler : IRequestHandler<GetAllAdminsQuery, List<Admin>>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public GetAllAdminsQueryHandler(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        public async Task<List<Admin>> Handle(GetAllAdminsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _adminRepository.GetAllAdmins();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting admin list: " + ex.Message);
            }
        }
    }
}

