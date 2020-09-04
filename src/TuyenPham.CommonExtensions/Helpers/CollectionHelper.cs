using System.Collections.Generic;

namespace TuyenPham.Base.Helpers
{
    public static class CollectionHelper
    {
        public static string Join(
            this IEnumerable<string> args,
            string separator = ", ")
        {
            return string.Join(separator, args);
        }
    }
}
