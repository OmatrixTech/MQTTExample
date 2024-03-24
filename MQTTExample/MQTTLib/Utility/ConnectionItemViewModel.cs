using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTLib.Utility
{
    public sealed class ConnectionItemViewModel : BaseViewModel
    {
        string _name = string.Empty;

        public ConnectionItemViewModel(ConnectionPageViewModel ownerPage)
        {
            OwnerPage = ownerPage ?? throw new ArgumentNullException(nameof(ownerPage));

            SessionOptions.UserProperties.AddEmptyItem();
        }

        public string Name
        {
            get => _name;
            set => this.OnPropertyChanged("Name");
        }

        public ConnectionPageViewModel OwnerPage { get; }

        public ConnectResponseViewModel Response { get; } = new();

        public ServerOptionsViewModel ServerOptions { get; } = new();

        public SessionOptionsViewModel SessionOptions { get; } = new();

        public Task Connect()
        {
            return OwnerPage.Connect(this);
        }

        public Task Disconnect()
        {
            return OwnerPage.Disconnect(this);
        }
    }
}
