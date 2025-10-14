using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;
using WOMS.Application.Features.Workflow.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Workflow.Commands.CreateWorkflow
{
    public class CreateWorkflowCommandHandler : IRequestHandler<CreateWorkflowCommand, WorkflowDto>
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        IHttpContextAccessor _httpContextAccessor;

        public CreateWorkflowCommandHandler(IWorkflowRepository workflowRepository, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
        {
            _workflowRepository = workflowRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContext;
        }

        public async Task<WorkflowDto> Handle(CreateWorkflowCommand request, CancellationToken cancellationToken)
        {
            // Get the current user ID from the JWT token
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("User ID not found in token or invalid format");
            }

            var workflow = new Domain.Entities.Workflow
            {
                Name = request.Name,
                Description = request.Description,
                Category = request.Category,
                CurrentVersion = 1,
                IsActive = true,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = userId,
                IsDeleted = false
            };

            await _workflowRepository.AddAsync(workflow, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<WorkflowDto>(workflow);
        }
    }
}
