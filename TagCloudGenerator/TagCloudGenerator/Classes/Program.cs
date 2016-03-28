using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using CommandLine;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class Program
    {
        static void Main(string[] args)
        {
            
            /*var options = new Options();
            Parser.Default.ParseArgumentsStrict(args, options);
            var textDecoder = new TxtDecoder(options.FilePath);
            var textHandler = new SimpleTextHandler(options.BoringWords);
            ICloudGenerator cloudGenerator = new ArchimedSpiralFunctionCloud(textDecoder, textHandler);
            ICloudImageGenerator imageGenerator = new ImageGenerator(cloudGenerator);
            var encoder = new PngEncoder(imageGenerator);
            encoder.SaveImage("out");*/

            var image = new Bitmap(1200, 1200);
            var gr = Graphics.FromImage(image);
            gr.Transform = new Matrix(1, 0, 0, 1, image.Width / 2, image.Height / 2);
            gr.Clear(Color.Aquamarine);
            gr.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            var word1 = new WordBlock("test");
            word1.FontSize = 30;
            var rect1 = new Rectangle(0,0,100,200);
            var rect2 = new Rectangle(0,200,50,50);
            gr.DrawRectangle(new Pen(Color.Black), rect1);
            gr.DrawRectangle(new Pen(Color.Black), rect2);
            var timer = Stopwatch.StartNew();
            Console.WriteLine("начал");
            ByRect(word1, rect1.GetPoints(), 10, new [] {rect1, rect2 }, gr);
            ByRect(word1, rect2.GetPoints(), 10, new[] { rect1, rect2 }, gr);
            Console.WriteLine("закончил");
            timer.Stop();
            //gr.DrawWordBlock(word1,Brushes.Black);
            gr.ResetTransform();
            image.Save("out.png", ImageFormat.Png);
            image.Dispose();
            gr.Dispose();
        }

        public static void ByRect(IWordBlock word, Point[] points, int count, IEnumerable<Rectangle> frames, Graphics gr)
        {
            for (var i = 0; i < points.Length; ++i)
            {
                MoveOnLine(word, points[i], points[(i+1) % points.Length], count, frames, gr);
            }
        }

        public static bool MoveOnLine(IWordBlock word, Point start, Point end, int count, IEnumerable<Rectangle> frames, Graphics gr)
        {
            var dist = start.OffsetTo(end);
            var startLocation = word.Location;
            var lineIsVertical = start.X == end.X;
            Rectangle tmpRect;
            for (var i = 0; i < count; ++i)
            {
                word.Location = new Point(start.X + i * dist.X / count, start.Y + i * dist.Y / count);
                if (lineIsVertical)
                {
                    if (!word.IntersectsWith(frames, gr))
                        gr.DrawWordBlock(word, Brushes.Black);
                    word.IsVertical = true;
                    if (!word.IntersectsWith(frames, gr))
                        gr.DrawWordBlock(word, Brushes.Black);
                    word.IsVertical = false;
                    tmpRect = word.GetWordRectangle(gr);
                    word.Location = new Point(word.Location.X - tmpRect.Width, word.Location.Y);
                    if (!word.IntersectsWith(frames, gr))
                        gr.DrawWordBlock(word, Brushes.Black);
                    word.Location = new Point(word.Location.X + tmpRect.Width, word.Location.Y);
                    word.IsVertical = true;
                    tmpRect = word.GetWordRectangle(gr);
                    word.Location = new Point(word.Location.X - tmpRect.Width, word.Location.Y);
                    if (!word.IntersectsWith(frames, gr))
                        gr.DrawWordBlock(word, Brushes.Black);
                }
                else
                {
                    if (!word.IntersectsWith(frames, gr))
                        gr.DrawWordBlock(word, Brushes.Black);
                    word.SaveLocation();
                    tmpRect = word.GetWordRectangle(gr);
                    word.Location = new Point(word.Location.X, word.Location.Y - tmpRect.Height);
                    if (!word.IntersectsWith(frames, gr))
                        gr.DrawWordBlock(word, Brushes.Black);
                    word.RestoreLocation();
                    word.IsVertical = true;
                    if (!word.IntersectsWith(frames, gr))
                        gr.DrawWordBlock(word, Brushes.Black);
                    tmpRect = word.GetWordRectangle(gr);
                    word.Location = new Point(word.Location.X, word.Location.Y + tmpRect.Height);
                    if (!word.IntersectsWith(frames, gr))
                        gr.DrawWordBlock(word, Brushes.Black);
                }
                word.IsVertical = false;
            }
            word.Location = startLocation;
            return false;
        }

        /*public GraphicsPath AddRect(GraphicsPath path, Rectangle rect)
        {
            var points = path.PathPoints;
            var result = new List<PointF>();
            var rectPoints = rect.GetPoints().ToList();
            foreach (var point in points)
            {
                result.Add(point);
                if (rect.GetEqualPoint(point.ToPointInt()) != null)
                {

                }
            }
        }*/
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
