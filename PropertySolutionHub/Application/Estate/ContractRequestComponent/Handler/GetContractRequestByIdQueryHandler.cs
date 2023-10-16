using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Estate.ContractRequestComponent.Query;
using PropertySolutionHub.Domain.Entities.Estate;
using ContractRequestSolutionHub.Domain.Repository.Estate;

namespace PropertySolutionHub.Application.Estate.ContractRequestComponent.Handler;

public class GetContractRequestByIdQueryHandler : IRequestHandler<GetContractRequestByIdQuery, ContractRequest>
{
    private readonly IContractRequestRepository _contractRequestRepository;
    private readonly IMapper _mapper;

    public GetContractRequestByIdQueryHandler(IContractRequestRepository contractRequestRepository, IMapper mapper)
    {
        _contractRequestRepository = contractRequestRepository;
        _mapper = mapper;
    }

    public async Task<ContractRequest> Handle(GetContractRequestByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _contractRequestRepository.GetContractRequestById(request.Id);
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting contract request: " + ex.Message);
        }
    }
}
