using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinhMVC;

namespace Listener
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine.Register(new Program());
            Engine.Execute("Home/Default");
            while (true) { }
        }
    }
}
