using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using System.Threading;
using MinhMVC;

namespace Listener.Controllers
{
    class BaseController : Controller
    {
        static MqttClient _client;
        static string _topic = "129311";
        static System.Net.IPAddress _hostIp;
        Queue<Thread> ExecutionThreads = new Queue<Thread>();
        protected void Subcribe()
        {
            _client.Subscribe(new string[] { _topic }, new byte[] { 0 });
        }
        public void Connect()
        {
            if (_client == null)
            {
                if (_hostIp == null)
                {
                    _hostIp = System.Net.Dns.GetHostEntry("broker.emqx.io").AddressList[0];
                }
                _client = new MqttClient(_hostIp);
                string clientId = Guid.NewGuid().ToString();
                // Connect to broker
                Console.Write("Connect to broker... ");
                while (true)
                {
                    _client.Connect(clientId);
                    if (_client.IsConnected) break;
                }
                Console.WriteLine("done");
                Subcribe();
                _client.MqttMsgPublishReceived += (s, e) =>
                {
                    Console.Write(e.Topic + ": ");
                    string text = Encoding.UTF8.GetString(e.Message);
                    Console.WriteLine(text);
                    var obj = Newtonsoft.Json.Linq.JObject.Parse(text);
                    var cid = (string)obj.GetValue("cid");
                    var url = (string)obj.GetValue("url");
                    var message = (object) obj.GetValue("message");
                    // Tam thoi
                    Engine.Execute(url, message, cid);
                    //Thread th = new Thread(() => Engine.Execute(url, message, cid));
                    //th.Start();
                    //ExecutionThreads.Enqueue(th);
                };
            }
        }
        public object Publish(string sUrl, object sMessage, string cid)
        {
            var obj = new
            {
                // Thong tin gui bao gom client id, url, message (payload)
                url = sUrl,
                message = sMessage
            };
            var s = Newtonsoft.Json.Linq.JObject.FromObject(obj).ToString();
            var msg = Encoding.UTF8.GetBytes(s);
            _client.Publish(_topic + "/" + cid, msg);
            // 
            //ExecutionThreads.Dequeue().Abort();
            return null;
        }
    }
}
