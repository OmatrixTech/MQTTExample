using MQTTnet.Formatter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace MQTTLib.Utility
{
    internal class ServerOptionsModel : BaseViewModel
    {
        int _communicationTimeout;
        string _host = string.Empty;
        bool _ignoreCertificateErrors;
        int _port;
        int _receiveMaximum;
        EnumViewModel<MqttProtocolVersion> _selectedProtocolVersion;

        EnumViewModel<SslProtocols> _selectedTlsVersion;
        TransportViewModel _selectedTransport;

        public ServerOptionsModel()
        {
            Host = "localhost";
            Port = 1883;
            CommunicationTimeout = 10;

            Transports.Add(new TransportViewModel("TCP", Transport.TCP)
            {
                IsPortAvailable = true,
                HostDisplayValue = "Host"
            });

            Transports.Add(new TransportViewModel("WebSocket", Transport.WebSocket)
            {
                HostDisplayValue = "URI"
            });

            _selectedTransport = Transports.First();

            TlsVersions.Add(new EnumViewModel<SslProtocols>("no TLS", SslProtocols.None));
            TlsVersions.Add(new EnumViewModel<SslProtocols>("TLS 1.0", SslProtocols.Tls));
            TlsVersions.Add(new EnumViewModel<SslProtocols>("TLS 1.1", SslProtocols.Tls11));
            TlsVersions.Add(new EnumViewModel<SslProtocols>("TLS 1.2", SslProtocols.Tls12));
            TlsVersions.Add(new EnumViewModel<SslProtocols>("TLS 1.3", SslProtocols.Tls13));
            _selectedTlsVersion = TlsVersions.First();

            ProtocolVersions.Add(new EnumViewModel<MqttProtocolVersion>("3.1.0", MqttProtocolVersion.V310));
            ProtocolVersions.Add(new EnumViewModel<MqttProtocolVersion>("3.1.1", MqttProtocolVersion.V311));
            ProtocolVersions.Add(new EnumViewModel<MqttProtocolVersion>("5.0.0", MqttProtocolVersion.V500));

            // 3.1.1 is the mostly used version so we preselect it.
            _selectedProtocolVersion = ProtocolVersions[1];
        }

        public int CommunicationTimeout
        {
            get => _communicationTimeout;
            set => this.OnPropertyChanged("CommunicationTimeout");
        }

        public string Host
        {
            get => _host;
            set => this.OnPropertyChanged("Host");
        }

        public bool IgnoreCertificateErrors
        {
            get => _ignoreCertificateErrors;
            set => this.OnPropertyChanged("IgnoreCertificateErrors");
        }

        public int Port
        {
            get => _port;
            set => this.OnPropertyChanged("Port");
        }

        public ObservableCollection<EnumViewModel<MqttProtocolVersion>> ProtocolVersions { get; } = new();

        public int ReceiveMaximum
        {
            get => _receiveMaximum;
            set => this.OnPropertyChanged("ReceiveMaximum");
        }

        public EnumViewModel<MqttProtocolVersion> SelectedProtocolVersion
        {
            get => _selectedProtocolVersion;
            set => this.OnPropertyChanged("SelectedProtocolVersion");
        }

        public EnumViewModel<SslProtocols> SelectedTlsVersion
        {
            get => _selectedTlsVersion;
            set => this.OnPropertyChanged("SelectedTlsVersion");
        }

        public TransportViewModel SelectedTransport
        {
            get => _selectedTransport;
            set => this.OnPropertyChanged("SelectedTransport");
        }

        public ObservableCollection<EnumViewModel<SslProtocols>> TlsVersions { get; } = new();

        public ObservableCollection<TransportViewModel> Transports { get; } = new();
    }
}
