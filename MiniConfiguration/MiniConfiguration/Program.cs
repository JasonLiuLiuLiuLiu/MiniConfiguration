using System;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace ChangeTokenPrinciple
{
    class Program
    {
        static void Main()
        {
            var phyFileProvider = new PhysicalFileProvider("C:\\Users\\liuzh\\MyBox\\TestSpace");
            ChangeToken.OnChange(() => phyFileProvider.Watch("*.*"),
                () => { Console.WriteLine("老鼠被蛇吃"); });
            Console.ReadKey();
        }

        //static void Main()
        //{
        //    //定义一个C:\Users\liuzh\MyBox\TestSpace目录的FileProvider
        //    var phyFileProvider = new PhysicalFileProvider("C:\\Users\\liuzh\\MyBox\\TestSpace");

        //    //让这个Provider开始监听这个目录下的所有文件
        //    var changeToken = phyFileProvider.Watch("*.*");

        //    //定义🐍吃🐀这件事
        //    changeToken.RegisterChangeCallback(_=> { Console.WriteLine("老鼠被蛇吃"); }, new object());

        //    //添加一个文件到被目录
        //    AddFileToPath();

        //    Console.ReadKey();

        //}

        //static void AddFileToPath()
        //{
        //    Console.WriteLine("老鼠出洞了");
        //    File.Create("C:\\Users\\liuzh\\MyBox\\TestSpace\\老鼠出洞了.txt").Dispose();
        //}
    }
}
