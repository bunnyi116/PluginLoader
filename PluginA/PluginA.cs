using PluginLoader.Interface;

namespace PluginA
{
    public class PluginA : IPlugin, IPluginLoader
    {
        public string ID => $"com.github.1160706050.{Name}";
        public string Name => "PluginA";
        public string? Description => "描述";
        public string[]? Author => new string[] { "Bunny_i" };
        public Version Version => new(1, 0, 0);
        public bool IsPublicPlugin => true;

        public void Initialize() { Console.WriteLine($"[{Name}] 插件被初始化"); }
        public void Close() { Console.WriteLine($"[{Name}] 插件被关闭"); }

        public void OnLoad() { Console.WriteLine($"[{Name}] 插件被加载"); }
        public void OnReLoad() { Console.WriteLine($"[{Name}] 插件被重载"); }
        public void OnUnLoad() { Console.WriteLine($"[{Name}] 插件被卸载"); }

        public void Test(string msg) { Console.WriteLine($"[{Name}] 插件Test()方法被调用，调用者消息：{msg}"); }
    }
}