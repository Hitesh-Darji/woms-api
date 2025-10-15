using MediatR;
using WOMS.Application.Features.WorkflowStatus.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.WorkflowStatus.Queries.GetAllWorkflowStatuses
{
    public class GetAllWorkflowStatusesQuery : IRequest<IEnumerable<WorkflowStatusDto>>
    {
        public bool ActiveOnly { get; set; } = true;
    }

    public class GetAllWorkflowStatusesQueryHandler : IRequestHandler<GetAllWorkflowStatusesQuery, IEnumerable<WorkflowStatusDto>>
    {
        private readonly IWorkflowStatusRepository _workflowStatusRepository;
        private readonly IMapper _mapper;

        public GetAllWorkflowStatusesQueryHandler(IWorkflowStatusRepository workflowStatusRepository, IMapper mapper)
        {
            _workflowStatusRepository = workflowStatusRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WorkflowStatusDto>> Handle(GetAllWorkflowStatusesQuery request, CancellationToken cancellationToken)
        {
            var workflowStatuses = request.ActiveOnly
                ? await _workflowStatusRepository.GetAllActiveAsync(cancellationToken)
                : await _workflowStatusRepository.GetByOrderAsync(cancellationToken);

            return _mapper.Map<IEnumerable<WorkflowStatusDto>>(workflowStatuses);
        }
    }
}
