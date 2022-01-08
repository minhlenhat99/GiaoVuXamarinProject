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
        public Role Role { get; set; }

        public User FindAccountInfo(List<Account> accountList)
        {
            var foundAcc = accountList.Find(s => s.Username == this.Username);
            var user = new User();
            if (foundAcc == null)
            {
                return user;
            }
            user.Account.Username = this.Username;
            if (foundAcc.Password != this.Password)
            {
                return user;
            }
            user.Account.Password = foundAcc.Password;
            user.Account.Role = foundAcc.Role;
            return user;
        }
    }
}
