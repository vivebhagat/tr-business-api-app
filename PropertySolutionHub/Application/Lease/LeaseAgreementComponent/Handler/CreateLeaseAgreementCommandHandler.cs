using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Estate.LeaseAgreementComponent.Command;
using PropertySolutionHub.Application.Users.LeaseAgreementComponent.Command;
using PropertySolutionHub.Domain.Repository.Estate;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Lease.LeaseAgreementComponent.Handler
{
    public class CreateLeaseAgreementCommandHandler : IRequestHandler<CreateLeaseAgreementCommand, int>
    {
        private readonly ILeaseAgreementRepository _leaseAgreementRepository;
        private readonly IMapper _mapper;

        public CreateLeaseAgreementCommandHandler(ILeaseAgreementRepository leaseAgreementRepository, IMapper mapper)
        {
            _leaseAgreementRepository = leaseAgreementRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateLeaseAgreementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _leaseAgreementRepository.CreateLeaseAgreement(request.LeaseAgreement);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating lease agreement: " + ex.Message);
            }
        }
    }
}
