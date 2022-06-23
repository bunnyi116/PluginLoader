using PluginLoader.Interface;

namespace PluginLoader.Host
{
    public class PluginHost : IPluginHost
    {
        private readonly List<IPlugin> plugins;

        public PluginHost() => plugins = new List<IPlugin>();

        public List<IPlugin> GetPlugins() => plugins;

    }
}