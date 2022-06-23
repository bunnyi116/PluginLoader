using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginLoader.Interface
{
    /// <summary>
    /// 前置插件API接口
    /// </summary>
    public interface IPluginAPI
    {
        /// <summary>
        /// 前置插件的"ID"属性标识
        /// </summary>
        string[] PluginIDs { get; }

        /// <summary>
        /// 已获取到的前置插件字典对象
        /// </summary>
        PluginDictionary Plugins { get; set; }
    }
}
