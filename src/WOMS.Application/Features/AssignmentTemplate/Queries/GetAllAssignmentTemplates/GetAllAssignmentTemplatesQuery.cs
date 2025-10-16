using MediatR;
using WOMS.Application.Features.AssignmentTemplate.DTOs;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.AssignmentTemplate.Queries.GetAllAssignmentTemplates
{
    public class GetAllAssignmentTemplatesQuery : IRequest<AssignmentTemplateListResponse>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
        public AssignmentTemplateStatus? Status { get; set; }
        public string SortBy { get; set; } = "CreatedOn";
        public bool SortDescending { get; set; } = true;
    }
}
