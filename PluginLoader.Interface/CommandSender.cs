namespace PluginLoader.Interface
{
    /// <summary>
    /// 命令操作人（用于分辨命令是由谁发出的）
    /// </summary>
    public enum CommandSender
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default,

        /// <summary>
        /// 控制台
        /// </summary>
        Console,

        /// <summary>
        /// 用户
        /// </summary>
        User
    }
}
