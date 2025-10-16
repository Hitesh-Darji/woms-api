using AutoMapper;
using WOMS.Application.Features.Forms.DTOs;
using WOMS.Domain.Entities;

namespace WOMS.Application.Profiles
{
    public class FormProfile : Profile
    {
        public FormProfile()
        {
            // FormTemplate mappings
            CreateMap<FormTemplate, FormTemplateDto>()
                .ForMember(dest => dest.Sections, opt => opt.MapFrom(src => src.FormSections))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.UpdatedOn, opt => opt.MapFrom(src => src.UpdatedOn))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy));

            CreateMap<FormSection, FormSectionDto>()
                .ForMember(dest => dest.Fields, opt => opt.MapFrom(src => src.FormFields))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.UpdatedOn, opt => opt.MapFrom(src => src.UpdatedOn));

            CreateMap<FormField, FormFieldDto>()
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.UpdatedOn, opt => opt.MapFrom(src => src.UpdatedOn));

            // Create DTO mappings
            CreateMap<CreateFormTemplateDto, FormTemplate>();
            CreateMap<CreateFormSectionDto, FormSection>();
            CreateMap<CreateFormFieldDto, FormField>();
            CreateMap<FormTemplateDto, FormTemplate>().ReverseMap();

            // Update DTO mappings
            CreateMap<UpdateFormTemplateDto, FormTemplate>();
            CreateMap<UpdateFormSectionDto, FormSection>();
            CreateMap<UpdateFormFieldDto, FormField>();
        }
    }
}

