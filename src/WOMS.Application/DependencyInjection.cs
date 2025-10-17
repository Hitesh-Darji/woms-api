using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WOMS.Application.Behaviors;
using WOMS.Application.Interfaces;
using WOMS.Application.Profiles;

namespace WOMS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // Add MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            // Add AutoMapper - register consolidated BillingProfiles and other profiles
            services.AddAutoMapper(typeof(BillingProfiles).Assembly);
            services.AddAutoMapper(typeof(UserProfile), typeof(RoleProfile), typeof(DepartmentProfile), typeof(WorkflowProfile), typeof(FormProfile), typeof(StockRequestProfile));

            // Add FluentValidation
            services.AddValidatorsFromAssembly(assembly);

            // Add pipeline behaviors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

            // Add custom mapper
            services.AddScoped<WOMS.Application.Interfaces.IMapper, AutoMapperAdapter>();

            return services;
        }
    }

    public class AutoMapperAdapter : WOMS.Application.Interfaces.IMapper
    {
        private readonly AutoMapper.IMapper _mapper;

        public AutoMapperAdapter(AutoMapper.IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TDestination>(object source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }
    }
}
