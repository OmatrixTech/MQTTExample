using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTLib.Utility
{
    public sealed class TransportViewModel : EnumViewModel<Transport>
    {
        public TransportViewModel(object? displayValue, Transport value) : base(displayValue, value)
        {
        }

        public object? HostDisplayValue { get; set; }

        public bool IsPortAvailable { get; set; }
    }
}
public enum Transport
{
    TCP,

    WebSocket
}