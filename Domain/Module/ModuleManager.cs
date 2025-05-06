using System.Collections.Generic;

namespace Domain.Module
{
    public static class ModuleManager
    {
        public static List<IModule> Modules = new List<IModule>();


        public static void RegisterModules(List<IModule> modules)
        {
            Modules = modules;
        }
    }
}
