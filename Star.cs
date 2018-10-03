//using System;
using System.Drawing;

namespace StarWars

{
    class Star : GameObject
    {

        Image image = Image.FromFile(@"Images\Star.png");

        public Star(Point Position, Point Speed, Size Size)
            : base(Position, Speed, Size) { }
         
        public override void Draw()
        {


            var g = Game.Buffer.Graphics;
            g.DrawImage(image, new Rectangle(_Position,_Size));



            // base.Draw();
        }
        public override void Update()
        {
            // base.Update();

            _Position.X -= _Speed.X;

            if (_Position.X < 0)
                _Position.X = Game.Width + _Size.Width;

        }

    }

}
