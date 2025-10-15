using MediatR;
using WOMS.Application.Features.WorkflowStatus.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.WorkflowStatus.Queries.GetWorkflowStatusById
{
    public class GetWorkflowStatusByIdQuery : IRequest<WorkflowStatusDto?>
    {
        public Guid Id { get; set; }
    }

    public class GetWorkflowStatusByIdQueryHandler : IRequestHandler<GetWorkflowStatusByIdQuery, WorkflowStatusDto?>
    {
        private readonly IWorkflowStatusRepository _workflowStatusRepository;
        private readonly IMapper _mapper;

        public GetWorkflowStatusByIdQueryHandler(IWorkflowStatusRepository workflowStatusRepository, IMapper mapper)
        {
            _workflowStatusRepository = workflowStatusRepository;
            _mapper = mapper;
        }

        public async Task<WorkflowStatusDto?> Handle(GetWorkflowStatusByIdQuery request, CancellationToken cancellationToken)
        {
            var workflowStatus = await _workflowStatusRepository.GetByIdAsync(request.Id, cancellationToken);
            return workflowStatus != null ? _mapper.Map<WorkflowStatusDto>(workflowStatus) : null;
        }
    }
}
