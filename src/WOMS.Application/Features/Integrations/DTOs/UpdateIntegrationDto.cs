using System.ComponentModel.DataAnnotations;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.Integrations.DTOs
{
    public class UpdateIntegrationDto
    {
        [MaxLength(255)]
        public string? Name { get; set; }

        public IntegrationCategory? Category { get; set; }

        public IntegrationStatus? Status { get; set; }

        public string? Description { get; set; }

        public string? IconName { get; set; }

        public List<string>? Features { get; set; }

        public string? Configuration { get; set; }

        public bool? IsActive { get; set; }
    }
}

