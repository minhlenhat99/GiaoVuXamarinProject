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
            Connect();
            return null;
        }
    }
}
