using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listener.Models
{
    public class ExtendClassRegister
    {
        private List<ExtendClass> _registerClassList;
        public string Username { get; set; }
        public List<ExtendClass> RegisterClassList {
            get 
            {
                if (_registerClassList == null)
                    RegisterClassList = new List<ExtendClass>();
                return _registerClassList;
            }
            set { _registerClassList = value; } 
        }
    }
}
