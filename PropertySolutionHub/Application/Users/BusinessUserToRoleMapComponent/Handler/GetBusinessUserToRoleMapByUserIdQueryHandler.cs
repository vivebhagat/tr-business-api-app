using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.BusinessUserToRoleMapComponent.Query;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserToRoleMapComponent.Handler
{
    public class GetBusinessUserToRoleMapByUserIdQueryHandler : IRequestHandler<GetBusinessUserToRoleMapByUserIdQuery, List<BusinessUserToRoleMap>>
    {
        private readonly IBusinessUserToRoleMapRepository _businessUserToRoleMapRepository;
        private readonly IMapper _mapper;

        public GetBusinessUserToRoleMapByUserIdQueryHandler(IBusinessUserToRoleMapRepository businessUserToRoleMapRepository, IMapper mapper)
        {
            _businessUserToRoleMapRepository = businessUserToRoleMapRepository;
            _mapper = mapper;
        }

        public async Task<List<BusinessUserToRoleMap>> Handle(GetBusinessUserToRoleMapByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _businessUserToRoleMapRepository.GetBusinessUserRoleByUserId(request.UserId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting businessUser roles: " + ex.Message);
            }
        }
    }
}