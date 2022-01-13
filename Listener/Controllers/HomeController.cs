using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinhMVC;

namespace Listener.Controllers
{
    class HomeController : BaseController
    {
        public object Default()
        {
            //Engine.Execute("Account/AddAdmin", new Models.Account
            //{
            //    Username = "admin",
            //    Password = Program.MD5Hash("0"),
            //    Role = new Models.Role
            //    {
            //        Id = 0
            //    }
            //});
            Connect();
            return null;
        }
    }
}
