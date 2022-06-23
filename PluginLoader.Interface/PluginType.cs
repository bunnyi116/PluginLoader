namespace PluginLoader.Interface
{
    /// <summary>
    /// 插件类型
    /// </summary>
    public enum PluginType
    {
        Default = 0,
        NeedPluginLoader = 1,
        NeedCommands = 2,
        NeedDepend = 4,
    }
}
