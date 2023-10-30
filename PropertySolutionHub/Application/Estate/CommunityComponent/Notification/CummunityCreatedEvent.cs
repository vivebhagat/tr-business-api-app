using System;
using MediatR;
using PropertySolutionHub.Api.Dto.Estate;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.CommunityComponent.Notification
{
	public class CommunityCreatedEvent : INotification
	{
        public int localId { get; internal set; }
        public CommunityDto community { get; internal set; }

        public CommunityCreatedEvent(CommunityDto community, int localId)
		{
			this.community = community;
			this.localId = localId;
		}
    }
}

