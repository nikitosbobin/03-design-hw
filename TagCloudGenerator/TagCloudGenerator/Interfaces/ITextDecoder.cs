﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudGenerator.Interfaces
{
    interface ITextDecoder
    {
        string[] GetDecodedText();
    }
}
