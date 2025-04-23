using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Shell.DI;
using Shell.forms;

namespace Shell
{
    static class Program
    {
        private static ServiceProvider _serviceProvider;

        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            ConfigureServices();

            var welcomeForm = _serviceProvider.GetRequiredService<WelcomeForm>();
            System.Windows.Forms.Application.Run(welcomeForm);

            if (_serviceProvider != null)
            {
                _serviceProvider.Dispose();
            }
        }

        private static void ConfigureServices()
        {
            var services = new ServiceCollection();

            var appSettings = new AppSettings();
            services.AddSingleton(appSettings);

            services.AddSingleton(provider =>
                new DbConnectionFactory(appSettings.ConnectionString));

            string modulesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules");
            services.AddSingleton(provider => new ModuleLoader(modulesPath));
            ServiceRegistrar.RegisterAll(services);


            _serviceProvider = services.BuildServiceProvider();
        }
    }
}