using System;
using System.Drawing;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class ArchimedSpiralFunctionCloud : PolarFunctionCloud
    {
        public override Point GetBlockCoords()
        {
            var nod = GetGreatestCommonDivisor(Size.Height, Size.Width);
            var x = (int)((double)Size.Width / nod * CurrentAngle * Math.Cos(CurrentAngle));
            var y = (int)((double)Size.Height / nod * CurrentAngle * Math.Sin(CurrentAngle));
            return new Point(x, y);
        }

        private static int GetGreatestCommonDivisor(int firstItem, int secondItem)
        {
            while (firstItem != secondItem)
            {
                if (firstItem > secondItem)
                    firstItem -= secondItem;
                else
                    secondItem -= firstItem;
            }
            return firstItem;
        }

        public ArchimedSpiralFunctionCloud(ITextDecoder decoder, ITextHandler textHandler) : base(decoder, textHandler)
        {
        }

        public ArchimedSpiralFunctionCloud(int width, int height, ITextDecoder decoder, ITextHandler textHandler) : base(width, height, decoder, textHandler)
        {
        }
    }
}