﻿
namespace PropertySolutionHub.Api.Dto.Estate
{
    public class CommunityDto
    {
        public int Id { get; set; }
        public int RemoteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrganizationId { get; set; }
        public string Url { get; set; }
        public bool IsPublished { get; set; }
        public double PriceFrom { get; set; }
        public double PriceTo { get; set; }
        public int BedFrom { get; set; }
        public int BedTo { get; set; }
        public int BathFrom { get; set; }
        public int BathTo { get; set; }
        public int AreaFrom { get; set; }
        public int AreaTo { get; set; }
        public string Location { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string CommunityType { get; set; }
        public int CommunityTypeId { get; set; }
        public double LandArea { get; set; }
        public int NumberOfUnits { get; set; }
    }
}
