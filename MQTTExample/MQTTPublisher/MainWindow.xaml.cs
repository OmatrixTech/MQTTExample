using MQTTnet.Client;
using MQTTnet;
using MQTTnet.Extensions.ManagedClient;
using System.Windows;
using MQTTnet.Server;
using MQTTLib.Utility;
using MQTTnet.Protocol;

namespace MQTTPublisher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HelperUtility helperUtility;
        private IMqttClient _mqttClient;
        public MainWindow()
        {
            InitializeComponent();
            helperUtility = new HelperUtility();
            helperUtility.ConnectionStatusEvent += HelperUtility_ConnectionStatusEvent;
        }

        private void HelperUtility_ConnectionStatusEvent(object? sender, EventArgs e)
        {
            TxtblckMessage.Text = "Connected with broker..";
        }

        private async void PublishButton_Click(object sender, RoutedEventArgs e)
        {
            helperUtility.PushMessageToBroker(MessageTextBox.Text);
        }
       

        private void ConnectBrokerButton_Click(object sender, RoutedEventArgs e)
        {
            helperUtility.ConnectWithMQTT();
        }
       
    }
}