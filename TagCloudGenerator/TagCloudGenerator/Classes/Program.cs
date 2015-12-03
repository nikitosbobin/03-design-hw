using System;
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
            kernel.Bind<ICloudGenerator>().To<ArchimedSpiralFunctionCloud>();
            kernel.Bind<CommandsParser>().ToSelf()
                .WithConstructorArgument("cloud", kernel.Get<ICloudGenerator>())
                .WithConstructorArgument("args", args);
            if (kernel.Get<CommandsParser>().ExecuteAllCommands())
            {
                kernel.Bind<IImageEncoder>().To<PngEncoder>()
                    .WithConstructorArgument("cloudImage", kernel.Get<CommandsParser>().Cloud.Generator);
                kernel.Get<IImageEncoder>().SaveImage("out");
                Console.WriteLine("Я всё");
            }
            Console.ReadKey();
        }
    }
}
