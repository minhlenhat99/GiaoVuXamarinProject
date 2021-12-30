using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using MinhMVC;
using uPLibrary.Networking.M2Mqtt;
using Xamarin.Essentials;

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
                while (true)
                {
                    _client.Connect(clientId);
                    if (_client.IsConnected) break;
                }
                Subcribe();

                _client.MqttMsgPublishReceived += (s, e) =>
                {
                    Debug.Write(e.Topic + ": ");
                    string text = Encoding.UTF8.GetString(e.Message);
                    Debug.WriteLine(text);
                    var obj = Newtonsoft.Json.Linq.JObject.Parse(text);
                    var url = (string)obj.GetValue("url");
                    var message = (Newtonsoft.Json.Linq.JObject)obj.GetValue("message");
                    MainThread.BeginInvokeOnMainThread(() => Engine.Execute(url, message));
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
