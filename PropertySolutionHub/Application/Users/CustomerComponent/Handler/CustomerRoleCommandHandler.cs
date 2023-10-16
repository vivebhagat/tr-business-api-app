using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.CustomerComponent.Command;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.CustomerComponent.Handler
{
    public class CustomerRoleCommandHandler : IRequestHandler<CustomerRoleCommand, RoleAuthResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerRoleCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<RoleAuthResponse> Handle(CustomerRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _customerRepository.RoleSelect(request.RoleSelectionRequestDto.Role, request.RoleSelectionRequestDto.RefreshToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
