using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinhMVC;

namespace Listener.Controllers
{
    using Models;
    using System.Diagnostics;
    using System.Security.Cryptography;

    public class AccountController : BaseController
    {
        public object Login(Newtonsoft.Json.Linq.JObject loginInfo, string cid)
        {
            // Lay thong tin dang nhap
            var login = loginInfo.ToObject<Account>();
            // Tim kiem thong tin dang nhap
            var user = new User();
            user.Login(login.Username, login.Password);
            if (user.Account.Password != null)
            {
                user.Token = Program.MD5Hash($"{user.Account.Username}{user.LoggedTime}");
                UserController.CreateUser(user);
            }
            // Gui lai thong tin dang nhap
            var message = new Dictionary<string, object>();
            message.Add("User", user);
            message.Add("SubjectList", DB.SubjectDb.GetCollection<Subject>().ToList<Subject>());
            RedirectToAction("Publish", "Account/Login", message, cid);
            return null;
        }
        public object CreateAcc(Newtonsoft.Json.Linq.JObject account, string cid)
        {
            bool createSuccess = false;
            var db = DB.AccountDb.GetCollection("Account");
            var acc = new Account
            {
                Username = (string)account.GetValue("Username"),
                Password = (string)account.GetValue("Password"),
                Role = new Role { Id = 1 },
                HadSendRegister = false,
            };
            db.Insert(acc.Username, acc);
            createSuccess = true;
            RedirectToAction("Publish", "Account/CreateAcc", createSuccess, cid);
            return null;
        }
        public object AddAdmin(Account account)
        {
            var db = DB.AccountDb.GetCollection<Account>();
            db.Insert(account.Username, account);
            return null;
        }
    }
}
