using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.LeaseRequestComponent.Command;
using PropertySolutionHub.Domain.Entities.Lease;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Estate;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.LeaseRequestComponent.Handler
{
    public class UpdateLeaseRequestCommandHandler : IRequestHandler<UpdateLeaseRequestCommand, LeaseRequest>
    {
        private readonly ILeaseRequestRepository _leaseRequestRepository;
        private readonly IMapper _mapper;

        public UpdateLeaseRequestCommandHandler(ILeaseRequestRepository leaseRequestRepository, IMapper mapper)
        {
            _leaseRequestRepository = leaseRequestRepository;
            _mapper = mapper;
        }
        public async Task<LeaseRequest> Handle(UpdateLeaseRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _leaseRequestRepository.UpdateLeaseRequest(request.LeaseRequest);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating lease request: " + ex.Message);
            }
        }
    }
}
