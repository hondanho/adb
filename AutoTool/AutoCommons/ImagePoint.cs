using Emgu.CV.Shape;
using System.Drawing;

namespace AutoTool.AutoCommons
{
    public class ImagePoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point Point { get; set; }

        public ImagePoint(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Point = new Point(x, y);
        }

        public ImagePoint(Point point)
        {
            this.Point = point;
            this.X = point.X;
            this.Y = point.Y;
        }
    }
}
