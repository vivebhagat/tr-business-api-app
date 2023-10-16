using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.BusinessUserComponent.Command;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserComponent.Handler
{
    public class BusinessUserRoleCommandHandler : IRequestHandler<BusinessUserRoleCommand, RoleAuthResponse>
    {
        private readonly IBusinessUserRepository _businessUserRepository;
        private readonly IMapper _mapper;

        public BusinessUserRoleCommandHandler(IBusinessUserRepository businessUserRepository, IMapper mapper)
        {
            _businessUserRepository = businessUserRepository;
            _mapper = mapper;
        }

        public async Task<RoleAuthResponse> Handle(BusinessUserRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _businessUserRepository.RoleSelect(request.RoleSelectionRequestDto.Role, request.RoleSelectionRequestDto.RefreshToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
