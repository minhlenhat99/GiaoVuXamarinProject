using MinhMVC;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinUser.Controllers
{
    class UserController : BaseController
    {
        public object Test()
        {
            return View();
        }
        public object Logout()
        {
            RedirectToAction("Publish", "User/Logout", Token);
            return null;
        }
        public object Logout(Newtonsoft.Json.Linq.JValue message)
        {
            var logoutSuccess = message.ToObject<bool>();
            if (logoutSuccess)
            {
                Engine.Execute("Account/Login");
            }
            else
            {
                
            }
            return null;
        }
    }
}
