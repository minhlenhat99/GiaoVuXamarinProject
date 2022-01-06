using MinhMVC;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinUser.Controllers
{
    class UserController : BaseController
    {
        public object ExtendClassRegister()
        {
            return View();
        }
        public object Begin()
        {
            return View(User);
        }
        public object Logout()
        {
            RedirectToAction("Publish", "User/Logout", User.Token);
            return null;
        }
        public object Logout(Newtonsoft.Json.Linq.JValue message)
        {
            var logoutSuccess = message.ToObject<bool>();
            if (logoutSuccess)
            {
                Engine.Execute("Account/Login");
            }
            return null;
        }
    }
}
