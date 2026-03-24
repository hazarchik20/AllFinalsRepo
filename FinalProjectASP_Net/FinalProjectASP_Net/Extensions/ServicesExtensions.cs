using FinalProjectASP_Net.Application.Services;
using FinalProjectASP_Net.Core.Abstractions.IServ;
using FinalProjectASP_Net.Middleware;

namespace FinalProjectASP_Net.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Here you can add your custom services, for example:
            // services.AddScoped<IMyService, MyService>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IVacancyServices, VacancyServices>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<ICompanyService, CompanyService>();

            services.AddTransient<GlobalExceptionHandler>();
            services.AddTransient<RequestLoggingMiddleware>();
            
            services.AddScoped<JwtTokenGenerator>();
            return services;

        }
    }
}
