using System.ComponentModel.DataAnnotations;

namespace WOMS.Application.Features.Integrations.DTOs
{
    public class ConnectIntegrationDto
    {
        [Required]
        public string Configuration { get; set; } = string.Empty;
    }
}

