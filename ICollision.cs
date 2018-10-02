using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace StarWars
{
    interface ICollision
    {
        Rectangle Rect { get; }
        bool Colision(ICollision obj);

    }
}
