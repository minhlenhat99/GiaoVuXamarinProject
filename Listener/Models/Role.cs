using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listener.Models
{
    public class Role
    {
        private string _name;
        public int Id { get; set; } 
        public string Name {
            get
            {
                if (Id == 0) _name = "Admin";
                else if (Id == 1) _name = "Student";
                return _name;
            }
            set { _name = value; }
        } 
    }
}
