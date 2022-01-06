using System;
using System.Collections.Generic;
using System.Text;
using MinhMVC;

namespace XamarinUser.Controllers
{
    using Models;
    using System.Security.Cryptography;
    using Xamarin.Essentials;

    class AccountController : BaseController
    {
        public object Login()
        {
            return View(User);
        }
        public object Login(Newtonsoft.Json.Linq.JObject message)
        {
            var user = message.ToObject<User>();
            var token = user.Token;
            if (token == null)
            {
                if(user.Username == null) Engine.Execute("Base/Alert", "Error", "Couldn't find your account.");
                else Engine.Execute("Base/Alert", "Error", "Wrong password.");
            }
            else
            {
                User = user;
                Engine.Execute("User/Begin");
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
