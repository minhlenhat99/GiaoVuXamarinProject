using MinhMVC;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinUser.Controllers
{
    using Models;
    class UserController : BaseController
    {
        public object ContactInfoUpdate()
        {
            return View(User);
        } 
        public object ContactInfoUpdate(Newtonsoft.Json.Linq.JObject message)
        {
            var isSuccess = message.GetValue("IsSuccess").ToObject<bool>();
            if (isSuccess)
            {
                Engine.Execute("Base/Alert", "Success", "Update contact information successfully");
                var user = message.GetValue("User").ToObject<User>();
                User = user;
            }
            return null;
        }
        public object PersonalInfoUpdate()
        {
            return View(User);
        }
        public object PersonalInfoUpdate(Newtonsoft.Json.Linq.JObject message)
        {
            var isSuccess = message.GetValue("IsSuccess").ToObject<bool>();
            if (isSuccess)
            {
                Engine.Execute("Base/Alert", "Success", "Update personal information successfully");
                var user = message.GetValue("User").ToObject<User>();
                User = user;
            }
            return null;
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
            return View(User);
        } 
        public object ExtendClassModify(Newtonsoft.Json.Linq.JValue message)
        {
            var isSuccess = message.ToObject<bool>();
            if (isSuccess)
            {
                Engine.Execute("Base/Alert", "Success", "Modify successfully");
            }
            return null;
        }
        public object ExtendClassRegister(Newtonsoft.Json.Linq.JObject message)
        {
            var isSuccess = message.GetValue("IsSuccess").ToObject<bool>();
            if (isSuccess)
            {
                Engine.Execute("Base/Alert", "Success", "Register successfully");
                var user = message.GetValue("User").ToObject<User>();
                User = user;
                RedirectToAction("ExtendClassMainPage");
            }
            return null;
        }
        public object ExtendClassMainPage()
        {
            return View(User);
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
