using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinUser.Models
{
    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public PersonalInfo PInfo { get; set; }
        public ContactInfo CInfo { get; set; }
        public List<ExtendClass> ClassList { get; set; }
        public List<ExtendClassRegister> AllRegisterClassList { get; set; }
    }
}
