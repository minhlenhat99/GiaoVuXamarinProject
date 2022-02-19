using Listener.Models;
using MinhMVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listener.Controllers
{
    public class UserController : BaseController
    {
        protected static OnlineUsers db = new OnlineUsers();

        public static void CreateUser(User loginAccount)
        {
            db.Add(loginAccount);
        }
        public object Logout(Newtonsoft.Json.Linq.JValue token, string cid)
        {
            var user = db.Find(u => u.Token == token.ToObject<string>());
            if(user != null)
            {
                db.Remove(user);
                RedirectToAction("Publish", "Account/Login", cid);
            }
            return null;
        }
        public object ChangePassword(Newtonsoft.Json.Linq.JObject message, string cid)
        {
            var isSuccess = false;
            var token = (string)message.GetValue("Token");
            var user = db.Find(token);
            // Neu user ton tai
            if (user != null)
            {
                var oldPassword = (string)message.GetValue("OldPass");
                if (user.Account.Password == oldPassword)
                {
                    var newPassword = (string)message.GetValue("NewPass");
                    user.Account.Password = newPassword;
                    DB.AccountDb.GetCollection<Account>().Update(user.Account.Username, user.Account);
                    isSuccess = true;
                }
            }
            user.UpdateUserAccount();
            RedirectToAction("Publish", "User/ChangePassword", isSuccess, cid);
            return null;
        }
        public object PersonalInfoUpdate(Newtonsoft.Json.Linq.JObject message, string cid)
        {
            var isSuccess = false;
            var token = message.GetValue("Token").ToString();
            var info = message.GetValue("Info").ToObject<PersonalInfo>();
            var user = db.Find(token);
            if (user != null)
            {
                user.Account.PInfo = info;
                DB.AccountDb.GetCollection<Account>().Update(user.Account.Username, user.Account);
                isSuccess = true;
            }
            user.UpdateUserAccount();
            var replied = new Dictionary<string, object>();
            replied.Add("IsSuccess", isSuccess);
            replied.Add("User", user);
            RedirectToAction("Publish", "User/PersonalInfoUpdate", replied, cid);
            return null;
        }
        public object ContactInfoUpdate(Newtonsoft.Json.Linq.JObject message, string cid)
        {
            var isSuccess = false;
            var token = message.GetValue("Token").ToString();
            var info = message.GetValue("Info").ToObject<ContactInfo>();
            var user = db.Find(token);
            if (user != null)
            {
                user.Account.CInfo = info;
                DB.AccountDb.GetCollection<Account>().Update(user.Account.Username, user.Account);
                isSuccess = true;
            }
            user.UpdateUserAccount();
            var replied = new Dictionary<string, object>();
            replied.Add("IsSuccess", isSuccess);
            replied.Add("User", user);
            RedirectToAction("Publish", "User/ContactInfoUpdate", replied, cid);
            return null;
        }
        public object ExtendClassModify(Newtonsoft.Json.Linq.JObject message, string cid)
        {
            var isSuccess = false;
            var token = message.GetValue("Token").ToString();
            var registerClass = message.GetValue("Info").ToObject<List<ExtendClass>>();
            var user = db.Find(token);
            if (user != null)
            {
                user.Account.ClassList = registerClass;
                DB.AccountDb.GetCollection<Account>().Update(user.Account.Username, user.Account);
                isSuccess = true;
            }
            user.UpdateUserAccount();
            RedirectToAction("Publish", "User/ExtendClassModify", isSuccess, cid);
            return null;
        }
        public object ExtendClassRegister(Newtonsoft.Json.Linq.JObject message, string cid)
        {
            var isSuccess = false;
            var token = message.GetValue("Token").ToString();
            var user = db.Find(token);
            if (user != null)
            {
                foreach(var c in user.Account.ClassList)
                {
                    c.Status.ID = 1;
                }
                user.Account.HadSendRegister = true;
                DB.AccountDb.GetCollection<Account>().Update(user.Account.Username, user.Account);
                DB.ExtendClassGiaoVuDb.GetCollection<ExtendClassRegister>().Insert(user.Account.Username,
                    new ExtendClassRegister
                    {
                        Username = user.Account.Username,
                        RegisterClassList = user.Account.ClassList
                    });
                isSuccess = true;
            }
            user.UpdateUserAccount();
            var replied = new Dictionary<string, object>();
            replied.Add("IsSuccess", isSuccess);
            replied.Add("User", user);
            RedirectToAction("Publish", "User/ExtendClassRegister", replied, cid);
            return null;
        }
        public object UpdateUserAccount(Newtonsoft.Json.Linq.JObject message, string cid)
        {
            var isSuccess = false;
            var user = message.GetValue("User").ToObject<User>();
            if (user != null)
            {
                user.UpdateUserAccount();
                isSuccess = true;
            }
            var replied = new Dictionary<string, object>();
            replied.Add("IsSuccess", isSuccess);
            replied.Add("User", user);
            replied.Add("NextUrl", message.GetValue("NextUrl"));
            RedirectToAction("Publish", "User/UpdateUserAccountReplied", replied, cid);
            return null;
        }
    }
}
