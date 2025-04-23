using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace Shell.DI
{
    public static class ServiceRegistrar
    {
        private static readonly string[] ProjectAssemblies =
        {
            "Shell", // Current project
            "Persistence", // Add other project names in your solution
            "Domain",
            "Application"
        };

        private static readonly List<Assembly> LoadedAssemblies = new List<Assembly>();

        public static void RegisterAll(IServiceCollection services)
        {
            // Load all assemblies from the application directory
            LoadProjectAssemblies();

            RegisterRepositories(services);
            RegisterServices(services);
            RegisterForms(services);
        }

        private static void LoadProjectAssemblies()
        {
            LoadedAssemblies.Clear();

            foreach (var assem in ProjectAssemblies)
                try
                {
                    var assembly = Assembly.Load(assem);
                    LoadedAssemblies.Add(assembly);
                    Debug.WriteLine($"Loaded assembly: {assem}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to load assembly: Error: {ex.Message}");
                }

            var executingAssembly = Assembly.GetExecutingAssembly();
            if (!LoadedAssemblies.Contains(executingAssembly))
            {
                LoadedAssemblies.Add(executingAssembly);
                Debug.WriteLine($"Loaded executing assembly: {executingAssembly.GetName().Name}");
            }
        }

        public static void RegisterRepositories(IServiceCollection services)
        {
            var repositoryTypes = LoadedAssemblies
                .SelectMany(a =>
                {
                    try
                    {
                        return a.GetTypes();
                    }
                    catch (Exception)
                    {
                        return Type.EmptyTypes;
                    }
                })
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository"));

            foreach (var repoType in repositoryTypes)
            {
                var interfaceType = repoType.GetInterfaces()
                    .FirstOrDefault(i => i.Name == $"I{repoType.Name}");

                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, repoType);
                    Debug.WriteLine($"Registered repository: {repoType.FullName}");
                }
            }
        }

        public static void RegisterServices(IServiceCollection services)
        {
            var serviceTypes = LoadedAssemblies
                .SelectMany(a =>
                {
                    try
                    {
                        return a.GetTypes();
                    }
                    catch (Exception)
                    {
                        return Type.EmptyTypes;
                    }
                })
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service"));

            foreach (var serviceType in serviceTypes)
            {
                var interfaceType = serviceType.GetInterfaces()
                    .FirstOrDefault(i => i.Name == $"I{serviceType.Name}");

                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, serviceType);
                    Debug.WriteLine($"Registered service: {serviceType.FullName}");
                }
            }
        }

        public static void RegisterForms(IServiceCollection services)
        {
            var formTypes = LoadedAssemblies
                .SelectMany(a =>
                {
                    try
                    {
                        return a.GetTypes();
                    }
                    catch (Exception)
                    {
                        return Type.EmptyTypes;
                    }
                })
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(Form)));

            foreach (var formType in formTypes)
            {
                services.AddTransient(formType);
                Debug.WriteLine($"Registered form: {formType.FullName}");
            }
        }
    }
}