using System;
using System.IO;
using TuyenPham.Base.Helpers;

namespace TuyenPham.Base.Tests
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            var di = new DirectoryInfo("E:/test");
            var diList = di.GetDirectories("Items*");
            diList.DeleteAll();

            Console.WriteLine("Hello World!");
        }
    }
}
