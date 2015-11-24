using System;
using System.Drawing;
using NUnit.Framework;

namespace TagCloudGenerator.Classes
{
    partial class PolarFunctionCloud
    {
        [TestFixture]
        class PolarFunctionCloud_Should
        {
            [TestCase(0, 100, 100, Result = new[] {0, 0})]
            [TestCase((float)Math.PI / 2, 100, 100, Result = new[] { 0, (int)Math.PI / 2 })]
            public static int[] GetRightPointToWritingWord(float angle, int width, int height)
            {
                var currentPoint = MainFunc(angle, new Size(width, height));
                return new[] { currentPoint.X, currentPoint.Y };
            }


            [TestCase(1000, 1, Result = 1)]
            [TestCase(144, 12, Result = 12)]
            [TestCase(25, 5, Result = 5)]
            public static int GetRightGreatestCommonDivisor(int a, int b)
            {
                return GetGreatestCommonDivisor(a, b);
            }

            [Test]
            public static void DetermineRectsIntersection()
            {
                var cloud = new PolarFunctionCloud(200,200,null,null);
                cloud.frames.Add(new Rectangle(50, 50, 50, 50));
                var result = cloud.IntersectsWithAny(new Rectangle(60, 60, 100, 100));
                Assert.AreEqual(true, result);
            }

            [Test]
            public static void DetermineRectsNonIntersection()
            {
                var cloud = new PolarFunctionCloud(200, 200, null, null);
                cloud.frames.Add(new Rectangle(0, 0, 50, 50));
                var result = cloud.IntersectsWithAny(new Rectangle(-40, -40, 10, 10));
                Assert.AreEqual(false, result);
            }
        }
    }
}
