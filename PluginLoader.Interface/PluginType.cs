namespace PluginLoader.Interface
{
    /// <summary>
    /// 插件类型
    /// </summary>
    public enum PluginType
    {
        /// <summary>
        /// 默认的
        /// </summary>
        Default = 0,

        /// <summary>
        /// 带插件加载接口
        /// </summary>
        NeedPluginLoader = 1,

        /// <summary>
        /// 带命令
        /// </summary>
        NeedCommands = 2,

        /// <summary>
        /// 带依赖
        /// </summary>
        NeedDepend = 4,
    }
}
