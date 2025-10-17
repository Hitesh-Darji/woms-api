using System.ComponentModel.DataAnnotations;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.StockRequest.DTOs
{
    public class StockRequestDto
    {
        public Guid Id { get; set; }
        public string RequesterId { get; set; } = string.Empty;
        public string? RequesterName { get; set; }
        public Guid FromLocationId { get; set; }
        public string FromLocationName { get; set; } = string.Empty;
        public Guid ToLocationId { get; set; }
        public string ToLocationName { get; set; } = string.Empty;
        public StockRequestStatus Status { get; set; }
        public string StatusDescription { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; }
        public string? ApprovedBy { get; set; }
        public string? ApprovedByName { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? Notes { get; set; }
        public Guid? WorkOrderId { get; set; }
        public string? WorkOrderNumber { get; set; }
        public List<RequestItemDto> RequestItems { get; set; } = new List<RequestItemDto>();
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }

    public class RequestItemDto
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public Guid ItemId { get; set; }
        public string ItemPartNumber { get; set; } = string.Empty;
        public string ItemDescription { get; set; } = string.Empty;
        public string ItemCategory { get; set; } = string.Empty;
        public string ItemManufacturer { get; set; } = string.Empty;
        public string ItemUnitOfMeasure { get; set; } = string.Empty;
        public decimal ItemUnitCost { get; set; }
        public int RequestedQuantity { get; set; }
        public int? ApprovedQuantity { get; set; }
        public int? FulfilledQuantity { get; set; }
        public string? Notes { get; set; }
        public int OrderIndex { get; set; }
    }

    public class CreateStockRequestDto
    {
        [Required]
        public Guid FromLocationId { get; set; }

        [Required]
        public Guid ToLocationId { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

        public Guid? WorkOrderId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one item is required")]
        public List<CreateRequestItemDto> RequestItems { get; set; } = new List<CreateRequestItemDto>();
    }

    public class CreateRequestItemDto
    {
        [Required]
        public Guid ItemId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int RequestedQuantity { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

        public int OrderIndex { get; set; }
    }

    public class UpdateStockRequestDto
    {
        [MaxLength(500)]
        public string? Notes { get; set; }

        public List<UpdateRequestItemDto> RequestItems { get; set; } = new List<UpdateRequestItemDto>();
    }

    public class UpdateRequestItemDto
    {
        public Guid? Id { get; set; } // null for new items

        [Required]
        public Guid ItemId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int RequestedQuantity { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

        public int OrderIndex { get; set; }
    }

    public class ApproveStockRequestDto
    {
        [MaxLength(500)]
        public string? ApprovalNotes { get; set; }

        public List<ApproveRequestItemDto> RequestItems { get; set; } = new List<ApproveRequestItemDto>();
    }

    public class ApproveRequestItemDto
    {
        [Required]
        public Guid Id { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Approved quantity cannot be negative")]
        public int? ApprovedQuantity { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }
    }

    public class StockRequestListResponse
    {
        public List<StockRequestDto> StockRequests { get; set; } = new List<StockRequestDto>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasPreviousPage => PageNumber > 1;
    }
}

