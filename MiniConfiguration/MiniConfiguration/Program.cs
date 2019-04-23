using System;
using Microsoft.Extensions.FileProviders;

namespace MiniConfiguration
{
    class Program
    {
        static void Main()
        {
            var phyFileProvider = new PhysicalFileProvider("C:\\Users\\liuzh\\MyBox\\TestSpace");
            ChangeToken.OnChange(()=>phyFileProvider.Watch("*.*"), () => { Console.WriteLine("Changed"); });
            Console.ReadKey();
        }
    }
}
