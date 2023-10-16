using MediatR;
using PropertySolutionHub.Application.Users.BusinessUserComponent.Command;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserComponent.Handler
{
    public class BusinessUserLogoutCommandHandler : IRequestHandler<BusinessUserLogoutCommand, bool>
    {
        private readonly IBusinessUserRepository _businessUserRepository;

        public BusinessUserLogoutCommandHandler(IBusinessUserRepository businessUserRepository)
        {
            _businessUserRepository = businessUserRepository;
        }

        public async Task<bool> Handle(BusinessUserLogoutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _businessUserRepository.Logout(request.UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
