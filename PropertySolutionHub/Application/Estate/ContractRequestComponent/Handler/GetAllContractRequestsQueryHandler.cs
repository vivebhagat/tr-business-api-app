using AutoMapper;
using ContractRequestSolutionHub.Domain.Repository.Estate;
using MediatR;
using PropertySolutionHub.Application.Estate.ContractRequestComponent.Query;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.ContractRequestComponent.Handler
{
    public class GetAllContractRequestsQueryHandler : IRequestHandler<GetAllContractRequestsQuery, List<ContractRequest>>
    {
        private readonly IContractRequestRepository _contractRequestRepository;
        private readonly IMapper _mapper;

        public GetAllContractRequestsQueryHandler(IContractRequestRepository contractRequestRepository, IMapper mapper)
        {
            _contractRequestRepository = contractRequestRepository;
            _mapper = mapper;
        }

        public async Task<List<ContractRequest>> Handle(GetAllContractRequestsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _contractRequestRepository.GetAllContractRequests();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting contract request list: " + ex.Message);
            }
        }
    }
}

