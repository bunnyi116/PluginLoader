using PluginLoader.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PluginLoader
{
    public class PluginManager : IPluginHost
    {
        public List<PluginContent> Plugins { get; set; } = new();

        public delegate void LoadErrorHandler(string errorMessage, string filePath, PluginContent? pluginContent);
        public event LoadErrorHandler? PluginLoadError;

        private PluginContent? LoadPluginContent(string pluginsFilePath)
        {
            // 检查插件文件是否存在
            var fileInfo = new FileInfo(pluginsFilePath);
            if (!fileInfo.Exists) { PluginLoadError?.Invoke("文件不存在", pluginsFilePath, null); }

            // 加载插件程序集加载内容实例
            var pluginLoadContext = new PluginLoadContext(pluginsFilePath);
            // 加载插件到程序集上下文
            Assembly assembly = pluginLoadContext.LoadFromAssemblyPath(pluginsFilePath);
            // 检查程序集是否为本程序的插件
            PluginContent? pluginContent = null;
            foreach (Type type in assembly.GetTypes())
            {
                // 检查程序集类型是否为插件接口实现的类型
                if (typeof(IPlugin).IsAssignableFrom(type))
                {
                    // 创建插件实例，并根据本程序插件接口检查是否为本程序插件
                    if (Activator.CreateInstance(type) is IPlugin plugin)
                    {
                        pluginContent = new PluginContent(pluginsFilePath, this, plugin, pluginLoadContext);
                    }
                }
            }
            if (pluginContent != null)
            {
                return pluginContent;
            }
            pluginLoadContext.Unload();
            return null;
        }

        public void LoadPlugin(string pluginsFilePath)
        {

            var pluginLoadContext = LoadPluginContent(pluginsFilePath);
            if (pluginLoadContext != null)
            {
                var plugins = new Queue<PluginContent>();
                if (pluginLoadContext.PluginAPI == null)
                {
                    Plugins.Add(pluginLoadContext);
                    if (pluginLoadContext.PluginLoader != null)
                    {
                        pluginLoadContext.PluginLoader.OnLoad();
                    }
                    pluginLoadContext.Plugin.Initialize();
                }
                // 存在需要前置插件的API，等基础插件加载完成后在获取API
                else plugins.Enqueue(pluginLoadContext);

                // 插件加载完成，进行下一步处理
                LoadSuccessHandler(plugins);
            }
        }


        public void LoadPlugins(string? pluginsDirectory = null)
        {
            pluginsDirectory ??= Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins\\");
            // 获取插件目录路径下的所有插件
            var directoryInfo = new DirectoryInfo(pluginsDirectory);
            if (!directoryInfo.Exists) { directoryInfo.Create(); }

            // 需要前置依赖插件的临时储存
            var needDependencyplugins = new Queue<PluginContent>();
            foreach (var fileInfo in directoryInfo.GetFiles())
            {
                var pluginLoadContext = LoadPluginContent(fileInfo.FullName);
                if (pluginLoadContext != null)
                {
                    if (pluginLoadContext.PluginAPI == null)
                    {
                        // 将不需要前置的API加载进列表
                        Plugins.Add(pluginLoadContext);
                        if (pluginLoadContext.PluginLoader != null)
                        {
                            pluginLoadContext.PluginLoader.OnLoad();
                        }
                        pluginLoadContext.Plugin.Initialize();
                    }
                    // 存在需要前置插件的API，等基础插件加载完成后在获取API
                    else needDependencyplugins.Enqueue(pluginLoadContext);
                }
            }

            // 插件全部加载完成，进行下一步依赖获取处理
            LoadSuccessHandler(needDependencyplugins);
        }


        private void LoadSuccessHandler(Queue<PluginContent> plugins)
        {
            var needDependPlugins = new Queue<PluginContent>();
            // 添加普通的
            while (plugins.Count > 0)
            {
                var pluginContent = plugins.Dequeue();
                if (pluginContent.PluginAPI != null)
                {
                    // 根据该插件 提供的前置插件的"ID"属性进行获取 前置插件对象
                    foreach (var id in pluginContent.PluginAPI.PluginIDs)
                    {
                        // 从已加载的插件列表内查找
                        foreach (var pc in Plugins)
                        {
                            // 检查已加载插件是否允许公开插件 和 匹配有符合"ID"条件的
                            if (pc.Plugin.IsPublicPlugin && pc.Plugin.ID.Equals(id))
                            {
                                pluginContent.PluginAPI.Plugins.Add(pc.Plugin.GetType(), pc.Plugin);
                            }
                        }
                    }
                    // 根据该插件获取前置插件数量情况判断是否获取成功
                    if (pluginContent.PluginAPI.PluginIDs.Length == pluginContent.PluginAPI.Plugins.Count)
                    {
                        Plugins.Add(pluginContent);
                        if (pluginContent.PluginLoader != null)
                        {
                            pluginContent.PluginLoader.OnLoad();
                        }
                        pluginContent.Plugin.Initialize();
                    }
                    else PluginLoadError?.Invoke("依赖未全部获取成功", pluginContent.FilePath, pluginContent);
                }
            }
        }

        /// <summary>
        /// 解析命令（粗略解析，具体是不是命令还得进行命令判断）
        /// </summary>
        /// <param name="cmdText">消息文本</param>
        /// <returns>处理后的命令和参数</returns>
        private static List<string>? ParseCmd(string cmdText)
        {
            cmdText = cmdText.Trim();

            var list = new List<string>();
            var stringState = false;
            var tmp = string.Empty;
            for (int i = 0; i < cmdText.Length; i++)
            {
                var c = cmdText[i];
                switch (c)
                {
                    case ' ':
                        {
                            // 如果在字符串状态，跳过该空格
                            if (stringState) { tmp += c; }
                            else
                            {
                                if (tmp.Length > 0)
                                {
                                    list.Add(tmp);
                                    tmp = string.Empty;
                                }
                            }
                            break;
                        }
                    case '\"':
                        {
                            // 判断一下当前引号是不是字符串结束引号
                            if (stringState)
                            {
                                list.Add(tmp);
                                tmp = string.Empty;
                                stringState = false;
                                break;
                            }

                            // 先查看一下是否还有字符可读防止索引溢出
                            if (i + 1 < cmdText.Length)
                            {
                                // 判断一下后面是否还有跟字符串匹配的引号
                                var pos = cmdText.IndexOf('\"', i + 1);
                                if (pos != -1)
                                {
                                    stringState = true;
                                }
                                else { tmp += c; }
                            }
                            break;
                        }

                    default: { tmp += c; break; }
                }

                // 检查是否最后一个字符
                if (i + 1 >= cmdText.Length)
                {
                    if (tmp.Length > 0)
                    {
                        list.Add(tmp);
                    }
                    break;
                }

            }
            if (list.Count > 0)
            {
                return list;
            }
            return null;
        }

        /// <summary>
        /// 命令处理程序
        /// </summary>
        /// <param name="commandSender">操作人类型</param>
        /// <param name="cmdText">命令文本内容</param>
        public void CommandHandler(CommandSender commandSender, string cmdText)
        {
            foreach (var pluginContent in Plugins)
            {
                if (pluginContent.Commands != null)
                {
                    foreach (var cmd in pluginContent.Commands.Commands)
                    {
                        var list = ParseCmd(cmdText);
                        if (list == null) { return; }
                        if (list.Count == 0) { return; }

                        // 判断插件命令是否忽略大小写进行比对
                        StringComparison stringComparison = cmd.IsIgnoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture;

                        // 判断前缀
                        var cmdName = list[0];
                        if (cmd.Prefix != null)
                        {
                            var prefix = cmdName[..cmd.Prefix.Length];
                            if (!prefix.Equals(cmd.Prefix, stringComparison))
                            {
                                return; // 前缀不正确，不继续处理了
                            }
                            // 前缀正确，将前缀移除，后续处理主命令
                            cmdName = cmdName[prefix.Length..];
                        }

                        // 判断命令
                        if (cmdName.Equals(cmd.Name, stringComparison))
                        {
                            List<string>? cmdArgs = null;
                            if (list.Count > 1)
                            {
                                cmdArgs = new List<string>();
                                for (int i = 1; i < list.Count; i++)
                                {
                                    cmdArgs.Add(list[i]);
                                }
                            }
                            cmd.Handler(commandSender, cmdArgs);
                        }
                    }
                }
            }
        }

    }
}
