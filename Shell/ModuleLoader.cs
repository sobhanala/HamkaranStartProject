using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Domain.Module;

namespace Shell
{
    public class ModuleLoader
    {
        private readonly string _modulesDirectory;

        public ModuleLoader(string modulesDirectory)
        {
            _modulesDirectory = modulesDirectory;
        }

        public List<IModule> LoadModules()
        {
            var modules = new List<IModule>();
            var dllFiles = Directory.GetFiles(_modulesDirectory, "*.dll");
            foreach (var dll in dllFiles)
                try
                {
                    var assembly = Assembly.LoadFrom(dll);
                    var modulesType = assembly.GetTypes().Where(t => typeof(IModule).IsAssignableFrom(t) &&
                                                                     !t.IsInterface
                                                                     && !t.IsAbstract).ToList();
                    foreach (var type in modulesType)
                    {
                        if (Activator.CreateInstance(type) is IModule module)
                            modules.Add(module);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error loading module from {dllFiles}: {e.Message}");
                    throw;
                }

            return modules;
        }
    }
}