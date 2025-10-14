using AutoMapper;
using WOMS.Application.Features.WorkflowStatus.DTOs;
using WOMS.Domain.Entities;
using WOMS.Domain.Enums;

namespace WOMS.Application.Profiles
{
    public class WorkflowStatusProfile : Profile
    {
        public WorkflowStatusProfile()
        {
            CreateMap<WorkflowStatus, WorkflowStatusDto>();

            CreateMap<WorkflowStatusTransition, WorkflowStatusTransitionDto>()
                .ForMember(dest => dest.ToStatusName, opt => opt.MapFrom(src => src.ToStatus.Name));

            CreateMap<CreateWorkflowStatusRequest, WorkflowStatus>();
            CreateMap<UpdateWorkflowStatusRequest, WorkflowStatus>();
            CreateMap<CreateWorkflowStatusTransitionRequest, WorkflowStatusTransition>();

            // WorkflowCategory enum mapping
            CreateMap<WorkflowCategory, WorkflowCategoryDto>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ToString()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => GetCategoryDescription(src)));
        }

        private static string GetCategoryDescription(WorkflowCategory category)
        {
            return category switch
            {
                WorkflowCategory.General => "General workflow processes",
                WorkflowCategory.Maintenance => "Maintenance and repair workflows",
                WorkflowCategory.Safety => "Safety compliance workflows",
                WorkflowCategory.Compliance => "Regulatory compliance workflows",
                _ => "Unknown category"
            };
        }
    }
}
