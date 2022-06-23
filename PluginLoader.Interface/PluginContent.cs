using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginLoader.Interface
{
    public enum PluginType
    {
        Default = 0,
        NeedPluginLoader = 1,
        NeedCommands = 2,
        NeedDepend = 4,
    }

    public class PluginContent
    {
        public string FilePath { get; set; }
        public IPluginHost Host { get; set; }
        public IPlugin Plugin { get; set; }
        public PluginLoadContext LoadContext { get; set; }
        public PluginType PluginType { get; set; }


        public IPluginAPI? PluginAPI { get; private set; }
        public ICommands? Commands { get; private set; }
        public IPluginLoader? PluginLoader { get; private set; }

        public PluginContent(string filePath, IPluginHost host, IPlugin plugin, PluginLoadContext loadContext)
        {
            FilePath = filePath;
            Host = host;
            Plugin = plugin;
            LoadContext = loadContext;

            SetInterface();
        }

        private void SetInterface()
        {
            var type = Plugin.GetType();

            if (typeof(IPluginLoader).IsAssignableFrom(type))
            {
                PluginType |= PluginType.NeedPluginLoader;
                PluginLoader = (IPluginLoader)Plugin;
            }
            if (typeof(ICommands).IsAssignableFrom(type))
            {
                PluginType |= PluginType.NeedCommands;
                Commands = (ICommands)Plugin;
            }
            if (typeof(IPluginAPI).IsAssignableFrom(type))
            {
                PluginType |= PluginType.NeedDepend;
                PluginAPI = (IPluginAPI)Plugin;
            }
        }

    }
}
