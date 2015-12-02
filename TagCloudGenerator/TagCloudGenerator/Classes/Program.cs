using System;
using System.Linq;
using Ninject;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Bind<ITextDecoder>().To<TxtDecoder>().WithConstructorArgument(args[0]);
            kernel.Bind<ITextHandler>().To<SimpleTextHandler>();
            kernel.Bind<ICloudGenerator>().To<PolarFunctionCloud>();
            kernel.Bind<CommandsParser>().ToSelf()
                .WithConstructorArgument("cloud", kernel.Get<ICloudGenerator>())
                .WithConstructorArgument("args", args);
            if (kernel.Get<CommandsParser>().ExecuteAllCommands())
            {
                kernel.Bind<ICloudImageGenerator>()
                    .To<ImageGenerator>()
                    .WithConstructorArgument("cloud", kernel.Get<CommandsParser>().Cloud);
                kernel.Bind<IImageEncoder>().To<PngEncoder>();
                kernel.Get<IImageEncoder>().SaveImage("out");
                Console.WriteLine("Я всё");
            }
            Console.ReadKey();
        }
    }
}
