using FinalProjectASP_Net.Core.Abstractions.IRepo;
using FinalProjectASP_Net.Storage;
using FinalProjectASP_Net.Storage.Repository;
using Microsoft.EntityFrameworkCore;



namespace FinalProjectASP_Net.Extensions
{
    public static class RepositoryExtensions
    {
        
        public static IServiceCollection AddRepositories(this IServiceCollection services, ConfigurationManager config)
        {
            // Here you can add your repositories, for example:
            // services.AddScoped<IMyRepository, MyRepository>();
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVacancyRepository, VacancyRepository>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();

            return services;




        }
    }
}
