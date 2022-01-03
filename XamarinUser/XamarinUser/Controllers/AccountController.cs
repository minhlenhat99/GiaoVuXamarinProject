using System;
using System.Collections.Generic;
using System.Text;
using MinhMVC;

namespace XamarinUser.Controllers
{
    using Models;
    using System.Security.Cryptography;

    class AccountController : BaseController
    {
        public object Login()
        {
            return View();
        }
        public object Login(Newtonsoft.Json.Linq.JObject message)
        {
            var token = message.GetValue("Token").ToObject<string>();
            if (token == null)
            {
                Engine.Execute("Base/Alert", "Error", "Couldn't find your account.");
            }
            else
            {
                Token = token;
                var roleId = message.GetValue("RoleId").ToObject<int>();
                Engine.Execute("User/Test", roleId);
            }
            return null;
        }
        
        public object CreateAcc()
        {
            return View();
        }
        public object CreateAcc(Newtonsoft.Json.Linq.JToken message)
        {
            var mess = message.ToObject<bool>();
            if (mess)
            {
                Engine.Execute("Base/Alert", "Success", "The account was created successfully.");
            }
            return null;
        }
        
    }
}
