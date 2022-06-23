namespace PluginLoader.Interface
{
    /// <summary>
    /// 插件接口
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// 插件内部标识ID（注：尽可能避免与其他插件重复，可使用域名倒写加上插件名称）
        /// </summary>
        public string ID { get; }

        /// <summary>
        /// 插件名称（注：显示名称）
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 插件描述
        /// </summary>
        public string? Description { get; }

        /// <summary>
        /// 插件作者（注：可选）
        /// </summary>
        public string[]? Author { get; }

        /// <summary>
        /// 插件版本号
        /// </summary>
        public Version Version { get; }

        /// <summary>
        /// 是否公开插件（如果为false，需要插件API作为前置的插件将无法获得该插件实例对象）
        /// </summary>
        public bool IsPublicPlugin { get; }

        /// <summary>
        /// 初始化插件
        /// </summary>
        void Initialize();

        /// <summary>
        /// 插件被关闭
        /// </summary>
        void Close();
    }
}