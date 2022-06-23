using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginLoader.Interface
{
    /// <summary>
    /// 插件宿主已插件加载列表集合
    /// </summary>
    public interface IPluginHost
    {
        /// <summary>
        /// 已加载插件
        /// </summary>
        List<PluginContent> Plugins { get; set; }
    }
}
