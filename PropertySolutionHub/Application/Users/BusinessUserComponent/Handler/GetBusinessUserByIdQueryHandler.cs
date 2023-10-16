using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.QueryModels.User;
using PropertySolutionHub.Application.Users.BusinessUserComponent.Query;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserComponent.Handler
{
    public class GetBusinessUserByIdQueryHandler : IRequestHandler<GetBusinessUserByIdQuery, GetBusinessuser>
    {
        private readonly IBusinessUserRepository _businessUserRepository;
        private readonly IBusinessUserToRoleMapRepository _businessUserToRoleMapRepository;
        private readonly IMapper _mapper;

        public GetBusinessUserByIdQueryHandler(IBusinessUserRepository businessUserRepository, IBusinessUserToRoleMapRepository businessUserToRoleMapRepository, IMapper mapper)
        {
            _businessUserRepository = businessUserRepository;
            _businessUserToRoleMapRepository = businessUserToRoleMapRepository;
            _mapper = mapper;
        }

        public async Task<GetBusinessuser> Handle(GetBusinessUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                BusinessUser businessUser = await _businessUserRepository.GetBusinessUserById(request.Id);
                List<BusinessUserToRoleMap> businessUserToRoleMapList = await _businessUserToRoleMapRepository.GetBusinessUserRoleListById(businessUser.Id);

                GetBusinessuser getBusinessUser = new GetBusinessuser
                {
                    BusinessUser = businessUser,
                    BusinessUserToRoleMapList = businessUserToRoleMapList
                };

                return getBusinessUser;

            }
            catch (Exception ex)
            {
                throw new Exception("Error getting businessUser: " + ex.Message);
            }
        }
    }
}

