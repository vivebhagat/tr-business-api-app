using AutoMapper;
using PropertySolutionHub.Domain.Repository.Estate;
using MediatR;
using Newtonsoft.Json;
using PropertySolutionHub.Application.Estate.ConstructionStatusComponent.Command;
using PropertySolutionHub.Application.Estate.PropertyComponent.Command;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.ConstructionStatusComponent.Handler
{
    public class DeleteConstructionStatusCommandHandler : IRequestHandler<DeleteConstructionStatusCommand, bool>
    {
        IConstructionStatusRepository _constructionStatusRepository;
        private readonly IMapper _mapper;

        public DeleteConstructionStatusCommandHandler(IConstructionStatusRepository constructionStatusRepository, IMapper mapper)
        {
            _constructionStatusRepository = constructionStatusRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteConstructionStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _constructionStatusRepository.DeleteConstructionStatus(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting status: " + ex.Message);
            }
        }
    }
}
