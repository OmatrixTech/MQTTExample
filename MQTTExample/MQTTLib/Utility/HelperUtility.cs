using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System.Windows;

namespace MQTTLib.Utility
{
    public class HelperUtility : BindableBase
    {
        #region Events
        public event EventHandler ConnectionStatusEvent;

        // Method to raise the event
        protected void TriggerConnectionStatusEvent(EventArgs e)
        {
            ConnectionStatusEvent?.Invoke(this, e);
        }
        #endregion
        public IMqttClient _mqttClient;
        public HelperUtility()
        {
            InitializeMQTTClient();
        }
        public async void InitializeMQTTClient()
        {
            _mqttClient = new MqttFactory().CreateMqttClient();
        }

        [Obsolete]
        public async Task ConnectWithMQTT()
        {
            var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            try
            {
                var clientOptionsBuilder = new MqttClientOptionsBuilder().WithTimeout(TimeSpan.FromSeconds(10))
               .WithProtocolVersion(MQTTnet.Formatter.MqttProtocolVersion.V500)
               .WithClientId(MQTTConfiguration.CLIENTID)
               .WithCleanSession(MQTTConfiguration.BROKERCLEANSESSION)
               .WithCredentials("", "")//.WithTls(new MqttClientOptionsBuilderTlsParameters
               //{
               //    UseTls = true,
               //    SslProtocol = System.Security.Authentication.SslProtocols.Tls12,
               //    CertificateValidationHandler = new Func<MqttClientCertificateValidationEventArgs, bool>((args) =>
               //    {
               //        // Custom certificate validation logic
               //        // Return true to accept the certificate or false to reject it
               //        Console.WriteLine($"Certificate validation result: {args.SslPolicyErrors}");
               //        return true; // Accept any certificate (unsafe)
               //    })
               //})
               .WithRequestProblemInformation(MQTTConfiguration.REQUESTPROBLEMINFORMATION)
               .WithRequestResponseInformation(MQTTConfiguration.REQUESTRESPONSEINFORMATION)
               .WithKeepAlivePeriod(TimeSpan.FromSeconds(10));
                clientOptionsBuilder.WithTcpServer(MQTTConfiguration.BROKERADDRESS, MQTTConfiguration.BROKERPORT);
                var resultSet = await _mqttClient.ConnectAsync(clientOptionsBuilder.Build(), timeout.Token);
                if (resultSet != null)
                {
                    if (resultSet.ResultCode== MqttClientConnectResultCode.Success)
                    {
                        TriggerConnectionStatusEvent(null);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public async Task DisconnectBroker()
        {
            try
            {
                // Disconnect from the MQTT broker when the application is closed
               await _mqttClient.DisconnectAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       // public async void 
        public async Task<bool> SubscribeToMQTTTopic()
        {
            try
            {
                var topicFilter = new MqttTopicFilterBuilder().WithTopic(MQTTConfiguration.MQTT_TOPIC)
               .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtMostOnce)
               .WithNoLocal(false)
               .WithRetainHandling(MqttRetainHandling.SendAtSubscribe)
               .WithRetainAsPublished(true)
               .Build();
                var subscribeOptionsBuilder = new MqttClientSubscribeOptionsBuilder().WithTopicFilter(topicFilter);
                var subscribeOptions = subscribeOptionsBuilder.Build();
                await _mqttClient!.SubscribeAsync(subscribeOptions).ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        public async Task UnsubscribeTopic()
        {
            try
            {
              await  _mqttClient.UnsubscribeAsync(MQTTConfiguration.MQTT_TOPIC);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public async void PushMessageToBroker(string messageFromTextbox)
        {
            try
            {
                var message = new MqttApplicationMessageBuilder()
                   .WithTopic(MQTTConfiguration.MQTT_TOPIC)
                   .WithPayload(messageFromTextbox).WithRetainFlag(true)
                   .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                   .WithRetainFlag()
                   .Build();

                await _mqttClient.PublishAsync(message);
                await Task.Delay(1000); // Wait for 1 second
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }

}
