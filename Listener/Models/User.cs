using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listener.Models
{
    class User
    {
        public Account Account { get; set; }
        public string Token { get; set; }
        public DateTime loggedInTime { get; set; }
    }

    class UsersData : List<User>
    {
       
    }
}
