using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Domain.Attribute;
using Domain.Module;
using Microsoft.Extensions.DependencyInjection;

namespace Shell.DI
{
    public static class ServiceRegistrar
    {
        public static string AssemblyPath;

        private static readonly List<Assembly> LoadedAssemblies = new List<Assembly>();

        public static void RegisterAll(IServiceCollection services)
        {
            LoadProjectAssemblies();
            RegisterRepositories(services);
            RegisterServices(services);
            RegisterModules(services);

            RegisterForms(services);
        }
        public static void LoadProjectAssemblies()
        {
            if (!Directory.Exists(AssemblyPath))
            {
                Console.WriteLine($"Directory not found: {AssemblyPath}");
                return;
            }

            LoadedAssemblies.Clear();

            foreach (var dll in Directory.GetFiles(AssemblyPath, "*.dll"))
            {
                try
                {
                    var assembly = Assembly.LoadFrom(dll);
                    LoadedAssemblies.Add(assembly);
                    Console.WriteLine($"Loaded assembly: {assembly.FullName}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to load {dll}: {ex.Message}");
                }
            }

            var executingAssembly = Assembly.GetExecutingAssembly();
            if (!LoadedAssemblies.Contains(executingAssembly))
            {
                LoadedAssemblies.Add(executingAssembly);
            }
        }


        public static void RegisterModules(IServiceCollection services)
        {
            var moduleTypes = LoadedAssemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(IModule).IsAssignableFrom(t) &&
                            !t.IsInterface &&
                            !t.IsAbstract);

            foreach (var type in moduleTypes)
            {
                services.AddTransient(typeof(IModule), type);
                services.AddTransient(type);
                Console.WriteLine($"Registered module: {type.FullName}");
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
                .Where(t => t.IsClass && !t.IsAbstract &&
                            t.GetCustomAttribute<RepositoryAttribute>() != null);

            foreach (var repoType in repositoryTypes)
            {
                var interfaces = repoType.GetInterfaces();
                foreach (var interfaceType in interfaces)
                {
                    services.AddScoped(interfaceType, repoType);
                    Console.WriteLine($"Registered repository: {repoType.FullName} as {interfaceType.FullName}");
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
                .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttribute<ServiceAttribute>()!=null);

            foreach (var serviceType in serviceTypes)
            {
                var interfaces = serviceType.GetInterfaces();

                foreach (var interfaceType in interfaces)
                {
                    services.AddScoped(interfaceType, serviceType);
                    Console.WriteLine($"Registered Service : {serviceType.FullName} as {interfaceType.FullName}");
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
                Console.WriteLine($"Registered form: {formType.FullName}");
            }
        }
        private static IEnumerable<Type> SafeGetTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(t => t != null);
            }
            catch
            {
                return Type.EmptyTypes;
            }
        }
    }
}
