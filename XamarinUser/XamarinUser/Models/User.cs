using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinUser.Models
{
    class User
    {
        private Account _account;
        public Account Account
        {
            get
            {
                if (_account == null) _account = new Account();
                return _account;
            }
            set { _account = value; }
        }
        public string Token { get; set; }
        public DateTime LoggedInTime { get; set; }
        
    }
}
