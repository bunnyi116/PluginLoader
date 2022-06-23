using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginLoader.Interface
{
    /// <summary>
    /// 命令接口（ICommand实现的类将被加载器识别）
    /// </summary>
    public interface ICommands
    {
        ICommand[] Commands { get; set; }
    }
}
