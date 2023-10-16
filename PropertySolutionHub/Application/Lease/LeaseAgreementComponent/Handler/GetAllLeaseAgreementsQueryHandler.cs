using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Estate.LeaseAgreementComponent.Query;
using PropertySolutionHub.Domain.Entities.Lease;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Estate;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.LeaseAgreementComponent.Handler
{
    public class GetAllLeaseAgreementsQueryHandler : IRequestHandler<GetAllLeaseAgreementsQuery, List<LeaseAgreement>>
    {
        private readonly ILeaseAgreementRepository _leaseAgreementRepository;
        private readonly IMapper _mapper;

        public GetAllLeaseAgreementsQueryHandler(ILeaseAgreementRepository leaseAgreementRepository, IMapper mapper)
        {
            _leaseAgreementRepository = leaseAgreementRepository;
            _mapper = mapper;
        }

        public async Task<List<LeaseAgreement>> Handle(GetAllLeaseAgreementsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _leaseAgreementRepository.GetAllLeaseAgreements();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting lease agreement list: " + ex.Message);
            }
        }
    }
}

