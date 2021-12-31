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
            var db = AccountDb.GetCollection<Account>().ToList<Account>();
            var acc = db.Find(a => a.StudentId == (string) account.GetValue("StudentId") && a.Password == (string)account.GetValue("Password"));
            if (acc != null)
            {
                token = Program.MD5Hash("Test");
            }
            RedirectToAction("Publish", "Account/Login", token, cid);
            return null;
        }
        public object CreateAcc(Newtonsoft.Json.Linq.JObject account, string cid)
        {
            bool createSuccess = false;
            var db = AccountDb.GetCollection<Account>();
            db.Insert(new Account 
            {
                StudentId = (string) account.GetValue("StudentId"),
                Password = (string) account.GetValue("Password")
            });
            Debug.WriteLine(db.Count());
            createSuccess = true;
            RedirectToAction("Publish", "Account/CreateAcc", createSuccess, cid);
            return null;
        }
        
    }
}
