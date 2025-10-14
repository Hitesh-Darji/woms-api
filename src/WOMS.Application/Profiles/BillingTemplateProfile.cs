using AutoMapper;
using WOMS.Application.Features.BillingRates.DTOs;
using WOMS.Application.Features.BillingTemplates.Commands.CreateBillingTemplate;
using WOMS.Application.Features.BillingTemplates.Commands.UpdateBillingTemplate;
using WOMS.Application.Features.BillingTemplates.DTOs;
using WOMS.Domain.Entities;

namespace WOMS.Application.Profiles
{
    public class BillingTemplateProfile : Profile
    {
        public BillingTemplateProfile()
        {
            CreateMap<BillingTemplate, BillingTemplateDto>()
                .ForMember(dest => dest.FieldOrder, opt => opt.Ignore()); // Field order is handled separately in handlers

            CreateMap<BillingTemplateFieldOrder, BillingTemplateFieldDto>();

            // DTO to Command mappings
            CreateMap<CreateBillingTemplateDto, CreateBillingTemplateCommand>();
            CreateMap<CreateBillingRateDto, RateTable>().ReverseMap();
            CreateMap<BillingRateDto, RateTable>().ReverseMap(); 
            // Custom mapping for UpdateBillingTemplateCommand that includes Id
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
        }
    }
}
