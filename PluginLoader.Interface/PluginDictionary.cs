using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginLoader.Interface
{
    /// <summary>
    /// 插件字典
    /// </summary>
    public class PluginDictionary : Dictionary<Type, IPlugin>
    {
        /// <summary>
        /// 便于获取前置插件API接口方法
        /// </summary>
        /// <typeparam name="TPlugin">插件Class</typeparam>
        /// <returns>插件Class</returns>
        public TPlugin? GetPlugin<TPlugin>() where TPlugin : IPlugin
        {
            if (TryGetValue(typeof(TPlugin), out IPlugin? plugin))
            {
                if (plugin != null)
                {
                    return (TPlugin)plugin;
                }
            }
            return default;
        }
    }
}
