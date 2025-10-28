using AutoMapper;
using System.Text.Json;
using WOMS.Application.Features.Integrations.DTOs;
using WOMS.Domain.Entities;

namespace WOMS.Application.Profiles
{
    public class IntegrationProfile : Profile
    {
        public IntegrationProfile()
        {
            // Map from Integration entity to IntegrationDto
            CreateMap<Integration, IntegrationDto>()
                .ForMember(dest => dest.Features, opt => opt.MapFrom(src => DeserializeFeatures(src.Features)))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.SyncStatus, opt => opt.MapFrom(src => src.SyncStatus));

            // Map from CreateIntegrationDto to Integration entity (ignore features, will be serialized in handler)
            CreateMap<CreateIntegrationDto, Integration>()
                .ForMember(dest => dest.Features, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.ConnectedOn, opt => opt.Ignore())
                .ForMember(dest => dest.LastSyncOn, opt => opt.Ignore())
                .ForMember(dest => dest.SyncStatus, opt => opt.Ignore());

            // Map from UpdateIntegrationDto to Integration entity (ignore features, will be handled in handler)
            CreateMap<UpdateIntegrationDto, Integration>()
                .ForMember(dest => dest.Features, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.ConnectedOn, opt => opt.Ignore())
                .ForMember(dest => dest.LastSyncOn, opt => opt.Ignore());
        }

        private static List<string>? DeserializeFeatures(string? featuresJson)
        {
            if (string.IsNullOrEmpty(featuresJson))
                return null;

            try
            {
                return JsonSerializer.Deserialize<List<string>>(featuresJson);
            }
            catch
            {
                return null;
            }
        }
    }
}

