using PluginLoader.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginB
{
    /// <summary>
    /// 插件命令实现
    /// </summary>
    public class CommandA : ICommand
    {
        public string? Prefix { get; set; } = "/asd";
        public bool IsIgnoreCase { get; set; } = true;
        public string Name { get; set; } = "TestA";
        public string? Alias { get; set; } = null;
        public string? Description { get; set; } = null;

        public void Handler(CommandSender senderType, List<string>? cmdArgs)
        {
            string? tmp = null;
            if (cmdArgs != null && cmdArgs.Count > 0)
            {
                tmp += "参数：{";
                tmp += string.Join(',', cmdArgs);
                tmp += "}";
            }
            Console.WriteLine($"操作人类型：{senderType}，执行成功。{tmp}");
            Console.WriteLine("----------------------------");
        }
    }
}
