using PluginLoader;
using PluginLoader.Interface;
var pluginsDirectory = @"C:\Users\11607\Desktop\PluginLoader\Plugins";
Console.WriteLine($"插件目录路径：{pluginsDirectory}");
//var file1 = @"D:\编程\源码\.Net\C#\PluginLoader\PluginLoader.Plugin.Test\bin\Debug\net6.0\PluginLoader.Plugin.Test.dll";
//var file2 = @"D:\编程\源码\.Net\C#\PluginLoader\PluginLoader.Plugin.Test1\bin\Debug\net6.0\PluginLoader.Plugin.Test1.dll";
//var file3 = @"C:\Users\11607\Desktop\PluginLoader\PluginB\bin\Debug\net6.0\PluginB.dll";

var pluginManager = new PluginManager();
pluginManager.LoadPlugins(pluginsDirectory);
//pluginManager.LoadPlugin(file3);

while (true)
{
    var lien = Console.ReadLine();
    if (lien != null)
    {
        pluginManager.CommandHandler(CommandSender.Console, lien);
    }
}







