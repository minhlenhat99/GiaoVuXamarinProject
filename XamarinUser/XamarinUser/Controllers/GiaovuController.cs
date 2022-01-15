using MinhMVC;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinUser.Controllers
{
    using Models;
    class GiaovuController : UserController
    {
        public new object ExtendClassMainPage()
        {
            return View(User);
        }
        public object ExtendClassRegisterUpdate(Newtonsoft.Json.Linq.JObject message)
        {
            var isSuccess = message.GetValue("IsSuccess").ToObject<bool>();
            if (isSuccess)
            {
                Engine.Execute("Base/Alert", "Success", "Success");
                var user = message.GetValue("User").ToObject<User>();
                User = user;
                Engine.Execute("User/ExtendClassMainPage");
            }
            return null;
        }
        public object ExtendClassRegisterFinish(Newtonsoft.Json.Linq.JObject message)
        {
            var isSuccess = message.GetValue("IsSuccess").ToObject<bool>();
            if (isSuccess)
            {
                Engine.Execute("Base/Alert", "Success", "Success");
                var user = message.GetValue("User").ToObject<User>();
                User = user;
                Engine.Execute("User/ExtendClassMainPage");
            }
            return null;
        }
        public object SetRegisterDuration()
        {
            return View(User);
        }
        public object SetRegisterDuration(Newtonsoft.Json.Linq.JObject message)
        {
            var isSuccess = message.GetValue("IsSuccess").ToObject<bool>();
            if (isSuccess)
            {
                Engine.Execute("Base/Alert", "Success", "Success");
                var user = message.GetValue("User").ToObject<User>();
                User = user;
            }
            return null;
        }
        public object AllowRegisterAgain()
        {
            return View(User);
        }
        public object AllowRegisterAgain(Newtonsoft.Json.Linq.JValue message)
        {
            var hadSendRegister = message.ToObject<bool>();
            if (hadSendRegister)
                Engine.Execute("Base/Alert", "Success", "Success");
            else
                Engine.Execute("Base/Alert", "Error", "Sinh viên này vẫn còn quyền hạn đăng ký mở rộng lớp");
            return null;
        }
    }
}
