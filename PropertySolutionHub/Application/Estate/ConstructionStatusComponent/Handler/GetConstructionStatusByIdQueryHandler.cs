using AutoMapper;
using ConstructionStatusSolutionHub.Domain.Repository.Estate;
using MediatR;
using PropertySolutionHub.Application.Estate.ConstructionStatusComponent.Query;
using PropertySolutionHub.Application.Estate.PropertyComponent.Command;
using PropertySolutionHub.Application.Estate.PropertyComponent.Query;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Repository.Estate;

namespace PropertySolutionHub.Application.Estate.ConstructionStatusComponent.Handler
{
    public class GetConstructionStatusByIdQueryHandler : IRequestHandler<GetConstructionStatusByIdQuery, ConstructionStatus>
    {
        IConstructionStatusRepository _constructionStatusRepository;
        private readonly IMapper _mapper;

        public GetConstructionStatusByIdQueryHandler(IConstructionStatusRepository constructionStatusRepository, IMapper mapper)
        {
            _constructionStatusRepository = constructionStatusRepository;
            _mapper = mapper;
        }

        public async Task<ConstructionStatus> Handle(GetConstructionStatusByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _constructionStatusRepository.GetConstructionStatusById(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting status: " + ex.Message);
            }
        }
    }
}
