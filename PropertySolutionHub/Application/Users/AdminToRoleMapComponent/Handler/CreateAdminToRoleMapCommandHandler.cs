using AutoMapper;
using FluentValidation;
using MediatR;
using PropertySolutionHub.Application.Users.AdminComponent.Command;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Users;
using System.ComponentModel.DataAnnotations;

namespace PropertySolutionHub.Application.Users.AdminComponent.Handler
{
    public class CreateAdminToRoleMapCommandHandler : IRequestHandler<CreateAdminToRoleMapCommand, int>
    {
        private readonly IAdminToRoleMapRepository _adminToRoleMap;
        private readonly IMapper _mapper;

        public CreateAdminToRoleMapCommandHandler(IAdminToRoleMapRepository adminToRoleMap, IMapper mapper)
        {
            _adminToRoleMap = adminToRoleMap;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateAdminToRoleMapCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _adminToRoleMap.AddAdminToRoleMap(request.AdminToRoleMap);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating admin: " + ex.Message);
            }
        }
    }
}
