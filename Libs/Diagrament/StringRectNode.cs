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
        public string Content = string.Empty;
        public Font Font = new Font("Courier New", 10);
        public Pen ContentPen = new Pen(Color.Black);

        protected StringFormat mFormat = new StringFormat();

        public AlignStyle Algin
        {
            get { return mAlign; }

            set
            {
                mAlign = value;

                if (mAlign == AlignStyle.Left)
                {
                    this.mFormat.Alignment = StringAlignment.Near;
                }
                else if(mAlign== AlignStyle.Right)
                {
                    this.mFormat.Alignment = StringAlignment.Far;
                }
            }
        }

        public StringRectNode()
        {
            this.mFormat.LineAlignment = StringAlignment.Center;
        }

        protected override void DrawContent(Graphics graphics)
        {
            graphics.DrawString(Content, this.Font, ContentPen.Brush, Position.X + Margin, Position.Y, this.mFormat);
        }
    }
    public class NodeConnection
    {
        public StringRectNode From;
        public StringRectNode To;

        public Pen Pen = new Pen(Color.Red);

        public void Draw(Graphics graphics)
        {
            if (From != null && To != null)
                graphics.DrawLine(this.Pen, From.Position, To.Position);
        }
    }
}
