using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using WOMS.Application.Interfaces;
using WOMS.Application.Services;
using WOMS.Domain.Entities;
using WOMS.Domain.Enums;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Workflow.Commands.SendWorkflowNotification
{
    public class SendWorkflowNotificationCommandHandler : IRequestHandler<SendWorkflowNotificationCommand, bool>
    {
        private readonly IRepository<WorkflowNotification> _notificationRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<SendWorkflowNotificationCommandHandler> _logger;

        public SendWorkflowNotificationCommandHandler(
            IRepository<WorkflowNotification> notificationRepository,
            IEmailService emailService,
            ILogger<SendWorkflowNotificationCommandHandler> logger)
        {
            _notificationRepository = notificationRepository;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<bool> Handle(SendWorkflowNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Get all active notifications for this workflow
                var notifications = await _notificationRepository.GetAllAsync(cancellationToken);
                
                // Filter notifications that have this trigger
                var matchingNotifications = notifications
                    .Where(n => n.WorkflowId == request.WorkflowId 
                             && n.IsActive 
                             && !n.IsDeleted
                             && HasTrigger(n.Triggers, request.Trigger))
                    .OrderBy(n => n.OrderIndex)
                    .ToList();

                if (!matchingNotifications.Any())
                {
                    _logger.LogInformation(
                        "No active notifications found for workflow {WorkflowId} with trigger '{Trigger}'",
                        request.WorkflowId, request.Trigger);
                    return false;
                }

                _logger.LogInformation(
                    "Found {Count} notifications to send for workflow {WorkflowId} with trigger '{Trigger}'",
                    matchingNotifications.Count, request.WorkflowId, request.Trigger);

                // Send each notification
                foreach (var notification in matchingNotifications)
                {
                    await SendNotificationAsync(notification, request.TemplateData, cancellationToken);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send workflow notifications for workflow {WorkflowId}", request.WorkflowId);
                return false;
            }
        }

        private bool HasTrigger(string? triggersJson, string triggerToCheck)
        {
            if (string.IsNullOrEmpty(triggersJson))
                return false;

            try
            {
                var triggers = JsonSerializer.Deserialize<List<string>>(triggersJson);
                return triggers?.Contains(triggerToCheck) ?? false;
            }
            catch
            {
                return false;
            }
        }

        private async Task SendNotificationAsync(
            WorkflowNotification notification,
            Dictionary<string, object>? templateData,
            CancellationToken cancellationToken)
        {
            try
            {
                var recipients = string.IsNullOrEmpty(notification.Recipients)
                    ? new List<string>()
                    : JsonSerializer.Deserialize<List<string>>(notification.Recipients) ?? new List<string>();

                if (!recipients.Any())
                    return;

                // Process template with data
                var processedTemplate = ProcessTemplate(notification.Template, templateData);
                var subject = $"Workflow Notification: {notification.Name}";

                switch (notification.Type)
                {
                    case WorkflowNotificationType.Email:
                        await _emailService.SendEmailWithTemplateAsync(
                            recipients,
                            subject,
                            processedTemplate,
                            templateData,
                            cancellationToken
                        );
                        _logger.LogInformation(
                            "Sent email notification '{NotificationName}' to {Count} recipients",
                            notification.Name, recipients.Count);
                        break;

                    case WorkflowNotificationType.InApp:
                        _logger.LogInformation(
                            "In-app notification '{NotificationName}' would be sent here (not implemented yet)",
                            notification.Name);
                        break;

                    case WorkflowNotificationType.SMS:
                        _logger.LogInformation(
                            "SMS notification '{NotificationName}' would be sent here (not implemented yet)",
                            notification.Name);
                        break;

                    case WorkflowNotificationType.Webhook:
                        _logger.LogInformation(
                            "Webhook notification '{NotificationName}' would be sent here (not implemented yet)",
                            notification.Name);
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Failed to send notification '{NotificationName}' for workflow {WorkflowId}",
                    notification.Name, notification.WorkflowId);
            }
        }

        private string ProcessTemplate(string template, Dictionary<string, object>? templateData)
        {
            if (templateData == null || !templateData.Any())
                return template;

            var processedTemplate = template;
            foreach (var (key, value) in templateData)
            {
                var placeholder = "{" + key + "}";
                processedTemplate = processedTemplate.Replace(placeholder, value?.ToString() ?? string.Empty);
            }

            return processedTemplate;
        }
    }
}

