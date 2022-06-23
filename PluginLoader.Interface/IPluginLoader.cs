using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginLoader.Interface
{
    /// <summary>
    /// 宿主处理插件时调用的接口程序
    /// </summary>
    public interface IPluginLoader
    {
        /// <summary>
        /// 载入插件（注：宿主加载时调用）
        /// </summary>
        void OnLoad();

        /// <summary>
        /// 卸载插件（注：宿主卸载时调用）
        /// </summary>
        void OnUnLoad();

        /// <summary>
        /// 重载插件（注：宿主重载时调用）
        /// </summary>
        void OnReLoad();
    }
}
