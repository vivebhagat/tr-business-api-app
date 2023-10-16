using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.BusinessUserComponent.Command;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserComponent.Handler
{
    public class BusinessUserLoginCommandHandler : IRequestHandler<BusinessUserLoginCommand, AuthResponse>
    {
        private readonly IBusinessUserRepository _businessUserRepository;
        private readonly IMapper _mapper;

        public BusinessUserLoginCommandHandler(IBusinessUserRepository businessUserRepository, IMapper mapper)
        {
            _businessUserRepository = businessUserRepository;
            _mapper = mapper;
        }

        public async Task<AuthResponse> Handle(BusinessUserLoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _businessUserRepository.Login(request.LoginRequestDto.UserName, request.LoginRequestDto.PassWord);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
