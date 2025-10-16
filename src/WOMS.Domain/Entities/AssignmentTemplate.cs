using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Common;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    public class AssignmentTemplate : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public AssignmentTemplateStatus Status { get; set; } = AssignmentTemplateStatus.Active;

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string DaysOfWeek { get; set; } = string.Empty; // JSON array of selected days

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string WorkTypes { get; set; } = string.Empty; // JSON array of work types

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Zones { get; set; } = string.Empty; // JSON array of zones

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string PreferredTechnicians { get; set; } = string.Empty; // JSON array of technician IDs

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string SkillsRequired { get; set; } = string.Empty; // JSON array of required skills

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string AutoAssignmentRules { get; set; } = string.Empty; // JSON array of rules

        public int UsageCount { get; set; } = 0;

        public DateTime? LastUsed { get; set; }

        public new string? CreatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public virtual ApplicationUser? CreatedByUser { get; set; }
    }
}