using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginLoader.Interface
{
    /// <summary>
    /// 插件储存内容
    /// </summary>
    public class PluginContent
    {
        public string FilePath { get; set; }
        public IPluginHost Host { get; set; }
        public IPlugin Plugin { get; set; }
        public PluginLoadContext LoadContext { get; set; }
        public PluginType PluginType { get; set; }


        #region 接口插件便于访问属性所设
        public IPluginAPI? PluginAPI { get; private set; }
        public ICommands? Commands { get; private set; }
        public IPluginLoader? PluginLoader { get; private set; }

        #endregion


        public PluginContent(string filePath, IPluginHost host, IPlugin plugin, PluginLoadContext loadContext)
        {
            FilePath = filePath;
            Host = host;
            Plugin = plugin;
            LoadContext = loadContext;

            // 设置接口属性
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
