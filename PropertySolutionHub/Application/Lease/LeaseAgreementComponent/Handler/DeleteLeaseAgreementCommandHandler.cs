using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.LeaseAgreementComponent.Command;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Estate;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.LeaseAgreementComponent.Handler
{
    public class DeleteLeaseAgreementCommandHandler : IRequestHandler<DeleteLeaseAgreementCommand, bool>
    {
        private readonly ILeaseAgreementRepository _leaseAgreementRepository;
        private readonly IMapper _mapper;

        public DeleteLeaseAgreementCommandHandler(ILeaseAgreementRepository leaseAgreementRepository, IMapper mapper)
        {
            _leaseAgreementRepository = leaseAgreementRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteLeaseAgreementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _leaseAgreementRepository.DeleteLeaseAgreement(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating lease agreement: " + ex.Message);
            }
        }
    }
}
