using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listener.Models
{
    using Controllers;
    public class Account
    {
        private PersonalInfo _pInfo;
        private ContactInfo _cInfo;
        private List<ExtendClass> _classList;
        private List<ExtendClassRegister> _registerClassList;
        private RegisterDuration _duration;
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public PersonalInfo PInfo { 
            get 
            {
                if(_pInfo == null) { _pInfo = new PersonalInfo(); }
                return _pInfo;
            }
            set { _pInfo = value; } 
        }
        public ContactInfo CInfo { 
            get 
            {
                if(_cInfo == null) { _cInfo = new ContactInfo(); }
                return _cInfo;
            }
            set { _cInfo = value; } 
        }
        public bool HadSendRegister { get; set; }
        public List<ExtendClass> ClassList 
        {
            get 
            {
                if (_classList == null) _classList = new List<ExtendClass>();
                return _classList;
            }
            set { _classList = value; }
        }
        public List<ExtendClassRegister> AllRegisterClassList { 
            get
            {
                if(Role != null)
                    if (Role.Id == 0)
                        _registerClassList = BaseController.ExtendClassGiaoVuDb.GetCollection<ExtendClassRegister>().ToList<ExtendClassRegister>();
                return _registerClassList;
            }
            set
            {
                _registerClassList = value;
            }
        }
        public RegisterDuration Duration { 
            get
            {
                if (_duration == null) _duration = new RegisterDuration
                {
                    Start = DateTime.Now,
                    End = DateTime.Now,
                };
                return _duration;
            }
            set
            {
                _duration = value;
            }
        }
        //public User FindAccountInfo(List<Account> accountList)
        //{
        //    var foundAcc = accountList.Find(s => s.Username == this.Username);
        //    var user = new User();
        //    if (foundAcc == null)
        //    {
        //        return user;
        //    }
        //    user.Account.Username = this.Username;
        //    if (foundAcc.Password != this.Password)
        //    {
        //        return user;
        //    }
        //    user.Account.Password = foundAcc.Password;
        //    user.Account.Role = foundAcc.Role;
        //    user.Account.PInfo = foundAcc.PInfo;
        //    user.Account.CInfo = foundAcc.CInfo;
        //    user.Account.ClassList = foundAcc.ClassList;
        //    user.Account.HadSendRegister = foundAcc.HadSendRegister;
        //    user.Account.Duration = foundAcc.Duration;
        //    if (foundAcc.Role.Id == 0)
        //    {
        //        user.Account.AllRegisterClassList = BaseController.ExtendClassGiaoVuDb.GetCollection<ExtendClassRegister>().ToList<ExtendClassRegister>();
        //        user.ListAccount = BaseController.AccountDb.GetCollection<Account>().ToList<Account>();
        //    }
        //    return user;
        //}
        public User CheckLoginInfo()
        {
            var foundAcc = BaseController.AccountDb.GetCollection<Account>().ToList<Account>().Find(s => s.Username == this.Username);
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
            user.Account = foundAcc;
            if (foundAcc.Role.Id == 0)
            {
                user.Account.AllRegisterClassList = BaseController.ExtendClassGiaoVuDb.GetCollection<ExtendClassRegister>().ToList<ExtendClassRegister>();
                user.ListAccount = BaseController.AccountDb.GetCollection<Account>().ToList<Account>();
            }
            return user;
        }
    }
}
