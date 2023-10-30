using MediatR;
using PropertySolutionHub.Application.Estate.CommunityToPropertyMapComponent.Command;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Domain.Repository.Estate;

namespace PropertySolutionHub.Application.Users.CommunityToPropertyMapComponent.Handler
{
    public class DeleteCommunityToPropertyMapCommandHandler : IRequestHandler<DeleteCommunityToPropertyMapCommand, bool>
    {
        private readonly ICommunityToPropertyMapRepository _communityToPropertyMapRepository;
        IHttpHelper httpHelper;

        public DeleteCommunityToPropertyMapCommandHandler(ICommunityToPropertyMapRepository communityToPropertyMapRepository, IHttpHelper httpHelper)
        {
            _communityToPropertyMapRepository = communityToPropertyMapRepository;
            this.httpHelper = httpHelper;
        }

        public async Task<bool> Handle(DeleteCommunityToPropertyMapCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _communityToPropertyMapRepository.DeleteCommunityToPropertyMap(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating proeprty: " + ex.Message);
            }
        }
    }

}
