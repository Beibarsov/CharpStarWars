//using System;
using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace StarWars

{
    abstract class GameObject : ICollision
    {
        protected Point _Position;
        protected Point _Speed;
        protected Size _Size;
        public bool _isEnemy { get; set; }
        public Rectangle Rect => new Rectangle(_Position, _Size);
        //Делегат для событий
        public delegate void Message();

        public GameObject(Point Position, Point Speed, Size Size)
        {
            _Position = Position;
            _Speed = Speed;
            _Size = Size;
            if (_Size.Height < 0 || _Size.Width < 0)
                throw new GameObjectException("Неправильный размер объекта");
            if (Math.Abs(_Speed.X) > 100 || Math.Abs(_Speed.Y) > 100)
                throw new GameObjectException("Крайне высокая скорость объекта");
            if (Math.Abs(_Position.X) > 1000 || Math.Abs(_Position.Y) > 1000)
                throw new GameObjectException("Неправдоподобная позиция объекта");

        }


        public bool Colision(ICollision obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            return Rect.IntersectsWith(obj.Rect);
        }

        public abstract void Draw();

            //var g = Game.Buffer.Graphics;
            //Image image = Image.FromFile(@"Images\Asteroid.png");
            //g.DrawImage(image, _Position);

        public abstract void Update();
        
    }

    [Serializable]
    internal class GameObjectException : Exception
    {
        public GameObjectException()
        {
        }

        public GameObjectException(string message) : base(message)
        {
        }

        public GameObjectException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GameObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
