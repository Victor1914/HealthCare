namespace HealthCare.API.Behaviour.Extensions
{
    using AutoMapper;
    using FluentValidation.AspNetCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using BusinessLayer.Interfaces;
    using BusinessLayer.Services;
    using BusinessLayer.Services.Authentication;
    using Contracts.Configuration;
    using Contracts.Interfaces;
    using DataLayer;
    using DataLayer.Utils;
    using Filters;
    using Interfaces;
    using Newtonsoft.Json;
    using Templates;
    using Utils;
    using Validation.ModelValidators;
    using Workers;

    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services
                .AddMvc(options =>  options.Filters.Add(typeof(GlobalExceptionFilter)))
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .AddFluentValidation(f => f.RegisterValidatorsFromAssemblyContaining<RegisterUserValidator>());

            services.AddSwaggerGen();

            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HealthCareDbContext>(op => op.UseSqlServer(configuration["ConnectionString"]));

            return services;
        }

        public static IServiceCollection AddConfigurations(this IServiceCollection service, IConfiguration configuration)
        {
            return service.Configure<SeedSettings>(configuration.GetSection("SeedSettings"))
                          .Configure<JwtAuthentication>(configuration.GetSection("JwtAuthentication"))
                          .Configure<CommonSettings>(configuration.GetSection("CommonSettings"))
                          .Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"))
                          .Configure<TwilioConfiguration>(configuration.GetSection("TwilioConfiguration"));
        }

        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            return services.AddScoped<IContactsService, ContactsService>()
                            .AddScoped<IUserService, UserService>()
                            .AddScoped<IPatientService, PatientService>()
                            .AddScoped<IMedicalManService, MedicalManService>()
                            .AddScoped<IMedicalCenterService, MedicalCenterService>()
                            .AddScoped<IMedicalDataService, MedicalDataService>()
                            .AddScoped<IAppointmentService, AppointmentService>()
                            .AddScoped<IPrescriptionService, PrescriptionService>()
                            .AddScoped<IOutPatientCardService, OutPatientCardService>()
                            .AddScoped<ICommonService, CommonService>()
                            .AddScoped<IImageService, ImageService>();
        }

        public static IServiceCollection AddSystemServices(this IServiceCollection services)
        {
            return services.AddScoped<IStorageService, DatabaseService>()
                           .AddScoped<IDataRetriever, DataRetriever>()
                           .AddScoped<ISessionResolver, HttpSessionResolver>()
                           .AddTransient<IAuthService, JWTService>();
        }

        public static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            return services.AddHostedService<DatabaseCleaner>();
        }

        public static IServiceCollection AddCommunicationServices(this IServiceCollection services)
        {
            return services.AddScoped<IEmailService, EmailService>()
                            .AddScoped<ICommunicationService, CommunicationService>()
                            .AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();
        }

        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));

            return services;
        }
    }
}
