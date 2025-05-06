using System;
using System.Collections.Generic;
using System.IO;
using Application;
using Domain.Module;
using Domain.Repositorys;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence;
using Shell.DI;
using Shell.forms;

namespace Shell
{
    internal static class Program
    {
        private static ServiceProvider _serviceProvider;
        private static ILoggerFactory _loggerFactory;


        [STAThread]
        private static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);


            ConfigureLogging();
            ConfigureServices();

            var welcomeForm = _serviceProvider.GetRequiredService<WelcomeForm>();
            System.Windows.Forms.Application.Run(welcomeForm);

            if (_serviceProvider != null) _serviceProvider.Dispose();
        }


        private static void ConfigureServices()
        {
            var services = new ServiceCollection();
            const string modulesPath = @"C:\Users\SobhanA\source\repos\Anbar\Dlls";



            ServiceRegistrar.AssemblyPath = modulesPath;

            services.AddSingleton(_loggerFactory);
            services.AddLogging();

            
            services.AddSingleton(new AppSettings());

            services.AddSingleton(provider =>
                new DbConnectionFactory(provider.GetRequiredService<AppSettings>().ConnectionString));

            ServiceRegistrar.RegisterAll(services);

            services.AddSingleton<ModuleLoader>();

            services.AddTransient<WelcomeForm>();

            _serviceProvider = services.BuildServiceProvider();
            InitializeModules();
        }

        private static void ConfigureLogging()
        {
            _loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("Shell", LogLevel.Debug)
                    .AddConsole()
                    .AddDebug();

            });
        }

        private static void InitializeModules()
        {
            var moduleLoader = _serviceProvider.GetRequiredService<ModuleLoader>();
            var modules = moduleLoader.LoadModules();

            ModuleManager.RegisterModules(modules);
            moduleLoader.InitializeModules(modules);
            moduleLoader.DisplayModuleInfo(modules);
        }



    }
}