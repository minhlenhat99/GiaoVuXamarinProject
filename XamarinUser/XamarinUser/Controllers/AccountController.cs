using System;
using System.Collections.Generic;
using System.Text;

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
        public object Login(Newtonsoft.Json.Linq.JObject account)
        {
            if (!(bool)account.GetValue("IsExisted"))
            {
                RedirectToAction("Alert", "Error", "Couldn't find your account.");
            }
            else
            {
                RedirectToAction("Alert", "Success", "Ok. You logged in");
            }
            return null;
        }
        public object Alert(string title, string message)
        {
            return View();
        }
        public object CreateAcc()
        {
            return View();
        }
        public object CreateAcc(Newtonsoft.Json.Linq.JObject account)
        {
            if ((bool)account.GetValue("IsExisted"))
            {
                RedirectToAction("Alert", "Success", "The account was created successfully.");
            }
            return null;
        }
        
    }
}
