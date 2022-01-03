using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinhMVC;
using SE = System.Environment;

namespace Listener.Controllers
{
    using Models;
    using System.Diagnostics;
    using System.Security.Cryptography;

    class AccountController : BaseController
    {
        static BsonData.DataBase _accountDb;
        public BsonData.DataBase AccountDb
        {
            get
            {
                if (_accountDb == null)
                {
                    _accountDb = new BsonData.DataBase(SE.GetFolderPath(SE.SpecialFolder.Personal), "AccountDb");
                }
                return _accountDb;
            }
        }
        public object Login(Newtonsoft.Json.Linq.JObject account, string cid)
        {
            string token = null;
            int? roleId = null;
            var db = AccountDb.GetCollection("Account").ToDictionary<Account>();
            KeyValuePair<string, Account> searchResult =
               db.FirstOrDefault(s => s.Value.Id == (string)account.GetValue("Id") && s.Value.Password == (string)account.GetValue("Password"));
            var acc = searchResult.Value;
            //var acc = db.Find(a => a.Id == (string)account.GetValue("Id") && a.Password == (string)account.GetValue("Password"));
            if (acc != null)
            {
                var now = DateTime.Now;
                token = Program.MD5Hash(acc.Id + " " + now);
                Engine.Execute("User/CreateUser", new User
                {
                    uAccount = acc,
                    loggedInTime = now,
                    Token = token
                });
                roleId = acc.accRole.Id;
            }

            var message = new
            {
                Token = token,
                RoleId = roleId
            };
            RedirectToAction("Publish", "Account/Login", message, cid);
            return null;
        }
        public object CreateAcc(Newtonsoft.Json.Linq.JObject account, string cid)
        {
            bool createSuccess = false;
            var db = AccountDb.GetCollection("Account");
            var acc = new Account
            {
                Id = (string)account.GetValue("Id"),
                Password = (string)account.GetValue("Password"),
                accRole = new Role()
                {
                    Id = 1
                }
            };
            db.Insert(acc);
            createSuccess = true;
            RedirectToAction("Publish", "Account/CreateAcc", createSuccess, cid);
            return null;
        }
        public object AddAdmin(Account account)
        {
            var db = AccountDb.GetCollection<Account>();
            db.Insert(new Account 
            {
                Id = account.Id,
                Password = account.Password,
                accRole = account.accRole
            });
            return null;
        }
        
    }
}
