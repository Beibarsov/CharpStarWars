using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace StarWars
{
    static class SplashScreen
    {
        private static BufferedGraphicsContext __Context;
        public static BufferedGraphics __Buffer;

        private static Timer __Timer = new Timer { Interval = 70 };

        private static List<Button> __Buttons;
        private static List<Label> __Labels;
        private static List<TextBox> __TextBox;


        public static int Width { get; private set; }
        public static int Height { get; private set; }

        public static void Init(Form form)
        {
            var graphics = form.CreateGraphics();
            Width = form.Width;
            Height = form.Height;

            __Context = BufferedGraphicsManager.Current;
            __Buffer = __Context.Allocate(graphics, new Rectangle(0, 0, Width, Height));



            __Timer.Tick += GetTimerTick;
            __Timer.Enabled = true;
        }

        private static void GetTimerTick(object Sender, EventArgs e)
        {
            Update();
            //Draw();
        }

        public static void Update()
        {
            return;
        }

        public static void Draw(Form form)
        {
            foreach (var item in __Buttons)
            {
                form.Controls.Add(item);
            }
            foreach (var item in __Labels)
            {
                form.Controls.Add(item);
            }
            foreach (var item in __TextBox)
            {
                form.Controls.Add(item);
            }
        }
        public static void Load(Form form)
        {
            __Labels = new List<Label>();
            __Labels.Add(new Label { Text = "StarWars", Location = (new Point(form.Width / 2 - 50, 100)), Size = (new Size(100, 40)) });
            __Labels.Add(new Label { Text = "Введите имя:", Location = (new Point(form.Width / 2 - 50, 140)), Size = (new Size(100, 20)) });


            __TextBox = new List<TextBox>();
            __TextBox.Add(new TextBox { Location = (new Point(300, 160)), Size = (new Size(200, 40)) });

            __Buttons = new List<Button>();
            __Buttons.Add(new Button { Text = "Новая игра", Location = (new Point(300, 200)), Size = (new Size(200, 40)) });
            __Buttons[0].Click += NewGame;
            __Buttons.Add(new Button { Text = "Закрыть игру", Location = (new Point(300, 240)), Size = (new Size(200, 40)) });
            __Buttons[1].Click += ExitGame;





        }

        private static void NewGame(object Sender, EventArgs e)
        {
            
            Form game_form = new Form();
            game_form.Width = 800;
            game_form.Height = 600;
            Game.Load();
            Game.Init(game_form);
            game_form.Show();
            Game.Draw();
            //Application.Run(game_form);
        }

        private static void ExitGame(object Sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
