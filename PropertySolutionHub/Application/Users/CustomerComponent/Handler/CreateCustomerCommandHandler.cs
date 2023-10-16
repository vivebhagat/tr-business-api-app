using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.CustomerComponent.Command;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.CustomerComponent.Handler
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _customerRepository.CreateCustomer(request.Customer, request.DataRoute);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating customer: " + ex.Message);
            }
        }
    }
}
