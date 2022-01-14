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
        public object Login(Newtonsoft.Json.Linq.JObject accountLogin, string cid)
        {
            var accountList = AccountDb.GetCollection<Account>().ToList<Account>();
            var accLogin = accountLogin.ToObject<Account>();
            var user = accLogin.FindAccountInfo(accountList);
            if(user.Account.Password != null)
            {
                user.Token = Program.MD5Hash($"{user.Account.Username}{user.LoggedTime}");
                UserController.CreateUser(user);
            }
            RedirectToAction("Publish", "Account/Login", user, cid);
            return null;
        }
        public object CreateAcc(Newtonsoft.Json.Linq.JObject account, string cid)
        {
            bool createSuccess = false;
            var db = AccountDb.GetCollection("Account");
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
            var db = AccountDb.GetCollection<Account>();
            db.Insert(account.Username, account);
            return null;
        }

    }
}
