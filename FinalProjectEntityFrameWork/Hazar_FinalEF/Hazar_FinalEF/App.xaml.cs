using System.Configuration;
using System.Data;
using System.Windows;
using BLL.Interfaces;
using BLL.Models;
using BLL.Configuration;
using BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hazar_FinalEF
{
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;
        private string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MusicRecordDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public App()
        {
            var service = new ServiceCollection();
            ConfigurationService(service);
            _serviceProvider = service.BuildServiceProvider();
        }

        private void ConfigurationService(ServiceCollection service)
        {
            service.AddTransient(typeof(IServices<MusicRecord>), typeof(MusicRecordService));
            service.AddTransient(typeof(AdminWindow));
            //service.AddTransient(typeof(PeopleViewModel));
            ConfihurationBLL.ConfigurationServiceCollection(service, _connectionString);
        }
        private void OnStartUp(object sender, StartupEventArgs arg)
        {
            var mainWindow = _serviceProvider.GetService<AdminWindow>();
            mainWindow.Show();
        }
    }
}
