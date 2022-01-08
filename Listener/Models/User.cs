using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listener.Models
{
    public class User
    {
        private Account _account;
        public Account Account 
        { 
            get { 
                if (_account == null) _account = new Account();
                return _account;
            }
            set { _account = value; }
        }
        public string Token { get; set; }
        public DateTime LoggedTime { get; set; } = DateTime.Now;
    }

    public class OnlineUsers : List<User>
    {
        public User Find(string token)
        {
            return base.Find(u => u.Token == token);
        }   
    }
}
