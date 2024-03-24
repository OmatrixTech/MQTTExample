
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTLib.Utility
{
    public class MQTTConfiguration
    {
        public static readonly string CLIENTID = "OmatrixTechExplorer-" + Guid.NewGuid().ToString();
        public const string MQTT_TOPIC = "OMATRIXTECH/MQTTMESSAGES";
        public const string BROKERADDRESS = "test.mosquitto.org";
        public const int BROKERPORT = 1883;
        public const bool BROKERCLEANSESSION = true;
        public const bool REQUESTPROBLEMINFORMATION = false;
        public const bool REQUESTRESPONSEINFORMATION = false;
        public const string ENCRYPTIONKEY = "O@!#$%009876"; // Replace with your own secret key
        public const string ENCRYPTIONKEYPASSWORD = "oMATRIXTECH12";
    }
}
