using AutoMapper;
using ContractRequestSolutionHub.Domain.Repository.Estate;
using MediatR;
using PropertySolutionHub.Application.Estate.ContractRequestComponent.Command;
using PropertySolutionHub.Domain.Helper;

namespace PropertySolutionHub.Application.Estate.ContractRequestComponent.Handler
{
    public class DeleteContractRequestCommandHandler : IRequestHandler<DeleteContractRequestCommand, bool>
    {
        private readonly IContractRequestRepository _contractRequestRepository;
        private readonly IMapper _mapper;
        IHttpHelper _httpHelper;


        public DeleteContractRequestCommandHandler(IContractRequestRepository contractRequestRepository, IMapper mapper, IHttpHelper httpHelper)
        {
            _contractRequestRepository = contractRequestRepository;
            _mapper = mapper;
            _httpHelper = httpHelper;
        }

        public async Task<bool> Handle(DeleteContractRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                int remoteId = await _contractRequestRepository.DeleteContractRequest(request.Id);

                if (remoteId != 0)
                {
                    var result = _contractRequestRepository.DeleteRemoteContractRequest(remoteId);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating contract request: " + ex.Message);
            }
        }
    }
}
