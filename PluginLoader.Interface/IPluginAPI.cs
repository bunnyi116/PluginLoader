using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginLoader.Interface
{
    public interface IPluginAPI
    {
        string[] PluginIDs { get; }
        PluginDictionary Plugins { get; set; }
    }
}
