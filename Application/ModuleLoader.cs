using Domain.Module;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application
{
    public class ModuleLoader
    {
        private readonly IServiceProvider _serviceProvider;

        public ModuleLoader(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public List<IModule> LoadModules()
        {
            var modules = _serviceProvider.GetServices<IModule>().ToList();

            Console.WriteLine($"Loaded {modules.Count} modules");
            return modules;
        }

        public void InitializeModules(List<IModule> modules)
        {
            foreach (var module in modules)
            {
                try
                {
                    module.Initialize();
                    Console.WriteLine($"Initialized module: {module.Name}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error initializing {module.Name}: {ex.Message}");
                }
            }
        }

        public void DisplayModuleInfo(List<IModule> modules)
        {
            Console.WriteLine($"\nLoaded {modules.Count} modules:");
            foreach (var module in modules)
            {
                Console.WriteLine($"- {module.Name}: {module.Subname}");
            }
        }
    }
}