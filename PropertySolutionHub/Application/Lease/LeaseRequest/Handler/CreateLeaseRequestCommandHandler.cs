using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Estate.LeaseRequestComponent.Command;
using PropertySolutionHub.Application.Users.LeaseRequestComponent.Command;
using PropertySolutionHub.Domain.Repository.Estate;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Lease.LeaseRequestComponent.Handler
{
    public class CreateLeaseRequestCommandHandler : IRequestHandler<CreateLeaseRequestCommand, int>
    {
        private readonly ILeaseRequestRepository _leaseRequestRepository;
        private readonly IMapper _mapper;

        public CreateLeaseRequestCommandHandler(ILeaseRequestRepository leaseRequestRepository, IMapper mapper)
        {
            _leaseRequestRepository = leaseRequestRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateLeaseRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _leaseRequestRepository.CreateLeaseRequest(request.LeaseRequest);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating lease request: " + ex.Message);
            }
        }
    }
}
