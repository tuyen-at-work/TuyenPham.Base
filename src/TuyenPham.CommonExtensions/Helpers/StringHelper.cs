using System.Collections.Generic;

namespace TuyenPham.CommonExtensions.Helpers
{
    public static class StringHelper
    {
        public static string Join(
            this IEnumerable<string> args,
            string separator = ", ")
        {
            return string.Join(separator, args);
        }
    }
}
