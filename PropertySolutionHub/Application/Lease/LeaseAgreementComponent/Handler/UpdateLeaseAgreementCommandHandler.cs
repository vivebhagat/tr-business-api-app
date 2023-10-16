using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.LeaseAgreementComponent.Command;
using PropertySolutionHub.Domain.Entities.Lease;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Estate;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.LeaseAgreementComponent.Handler
{
    public class UpdateLeaseAgreementCommandHandler : IRequestHandler<UpdateLeaseAgreementCommand, LeaseAgreement>
    {
        private readonly ILeaseAgreementRepository _leaseAgreementRepository;
        private readonly IMapper _mapper;

        public UpdateLeaseAgreementCommandHandler(ILeaseAgreementRepository leaseAgreementRepository, IMapper mapper)
        {
            _leaseAgreementRepository = leaseAgreementRepository;
            _mapper = mapper;
        }
        public async Task<LeaseAgreement> Handle(UpdateLeaseAgreementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _leaseAgreementRepository.UpdateLeaseAgreement(request.LeaseAgreement);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating lease agreement: " + ex.Message);
            }
        }
    }
}
