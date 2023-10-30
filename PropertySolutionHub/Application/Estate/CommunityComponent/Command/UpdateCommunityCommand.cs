using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionHub.Api.Dto.Estate;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.CommunityComponent.Command
{
    public class UpdateCommunityCommand : IRequest<Community>
    {
        public CommunityDto Community { get; set; }
        public List<CommunityToPropertyMapDto> CommunityToPropertyMapList { get; set; }
        public IFormFile CommunityImage { get; set; }

    }
}
