using MediatR;
using WOMS.Application.Interfaces;
using WOMS.Application.Features.Workflow.DTOs;
using WOMS.Domain.Repositories;
using System.Text.Json;

namespace WOMS.Application.Features.Workflow.Commands.CreateWorkflowNotification
{
    public class CreateWorkflowNotificationCommandHandler : IRequestHandler<CreateWorkflowNotificationCommand, WorkflowNotificationDto>
    {
        private readonly IRepository<Domain.Entities.WorkflowNotification> _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateWorkflowNotificationCommandHandler(
            IRepository<Domain.Entities.WorkflowNotification> notificationRepository,
            IUnitOfWork unitOfWork)
        {
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkflowNotificationDto> Handle(CreateWorkflowNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = new Domain.Entities.WorkflowNotification
            {
                WorkflowId = request.WorkflowId,
                Name = request.Name,
                Type = request.Type,
                Template = request.Template,
                IsActive = request.IsActive,
                OrderIndex = request.OrderIndex,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            if (request.Recipients.Any())
            {
                notification.Recipients = JsonSerializer.Serialize(request.Recipients);
            }

            if (request.Triggers.Any())
            {
                notification.Triggers = JsonSerializer.Serialize(request.Triggers);
            }

            await _notificationRepository.AddAsync(notification, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Note: Email sending on notification creation is removed to follow pure CQRS pattern
            // Notifications will be sent via SendWorkflowNotificationCommand when events occur

            return new WorkflowNotificationDto
            {
                Id = notification.Id,
                WorkflowId = notification.WorkflowId,
                Name = notification.Name,
                Type = notification.Type,
                Recipients = request.Recipients,
                Template = notification.Template,
                Triggers = request.Triggers,
                IsActive = notification.IsActive,
                OrderIndex = notification.OrderIndex
            };
        }
    }
}

