using AutoMapper;
using WOMS.Application.Features.Workflow.DTOs;
using WOMS.Domain.Entities;
using WOMS.Domain.Enums;
using System.Text.Json;

namespace WOMS.Application.Profiles
{
    public class WorkflowProfile : Profile
    {
        public WorkflowProfile()
        {
            CreateMap<Workflow, WorkflowDto>()
                .ForMember(dest => dest.Nodes, opt => opt.MapFrom(src => src.Nodes));

            CreateMap<WorkflowNode, WorkflowNodeDto>()
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => DeserializePosition(src.Position)))
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => DeserializeData(src.Data)))
                .ForMember(dest => dest.Connections, opt => opt.MapFrom(src => DeserializeConnections(src.Connections)))
                .ForMember(dest => dest.StartConfig, opt => opt.MapFrom(src => DeserializeNodeConfig<StartNodeConfigDto>(src.Data, src.Type, "start")))
                .ForMember(dest => dest.StatusConfig, opt => opt.MapFrom(src => DeserializeNodeConfig<StatusNodeConfigDto>(src.Data, src.Type, "status")))
                .ForMember(dest => dest.ConditionConfig, opt => opt.MapFrom(src => DeserializeNodeConfig<ConditionNodeConfigDto>(src.Data, src.Type, "condition")))
                .ForMember(dest => dest.ApprovalConfig, opt => opt.MapFrom(src => DeserializeNodeConfig<ApprovalNodeConfigDto>(src.Data, src.Type, "approval")))
                .ForMember(dest => dest.NotificationConfig, opt => opt.MapFrom(src => DeserializeNodeConfig<NotificationNodeConfigDto>(src.Data, src.Type, "notification")))
                .ForMember(dest => dest.EscalationConfig, opt => opt.MapFrom(src => DeserializeNodeConfig<EscalationNodeConfigDto>(src.Data, src.Type, "escalation")))
                .ForMember(dest => dest.EndConfig, opt => opt.MapFrom(src => DeserializeNodeConfig<EndNodeConfigDto>(src.Data, src.Type, "end")));

            // Reverse mapping for creating/updating workflows
            CreateMap<WorkflowDto, Workflow>();
        }

        private static NodePositionDto? DeserializePosition(string? json)
        {
            if (string.IsNullOrEmpty(json))
                return null;
            try { return JsonSerializer.Deserialize<NodePositionDto>(json); }
            catch { return null; }
        }

        private static Dictionary<string, object>? DeserializeData(string? json)
        {
            if (string.IsNullOrEmpty(json))
                return null;
            try { return JsonSerializer.Deserialize<Dictionary<string, object>>(json); }
            catch { return null; }
        }

        private static List<string>? DeserializeConnections(string? json)
        {
            if (string.IsNullOrEmpty(json))
                return null;
            try { return JsonSerializer.Deserialize<List<string>>(json); }
            catch { return null; }
        }

        private static T? DeserializeNodeConfig<T>(string? json, string nodeType, string expectedType) where T : class
        {
            if (string.IsNullOrEmpty(json) || !nodeType.Equals(expectedType, StringComparison.OrdinalIgnoreCase))
                return null;
            
            try
            {
                var data = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
                if (data == null) return null;

                // Handle special cases for enums
                if (typeof(T) == typeof(StartNodeConfigDto) && data.ContainsKey("triggerType"))
                {
                    var triggerTypeString = data["triggerType"]?.ToString();
                    if (Enum.TryParse<WorkflowTriggerType>(triggerTypeString, out var triggerType))
                    {
                        data["triggerType"] = triggerType;
                    }
                }
                
                if (typeof(T) == typeof(StatusNodeConfigDto) && data.ContainsKey("autoAssignTo"))
                {
                    var autoAssignToString = data["autoAssignTo"]?.ToString();
                    if (Enum.TryParse<WorkflowAssigneeType>(autoAssignToString, out var autoAssignTo))
                    {
                        data["autoAssignTo"] = autoAssignTo;
                    }
                }
                
                if (typeof(T) == typeof(ApprovalNodeConfigDto) && data.ContainsKey("approvalType"))
                {
                    var approvalTypeString = data["approvalType"]?.ToString();
                    if (Enum.TryParse<WorkflowApprovalType>(approvalTypeString, out var approvalType))
                    {
                        data["approvalType"] = approvalType;
                    }
                }
                
                if (typeof(T) == typeof(ConditionNodeConfigDto))
                {
                    if (data.ContainsKey("fieldToCheck"))
                    {
                        var fieldToCheckString = data["fieldToCheck"]?.ToString();
                        if (Enum.TryParse<WorkflowConditionField>(fieldToCheckString, out var fieldToCheck))
                        {
                            data["fieldToCheck"] = fieldToCheck;
                        }
                    }
                    
                    if (data.ContainsKey("operator"))
                    {
                        var operatorString = data["operator"]?.ToString();
                        if (Enum.TryParse<WorkflowConditionOperator>(operatorString, out var operatorValue))
                        {
                            data["operator"] = operatorValue;
                        }
                    }
                }
                
                if (typeof(T) == typeof(NotificationNodeConfigDto))
                {
                    if (data.ContainsKey("notificationType"))
                    {
                        var notificationTypeString = data["notificationType"]?.ToString();
                        if (Enum.TryParse<WorkflowNotificationType>(notificationTypeString, out var notificationType))
                        {
                            data["notificationType"] = notificationType;
                        }
                    }
                    
                    if (data.ContainsKey("recipient"))
                    {
                        var recipientString = data["recipient"]?.ToString();
                        if (Enum.TryParse<WorkflowRecipientType>(recipientString, out var recipient))
                        {
                            data["recipient"] = recipient;
                        }
                    }
                    
                    if (data.ContainsKey("messageTemplate"))
                    {
                        var messageTemplateString = data["messageTemplate"]?.ToString();
                        if (Enum.TryParse<WorkflowMessageTemplate>(messageTemplateString, out var messageTemplate))
                        {
                            data["messageTemplate"] = messageTemplate;
                        }
                    }
                }
                
                if (typeof(T) == typeof(EscalationNodeConfigDto))
                {
                    if (data.ContainsKey("trigger"))
                    {
                        var triggerString = data["trigger"]?.ToString();
                        if (Enum.TryParse<WorkflowEscalationTrigger>(triggerString, out var trigger))
                        {
                            data["trigger"] = trigger;
                        }
                    }
                    
                    if (data.ContainsKey("action"))
                    {
                        var actionString = data["action"]?.ToString();
                        if (Enum.TryParse<WorkflowEscalationAction>(actionString, out var action))
                        {
                            data["action"] = action;
                        }
                    }
                }
                
                if (typeof(T) == typeof(EndNodeConfigDto) && data.ContainsKey("completionAction"))
                {
                    var completionActionString = data["completionAction"]?.ToString();
                    if (Enum.TryParse<WorkflowCompletionAction>(completionActionString, out var completionAction))
                    {
                        data["completionAction"] = completionAction;
                    }
                }

                // Convert the dictionary to the specific config type
                var configJson = JsonSerializer.Serialize(data);
                return JsonSerializer.Deserialize<T>(configJson);
            }
            catch { return null; }
        }
    }
}