using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Reflection.Metadata.BlobBuilder;

namespace MQTTLib.Utility
{
    public class MessageModel
    {
        public string Topic { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

}
