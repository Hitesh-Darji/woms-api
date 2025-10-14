using AutoMapper;
using WOMS.Application.Features.BillingRates.DTOs;
using WOMS.Domain.Entities;

namespace WOMS.Application.Profiles
{
    public class BillingRateProfile : Profile
    {
        public BillingRateProfile()
        {
            // Entity to DTO mapping
            CreateMap<RateTable, BillingRateDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.RateType, opt => opt.MapFrom(src => src.RateType))
                .ForMember(dest => dest.BaseRate, opt => opt.MapFrom(src => src.BaseRate))
                .ForMember(dest => dest.EffectiveStartDate, opt => opt.MapFrom(src => src.EffectiveStartDate))
                .ForMember(dest => dest.EffectiveEndDate, opt => opt.MapFrom(src => src.EffectiveEndDate))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.UpdatedOn, opt => opt.MapFrom(src => src.UpdatedOn))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy));
        }
    }
}
