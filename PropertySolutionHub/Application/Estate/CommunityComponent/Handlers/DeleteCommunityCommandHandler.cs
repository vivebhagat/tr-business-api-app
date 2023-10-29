using MediatR;
using PropertySolutionHub.Application.Estate.CommunityComponent.Command;
using PropertySolutionHub.Domain.Helper;
using CommunitySolutionHub.Domain.Repository.Estate;

namespace PropertySolutionHub.Application.Users.CommunityComponent.Handler
{
    public class DeleteCommunityCommandHandler : IRequestHandler<DeleteCommunityCommand, bool>
    {
        private readonly ICommunityRepository _communityRepository;
        IHttpHelper httpHelper;

        public DeleteCommunityCommandHandler(ICommunityRepository communityRepository, IHttpHelper httpHelper)
        {
            _communityRepository = communityRepository;
            this.httpHelper = httpHelper;
        }

        public async Task<bool> Handle(DeleteCommunityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                int remoteId = await _communityRepository.DeleteCommunity(request.Id);

                if (remoteId != 0)
                {
                    var result = await _communityRepository.DeleteRemoteCommunity(remoteId);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating proeprty: " + ex.Message);
            }
        }
    }

}
