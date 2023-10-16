using AutoMapper;
using ContractRequestSolutionHub.Domain.Repository.Estate;
using MediatR;
using Newtonsoft.Json;
using PropertySolutionHub.Application.Estate.ContractRequestComponent.Command;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Helper;

namespace PropertySolutionHub.Application.Estate.ContractRequestComponent.Handler
{
    public class CreateContractRequestCommandHandler : IRequestHandler<CreateContractRequestCommand, int>
    {
        private readonly IContractRequestRepository _contractRequestRepository;
        private readonly IMapper _mapper;
        IHttpHelper _httpHelper;

        public CreateContractRequestCommandHandler(IContractRequestRepository contractRequestRepository, IMapper mapper, IHttpHelper httpHelper)
        {
            _contractRequestRepository = contractRequestRepository;
            _mapper = mapper;
            _httpHelper = httpHelper;
        }

        public async Task<int> Handle(CreateContractRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var contractRequestEntity = _mapper.Map<ContractRequest>(request.ContractRequest);
                int requestId = await _contractRequestRepository.CreateContractRequest(contractRequestEntity);
                return requestId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating contract request: " + ex.Message);
            }
        }
    }
}
