using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace PluginLoader.Interface
{
    public class PluginLoadContext : AssemblyLoadContext
    {
        private AssemblyDependencyResolver resolver;

        public PluginLoadContext(string componentAssemblyPath) : base(name: componentAssemblyPath, isCollectible: true)
        {
            resolver = new AssemblyDependencyResolver(componentAssemblyPath);
        }

        protected override Assembly? Load(AssemblyName assemblyName)
        {
            foreach (var item in All)
            {
                foreach (var assembly in item.Assemblies)
                {
                    if (assembly.FullName == assemblyName.FullName)
                    {
                        return assembly;
                    }
                }
            }

            var assemblyPath = resolver.ResolveAssemblyToPath(assemblyName);
            if (assemblyPath != null)
            {
                return LoadFromAssemblyPath(assemblyPath);
            }
            return null;
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            var libraryPath = resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            if (libraryPath != null)
            {
                return LoadUnmanagedDllFromPath(libraryPath);
            }
            return IntPtr.Zero;
        }
    }
}
