using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PluginLoader.Interface
{
    [Serializable]
    internal class PluginLoaderException : Exception
    {
        public PluginLoaderException() { }
        public PluginLoaderException(string? message) : base(message) { }
        public PluginLoaderException(string? message, Exception? innerException) : base(message, innerException) { }
        protected PluginLoaderException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
