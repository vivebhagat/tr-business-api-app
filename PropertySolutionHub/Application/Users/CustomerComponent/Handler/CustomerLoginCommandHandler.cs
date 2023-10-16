using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.CustomerComponent.Command;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.CustomerComponent.Handler
{
    public class CustomerLoginCommandHandler : IRequestHandler<CustomerLoginCommand, RoleAuthResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerLoginCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<RoleAuthResponse> Handle(CustomerLoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _customerRepository.Login(request.LoginRequestDto.UserName, request.LoginRequestDto.PassWord);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
