using AutoMapper;
using FluentValidation;
using MediatR;
using PropertySolutionHub.Application.Users.AdminComponent.Command;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Users;
using System.ComponentModel.DataAnnotations;

namespace PropertySolutionHub.Application.Users.AdminComponent.Handler
{
    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, int>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public CreateAdminCommandHandler(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _adminRepository.CreateAdmin(request.Admin, request.DataRoute);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating admin: " + ex.Message);
            }
        }
    }
}
