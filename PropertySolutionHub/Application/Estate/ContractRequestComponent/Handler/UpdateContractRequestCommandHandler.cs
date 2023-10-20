using AutoMapper;
using ContractRequestSolutionHub.Domain.Repository.Estate;
using MediatR;
using Newtonsoft.Json;
using PropertySolutionHub.Application.Estate.ContractRequestComponent.Command;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Helper;

namespace PropertySolutionHub.Application.Estate.ContractRequestComponent.Handler
{
    public class UpdateContractRequestCommandHandler : IRequestHandler<UpdateContractRequestCommand, ContractRequest>
    {
        private readonly IContractRequestRepository _contractRequestRepository;
        private readonly IMapper _mapper;
        IHttpHelper _httpHelper;

        public UpdateContractRequestCommandHandler(IContractRequestRepository contractRequestRepository, IMapper mapper, IHttpHelper httpHelper)
        {
            _contractRequestRepository = contractRequestRepository;
            _mapper = mapper;
            _httpHelper = httpHelper;
        }
        public async Task<ContractRequest> Handle(UpdateContractRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var contractRequestEntity = _mapper.Map<ContractRequest>(request.ContractRequest);
                ContractRequest contractRequest = await _contractRequestRepository.UpdateContractRequest(contractRequestEntity);

                if (contractRequest != null)
                {
                    int tempPropertyId = request.ContractRequest.PropertyId;
                    request.ContractRequest.PropertyId = contractRequest.Property.RemoteId;
                    string postData = JsonConvert.SerializeObject(request);
                    await _contractRequestRepository.UpdateRemoteContractRequest(postData);
                    request.ContractRequest.PropertyId = tempPropertyId;
                }

                return contractRequest;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating contract request: " + ex.Message);
            }
        }
    }
}
