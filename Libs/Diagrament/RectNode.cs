using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagrament
{
    public class RectNode : Node
    {
        protected override void DrawEdage(Graphics graphics)
        {
            switch (mAlign)
            {
                case Align.Left:
                    {
                        graphics.DrawRectangle(mMainPen, Position.X, Position.Y - mSize.Height * 0.5f, mSize.Width, mSize.Height);
                    }
                    break;
                case Align.Center:
                    {
                        graphics.DrawRectangle(mMainPen, Position.X - mSize.Width * 0.5f, Position.Y - mSize.Height * 0.5f, mSize.Width, mSize.Height);
                    }
                    break;
                case Align.Right:
                    {
                        graphics.DrawRectangle(mMainPen, Position.X-mSize.Width,Position.Y - mSize.Height * 0.5f, mSize.Width,mSize.Height);
                    }
                    break;
            }
        }
    }
}
