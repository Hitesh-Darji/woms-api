using System.ComponentModel.DataAnnotations;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.CycleCount.DTOs
{
    public class CycleCountDto
    {
        public Guid Id { get; set; }
        public Guid LocationId { get; set; }
        public string LocationName { get; set; } = string.Empty;
        public CycleCountType Type { get; set; }
        public string TypeDescription { get; set; } = string.Empty;
        public CycleCountStatus Status { get; set; }
        public string StatusDescription { get; set; } = string.Empty;
        public DateTime PlannedDate { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string CountedBy { get; set; } = string.Empty;
        public string? CountedByName { get; set; }
        public string? SupervisorApproval { get; set; }
        public string? SupervisorApprovalName { get; set; }
        public string? Notes { get; set; }
        public List<CycleCountItemDto> CountItems { get; set; } = new List<CycleCountItemDto>();
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }

    public class CycleCountItemDto
    {
        public Guid Id { get; set; }
        public Guid CycleCountId { get; set; }
        public Guid InventoryId { get; set; }
        public string InventoryPartNumber { get; set; } = string.Empty;
        public string InventoryDescription { get; set; } = string.Empty;
        public string InventoryCategory { get; set; } = string.Empty;
        public string InventoryManufacturer { get; set; } = string.Empty;
        public string InventoryUnitOfMeasure { get; set; } = string.Empty;
        public int SystemQuantity { get; set; }
        public int CountedQuantity { get; set; }
        public int Variance { get; set; }
        public string? Notes { get; set; }
        public CycleCountItemStatus Status { get; set; }
        public string StatusDescription { get; set; } = string.Empty;
        public bool IsVariance { get; set; }
    }

    public class CreateCycleCountDto
    {
        [Required]
        public Guid LocationId { get; set; }

        [Required]
        public CycleCountType Type { get; set; }

        [Required]
        public DateTime PlannedDate { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }
    }

    public class UpdateCycleCountDto
    {
        [MaxLength(500)]
        public string? Notes { get; set; }
    }

    public class StartCycleCountDto
    {
        [MaxLength(500)]
        public string? Notes { get; set; }
    }

    public class CompleteCycleCountDto
    {
        [MaxLength(500)]
        public string? CompletionNotes { get; set; }
    }

    public class CancelCycleCountDto
    {
        [Required]
        [MaxLength(500)]
        public string Reason { get; set; } = string.Empty;
    }

    public class AddCountItemDto
    {
        [Required]
        public Guid InventoryId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Counted quantity cannot be negative")]
        public int CountedQuantity { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }
    }

    public class UpdateCountItemDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Counted quantity cannot be negative")]
        public int CountedQuantity { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }
    }

    public class CycleCountListResponse
    {
        public List<CycleCountDto> CycleCounts { get; set; } = new List<CycleCountDto>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasPreviousPage => PageNumber > 1;
    }
}

