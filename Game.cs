//using System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace StarWars

{
    public static class Game
    {
        private static BufferedGraphicsContext __Context;
        public static BufferedGraphics Buffer { get; private set; }

        private static Timer __Timer = new Timer { Interval = 70 };


        private static GameObject[] __GameObjects;
        private static List<Asteroid> __Asteroids;
        private static Bullet __Bullet;
        private static StarShip __Ship;
        private static EnergyTank __EnergyTank;
        //Надписи
        private static LabelGame __ScoreCount;
        private static LabelGame __EnergeCount;
        //Количество астероидов на уровне
        private static int __asteroidCount = 0;

        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Score { get; set; } = 0;

        static Logger logger = new Logger();

        //Делегат и событие для событий логгера
        public delegate void Message(string message);
        private static event Message _message;


        public static void Init(Form form)
        {
            var graphics = form.CreateGraphics();
            Width = form.Width;
            Height = form.Height;

            __Context = BufferedGraphicsManager.Current;
            Buffer = __Context.Allocate(graphics, new Rectangle(0, 0, Width, Height));
            __Timer.Tick += GetTimerTick;
            __Timer.Enabled = true;

            //Перехват события формы "нажата кнопка". Вызывается функция Form_KeyDown
            form.KeyDown += Form_KeyDown;
            //Перехват события "MessageDie", что вызывает функцию конца игры
            __Ship.MessageDie += Finish;

        }


        /// <summary>
        /// Функция, срабатывающая по тику таймера. Содержимое - то, что выполняется по тику таймера.
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        /// 
        private static void GetTimerTick(object Sender, EventArgs e)
        {
            try
            {

                Update(logger.Do);
                Draw();
                SizeWindowCheck();



            }
            catch (Exception ex)
            {
                __Timer.Tick -= GetTimerTick;
                MessageBox.Show(ex.Message);
                MessageBox.Show("Приложение будет закрыто");
                Application.Exit();


            }

        }

        /// <summary>
        /// Логика создания объектов
        /// </summary>
        public static void Load()
        {

            try
            {



                __GameObjects = new GameObject[20];
                //Звездочки
                for (int i = 0; i < __GameObjects.Length -1; i++)
                {
                    __GameObjects[i] = new Star(new Point(600, 15 + i * 27), new Point(5 + i, 25 - i), new Size(15, 15));

                }
                //Звездная база
                for (int i = __GameObjects.Length -1; i < __GameObjects.Length; i++)
                {
                    __GameObjects[i] = new StarBase(new Point(300, 200), new Point(1, 1), new Size(200, 200));

                }
                //Астероиды
                LoadAsteroid();

                

                //for (int i = __GameObjects.Length / 2 + 1; i < __GameObjects.Length; i++)
                //{
                //    __GameObjects[i] = new Asteroid(new Point(600, 50 - i), new Point(15 - i, 25 - i), new Size(30 + i, 40 + i), true);
                //}

                __Bullet = new Bullet(new Point(20, 200), new Point(15, 0), new Size(40, 40));
                __Ship = new StarShip(new Point(20, 200), new Point(0, 0), new Size(50, 50));
                __EnergyTank = new EnergyTank(new Point(20, 400), new Point(0, 0), new Size(50, 50));

                //надпись подсчета очков
                __ScoreCount = new LabelGame("Очков: ", new Point(450, 10));
                __EnergeCount = new LabelGame("Энергии: ", new Point(450, 50));



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                MessageBox.Show("Приложение будет закрыто");
                Application.Exit();
            }
        }
        /// <summary>
        /// Загрузка астероидов, создание коллекции
        /// </summary>
        private static void LoadAsteroid()
        {
            __Asteroids = new List<Asteroid>();
            for (int i = 0; i <= __asteroidCount; i++)
            {
                __Asteroids.Add(new Asteroid(new Point(600, 50 - i), new Point(15 - i, 25 - i), new Size(30 + i, 40 + i)));
            }
        }

        /// <summary>
        /// Логика рисования объектов
        /// </summary>
        public static void Draw()
        {
            var g = Buffer.Graphics;
            g.Clear(Color.Black);
            // g.DrawRectangle(Pens.White, 100, 100, 200, 200);
            // g.FillEllipse(Brushes.Red, 100, 100, 200, 200);

            //foreach (var game_object in __GameObjects)
            //{
            //    game_object.Draw();
            //}

            for (int i = 0; i < __GameObjects.Length; i++)
            {
                __GameObjects[i]?.Draw();
            }
            foreach(var ast in __Asteroids)
            {
                ast?.Draw();
            }
            __Bullet?.Draw();
            __Ship.Draw();
            __EnergyTank.Draw();
            __ScoreCount.Draw();
            __EnergeCount.Draw();
            //После закрытия игрового окна тут возникает исключение ArgumentException
            Buffer.Render();


        }

        /// <summary>
        /// Объявление функции "обновления". Прежде всего обновления позиции и проверка на столкновения
        /// </summary>
        private static void Update(Message message)
        {
            if (__Asteroids.Count == 0)
            {
                __asteroidCount++;
                LoadAsteroid();
            }
            for (int i = 0; i < __GameObjects.Length; i++)
            {

                if (__GameObjects[i] == null) continue;
                var game_object = __GameObjects[i];
                game_object?.Update();
            }
            for (int i = 0; i < __Asteroids.Count; i++)
            {
                
                if (__Asteroids[i] == null) continue;
                __Asteroids[i]?.Update();
                //Проверка на столкновения
                //Пулей со враждебным объектом
                if (__Bullet != null && __Bullet.Colision(__Asteroids[i]))
                {
                        System.Media.SystemSounds.Hand.Play();
                        Score++;
                        _message = message;
                        _message?.Invoke("Попал по " + __Asteroids[i].ToString());
                    __Asteroids.RemoveAt(i);
                        __Bullet = null;
                        continue;
                }
                //Астероида (или иного враждебного) с кораблем
                if (__Ship.Colision(__Asteroids[i]))
                {
                        __Ship.Energy--;
                        _message = message;
                        _message?.Invoke("В тебя попали " + __Asteroids[i].ToString());
                        continue;
                }


               

                
                
            }
            //Кораблем с аптечкой
            if (__Ship.Colision(__EnergyTank))
            {
                __Ship.Energy += __EnergyTank.EnergyCount;
                _message = message;
                _message?.Invoke("Подобрал " + __EnergyTank.ToString());
            }

            __Bullet?.Update();
            __Ship?.Update();
            __EnergyTank?.Update();
            __ScoreCount.Text = "Очков: " + Score;
            __EnergeCount.Text = "Энергии " + __Ship.Energy;



        }


        private static void SizeWindowCheck()
        {

            Width = Form.ActiveForm.Width;
            Height = Form.ActiveForm.Height;
            if (Width > 1000 || Height > 1000 || Width < 0 || Height < 0)
            {
                throw new ArgumentOutOfRangeException("Высота или ширина окна недопустимых размеров");
            }
        }

        /// <summary>
        /// Функция обработки ввода с клавиатуры (управления кораблем).
        /// </summary>
        /// <param name="sender">Что вызывает событие (тип перефирии ввода? Клавиатура, мышь и т.д?)</param>
        /// <param name="args">Информация о нажатой клавише, в частности для функции важны его KeyCode</param>
        private static void Form_KeyDown(object sender, KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.ControlKey:
                    __Bullet = new Bullet(__Ship.Position, new Point(15, 0), new Size(40, 20));
                    break;
                case Keys.W:
                    __Ship.Up();
                    break;
                case Keys.S:
                    __Ship.Down();
                    break;
                case Keys.Space:
                    __Ship.Die();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Функция конца игры.
        /// </summary>
        public static void Finish()
        {
            //_message = message;
            _message?.Invoke("Игра кончилась");
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, new Point(200, 100));
            Buffer.Render();
            __Timer.Stop();

        }
    }
}
