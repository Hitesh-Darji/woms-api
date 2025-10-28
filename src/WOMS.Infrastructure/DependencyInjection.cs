using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using WOMS.Application.Interfaces;
using WOMS.Application.Features.Auth.Services;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
using WOMS.Infrastructure.Data;
using WOMS.Infrastructure.Repositories;
using WOMS.Infrastructure.Services;

namespace WOMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Add DbContext
            services.AddDbContext<WomsDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(WomsDbContext).Assembly.FullName)));

            // Identity (Core) - avoid cookie auth overriding JWT defaults
            var identityBuilder = services.AddIdentityCore<ApplicationUser>();
            identityBuilder = new IdentityBuilder(identityBuilder.UserType, typeof(ApplicationRole), services);
            identityBuilder
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<WomsDbContext>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddDefaultTokenProviders();

            // Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IWorkOrderRepository, WorkOrderRepository>();
            services.AddScoped<IWorkOrderAssignmentRepository, WorkOrderAssignmentRepository>();
            services.AddScoped<IWorkflowRepository, WorkflowRepository>();
            services.AddScoped<IWorkflowStatusRepository, WorkflowStatusRepository>();
            services.AddScoped<IBillingTemplateRepository, BillingTemplateRepository>();
            services.AddScoped<IBillingScheduleRepository, BillingScheduleRepository>();
            services.AddScoped<IRateTableRepository, RateTableRepository>();
            services.AddScoped<IFormTemplateRepository, FormTemplateRepository>();
            services.AddScoped<IFormSectionRepository, FormSectionRepository>();
            services.AddScoped<IFormFieldRepository, FormFieldRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<IOptimizationSettingsRepository, OptimizationSettingsRepository>();
            services.AddScoped<ITechnicianEquipmentRepository, TechnicianEquipmentRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();

            // Services
            services.AddScoped<IDistanceCalculationService, DistanceCalculationService>();
            services.AddScoped<IStockRequestRepository, StockRequestRepository>();
            services.AddScoped<IIntegrationRepository, IntegrationRepository>();

            // Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // JWT Token service
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            return services;
        }
    }
}
