using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace StarWars
{
    /// <summary>
    /// Класс, содержащий логику для надписей поверх игры
    /// </summary>
    class LabelGame
    {
        public string Text;
        protected Font _Font;
        protected Brush _Brush;
        protected Point _Point;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="text">Надпись</param>
        /// <param name="point">Координаты</param>
        public LabelGame(string text, Point point)
        {
            Text = text;
            _Font = new Font(FontFamily.GenericSansSerif, 30, FontStyle.Regular);
            _Brush = Brushes.White;
            _Point = point;
        }

        public void Draw()
        {
            Game.Buffer.Graphics.DrawString(Text, _Font, _Brush, _Point);
        }
        //public void Update()
        //{
        //    _Text = _Text + 
        //}

    }
}
