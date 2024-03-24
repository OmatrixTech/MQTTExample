using MQTTLib.Utility;
using MQTTnet.Client;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace MQTTClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        HelperUtility helperUtility;
        private ObservableCollection<MessageModel> _MQTTMessages = new ObservableCollection<MessageModel>();
        public ObservableCollection<MessageModel> MQTTMessages
        {
            get { return _MQTTMessages; }
            set { _MQTTMessages = value; }
        }


        public MainWindow()
        {
            InitializeComponent();
            helperUtility = new HelperUtility();
            helperUtility._mqttClient.ApplicationMessageReceivedAsync += MqttClient_ApplicationMessageReceivedAsync;// Handle received messages
            helperUtility.ConnectionStatusEvent += HelperUtility_ConnectionStatusEvent;
        }

        private void HelperUtility_ConnectionStatusEvent(object? sender, EventArgs e)
        {
            TxtblckMessage.Text = "Connected with broker..";
        }

        private Task MqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {

            return Task.Run(() =>
            {
                if (arg.ApplicationMessage.Payload!=null)
                {
                    string payloadAsString = Encoding.UTF8.GetString(arg.ApplicationMessage.Payload);
                    string topic = arg.ApplicationMessage.Topic;

                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        MQTTMessages.Add(new MessageModel
                        {
                            Message = payloadAsString,
                            Topic = topic
                        });
                        MessageListView.ItemsSource = MQTTMessages;
                    }));
                }
            });

        }

        private async void Connect_Click(object sender, RoutedEventArgs e)
        {
            await helperUtility.ConnectWithMQTT();
        }

        private async void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await helperUtility._mqttClient.DisconnectAsync();
                TxtblckMessage.Text = "Disconnected with broker..";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void Subscribe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await helperUtility.SubscribeToMQTTTopic();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         
        }

        private async void Unsubscribe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await helperUtility.UnsubscribeTopic();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}