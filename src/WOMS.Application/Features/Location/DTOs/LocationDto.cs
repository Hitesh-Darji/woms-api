using System.ComponentModel.DataAnnotations;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.Location.DTOs
{
    public class LocationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public LocationType Type { get; set; } = LocationType.Warehouse;
        public string TypeDescription { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Manager { get; set; } = string.Empty;
        public Guid? ParentLocationId { get; set; }
        public string? ParentLocationName { get; set; }
        public bool IsActive { get; set; } = true;
        public List<LocationDto> SubLocations { get; set; } = new List<LocationDto>();
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }

    public class CreateLocationDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public LocationType Type { get; set; } = LocationType.Warehouse;

        [Required]
        [MaxLength(1000)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Manager { get; set; } = string.Empty;

        public Guid? ParentLocationId { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class UpdateLocationDto
    {
        public Guid Id { get; set; } // Optional - will be set from URL path

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public LocationType Type { get; set; } = LocationType.Warehouse;

        [Required]
        [MaxLength(1000)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Manager { get; set; } = string.Empty;

        public Guid? ParentLocationId { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
