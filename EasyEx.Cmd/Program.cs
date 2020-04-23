using EasyEx.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyEx.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new Test();
            var msg = test.GetMessage("Hello", "Easy", "Aop");
            Console.WriteLine(msg);
            Console.ReadKey();
        }
    }
}
