using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sqlite_c_shape {
    class Program {
        static void Main(string[] args)
        {
            sqlite_helper helper = sqlite_helper.get_instance();
            helper.CreateTable();
            helper.InsertData();
            helper.ReadData();

            Console.ReadLine();
        }
    }
}
