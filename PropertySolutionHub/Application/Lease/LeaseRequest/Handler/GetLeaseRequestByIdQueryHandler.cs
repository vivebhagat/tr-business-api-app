using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Estate.LeaseRequestComponent.Query;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Entities.Lease;
using PropertySolutionHub.Domain.Repository.Estate;

namespace PropertySolutionHub.Application.Estate.LeaseRequestComponent.Handler
{
    public class GetLeaseRequestByIdQueryHandler : IRequestHandler<GetLeaseRequestByIdQuery, LeaseRequest>
    {
        private readonly ILeaseRequestRepository _leaseRequestRepository;
        private readonly IMapper _mapper;

        public GetLeaseRequestByIdQueryHandler(ILeaseRequestRepository leaseRequestRepository, IMapper mapper)
        {
            _leaseRequestRepository = leaseRequestRepository;
            _mapper = mapper;
        }

        public async Task<LeaseRequest> Handle(GetLeaseRequestByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _leaseRequestRepository.GetLeaseRequestById(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting lease request: " + ex.Message);
            }
        }
    }
}

