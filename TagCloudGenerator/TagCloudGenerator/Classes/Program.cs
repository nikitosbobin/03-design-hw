using System;
using Ninject;
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
            kernel.Bind<ICloudGenerator>()
                .To<PolarFunctionCloud>()
                .WithConstructorArgument("commands", parser.GetCommands());
            kernel.Bind<ICloudImageGenerator>().To<ImageGenerator>();
            kernel.Bind<IImageEncoder>().To<PngEncoder>();
            kernel.Get<IImageEncoder>().SaveImage("out");
            Console.WriteLine("Я всё");
            Console.ReadKey();
        }
    }
}
