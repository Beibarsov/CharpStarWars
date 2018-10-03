//using System;
using System.Drawing;
using System;


namespace StarWars

{
    class StarBase : GameObject
    {
        private int _DeltaX;
        private int _DeltaY;
        

        public StarBase(Point Position, Point Speed, Size Size) : base(Position, Speed, Size)
        {
        }

        public override void Draw()
        {
            var g = Game.Buffer.Graphics;
            Image image = Image.FromFile(@"Images\starbase-tex.png");
            g.DrawImage(image, new Rectangle(_Position, _Size));
        }
        public override void Update()
        {
            if (_DeltaY == 10 || _DeltaY == -1)
                _Speed.Y *= -1;
            if (_DeltaX > 10 || _DeltaX == -1)
                _Speed.X *= -1;


            _Position.X += _Speed.X;
            _DeltaX += 1 * Math.Sign(_Speed.X);
            _Position.Y += _Speed.Y;
            _DeltaY += 1 * Math.Sign(_Speed.Y);



            //if (_DeltaX == 10 && _DeltaY == 10)
            //{
            //    _Position.X += _Speed.X;
            //    _DeltaX += _Speed.X;
            //    _Position.Y += _Speed.Y;
            //    _DeltaY += _Speed.Y;
            //}
        }


    }

}
