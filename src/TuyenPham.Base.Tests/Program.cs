using System.IO;
using TuyenPham.Base.Helpers;

namespace TuyenPham.Base.Tests
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            new DirectoryInfo("E:/test")
                .FilterFolders("Item*")
                ?.DeleteAll();
        }
    }
}
