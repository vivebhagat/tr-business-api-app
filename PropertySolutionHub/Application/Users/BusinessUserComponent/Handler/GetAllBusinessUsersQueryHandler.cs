using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.BusinessUserComponent.Query;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserComponent.Handler
{
    public class GetAllBusinessUsersQueryHandler : IRequestHandler<GetAllBusinessUsersQuery, List<BusinessUser>>
    {
        private readonly IBusinessUserRepository _businessUserRepository;
        private readonly IMapper _mapper;

        public GetAllBusinessUsersQueryHandler(IBusinessUserRepository businessUserRepository, IMapper mapper)
        {
            _businessUserRepository = businessUserRepository;
            _mapper = mapper;
        }

        public async Task<List<BusinessUser>> Handle(GetAllBusinessUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _businessUserRepository.GetAllBusinessUsers();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting business User list: " + ex.Message);
            }
        }
    }
}

