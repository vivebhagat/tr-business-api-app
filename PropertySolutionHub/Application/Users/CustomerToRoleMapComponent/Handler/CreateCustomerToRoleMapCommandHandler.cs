using AutoMapper;
using FluentValidation;
using MediatR;
using PropertySolutionHub.Application.Users.AdminComponent.Command;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Users;
using System.ComponentModel.DataAnnotations;

namespace PropertySolutionHub.Application.Users.AdminComponent.Handler
{
    public class CreateCustomerToRoleMapCommandHandler : IRequestHandler<CreateAdminToRoleMapCommand, int>
    {
        private readonly IAdminToRoleMapRepository _customerToRoleMap;
        private readonly IMapper _mapper;

        public CreateCustomerToRoleMapCommandHandler(IAdminToRoleMapRepository customerToRoleMap, IMapper mapper)
        {
            _customerToRoleMap = customerToRoleMap;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateAdminToRoleMapCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _customerToRoleMap.AddAdminToRoleMap(request.AdminToRoleMap);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating customer: " + ex.Message);
            }
        }
    }
}
