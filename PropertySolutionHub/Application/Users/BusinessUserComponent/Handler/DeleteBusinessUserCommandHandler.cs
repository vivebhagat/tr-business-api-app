using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.BusinessUserComponent.Command;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserComponent.Handler
{
    public class DeleteBusinessUserCommandHandler : IRequestHandler<DeleteBusinessUserCommand, bool>
    {
        private readonly IBusinessUserRepository _businessUserRepository;
        private readonly IMapper _mapper;

        public DeleteBusinessUserCommandHandler(IBusinessUserRepository BusinessUserRepository, IMapper mapper)
        {
            _businessUserRepository = BusinessUserRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteBusinessUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _businessUserRepository.DeleteBusinessUser(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating Business User: " + ex.Message);
            }
        }
    }
}
