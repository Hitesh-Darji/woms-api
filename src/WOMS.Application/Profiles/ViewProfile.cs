using AutoMapper;
using WOMS.Application.Features.View.DTOs;
using WOMS.Domain.Entities;

namespace WOMS.Application.Profiles
{
    public class ViewProfile : Profile
    {
        public ViewProfile()
        {
            // Note: We're not using AutoMapper for ViewDto anymore since we manually create it
            // This profile is kept for potential future use
        }
    }
}
