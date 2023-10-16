using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.BusinessUserToDomainKeyMapComponent.Query;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Repository.Auth;

namespace PropertySolutionHub.Application.Users.BusinessUserToDomainKeyMapComponent.Handlers
{
    public class ValidateDomainKeyQueryHandler : IRequestHandler<ValidateDomainKeyQuery, DomainDetail>
    {
        private readonly IBaseApplicationUserToDomainKeyMapRepository _baseApplicationUserToDomainKeyMapRepository;
        private readonly IMapper _mapper;

        public ValidateDomainKeyQueryHandler(IBaseApplicationUserToDomainKeyMapRepository baseApplicationUserToDomainKeyMapRepository, IMapper mapper)
        {
            _baseApplicationUserToDomainKeyMapRepository = baseApplicationUserToDomainKeyMapRepository;
            _mapper = mapper;
        }

        public async Task<DomainDetail> Handle(ValidateDomainKeyQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _baseApplicationUserToDomainKeyMapRepository.SelectDomain(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting domain keys: " + ex.Message);
            }
        }
    }
}