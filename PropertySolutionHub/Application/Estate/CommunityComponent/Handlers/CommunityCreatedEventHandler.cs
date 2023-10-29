using CommunitySolutionHub.Domain.Repository.Estate;
using MediatR;
using Newtonsoft.Json;
using PropertySolutionHub.Application.Estate.CommunityComponent.Notification;

namespace PropertySolutionHub.Application.Estate.CommunityComponent.Handlers
{
    public class CommunityCreatedEventHandler: INotificationHandler<CommunityCreatedEvent>
	{
        private CommunityRepository _communityRepository;
        public CommunityCreatedEventHandler(CommunityRepository communityRepository)
		{
            this._communityRepository = communityRepository;
        }

        public async Task Handle(CommunityCreatedEvent request, CancellationToken cancellationToken)
        {
            try
            {
                string postData = JsonConvert.SerializeObject(request);
                await _communityRepository.UpdateRemoteId(postData, request.localId);
                return;
            }
            catch (Exception ex)
            {
                throw new Exception("Error publishing community: " + ex.Message);
            }
        }        
    }
}

