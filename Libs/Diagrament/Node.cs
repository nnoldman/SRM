using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diagrament
{
    public enum AlignStyle
    {
        Left,
        Center,
        Right,
    }
    public class Node
    {
        protected Size mSize = new Size(100,20);
        public Point Position = new Point(100, 100);
        protected Pen mMainPen = new Pen(Color.Green);
        protected Pin mPin = new Pin();

        protected AlignStyle mAlign = AlignStyle.Left;

        public bool ShowPin { get; set; }
        public bool ShowEdage { get; set; }
        public int Margin = 3;

        public Node()
        {
            this.ShowEdage = true;
            this.ShowPin = true;
            this.mPin.Owner = this;
        }

        protected virtual void DrawContent(Graphics graphics)
        {
        }
        protected virtual void DrawEdage(Graphics graphics)
        {
        }
        public void Draw(Graphics graphics)
        {
            if (ShowPin)
                this.DrawPin(graphics);
            if(ShowEdage)
                this.DrawEdage(graphics);

            this.DrawContent(graphics);
        }

        void DrawPin(Graphics graphics)
        {
            mPin.Draw(graphics);
        }
    }
}
