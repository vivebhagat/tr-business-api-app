using AutoMapper;
using PropertySolutionHub.Domain.Repository.Estate;
using MediatR;
using PropertySolutionHub.Application.Estate.CommunityTypeComponent.Query;
using PropertySolutionHub.Application.Estate.PropertyComponent.Command;
using PropertySolutionHub.Application.Estate.PropertyComponent.Query;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.CommunityTypeComponent.Handler
{
    public class GetCommunityTypeByIdQueryHandler : IRequestHandler<GetCommunityTypeByIdQuery, CommunityType>
    {
        ICommunityTypeRepository _communityTypeRepository;
        private readonly IMapper _mapper;

        public GetCommunityTypeByIdQueryHandler(ICommunityTypeRepository communityTypeRepository, IMapper mapper)
        {
            _communityTypeRepository = communityTypeRepository;
            _mapper = mapper;
        }

        public async Task<CommunityType> Handle(GetCommunityTypeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _communityTypeRepository.GetCommunityTypeById(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting community type: " + ex.Message);
            }
        }
    }
}
