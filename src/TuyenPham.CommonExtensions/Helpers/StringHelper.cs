using System;

namespace TuyenPham.Base.Helpers
{
    public static class StringHelper
    {
        public static bool InvariantCultureIgnoreCaseEquals(this string a, string b)
        {
            return a?.Equals(b, StringComparison.InvariantCultureIgnoreCase) ?? b == null;
        }

        public static bool InvariantCultureEquals(this string a, string b)
        {
            return a?.Equals(b, StringComparison.InvariantCulture) ?? b == null;
        }

        public static bool OrdinalIgnoreCaseEquals(this string a, string b)
        {
            return a?.Equals(b, StringComparison.OrdinalIgnoreCase) ?? b == null;
        }

        public static bool OrdinalEquals(this string a, string b)
        {
            return a?.Equals(b, StringComparison.Ordinal) ?? b == null;
        }
    }
}
