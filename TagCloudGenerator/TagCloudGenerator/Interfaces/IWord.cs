﻿using System.Drawing;

namespace TagCloudGenerator.Interfaces
{
    public interface IWord
    {
        string Source { get; set; }
        int Frequency { get; set; }
        Rectangle WordBlock { get; set; }
        Font Font { get; set; }
        SolidBrush SolidBrush { get; set; }
    }
}