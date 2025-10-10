using System.ComponentModel.DataAnnotations;

namespace WOMS.Application.Features.Departments.DTOs
{
    public class UpdateDepartmentDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(50)]
        public string? Code { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active";

        public bool IsActive { get; set; } = true;
    }
}
