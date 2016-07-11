using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagrament
{
    public class Pin 
    {
        public Node Owner;

        protected Size mSize = new Size(8, 8);
        public Point Position = new Point(0, 0);
        Pen mMainPen = new Pen(Color.Green);

        internal void Draw(Graphics graphics)
        {
            if(Owner!=null)
                graphics.DrawArc(mMainPen,
                Owner.Position.X + this.Position.X - mSize.Width * 0.5f,
                Owner.Position.Y + Position.Y - mSize.Height * 0.5f,
               mSize.Width, mSize.Height, 0, 360);
        }
    }
}
