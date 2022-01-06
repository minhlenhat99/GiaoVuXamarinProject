using System;
using System.Collections.Generic;
using System.Text;
using MinhMVC;

namespace XamarinUser.Controllers
{
    class HomeController : BaseController
    {
        public object Default()
        {
            Connect();
            if(User.Token == null)
            {
                Engine.Execute("Account/Login");
                return null;
            }
            return View();
        }
    }
}
