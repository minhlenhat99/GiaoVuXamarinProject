using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinUser.Models
{
    class User : Account
    {
        public string Token { get; set; }
        public DateTime LoggedInTime { get; set; }
        public Role Role { get; set; }
    }
}
