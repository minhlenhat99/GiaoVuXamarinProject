using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listener.Models
{
    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public User FindAccount(List<Account> accountList)
        {
            var foundAcc = accountList.Find(s => s.Username == this.Username);
            var user = new User();
            if (foundAcc == null)
            {
                return user;
            }
            user.Username = this.Username;
            if (foundAcc.Password != this.Password)
            {
                return user;
            }
            user.Password = this.Password;
            user.LoggedInTime = DateTime.Now;
            user.Token = Program.MD5Hash($"{user.Username}{user.LoggedInTime}");
            user.Role = new Role { Id = 1 };
            return user;
        }
    }
}
