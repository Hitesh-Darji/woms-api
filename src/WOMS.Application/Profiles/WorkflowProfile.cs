using AutoMapper;
using WOMS.Application.Features.Workflow.DTOs;
using WOMS.Domain.Entities;
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
                .ForMember(dest => dest.Connections, opt => opt.MapFrom(src => DeserializeConnections(src.Connections)));
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
    }
}