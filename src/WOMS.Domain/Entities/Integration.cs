using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    [Table("Integration")]
    public class Integration : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public IntegrationCategory Category { get; set; } = IntegrationCategory.Communication;

        public IntegrationStatus Status { get; set; } = IntegrationStatus.Available;

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(50)]
        public string? IconName { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Features { get; set; } // JSON array of features

        [Column(TypeName = "nvarchar(max)")]
        public string? Configuration { get; set; } // JSON configuration specific to each integration

        [Required]
        public bool IsActive { get; set; } = true;

        public DateTime? ConnectedOn { get; set; }

        public DateTime? LastSyncOn { get; set; }

        public SyncStatus? SyncStatus { get; set; }
    }
}

