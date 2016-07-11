using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagrament
{
    public class StringRectNode : RectNode
    {
        public StringFormat Format = new StringFormat();
        public string Content = string.Empty;
        public Font Font = new Font("Courier New", 10);

        public StringRectNode()
        {
            this.Format.Alignment = StringAlignment.Center;
        }

        protected override void DrawContent(Graphics graphics)
        {
            graphics.DrawString(Content, this.Font, mMainPen.Brush, Position, this.Format);
        }
    }
    public class NodeConnection
    {
        public StringRectNode From;
        public StringRectNode To;
        public Pen Pen = new Pen(Color.Red);
        public void Draw(Graphics graphics)
        {
            if(From!=null&& To != null)
                graphics.DrawLine(this.Pen, From.Position, To.Position);
        }
    }
}
