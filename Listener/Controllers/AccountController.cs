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
        // Co so du lieu luu tru Account trong may
        static BsonData.DataBase _accountDb;
        public static BsonData.DataBase AccountDb
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
            var accountList = AccountDb.GetCollection<Account>().ToList<Account>();
            var acc = account.ToObject<Account>();
            var user = acc.FindAccount(accountList);
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
                Username = account.Username,
                Password = account.Password,
            });
            return null;
        }

    }
}
