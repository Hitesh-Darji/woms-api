using AutoMapper;
using WOMS.Application.Features.BillingRates.DTOs;
using WOMS.Application.Features.BillingTemplates.Commands.CreateBillingTemplate;
using WOMS.Application.Features.BillingTemplates.Commands.UpdateBillingTemplate;
using WOMS.Application.Features.BillingTemplates.DTOs;
using WOMS.Application.Features.BillingSchedules.DTOs;
using WOMS.Domain.Entities;

namespace WOMS.Application.Profiles
{
    public class BillingProfiles : Profile
    {
        public BillingProfiles()
        {
            // Billing Rates
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

            CreateMap<BillingRateDto, RateTable>().ReverseMap();
            CreateMap<CreateBillingRateDto, RateTable>().ReverseMap();

            // Billing Templates
            CreateMap<BillingTemplate, BillingTemplateDto>()
                .ForMember(dest => dest.FieldOrder, opt => opt.Ignore());

            CreateMap<BillingTemplateFieldOrder, BillingTemplateFieldDto>();

            CreateMap<CreateBillingTemplateDto, CreateBillingTemplateCommand>();

            CreateMap<(UpdateBillingTemplateDto dto, Guid id), UpdateBillingTemplateCommand>()
                .ConstructUsing(src => new UpdateBillingTemplateCommand
                {
                    Id = src.id,
                    Name = src.dto.Name,
                    CustomerId = src.dto.CustomerId,
                    CustomerName = src.dto.CustomerName,
                    OutputFormat = src.dto.OutputFormat,
                    FileNamingConvention = src.dto.FileNamingConvention,
                    DeliveryMethod = src.dto.DeliveryMethod,
                    InvoiceType = src.dto.InvoiceType,
                    FieldOrder = src.dto.FieldOrder,
                    IsActive = src.dto.IsActive
                });

            // Billing Schedules
            CreateMap<BillingSchedule, BillingScheduleDto>()
                .ForMember(dest => dest.TemplateIds, opt => opt.Ignore());

            CreateMap<CreateBillingScheduleDto, BillingSchedule>()
                .ForMember(dest => dest.TemplateIds, opt => opt.Ignore());

            CreateMap<UpdateBillingScheduleDto, BillingSchedule>()
                .ForMember(dest => dest.TemplateIds, opt => opt.Ignore());
        }
    }
}


