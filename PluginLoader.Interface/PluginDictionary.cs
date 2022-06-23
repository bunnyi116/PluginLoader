using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginLoader.Interface
{
    public class PluginDictionary : Dictionary<Type, IPlugin>
    {

        public TPlugin? GetPlugin<TPlugin>() where TPlugin : IPlugin
        {
            if (TryGetValue(typeof(TPlugin), out IPlugin? plugin))
            {
                if (plugin != null)
                {
                    return (TPlugin)plugin;
                }
            }
            return default;
        }
    }
}
