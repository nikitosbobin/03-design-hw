using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using Ninject;
using NUnit.Framework.Constraints;
using TagCloudGenerator.Classes.DefaultCommands;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new CommandsParser();
            if (!parser.CreateCommands(args))
                Console.WriteLine("Commands parsing error");
            var kernel = new StandardKernel();
            kernel.Bind<ITextDecoder>().To<TxtDecoder>().WithConstructorArgument(args[0]);
            kernel.Bind<ITextHandler>().To<SimpleTextHandler>();
            kernel.Bind<ICloudImageGenerator>()
                .To<PolarFunctionCloud>()
                .WithConstructorArgument("commands", parser.GetCommands());
            kernel.Bind<IImageEncoder>().To<PngEncoder>();
            if (kernel.Get<IImageEncoder>().SaveImage("out"))
                Console.WriteLine("Запись прошла успешно");
            else
                Console.WriteLine("Запись не удалась");
            Console.ReadKey();
        }
    }
}
