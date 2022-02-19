using Listener.Controllers;
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
        public int ItemSelected { get; set; } = -1;
        public string StudentProcessing { get; set; }
        public List<Account> ListAccount { get; set; }
        public void UpdateUserAccount()
        {
            Account = DB.AccountDb.GetCollection<Account>().FindById(Account.Username).ToObject<Account>();
            if (Account.Role.Id == 0)
            {
                Account.AllRegisterClassList = DB.ExtendClassGiaoVuDb.GetCollection<ExtendClassRegister>().ToList<ExtendClassRegister>();
                ListAccount = DB.AccountDb.GetCollection<Account>().ToList<Account>();
            }
        }
        public void Login(string username, string password)
        {
            var foundAcc = DB.AccountDb.GetCollection<Account>().ToList<Account>().Find(a => a.Username == username);
            if(foundAcc != null)
            {
                this.Account.Username = username;
                if (foundAcc.Password == password)
                {
                    this.Account = foundAcc;
                }
            }
        }
    }

    public class OnlineUsers : List<User>
    {
        public User Find(string token)
        {
            return base.Find(u => u.Token == token);
        }   
    }
}
