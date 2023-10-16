using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Users.BusinessUserComponent.Query;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserComponent.Handler
{
    public class GetBusinessUserByUserIdQueryHandler : IRequestHandler<GetBusinessUserByUserIdQuery, BusinessUser>
    {
        private readonly IBusinessUserRepository _businessUserRepository;
        private readonly IMapper _mapper;

        public GetBusinessUserByUserIdQueryHandler(IBusinessUserRepository businessUserRepository, IMapper mapper)
        {
            _businessUserRepository = businessUserRepository;
            _mapper = mapper;
        }

        public async Task<BusinessUser> Handle(GetBusinessUserByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _businessUserRepository.GetBusinessUserByUserId(request.UserId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting businessUser: " + ex.Message);
            }
        }
    }
}

