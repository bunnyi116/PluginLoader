using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginLoader.Interface
{

    /// <summary>
    /// 命令接口
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// 插件是否需要前置
        /// </summary>
        string? Prefix { get; set; }

        /// <summary>
        /// 是否忽略Name大小写进行比较（"true" 为忽略大小写）
        /// </summary>
        bool IsIgnoreCase { get; set; }

        /// <summary>
        /// 命令名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 命令别名
        /// </summary>
        string? Alias { get; set; }

        /// <summary>
        /// 主命令描述
        /// </summary>
        string? Description { get; set; }

        /// <summary>
        /// 命令参数处理方法
        /// </summary>
        /// <param name="senderType">命令操作人的类型</param>
        /// <param name="cmdName">命令名称</param>
        /// <param name="cmdArgs">命令参数</param>
        void Handler(CommandSender senderType, List<string>? cmdArgs);
    }
}
