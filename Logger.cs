using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace StarWars
{
    class Logger
    {
        string _FileName = "log.txt";



        public void Do(string message)
        {
            using (var file = File.AppendText(_FileName))
            {
                file.WriteLine(DateTime.Now.ToLongTimeString() +" " + message);
            }
            Console.WriteLine(message);
        }
    }
}
