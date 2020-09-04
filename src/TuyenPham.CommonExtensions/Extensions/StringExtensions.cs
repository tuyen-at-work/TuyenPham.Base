using System.Collections.Generic;

namespace TuyenPham.Base.Extensions
{
    public static class StringExtensions
    {
        public static string Join(
            this IEnumerable<string> args,
            string separator = ", ")
        {
            return string.Join(separator, args);
        }
    }
}
