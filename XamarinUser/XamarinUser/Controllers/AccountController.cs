using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinUser.Controllers
{
    using Models;
    class AccountController : BaseController
    {
        public object Login()
        {
            return View();
        }
        public void Login(Newtonsoft.Json.Linq.JObject account)
        {
            if ((bool)account.GetValue("IsExisted"))
            {

            }
            else
            {
                RedirectToAction("AlertError", "Couldn't find your account");
            } 
        }
        public object AlertError(string message)
        {
            return View();
        }
        public object CreateAcc()
        {
            return View();
        }
    }
}
