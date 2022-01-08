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
        static OnlineUsers db = new OnlineUsers();

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
                    AccountDb.GetCollection<Account>().Update(user.Account.Username, user.Account);
                    isSuccess = true;
                }
            }
            RedirectToAction("Publish", "User/ChangePassword", isSuccess, cid);
            return null;
        }
    }
}
