using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.QueryModels.Estate
{
    public class GetCommunication
    {
        public Community Community { get; set; }
        public List<CommunityToPropertyMap> CommunityToPropertyMapList { get; set; }

    }
}
