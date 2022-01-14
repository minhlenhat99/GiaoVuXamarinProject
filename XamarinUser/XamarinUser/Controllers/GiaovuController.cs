﻿using MinhMVC;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinUser.Controllers
{
    using Models;
    class GiaovuController : UserController
    {
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
    }
}
