using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.CustomerToRoleMapComponent.Query;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.CustomerToRoleMapComponent.Handler
{
    public class GetCustomerRolesByUserIdQueryHandler : IRequestHandler<GetCustomerRolesByUserIdQuery, List<CustomerToRoleMap>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerRolesByUserIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<List<CustomerToRoleMap>> Handle(GetCustomerRolesByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _customerRepository.GetCustomerRoleByUserId(request.UserId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting customer roles: " + ex.Message);
            }
        }
    }
}