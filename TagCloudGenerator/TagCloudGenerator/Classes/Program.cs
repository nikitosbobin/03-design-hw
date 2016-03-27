using System;
using System.Drawing;
using System.Drawing.Imaging;
using CommandLine;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            var options = new Options();
            Parser.Default.ParseArgumentsStrict(args, options);
            var textDecoder = new TxtDecoder(options.FilePath);
            var textHandler = new SimpleTextHandler(options.BoringWords);
            ICloudGenerator cloudGenerator = new ArchimedSpiralFunctionCloud(textDecoder, textHandler);
            ICloudImageGenerator imageGenerator = new ImageGenerator(cloudGenerator);
            var encoder = new PngEncoder(imageGenerator);
            encoder.SaveImage("out");*/

            var image = new Bitmap(5000, 5000);
            var gr = Graphics.FromImage(image);
            gr.Transform = new System.Drawing.Drawing2D.Matrix(1, 0, 0, 1, image.Width / 2, image.Height / 2);
            gr.Clear(Color.Aquamarine);
            gr.DrawRectangle(new Pen(Color.Black), 0,0,2,2);
            var word1 = new WordBlock("test1yyyyyy");
            var word2 = new WordBlock("test2yyyyyy");
            word2.IsVertical = true;
            word1.Location = new Point(-200,200);
            word1.FontSize = word2.FontSize = 150;
            gr.DrawWordBlock(word1, Brushes.Black);
            gr.DrawWordBlock(word2, Brushes.Black);
            gr.ResetTransform();
            image.Save("test.png", ImageFormat.Png);
            image.Dispose();
            gr.Dispose();
        }
    }

    /*
    class Program
    {
        public static void Main()
        {
            var image = new Bitmap(800, 800);
            var graphics = Graphics.FromImage(image);
            graphics.Clear(Color.Aqua);
            var rect1 = new Rectangle(250, 250, 250, 70);
            var rect2 = new Rectangle(550, 270, 250, 70);
            GraphicsPath f = new GraphicsPath();
            var reg = new Region(f);
            reg.Union(rect2);
            var b = f.IntersectWith(rect1, graphics);
            graphics.DrawPath(new Pen(Color.Black), f);
            //MoveOnLine(graphics, 1, rect1.LeftBottom(), rect1.RigthBottom(), rect1);
            //MoveOnLine(graphics, 0, rect1.RightTop(),rect1.RigthBottom(), rect1);
            //MoveOnLine(graphics, -1, rect1.Location, rect1.RightTop(), rect1);
            image.Save("out.png", ImageFormat.Png);
        }

        public static void DoSmth(Rectangle rect, Graphics gr)
        {
            var rr = new Rectangle(0,0,100,40);
            gr.DrawRectangle(new Pen(Color.Black), rr);
        }

        public static void MoveOnLine(Graphics gr, int angle, Point start, Point end, Rectangle rect)
        {
            var dist = start.OffsetTo(end);
            var tmpRect = rect.Rotate(angle);
            tmpRect.Location = start;
            var loc = tmpRect.Location;
            for (var i = 0; i < 20; ++i)
            {
                tmpRect.Location = loc;
                gr.DrawRectangle(new Pen(Color.Black), tmpRect);
                loc.Offset(dist.X / 20, dist.Y / 20);
            }
        }
    }

    static class MyExtensions
    {
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

        public static Rectangle Rotate(this Rectangle rectangle, int halfPiCount)
        {
            if (halfPiCount == 0) return rectangle;
            var result = new Rectangle(rectangle.Location, new Size(rectangle.Height, rectangle.Width));
            if (halfPiCount == -1)
                result.Y -= result.Height;
            else if (halfPiCount == 1)
                result.X -= result.Width;
            return result;
        }
    }
    */
}
