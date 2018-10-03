using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace StarWars
{
    class StarShip : GameObject
    {
        //Событие для смерти корабля
        public event Message MessageDie;

        public int Energy { get; set; } = 100;
        public Point Position
        {
            get { return _Position; }
        }
        public void TakeDamage(int n)
        {
            Energy -= n;
        }
        public StarShip(Point _Position, Point _Speed, Size _Size) : base(_Position, _Speed, _Size)
        {
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.White, new RectangleF(_Position, _Size));
        }
        public override void Update()
        {
            if (Energy < 0) Die();
        }

        public void Up()
        {
            if (_Position.Y > 0) _Position.Y -= 5;
        }
        public void Down()
        {
            if (_Position.Y < Game.Height) _Position.Y += 5;
        }


        /// <summary>
        /// Функция смерти корабля. При сработке вызывается событие. За событием закрепен обработчик функции конца игры
        /// </summary>
        public void Die()
        {
            MessageDie?.Invoke();
        }

    }
}
