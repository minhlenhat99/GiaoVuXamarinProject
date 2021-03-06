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
                DB.ExtendClassGiaoVuDb.GetCollection<ExtendClassRegister>().Update(classList.Username, classList);
                user.StudentProcessing = classList.Username;
                isSuccess = true;
            }
            user.UpdateUserAccount();
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
                var stu = DB.AccountDb.GetCollection<Account>().FindById(data.Username).ToObject<Account>();
                stu.ClassList = data.RegisterClassList;
                DB.AccountDb.GetCollection<Account>().Update(stu.Username, stu);
                // Phia giao vu
                DB.ExtendClassGiaoVuDb.GetCollection<ExtendClassRegister>().Delete(stu.Username);
                isSuccess = true;
                user.StudentProcessing = "";
            }
            user.UpdateUserAccount();
            var replied = new Dictionary<string, object>();
            replied.Add("IsSuccess", isSuccess);
            replied.Add("User", user);
            RedirectToAction("Publish", "Giaovu/ExtendClassRegisterFinish", replied, cid);
            return null;
        }
        public object SetRegisterDuration(Newtonsoft.Json.Linq.JObject message, string cid)
        {
            var accountList = DB.AccountDb.GetCollection<Account>().ToList<Account>();
            var isSuccess = false;
            var token = message.GetValue("Token").ToString();
            var user = db.Find(token);
            if (user != null)
            {
                var duration = message.GetValue("Duration").ToObject<RegisterDuration>();
                var isNewRegisterDuration = message.GetValue("IsNewRegisterDuration").ToObject<bool>();
                user.Account.Duration = duration;
                foreach (var a in accountList)
                {
                    if (isNewRegisterDuration)
                    {
                        a.ClassList.Clear();
                        a.HadSendRegister = false;
                    }
                    a.Duration = duration;
                    DB.AccountDb.GetCollection<Account>().Update(a.Username, a);
                }
                if (isNewRegisterDuration) DB.ExtendClassGiaoVuDb.Clear();
                isSuccess = true;
                var replied = new Dictionary<string, object>();
                replied.Add("IsSuccess", isSuccess);
                replied.Add("User", user);
                RedirectToAction("Publish", "Giaovu/SetRegisterDuration", replied, cid);
            }
            return null;
        }
        public object AllowRegisterAgain(Newtonsoft.Json.Linq.JValue message, string cid)
        {
            var hadSendRegister = false;
            var mssv = message.ToObject<string>();
            var foundAcc = DB.AccountDb.GetCollection<Account>().ToList<Account>().Find(a => a.Username == mssv);
            if (foundAcc.HadSendRegister)
            {
                hadSendRegister = true;
                foundAcc.HadSendRegister = false;
                for(int i = 0; i < foundAcc.ClassList.Count; i++)
                {
                    foundAcc.ClassList[i].Status.ID = 0;
                }
                DB.AccountDb.GetCollection<Account>().Update(foundAcc.Username, foundAcc);
            }
            RedirectToAction("Publish", "Giaovu/AllowRegisterAgain", hadSendRegister, cid);
            return null;
        }
    }
}
