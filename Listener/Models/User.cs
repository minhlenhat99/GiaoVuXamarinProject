﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listener.Models
{
    public class User : Account
    {
        public string Token { get; set; }
        public DateTime LoggedInTime { get; set; }
        public Role Role { get; set; }
    }

    public class UsersData : List<User>
    {
       
    }
}
