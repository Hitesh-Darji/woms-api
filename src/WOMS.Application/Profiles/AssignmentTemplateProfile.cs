using AutoMapper;
using WOMS.Application.Features.AssignmentTemplate.DTOs;
using WOMS.Application.Features.AssignmentTemplate.Commands.CreateAssignmentTemplate;
using WOMS.Application.Features.AssignmentTemplate.Commands.UpdateAssignmentTemplate;
using WOMS.Domain.Entities;
using WOMS.Domain.Enums;
using System.Text.Json;

namespace WOMS.Application.Profiles
{
    public class AssignmentTemplateProfile : Profile
    {
        public AssignmentTemplateProfile()
        {
            CreateMap<AssignmentTemplate, AssignmentTemplateDto>()
                .ConvertUsing(src => new AssignmentTemplateDto
                {
                    Id = src.Id,
                    Name = src.Name,
                    Description = src.Description,
                    Status = src.Status,
                    StartTime = src.StartTime,
                    EndTime = src.EndTime,
                    DaysOfWeek = DeserializeDaysOfWeek(src.DaysOfWeek),
                    WorkTypes = DeserializeStringList(src.WorkTypes),
                    Zones = DeserializeStringList(src.Zones),
                    PreferredTechnicians = DeserializeStringList(src.PreferredTechnicians),
                    SkillsRequired = DeserializeStringList(src.SkillsRequired),
                    AutoAssignmentRules = DeserializeStringList(src.AutoAssignmentRules),
                    UsageCount = src.UsageCount,
                    LastUsed = src.LastUsed,
                    CreatedOn = src.CreatedOn,
                    UpdatedOn = src.UpdatedOn ?? src.CreatedOn,
                    CreatedBy = src.CreatedBy
                });

            CreateMap<CreateAssignmentTemplateCommand, AssignmentTemplate>()
                .ForMember(dest => dest.DaysOfWeek, opt => opt.Ignore())
                .ForMember(dest => dest.WorkTypes, opt => opt.Ignore())
                .ForMember(dest => dest.Zones, opt => opt.Ignore())
                .ForMember(dest => dest.PreferredTechnicians, opt => opt.Ignore())
                .ForMember(dest => dest.SkillsRequired, opt => opt.Ignore())
                .ForMember(dest => dest.AutoAssignmentRules, opt => opt.Ignore())
                .ForMember(dest => dest.UsageCount, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.LastUsed, opt => opt.MapFrom(src => (DateTime?)null))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));

            CreateMap<UpdateAssignmentTemplateCommand, AssignmentTemplate>()
                .ForMember(dest => dest.DaysOfWeek, opt => opt.Ignore())
                .ForMember(dest => dest.WorkTypes, opt => opt.Ignore())
                .ForMember(dest => dest.Zones, opt => opt.Ignore())
                .ForMember(dest => dest.PreferredTechnicians, opt => opt.Ignore())
                .ForMember(dest => dest.SkillsRequired, opt => opt.Ignore())
                .ForMember(dest => dest.AutoAssignmentRules, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedOn, opt => opt.MapFrom(src => DateTime.UtcNow));
        }

        private static List<DayOfWeekEnum> DeserializeDaysOfWeek(string json)
        {
            if (string.IsNullOrEmpty(json))
                return new List<DayOfWeekEnum>();
            
            try
            {
                var intList = JsonSerializer.Deserialize<List<int>>(json);
                return intList?.Select(d => (DayOfWeekEnum)d).ToList() ?? new List<DayOfWeekEnum>();
            }
            catch
            {
                return new List<DayOfWeekEnum>();
            }
        }

        private static List<string> DeserializeStringList(string json)
        {
            if (string.IsNullOrEmpty(json))
                return new List<string>();
            
            try
            {
                return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
            }
            catch
            {
                return new List<string>();
            }
        }
    }
}
