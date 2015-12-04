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
            [Test]
            public static void DetermineRectsIntersection()
            {
                var cloud = new ArchimedSpiralFunctionCloud(200,200,null,null);
                cloud.frames.Add(new Rectangle(50, 50, 50, 50));
                var result = cloud.IntersectsWithAny(new Rectangle(60, 60, 100, 100));
                Assert.AreEqual(true, result);
            }

            [Test]
            public static void DetermineRectsNonIntersection()
            {
                var cloud = new ArchimedSpiralFunctionCloud(200, 200, null, null);
                cloud.frames.Add(new Rectangle(0, 0, 50, 50));
                var result = cloud.IntersectsWithAny(new Rectangle(-40, -40, 10, 10));
                Assert.AreEqual(false, result);
            }
        }
    }
}
