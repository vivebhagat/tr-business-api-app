﻿using PropertySolutionHub.Domain.Entities.Shared;
using PropertySolutionHub.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace PropertySolutionHub.Domain.Entities.Estate
{
    public class Property : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int RemoteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UnitType { get; set; }
        public string Url { get; set; }
        public double Price { get; set; }
        public virtual BusinessUser PropertyManager { get; set; }
        public int? PropertyManagerId { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime? ConstructionDate { get; set; }
        public PropertyStatus Status { get; set; }
        public bool IsFeatured { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public double Area { get; set; }
        public bool IsFurnished { get; set; }
        public bool AirConditioning { get; set; }
        public bool IsGarage { get; set; }
        public bool Heating { get; set; }
        public bool WaterFront { get; set; }
        public bool ParkingAvailable { get; set; }
        public bool PetFriendly { get; set; }
        public bool HasSwimmingPool { get; set; }
        public bool HasGym { get; set; }
        public bool HasFirePlace { get; set; }
        public bool IsSmokingAllowed { get; set; }
        public bool IsWheelchairAccessible { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
    }
}
