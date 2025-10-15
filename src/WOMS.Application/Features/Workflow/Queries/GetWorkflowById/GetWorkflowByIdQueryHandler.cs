using MediatR;
using WOMS.Application.Features.Workflow.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Workflow.Queries.GetWorkflowById
{
    public class GetWorkflowByIdQueryHandler : IRequestHandler<GetWorkflowByIdQuery, WorkflowGetDto?>
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IMapper _mapper;

        public GetWorkflowByIdQueryHandler(IWorkflowRepository workflowRepository, IMapper mapper)
        {
            _workflowRepository = workflowRepository;
            _mapper = mapper;
        }

        public async Task<WorkflowGetDto?> Handle(GetWorkflowByIdQuery request, CancellationToken cancellationToken)
        {
            var workflow = await _workflowRepository.GetByIdWithNodesAsync(request.Id, cancellationToken);
            return workflow != null ? _mapper.Map<WorkflowGetDto>(workflow) : null;
        }
    }
}
