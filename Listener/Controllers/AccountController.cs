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
            var db = AccountDb.GetCollection<Account>().ToList<Account>();
            var acc = db.Find(a => a.StudentId == (string) account.GetValue("StudentId") && a.Password == (string)account.GetValue("Password"));
            if (acc != null)
            {
                account["IsExisted"] = true;
            }
            RedirectToAction("Publish", "Account/Login", account, cid);
            return null;
        }
        public object CreateAcc(Newtonsoft.Json.Linq.JObject account, string cid)
        {
            var db = AccountDb.GetCollection<Account>();
            db.Insert(new Account 
            {
                StudentId = (string) account.GetValue("StudentId"),
                Password = (string) account.GetValue("Password")
            });
            Debug.WriteLine(db.Count());
            account["IsExisted"] = true;
            RedirectToAction("Publish", "Account/CreateAcc", account, cid);
            return null;
        }
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
