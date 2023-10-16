using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Shared.BusinessUserToDomainKeyMapComponent.Query;
using PropertySolutionHub.Domain.Entities.Shared;
using PropertySolutionHub.Domain.Repository.Auth;

namespace PropertySolutionHub.Application.Shared.BusinessUserToDomainKeyMapComponent.Handlers
{
    public class GetDomainKeyForRoleQueryHandler : IRequestHandler<GetDomainKeyForUserQuery, List<BaseApplicationUserToDomainKeyMap>>
    {
        private readonly IBaseApplicationUserToDomainKeyMapRepository _baseApplicationUserToDomainKeyMapRepository;
        private readonly IMapper _mapper;

        public GetDomainKeyForRoleQueryHandler(IBaseApplicationUserToDomainKeyMapRepository baseApplicationUserToDomainKeyMapRepository, IMapper mapper)
        {
            _baseApplicationUserToDomainKeyMapRepository = baseApplicationUserToDomainKeyMapRepository;
            _mapper = mapper;
        }

        public async Task<List<BaseApplicationUserToDomainKeyMap>> Handle(GetDomainKeyForUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _baseApplicationUserToDomainKeyMapRepository.GetDomainKeyForUsers(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting domain keys: " + ex.Message);
            }
        }
    }
}