using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.QueryModels.User;
using PropertySolutionHub.Application.Users.BusinessUserComponent.Query;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Repository.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserComponent.Handler
{
    public class GetBusinessUserDetailsByIdQueryHandler : IRequestHandler<GetBusinessUserDetailsByIdQuery, BusinessUser>
    {
        private readonly IBusinessUserRepository _businessUserRepository;
        private readonly IMapper _mapper;

        public GetBusinessUserDetailsByIdQueryHandler(IBusinessUserRepository businessUserRepository, IMapper mapper)
        {
            _businessUserRepository = businessUserRepository;
            _mapper = mapper;
        }

        public async Task<BusinessUser> Handle(GetBusinessUserDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
               return await _businessUserRepository.GetBusinessUserById(request.Id);

            }
            catch (Exception ex)
            {
                throw new Exception("Error getting businessUser: " + ex.Message);
            }
        }
    }
}

