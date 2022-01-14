using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listener.Controllers
{
    using Models;
    class GiaovuController : UserController
    {
        public object ExtendClassRegisterUpdate(Newtonsoft.Json.Linq.JObject message, string cid)
        {
            var isSuccess = false;
            var token = message.GetValue("Token").ToString();
            var user = db.Find(token);
            if(user != null)
            {
                var classList = message.GetValue("ClassList").ToObject<ExtendClassRegister>();
                ExtendClassGiaoVuDb.GetCollection<ExtendClassRegister>().Update(classList.Username, classList);
                isSuccess = true;
            }
            var replied = new Dictionary<string, object>();
            replied.Add("IsSuccess", isSuccess);
            replied.Add("User", user);
            RedirectToAction("Publish", "Giaovu/ExtendClassRegisterUpdate", replied, cid);
            return null;
        }
        public object ExtendClassRegisterFinish(Newtonsoft.Json.Linq.JObject message, string cid)
        {
            var isSuccess = false;
            var token = message.GetValue("Token").ToString();
            var user = db.Find(token);
            if(user != null)
            {
                var data = message.GetValue("Data").ToObject<ExtendClassRegister>();
                // Phia sinh vien
                var stu = AccountDb.GetCollection<Account>().FindById(data.Username).ToObject<Account>();
                stu.ClassList = data.RegisterClassList;
                AccountDb.GetCollection<Account>().Update(stu.Username, stu);
                // Phia giao vu
                ExtendClassGiaoVuDb.GetCollection<ExtendClassRegister>().Delete(stu.Username);
                isSuccess = true;
            }
            var replied = new Dictionary<string, object>();
            replied.Add("IsSuccess", isSuccess);
            replied.Add("User", user);
            RedirectToAction("Publish", "Giaovu/ExtendClassRegisterFinish", replied, cid);
            return null;
        }
    }
}
