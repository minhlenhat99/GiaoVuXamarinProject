using MinhMVC;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinUser.Controllers
{
    class UserController : BaseController
    {
        public object PersonalInfoUpdate()
        {
            return View(User);
        }
        public object ChangePassword()
        {
            return View(User);
        }
        public object ChangePassword(Newtonsoft.Json.Linq.JValue message)
        {
            var isSuccess = message.ToObject<bool>();
            var alert = "";
            var detail = "";
            if (isSuccess)
            {
                alert = "Success";
                detail = "Change password successfully";
            }
            else
            {
                alert = "Erorr";
                detail = "Enter wrong password";
                RedirectToAction("Logout");
            }
            Engine.Execute("Base/Alert", alert, detail);
            return null;
        }

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
