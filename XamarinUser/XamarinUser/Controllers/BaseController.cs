using System;
using System.Collections.Generic;
using System.Text;
using MinhMVC;
using uPLibrary.Networking.M2Mqtt;


namespace XamarinUser.Controllers
{
    class BaseController : Controller
    {
        public static string Token { get; set; }
        static MqttClient _client;
        static string _topic = "129311";
        static System.Net.IPAddress _hostIp;

        protected void Subcribe()
        {
            _client.Subscribe(new string[] { _topic + "/" + _client.ClientId }, new byte[] { 0 });
        }
        public void Connect()
        {
            if (_client == null)
            {
                if(_hostIp == null)
                {
                    _hostIp = System.Net.Dns.GetHostEntry("broker.emqx.io").AddressList[0];
                }
                // broker.emqx.io
                _client = new MqttClient(_hostIp);
                string clientId = Guid.NewGuid().ToString();
                // Connect to mqtt cloud
                //int count = 5;
                while (true)
                {
                    _client.Connect(clientId);
                    if (_client.IsConnected) break;
                    //System.Threading.Thread.Sleep(1000);
                    //if (--count == 0)
                    //{

                    //}
                }
                Subcribe();

                _client.MqttMsgPublishReceived += (s, e) =>
                {
                    Console.Write(e.Topic + ": ");
                    string text = Encoding.UTF8.GetString(e.Message);
                    Console.WriteLine(text);
                    var obj = Newtonsoft.Json.Linq.JObject.Parse(text);
                    var url = (string)obj.GetValue("url");
                    var message = (string)obj.GetValue("message");
                    Engine.Execute(url, message);
                };
            }
        }
        public object Publish(string sUrl, object sMessage)
        {
            var obj = new
            {
                // Thong tin gui bao gom client id, url, message (payload)
                cid = _client.ClientId,
                url = sUrl,
                message = sMessage
            };
            var s = Newtonsoft.Json.Linq.JObject.FromObject(obj).ToString();
            var msg = Encoding.UTF8.GetBytes(s);
            _client.Publish(_topic, msg);
            return null;
        }
    }
}
