using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloudGenerator.Classes;

namespace TagCloudGenerator.Interfaces
{
    interface ITextParser
    {
        Word[] GetWords();
    }
}
