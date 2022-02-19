using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Listener.Models;
using MinhMVC;

namespace Listener.Controllers
{
    class HomeController : BaseController
    {
        public object Default()
        {
            //Engine.Execute("Account/AddAdmin", new Models.Account
            //{
            //    Username = "ADMIN",
            //    Password = Program.MD5Hash("0"),
            //    Role = new Models.Role
            //    {
            //        Id = 0
            //    }
            //});
            DB.SubjectDb.GetCollection<Subject>().Insert("ET4060", new Subject {ID = "ET4060", Name = "Phan tich va thiet ke huong doi tuong", RequiredTN = false });
            DB.SubjectDb.GetCollection<Subject>().Insert("ET4710", new Subject {ID = "ET4710", Name = "Lap trinh ung dung di dong", RequiredTN = false });
            DB.SubjectDb.GetCollection<Subject>().Insert("ET4361", new Subject {ID = "ET4361", Name = "He thong nhung va thiet ke giao tiep nhung", RequiredTN = true });
            Connect();
            return null;
        }
    }
}
