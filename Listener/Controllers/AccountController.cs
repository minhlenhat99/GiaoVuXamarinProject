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
            var accLogin = loginInfo.ToObject<Account>();
            var user = accLogin.CheckLoginInfo();
            var message = new Dictionary<string, object>();
            if (user.Account.Password != null)
            {
                user.Token = Program.MD5Hash($"{user.Account.Username}{user.LoggedTime}");
                UserController.CreateUser(user);
                var subjectList = SubjectDb.GetCollection<Subject>().ToList<Subject>();
                subjectList.Add(new Subject
                {
                    ID = "ET4060",
                    Name = "Phân tích và thiết kế hướng đối tượng",
                    RequiredTN = false
                });
                subjectList.Add(new Subject
                {
                    ID = "ET4430",
                    Name = "Lập trình nâng cao",
                    RequiredTN = false
                });
                subjectList.Add(new Subject
                {
                    ID = "ET4070",
                    Name = "Cơ sở truyền số liệu",
                    RequiredTN = true
                });
                message.Add("SubjectList", subjectList);
            }
            message.Add("User", user);
            RedirectToAction("Publish", "Account/Login", message, cid);
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
