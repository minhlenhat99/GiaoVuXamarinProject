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
    }
}
