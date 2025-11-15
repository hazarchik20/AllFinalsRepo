using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL;
using DLL.Interfaces;
using DLL.ModelsInfo;
using DLL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Configuration
{
    public class ConfihurationBLL
    {
        public static void ConfigurationServiceCollection(ServiceCollection services, string connectionString)
        {
            services.AddTransient(typeof(IRepository<MusicRecordInfo>), typeof(MusicRecordRepository));
            services.AddDbContext<MusicRecorContext>(opt => {
                opt.UseSqlServer(connectionString);
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }
    }
}
