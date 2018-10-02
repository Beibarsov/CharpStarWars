using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StarWars
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);




            Form splash_screen = new Form();
            splash_screen.Width = 800;
            splash_screen.Height = 600;
            SplashScreen.Load(splash_screen);
            SplashScreen.Init(splash_screen);
            splash_screen.Show();
            SplashScreen.Draw(splash_screen);
            Application.Run(splash_screen);


            //System.Threading.Thread.Sleep(10000);
        }
    }
}
