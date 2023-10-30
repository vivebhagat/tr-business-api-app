using AutoMapper;
using PropertySolutionHub.Domain.Repository.Estate;
using MediatR;
using Newtonsoft.Json;
using PropertySolutionHub.Application.Estate.ConstructionStatusComponent.Command;
using PropertySolutionHub.Application.Estate.PropertyComponent.Command;

namespace PropertySolutionHub.Application.Estate.ConstructionStatusComponent.Handler
{
    public class CreateConstructionStatusCommandHandler : IRequestHandler<CreateConstructionStatusCommand, int>
    {
        IConstructionStatusRepository _constructionStatusRepository;
        private readonly IMapper _mapper;

        public CreateConstructionStatusCommandHandler(IConstructionStatusRepository constructionStatusRepository, IMapper mapper)
        {
            _constructionStatusRepository = constructionStatusRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateConstructionStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _constructionStatusRepository.CreateConstructionStatus(request.ConstructionStatus);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating status: " + ex.Message);
            }
        }
    }
}
