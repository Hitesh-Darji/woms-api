using MediatR;
using WOMS.Application.Features.Workflow.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Workflow.Queries.GetAllWorkflows
{
    public class GetAllWorkflowsQueryHandler : IRequestHandler<GetAllWorkflowsQuery, WorkflowListGetResponse>
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IMapper _mapper;

        public GetAllWorkflowsQueryHandler(IWorkflowRepository workflowRepository, IMapper mapper)
        {
            _workflowRepository = workflowRepository;
            _mapper = mapper;
        }

        public async Task<WorkflowListGetResponse> Handle(GetAllWorkflowsQuery request, CancellationToken cancellationToken)
        {
            var (workflows, totalCount) = await _workflowRepository.GetPaginatedAsync(
                request.PageNumber,
                request.PageSize,
                request.SearchTerm,
                request.Category,
                request.IsActive,
                request.SortBy,
                request.SortDescending,
                cancellationToken);

            var workflowDtos = _mapper.Map<List<WorkflowGetDto>>(workflows);

            return new WorkflowListGetResponse
            {
                Workflows = workflowDtos,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
