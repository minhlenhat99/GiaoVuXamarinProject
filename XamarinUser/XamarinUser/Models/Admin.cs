using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinUser.Models
{
    public class Admin : Account
    {
        public string StudentProcessing { get; set; }
        new public List<ExtendClassRegister> AllRegisterClassList { get; set; }
    }
}
