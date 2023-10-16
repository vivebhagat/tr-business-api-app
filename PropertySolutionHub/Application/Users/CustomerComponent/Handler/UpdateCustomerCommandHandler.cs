using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.CustomerComponent.Command;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.CustomerComponent.Handler
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _customerRepository.UpdateCustomer(request.Customer);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating customer: " + ex.Message);
            }
        }
    }
}
