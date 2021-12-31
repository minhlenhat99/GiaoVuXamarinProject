using Listener.Models;
using MinhMVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listener.Controllers
{
    class UserController : BaseController
    {
        static UsersData db = new UsersData();

        public object CreateUser(User newUser)
        {
            db.Add(newUser);
            return null;
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
    }
}
