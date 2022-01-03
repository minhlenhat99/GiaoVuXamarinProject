using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listener.Models
{
    public class Account
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public Role accRole { get; set; }
    }
}
