using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using WOMS.Api.Extension;
using WOMS.Application;
using WOMS.Application.Profiles;
using WOMS.Infrastructure;
using WOMS.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new WOMS.Application.Converters.TimeSpanConverter());
        options.JsonSerializerOptions.Converters.Add(new WOMS.Application.Converters.NullableGuidConverter());
    });

builder.Services.AddHttpContextAccessor();

//Mapping
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<UserProfile>();
    cfg.AddProfile<RoleProfile>();
    cfg.AddProfile<ViewProfile>();
    cfg.AddProfile<BillingProfiles>();
    cfg.AddProfile<WorkflowProfile>();
    cfg.AddProfile<WorkflowStatusProfile>();
    cfg.AddProfile<WorkOrderProfile>();
    cfg.AddProfile<FormProfile>();
    cfg.AddProfile<StockRequestProfile>();
});

var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "WOMS", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },Array.Empty<string>()
        }
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
    builder.WithOrigins("*")
    .AllowAnyHeader()
    .AllowAnyMethod());
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtSection = builder.Configuration.GetSection("Jwt");
        var key = jwtSection.GetValue<string>("Key") ?? string.Empty;
        var issuer = jwtSection.GetValue<string>("Issuer") ?? string.Empty;
        var audience = jwtSection.GetValue<string>("Audience") ?? string.Empty;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddAuthorization();
builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
});

// Add Application Layer
builder.Services.AddApplication();

// Add Infrastructure Layer
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Apply database migrations automatically
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<WomsDbContext>();
    try
    {
        context.Database.Migrate();
        Log.Information("Database migration completed successfully");
    }
    catch (Exception ex)
    {
        Log.Error(ex, "An error occurred while migrating the database");
        // Don't throw - let the application start even if migration fails
        // The concurrency handling in controllers will manage conflicts gracefully
    }
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseExceptionHandlerMiddleware();

app.UseCors("AllowLocalhost");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }
