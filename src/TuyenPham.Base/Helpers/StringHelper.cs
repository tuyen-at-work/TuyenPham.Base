using System;

namespace TuyenPham.Base.Helpers
{
    public static class StringHelper
    {
        public static bool InvariantCultureIgnoreCaseEquals(this string a, string b) => string.Equals(a, b, StringComparison.InvariantCultureIgnoreCase);

        public static bool InvariantCultureEquals(this string a, string b) => string.Equals(a, b, StringComparison.InvariantCulture);

        public static bool OrdinalIgnoreCaseEquals(this string a, string b) => string.Equals(a, b, StringComparison.OrdinalIgnoreCase);

        public static bool OrdinalEquals(this string a, string b) => string.Equals(a, b, StringComparison.Ordinal);

        public static bool IsNull(this string s) => s == null;

        public static bool IsNullOrEmpty(this string s) => string.IsNullOrEmpty(s);

        public static bool IsNullOrWhiteSpace(this string s) => string.IsNullOrWhiteSpace(s);
    }
}
