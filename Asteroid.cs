using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace StarWars
{
    class Asteroid : GameObject
    {
        
        Image image = Image.FromFile(@"Images\Asteroid.png");

        public Asteroid(Point Position, Point Speed, Size Size, bool isEnemy)
    : base(Position, Speed, Size, isEnemy) { }

        public override void Update()
        {
            {
                _Position.X += _Speed.X;
                _Position.Y += _Speed.Y;

                if (_Position.X < 0 || _Position.X > Game.Width - _Size.Width)
                {
                    _Speed.X *= -1;
                }
                if (_Position.Y < 0 || _Position.Y > Game.Height - _Size.Height)
                {
                    _Speed.Y *= -1;
                }
            }
        }

        public override void Draw()
        {
            var g = Game.Buffer.Graphics;

            g.DrawImage(image, new Rectangle(_Position, _Size));
        }

    }
}
