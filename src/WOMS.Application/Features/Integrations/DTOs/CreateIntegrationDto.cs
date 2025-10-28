using System.ComponentModel.DataAnnotations;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.Integrations.DTOs
{
    public class CreateIntegrationDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public IntegrationCategory Category { get; set; } = IntegrationCategory.Communication;

        public IntegrationStatus Status { get; set; } = IntegrationStatus.Available;

        public string? Description { get; set; }

        public string? IconName { get; set; }

        public List<string>? Features { get; set; }

        public string? Configuration { get; set; }
    }
}

