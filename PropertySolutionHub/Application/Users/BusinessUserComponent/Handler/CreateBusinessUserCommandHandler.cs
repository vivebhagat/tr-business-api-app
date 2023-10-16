using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.BusinessUserComponent.Command;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserComponent.Handler
{
    public class CreateBusinessUserCommandHandler : IRequestHandler<CreateBusinessUserCommand, int>
    {
        private readonly IBusinessUserRepository _businessUserRepository;
        private readonly IBusinessUserToRoleMapRepository _businessUserToRoleMapRepository;

        private readonly IMapper _mapper;

        public CreateBusinessUserCommandHandler(IBusinessUserRepository businessUserRepository, IBusinessUserToRoleMapRepository businessUserToRoleMapRepository, IMapper mapper)
        {
            _businessUserRepository = businessUserRepository;
            _businessUserToRoleMapRepository = businessUserToRoleMapRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateBusinessUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var businessUserEntity = _mapper.Map<BusinessUser>(request.BusinessUser);
                int businessuserId =  await _businessUserRepository.CreateBusinessUser(businessUserEntity);

                if(businessuserId != 0)
                {
                    List<BusinessUserToRoleMap> businessUserToRoleMapList = _mapper.Map<List<BusinessUserToRoleMap>>(request.BusinessUserToRoleMapList);

                    foreach (var roleMap in businessUserToRoleMapList)
                    {
                        roleMap.BusinessUserId = businessuserId;
                        await _businessUserToRoleMapRepository.CreateBusinessuserToRoleMap(roleMap);
                    }
                }

                return businessuserId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating business User: " + ex.Message);
            }
        }
    }
}
