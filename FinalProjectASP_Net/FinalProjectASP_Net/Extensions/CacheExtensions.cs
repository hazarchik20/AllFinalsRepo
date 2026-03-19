namespace FinalProjectASP_Net.Extensions
{
    public static class CacheExtensions
    {
        public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = "VacancyApp:";
            });

            return services;
        }
    }
}
