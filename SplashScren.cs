using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StarWars
{
    public partial class SplashScren : Form
    {
        public SplashScren()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            this.Close();

           // Form splash_screen = new Form();
           // splash_screen.Width = 800;
           // splash_screen.Height = 600;

           // SplashScreen.Load(splash_screen);
           // SplashScreen.Init(splash_screen);

           // splash_screen.Show();


           //// SplashScreen.Draw();

           // Application.Run(splash_screen);
        }


        public void ExitWindow()
        {
            this.Close();
        }
    }

}
