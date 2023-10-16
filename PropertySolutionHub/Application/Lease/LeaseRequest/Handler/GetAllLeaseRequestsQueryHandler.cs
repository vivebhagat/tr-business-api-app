using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Estate.LeaseRequestComponent.Query;
using PropertySolutionHub.Domain.Entities.Lease;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Estate;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.LeaseRequestComponent.Handler
{
    public class GetAllLeaseRequestsQueryHandler : IRequestHandler<GetAllLeaseRequestsQuery, List<LeaseRequest>>
    {
        private readonly ILeaseRequestRepository _leaseRequestRepository;
        private readonly IMapper _mapper;

        public GetAllLeaseRequestsQueryHandler(ILeaseRequestRepository leaseRequestRepository, IMapper mapper)
        {
            _leaseRequestRepository = leaseRequestRepository;
            _mapper = mapper;
        }

        public async Task<List<LeaseRequest>> Handle(GetAllLeaseRequestsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _leaseRequestRepository.GetAllLeaseRequests();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting lease request list: " + ex.Message);
            }
        }
    }
}

