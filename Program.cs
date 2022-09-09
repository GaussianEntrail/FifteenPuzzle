using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Fifteen f = new Fifteen(6);
            while (f.runGame)
            {
                f.draw();
                f.control();
            }
            Console.ReadKey();
        }
    }
}
