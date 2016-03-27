using System.Drawing;
using System.Drawing.Drawing2D;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    static class MyExtensions
    {
        public static void DrawWordBlock(this Graphics graphics, IWordBlock source, Brush brush)
        {
            source.Draw(graphics, brush);
        }

        public static bool IntersectWith(this GraphicsPath path, IWordBlock word, Graphics graphics)
        {
            return path.IntersectWith(word.GetWordRectangle(graphics), graphics);
        }

        public static bool IntersectWith(this GraphicsPath path, Rectangle rect, Graphics graphics)
        {
            var region = new Region(path);
            region.Intersect(rect);
            return !region.IsEmpty(graphics);
        }

        public static Point RigthBottom(this Rectangle rectangle)
        {
            return new Point(rectangle.Right, rectangle.Bottom);
        }

        public static Point LeftBottom(this Rectangle rectangle)
        {
            return new Point(rectangle.Left, rectangle.Bottom);
        }

        public static Point RightTop(this Rectangle rectangle)
        {
            return new Point(rectangle.Right, rectangle.Top);
        }

        public static Point OffsetTo(this Point p1, Point p2)
        {
            var dx = p2.X - p1.X;
            var dy = p2.Y - p1.Y;
            return new Point(dx, dy);
        }
    }
}
