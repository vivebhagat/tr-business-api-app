﻿using PropertySolutionHub.Domain.Entities.Shared;

namespace PropertySolutionHub.Domain.Entities.Estate
{
    public class PropertyImage : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public virtual Property Property { get; set; }
        public int PropertyId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
    }
}
