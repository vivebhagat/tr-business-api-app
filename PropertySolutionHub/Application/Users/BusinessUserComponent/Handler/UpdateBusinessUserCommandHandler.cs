using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.BusinessUserComponent.Command;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserComponent.Handler
{
    public class UpdateBusinessUserCommandHandler : IRequestHandler<UpdateBusinessUserCommand, BusinessUser>
    {
        private readonly IBusinessUserRepository _businessUserRepository;
        private readonly IBusinessUserToRoleMapRepository _businessUserToRoleMapRepository;
        private readonly IMapper _mapper;

        public UpdateBusinessUserCommandHandler(IBusinessUserRepository businessUserRepository, IBusinessUserToRoleMapRepository businessUserToRoleMapRepository, IMapper mapper)
        {
            _businessUserRepository = businessUserRepository;
            _businessUserToRoleMapRepository = businessUserToRoleMapRepository;
            _mapper = mapper;
        }
        public async Task<BusinessUser> Handle(UpdateBusinessUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var businessUserEntity = _mapper.Map<BusinessUser>(request.BusinessUser);
                var businessUser =  await _businessUserRepository.UpdateBusinessUser(businessUserEntity);


                if (businessUser != null)
                {
                    List<BusinessUserToRoleMap> businessUserToRoleMapList = _mapper.Map<List<BusinessUserToRoleMap>>(request.BusinessUserToRoleMapList);

                    foreach (var roleMap in businessUserToRoleMapList)
                    {
                        roleMap.BusinessUserId = businessUser.Id;
                        await _businessUserToRoleMapRepository.UpdateBusinessuserToRoleMap(roleMap);
                    }
                }

                return businessUser;

            }
            catch (Exception ex)
            {
                throw new Exception("Error updating business User: " + ex.Message);
            }
        }
    }
}
