using PluginLoader;
using PluginLoader.Interface;

var appDir = AppDomain.CurrentDomain.BaseDirectory;
var solution = new DirectoryInfo($"{appDir}..\\..\\..\\..\\").FullName;
var pluginsDirectory = Path.Combine(solution, "Plugins");

Console.WriteLine($"插件目录路径：{pluginsDirectory}");

var pluginManager = new PluginManager();
pluginManager.LoadPlugins(pluginsDirectory);

while (true)
{
    var lien = Console.ReadLine();
    if (lien != null)
    {
        // 将文本内容传入加载器进行处理
        pluginManager.CommandHandler(CommandSender.Console, lien);
    }
}







