using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace StarWars 
{
    class Bullet : GameObject
    {



        public Bullet(Point Position, Point Speed, Size Size)
    : base(Position, Speed, Size) { }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.PapayaWhip, new RectangleF(_Position, _Size));
        }

        public override void Update()
        {
            _Position.X += _Speed.X;
        }
    }
}
