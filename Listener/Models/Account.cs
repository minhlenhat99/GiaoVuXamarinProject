using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SE = System.Environment;

namespace Listener.Models
{
    using Controllers;
    public class Account
    {
        private PersonalInfo _pInfo;
        private ContactInfo _cInfo;
        private List<ExtendClass> _classList;
        private List<ExtendClassRegister> _registerClassList;
        private static RegisterDuration _duration;
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
                        _registerClassList = DB.ExtendClassGiaoVuDb.GetCollection<ExtendClassRegister>().ToList<ExtendClassRegister>();
                return _registerClassList;
            }
            set
            {
                _registerClassList = value;
            }
        }
        public RegisterDuration Duration
        {
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
    }

    partial class DB
    {
        static BsonData.DataBase _accountDb;
        static BsonData.DataBase _extendClassGiaoVuDb;
        static BsonData.DataBase _subjectDb;
        public static BsonData.DataBase AccountDb
        {
            get
            {
                if (_accountDb == null)
                {
                    _accountDb = new BsonData.DataBase(SE.GetFolderPath(SE.SpecialFolder.Personal), "AccountDb");
                }
                return _accountDb;
            }
        }
        public static BsonData.DataBase ExtendClassGiaoVuDb
        {
            get
            {
                if (_extendClassGiaoVuDb == null)
                {
                    _extendClassGiaoVuDb = new BsonData.DataBase(SE.GetFolderPath(SE.SpecialFolder.Personal), "ExtendClassGiaoVuDb");
                }
                return _extendClassGiaoVuDb;
            }
        }
        public static BsonData.DataBase SubjectDb
        {
            get
            {
                if (_subjectDb == null)
                {
                    _subjectDb = new BsonData.DataBase(SE.GetFolderPath(SE.SpecialFolder.Personal), "SubjectDb");
                }
                return _subjectDb;
            }
        }
    }
}
