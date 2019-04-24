using System;
using Microsoft.Extensions.FileProviders;

namespace ChangeTokenPrinciple
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
