using System.Collections.Generic;

namespace TuyenPham.CommonExtensions.Helpers
{
    internal static class StringHelper
    {
        internal static string Join(
            this IEnumerable<string> args,
            string separator = ", ")
        {
            return string.Join(separator, args);
        }
    }
}
