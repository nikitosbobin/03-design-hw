using System;
using System.Collections.Generic;
using System.Drawing;
using Ninject;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SolidBrush> solidBrushes = new List<SolidBrush>();
            solidBrushes.Add(new SolidBrush(Color.AliceBlue));
            solidBrushes.Add(new SolidBrush(Color.Black));
            solidBrushes.Add(new SolidBrush(Color.Aqua));
            solidBrushes.Add(new SolidBrush(Color.DarkRed));
            solidBrushes.Add(new SolidBrush(Color.DarkGreen));
            solidBrushes.Add(new SolidBrush(Color.Crimson));
            solidBrushes.Add(new SolidBrush(Color.Goldenrod));
            solidBrushes.Add(new SolidBrush(Color.BlueViolet));
            solidBrushes.Add(new SolidBrush(Color.LawnGreen));
            var kernel = new StandardKernel();
            kernel.Bind<ITextDecoder>().To<TxtDecoder>().WithConstructorArgument(args[0]);
            kernel.Bind<ITextHandler>().To<SimpleTextHandler>();
            kernel.Bind<ICloudImageGenerator>()
                .To<PolarFunctionCloud>()
                .WithConstructorArgument("width", int.Parse(args[1]))
                .WithConstructorArgument("height", int.Parse(args[2]))
                .WithConstructorArgument("wordsBrushes", solidBrushes);
            kernel.Bind<IImageEncoder>().To<PngEncoder>();
            if (kernel.Get<IImageEncoder>().SaveImage("out"))
                Console.WriteLine("Запись прошла успешно");
            else
                Console.WriteLine("Запись не удалась");
            Console.ReadKey();
        }
    }
}
