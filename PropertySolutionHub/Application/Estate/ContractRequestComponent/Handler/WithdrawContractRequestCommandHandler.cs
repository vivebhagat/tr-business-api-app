using AutoMapper;
using ContractRequestSolutionHub.Domain.Repository.Estate;
using MediatR;
using PropertySolutionHub.Application.Estate.ContractRequestComponent.Command;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Helper;

namespace PropertySolutionHub.Application.Estate.ContractRequestComponent.Handler
{
    public class WithdrawContractRequestCommandHandler : IRequestHandler<WithdrawContractRequestCommand, bool>
    {
        private readonly IContractRequestRepository _contractRequestRepository;
        private readonly IMapper _mapper;
        IHttpHelper _httpHelper;

        public WithdrawContractRequestCommandHandler(IContractRequestRepository contractRequestRepository, IMapper mapper, IHttpHelper httpHelper)
        {
            _contractRequestRepository = contractRequestRepository;
            _mapper = mapper;
            _httpHelper = httpHelper;
        }
        public async Task<bool> Handle(WithdrawContractRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _contractRequestRepository.WithdrawContractRequest(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating application: " + ex.Message);
            }
        }
    }
}
