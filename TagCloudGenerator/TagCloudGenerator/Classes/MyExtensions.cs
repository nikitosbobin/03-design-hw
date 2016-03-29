﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

        public static void AddWordBlock(this GraphicsPath path, IWordBlock word, Graphics graphics)
        {
            path.AddRectangle(word.GetWordRectangle(graphics));
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

        public static Point ToPointInt(this PointF p1)
        {
            return new Point((int) p1.X, (int) p1.Y);
        }

        public static Point? GetEqualPoint(this Rectangle rect, Point targetPoint)
        {
            var points = rect.GetPoints();
            foreach (var point in points.Where(point => point.X == targetPoint.X && point.Y == targetPoint.Y))
                return point;
            return null;
        }

        public static Point[] GetPoints(this Rectangle rect)
        {
            return new[] {rect.Location, rect.RightTop(), rect.RigthBottom(), rect.LeftBottom()};
        }

        public static PointF OffsetTo(this PointF p1, PointF p2)
        {
            var dx = p2.X - p1.X;
            var dy = p2.Y - p1.Y;
            return new PointF(dx, dy);
        }

        public static Point OffsetTo(this Point p1, Point p2)
        {
            var dx = p2.X - p1.X;
            var dy = p2.Y - p1.Y;
            return new Point(dx, dy);
        }

        public static T GetRandomElement<T>(this IEnumerable<T> source)
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            return source.ElementAt(rnd.Next(0, source.Count()));
        }

        public static T[] OffsetArray<T>(this T[] source, int offset)
        {
            var result = new List<T>();
            foreach (var element in source)
            {
                result.Add(source[offset % source.Length]);
                offset++;
            }
            return result.ToArray();
        }

        public static IList<T> Shuffle<T>(this IList<T> source)
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            var n = source.Count;
            while (n > 1)
            {
                n--;
                var k = rnd.Next(n + 1);
                var value = source[k];
                source[k] = source[n];
                source[n] = value;
            }
            return source;
        }
    }
}
