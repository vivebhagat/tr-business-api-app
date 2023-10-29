using System;
using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.CommunityComponent.Notification
{
	public class CommunityCreatedEvent : INotification
	{
        public int localId { get; internal set; }
        public Community community { get; internal set; }

        public CommunityCreatedEvent(Community community, int localId)
		{
			this.community = community;
			this.localId = localId;
		}
    }
}

