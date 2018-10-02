using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace StarWars
{
    class EnergyTank : GameObject
    {
        public int EnergyCount { get; set; }

        public EnergyTank(Point _Position, Point _Speed, Size _Size, bool _IsEnemy) : base(_Position, _Speed, _Size, _IsEnemy)
        {
            EnergyCount = 2;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Red, new RectangleF(_Position, _Size));
        }

        public override void Update()
        {

        }

        public void Die()
        {

        }

    }
}
